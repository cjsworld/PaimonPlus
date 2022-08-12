using System;
using System.Collections.Generic;
using System.Windows;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class SkillData {
		public readonly int Id;
		public readonly string Name;
		public readonly string Desc;
		public readonly int TriggerID;
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
