using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 角色技能配置数据
    /// </summary>
    public class SkillData {
        public readonly int Id;
        public readonly string Name;
        public readonly string Desc;

        /// <summary>
        /// 触发器ID，可能对应到界面上各种按钮，比如普通，E，Q，冲刺之类
        /// </summary>
        public readonly int TriggerID;

        /// <summary>
        /// 对应的固有天赋组
        /// </summary>
        public readonly ProudSkillGroupData? ProudSkillGroup;

        public SkillData(JObject data) {
            Id = data["id"].AsInt();
            Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
            Desc = CoreEngine.Ins.GetText(data["descTextMapHash"].AsLong());
            TriggerID = data["triggerID"].OptInt();
            var proudSkillGroupId = data["proudSkillGroupId"].GetInt();
            if (proudSkillGroupId != null) {
                ProudSkillGroup = CoreEngine.Ins.Skill.ProudSkillGroups[(int)proudSkillGroupId];
            }
        }
    }
}
