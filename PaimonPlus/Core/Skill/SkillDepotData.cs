using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    public class SkillDepotData {
        public readonly int Id;
        public readonly List<SkillData> Skills;
        public readonly List<TalentData> Talents;
        public readonly List<ProudSkillGroupData> InherentProudSkill;

        public SkillDepotData(JObject data) {
            Id = data["id"].AsInt();

            Skills = new();
            var l = data["skills"].AsArray();
            foreach (var it in l) {
                var id = it.AsInt();
                if (id == 0) {
                    continue;
                }
                Skills.Add(CoreEngine.Ins.Skill.Skills[id]);
            }

            var skillId = data["energySkill"].OptInt();
            if (skillId != 0) {
                Skills.Add(CoreEngine.Ins.Skill.Skills[skillId]);
            }

            Talents = new();
            l = data["talents"].AsArray();
            foreach (var it in l) {
                var id = it.AsInt();
                if (id == 0) {
                    continue;
                }
                Talents.Add(CoreEngine.Ins.Skill.Talents[id]);
            }

            InherentProudSkill = new();
            l = data["inherentProudSkillOpens"].AsArray();
            foreach (JObject item in l) {
                var id = item["proudSkillGroupId"].OptInt();
                if (id == 0) {
                    continue;
                }
                InherentProudSkill.Add(CoreEngine.Ins.Skill.ProudSkillGroups[id]);
            }
        }

        public SkillData? GetSkill(SkillType type) {
            return Skills.Find(e => e.TriggerID == type.TriggerID);
        }
    }
}
