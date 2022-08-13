using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 角色固有天赋组配置数据
    /// </summary>
    public class ProudSkillGroupData {
        public readonly int Id;

        /// <summary>
        /// 各等级下的数据
        /// </summary>
        public readonly Dictionary<int, ProudSkillData> Levels = new();

        public ProudSkillGroupData(int id) {
            Id = id;
        }

        public void AddSkill(JObject data) {
            var skill = new ProudSkillData(data);
            Levels[skill.Level] = skill;
        }

        override public string ToString() {
            return $"ProudSkillGroup{Id}";
        }
    }
}
