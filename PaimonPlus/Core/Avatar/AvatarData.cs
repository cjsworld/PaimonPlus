using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class AvatarData {
		public readonly int Id;
		public readonly string Icon;
		public readonly string Name;
		public readonly WeaponType WeaponType;
		public readonly string QualityType;
		public readonly PropPanel BaseProps;
		public readonly Dictionary<PropType, CurveData> Curves;
		public readonly PromoteData Promote;
		public readonly SkillDepotData SkillDepot;
				

		public AvatarData(JObject data) {
			Id = data["id"].AsInt();
			var icon = data["iconName"].AsString();
			Icon = icon.Replace("UI_AvatarIcon_", "");
			Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
			WeaponType = WeaponTypeExt.FromConfigName(data["weaponType"].AsString());
			QualityType = data["qualityType"].AsString();
			BaseProps = new PropPanel(
				PropType.BaseHP.By(data["hpBase"].AsDouble()),
				PropType.BaseATK.By(data["attackBase"].AsDouble()),
				PropType.BaseDEF.By(data["defenseBase"].AsDouble()),
				PropType.ChargeRate.By(1),
				PropType.CritRate.By(data["critical"].AsDouble()),
				PropType.CritHurt.By(data["criticalHurt"].AsDouble())
			);

			Curves = new Dictionary<PropType, CurveData>();
			var curves = data["propGrowCurves"].AsArray();
			foreach (JObject item in curves) {
				var propType = PropTypeExt.FromConfigName(item["type"].AsString());
				var curveType = item["growCurve"].AsString();
				var curve = CoreEngine.Ins.Upgrade.Curves[curveType];
				Curves[propType] = curve;
			}

			var promoteId = data["avatarPromoteId"].AsInt();
			Promote = CoreEngine.Ins.Upgrade.Promotes[promoteId];

			var depotId = data["skillDepotId"].AsInt();
			SkillDepot = CoreEngine.Ins.Skill.SkillDepots[depotId];
		}

		public AvatarInfo NewInfo() {
			return new AvatarInfo(this);
		}

		public PropPanel GetBasePanelAt(int level, bool promoted) {
			var panel = BaseProps.ClonePanel();
			foreach (var entry in Curves) {
				panel *= entry.Key.By(entry.Value[level]);
			}
			panel += Promote.GetAddPropsAt(level, promoted);
			return panel;
		}

		public override string ToString() {
			return Name;
		}
	}
}
