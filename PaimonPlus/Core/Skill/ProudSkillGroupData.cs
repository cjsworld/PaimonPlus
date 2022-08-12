using System;
using System.Collections.Generic;
using System.Xml.Linq;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class ProudSkillGroupData {
		public readonly int Id;
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
