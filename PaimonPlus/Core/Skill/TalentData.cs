using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 命座配置数据
    /// </summary>
    public class TalentData {
        public readonly int Id;
        public readonly string Name;
        public readonly string Desc;

        /// <summary>
        /// 增加属性
        /// </summary>
        public readonly PropPanel AddProps;

        /// <summary>
        /// 实现名称
        /// </summary>
        public readonly string OpenConfig;

        /// <summary>
        /// 参数
        /// </summary>
        public readonly double[] Params;

        public TalentData(JObject data) {
            Id = data["talentId"].AsInt();
            Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
            Desc = CoreEngine.Ins.GetText(data["descTextMapHash"].AsLong());
            
            AddProps = new();
            var l = data["addProps"].AsArray();
            foreach (JObject it in l) {
                if (it.Count == 0) {
                    continue;
                }
                var propType = PropType.GetByConfigName(it["propType"].AsString());
                AddProps[propType] = it["value"].OptDouble();
            }

            OpenConfig = data["openConfig"].AsString();
            Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
        }
    }
}
