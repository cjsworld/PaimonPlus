using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物主属性等级配置数据
    /// </summary>
    public class RelicMainPropLevelData {
        /// <summary>
        /// 强化等级
        /// </summary>
        public readonly int Level;

        /// <summary>
        /// 各个属性类型在此强化等级对应的值
        /// </summary>
        private readonly PropPanel AddProps;

        public RelicMainPropLevelData(JObject data) {
            Level = data["level"].OptInt();
            AddProps = new();
            var list = data["addProps"].AsArray();
            foreach (JObject prop in list) {
                var propType = PropType.GetByConfigName(prop["propType"].AsString());
                AddProps[propType] = prop["value"].OptDouble();
            }
        }

        public Prop GetProp(PropType type) {
            return AddProps.GetProp(type);
        }
    }
}
