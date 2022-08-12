using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class SkillModule: CoreEngineModule {
		public Dictionary<int, ProudSkillGroupData> ProudSkillGroups = new();
		public Dictionary<int, SkillData> Skills = new();
		public Dictionary<int, TalentData> Talents = new();
		public Dictionary<int, SkillDepotData> SkillDepots = new();

		public override void Init() {
			var config = CoreEngine.ReadJArrayConfig("ProudSkillExcelConfigData");
			foreach (JObject item in config) {
				var groupId = item["proudSkillGroupId"].AsInt();
				if (!ProudSkillGroups.TryGetValue(groupId, out ProudSkillGroupData? group)) {
					group = new ProudSkillGroupData(groupId);
					ProudSkillGroups.Add(groupId, group);
				}
				group.AddSkill(item);
			}

			config = CoreEngine.ReadJArrayConfig("AvatarSkillExcelConfigData");
			foreach (JObject item in config) {
				var skill = new SkillData(item);
				Skills.Add(skill.Id, skill);
			}

			config = CoreEngine.ReadJArrayConfig("AvatarTalentExcelConfigData");
			foreach (JObject item in config) {
				var talent = new TalentData(item);
				Talents.Add(talent.Id, talent);
			}

			config = CoreEngine.ReadJArrayConfig("AvatarSkillDepotExcelConfigData");
			foreach (JObject item in config) {
				var skill = new SkillDepotData(item);
				SkillDepots.Add(skill.Id, skill);
			}
		}
	}
}
