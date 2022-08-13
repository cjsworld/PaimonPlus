using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 角色固有天赋配置数据
    /// </summary>
    public class ProudSkillData {
        public readonly int Id;

        /// <summary>
        /// 天赋等级
        /// </summary>
        public readonly int Level;
        
        /// <summary>
        /// 技能类型
        /// </summary>
        public readonly int SkillType;

        /// <summary>
        /// 名称
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// 描述
        /// </summary>
        public readonly string Desc;

        /// <summary>
        /// 
        /// </summary>
        public readonly PropPanel AddProps;

        /// <summary>
        /// 实现名称
        /// </summary>
        public readonly string OpenConfig;

        /// <summary>
        /// 参数描述
        /// </summary>
        public readonly string[] ParamDesc;

        /// <summary>
        /// 参数列表
        /// </summary>
        public readonly double[] Params;

        public ProudSkillData(JObject data) {
            Id = data["proudSkillId"].AsInt();
            Level = data["level"].AsInt();
            SkillType = data["proudSkillType"].AsInt();
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
            ParamDesc = data["paramDescList"].AsArray().ToArray(e => CoreEngine.Ins.GetText(e.AsLong()));
            Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
        }

        override public string ToString() {
            return Name;
        }
    }
}
