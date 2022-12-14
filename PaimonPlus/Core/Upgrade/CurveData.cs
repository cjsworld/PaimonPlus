using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 升级曲线配置数据
    /// <br/>
    /// 各等级下的数据，是由基础值*曲线上对应的倍率得到。
    /// </summary>
    public class CurveData {
        /// <summary>
        /// 曲线类型
        /// </summary>
        public readonly string Type;

        /// <summary>
        /// 各等级对应的倍率值
        /// </summary>
        public readonly Dictionary<int, double> Values;
        public int MinLevel { get; private set; }
        public int MaxLevel { get; private set; }

        public CurveData(string type) {
            Type = type;
            Values = new();
            MinLevel = int.MaxValue;
            MaxLevel = int.MinValue;
        }

        public void AddLevel(int level, double value) {
            if (Values.ContainsKey(level)) {
                throw new Exception($"Curve {Type} alread has level {level}");
            }
            Values[level] = value;
            MinLevel = Math.Min(MinLevel, level);
            MaxLevel = Math.Max(MaxLevel, level);
        }

        public double this[int level] {
            get {
                if (Values.TryGetValue(level, out var v)) {
                    return v;
                } else {
                    return 0d;
                }
            }
        }
    }
}
