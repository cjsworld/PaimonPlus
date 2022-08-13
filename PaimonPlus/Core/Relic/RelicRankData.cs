using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物星级配置数据
    /// </summary>
    public class RelicRankData {
        /// <summary>
        /// 星级
        /// </summary>
        public readonly int Rank;

        /// <summary>
        /// 主属性配置数据
        /// </summary>
        public readonly Dictionary<int, RelicMainPropLevelData> MainProps = new();

        /// <summary>
        /// 副词条配置数据
        /// </summary>
        public readonly Dictionary<PropType, RelicSubPropData> SubProps = new();

        public RelicRankData(int rank) {
            Rank = rank;
        }

        public void AddMainPropData(JObject data) {
            var main = new RelicMainPropLevelData(data);
            MainProps.Add(main.Level - 1, main);
        }

        public void AddSubPropData(JObject data) {
            var propType = PropType.GetByConfigName(data["propType"].AsString());
            var value = data["propValue"].AsDouble();
            if (!SubProps.TryGetValue(propType, out RelicSubPropData? sub)) {
                sub = new RelicSubPropData(propType);
                SubProps.Add(propType, sub);
            }
            sub.AddValue(value);
        }

        /// <summary>
        /// 获取某个等级的主词条数值
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public Prop GetMainProp(PropType propType, int level) {
            return MainProps[level].GetProp(propType);
        }

        /// <summary>
        /// 获取副词条数值（会自动猜测精确值）
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Prop GetSubProp(PropType propType, double value) {
            return propType.By(SubProps[propType].CalcPreciseValue(value));
        }
    }
}
