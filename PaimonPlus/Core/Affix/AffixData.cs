using System;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 效果配置数据（作用于武器、圣遗物等）
    /// </summary>
    public class AffixData {
        public readonly int Id;
        public readonly string Name;
        public readonly string Desc;

        /// <summary>
        /// 效果等级
        /// </summary>
        public readonly int Level;

        /// <summary>
        /// 增加属性
        /// </summary>
        public readonly PropPanel AddProps;

        /// <summary>
        /// 效果实现名称
        /// </summary>
        public readonly string Config;

        /// <summary>
        /// 参数
        /// </summary>
        public readonly double[] Params;

        public AffixData(JObject data) {
            Id = data["affixId"].AsInt();
            Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
            Desc = CoreEngine.Ins.GetText(data["descTextMapHash"].AsLong());
            Level = data["level"].OptInt();

            AddProps = new PropPanel();
            var list = data["addProps"].AsArray();
            foreach (JObject item in list) {
                var propTypeStr = item["propType"].GetString();
                if (propTypeStr == null) {
                    continue;
                }
                var propType = PropType.GetByConfigName(propTypeStr);
                var value = item["value"].AsDouble();
                AddProps += propType.By(value);
            }

            Config = data["openConfig"].AsString();
            Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
        }

        public override string ToString() {
            return Name;
        }
    }
}
