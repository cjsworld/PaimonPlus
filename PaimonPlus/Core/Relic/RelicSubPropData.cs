using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物副词条配置数据
    /// </summary>
    public class RelicSubPropData {
        /// <summary>
        /// 属性类型
        /// </summary>
        public readonly PropType PropType;

        /// <summary>
        /// 可能出现的数值（目前规律是等差数列）
        /// </summary>
        public readonly List<double> Values = new();

        /// <summary>
        /// 数值最小值
        /// </summary>
        private double BaseValue;

        /// <summary>
        /// 公差的平均值
        /// </summary>
        private double DiffAvg;

        public RelicSubPropData(PropType propType) {
            PropType = propType;
        }

        /// <summary>
        /// 增加一个可能出现的属性值
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(double value) {
            Values.Add(value);
            BaseValue = Values[0];
            if (Values.Count == 0) {
                DiffAvg = 0.0;
            } else {
                var last = Values[Values.Count - 1];
                DiffAvg = (last - BaseValue) / (Values.Count - 1);
            }
        }

        /// <summary>
        /// 根据游戏中显示的数值猜测原始精确值
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>如果猜测失败，则直接返回传入的值</returns>
        public double CalcPreciseValue(double value) {
            double result = 0;
            int hitCount = 0;
            int totalRank = 0;
            if (CalcPreciseValue(value, ref result, ref hitCount, ref totalRank)) {
                return result;
            } else {
                return value;
            }
        }

        /// <summary>
        /// 根据游戏中显示的数值猜测原始精确值
        /// <br/>
        /// 属性值目前是等差数列，一般是4个取值，每次强化得到的值为 <c>Base + k * Diff</c>，其中k取[0,3]。<br/>
        /// 经过n次强化，得到的最终值为 <c>n * Base + (k1 + k2 + ... + kn) * Diff</c>。不妨把k的总和记为t。<br/>
        /// 已知目前圣遗物可以强化5个词条，那么单属性就是最多6个词条。n取值为[1,6], t取值为[0, 3 * n]。<br/>
        /// 对于游戏中显示的一个属性值来说，如果能够找到合适的整数n和t，使上述结果接近与目标值，那么基本就能确定精确值了。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result">猜测成功的结果</param>
        /// <param name="hitCount">当前词条命中次数</param>
        /// <param name="totalRank">当前词条命中的品质总和</param>
        /// <returns>是否猜测成功</returns>
        public bool CalcPreciseValue(double value, ref double result, ref int hitCount, ref int totalRank) {
            for (var n = 1; n <= 6; n++) {
                var x = (value - n * BaseValue);
                x /= DiffAvg;

                //Trace.WriteLine($">>>n={n},x={x}");
                var k = (int)Math.Round(x);
                if (Math.Abs(x - k) < 0.25) {
                    if (k < 0 || k > (Values.Count - 1) * n) {
                        continue;
                    }
                    hitCount = n;
                    totalRank = k;
                    result = BaseValue * n + DiffAvg * k;
                    return true;
                }
            }
            Trace.WriteLine($"Unable to calc percise value for {PropType} with {value}");
            return false;
        }
    }
}
