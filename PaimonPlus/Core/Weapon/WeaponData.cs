using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class WeaponData {
		public readonly int Id;
		public readonly string Icon;
		public readonly string Name;
		public readonly WeaponType WeaponType;
		public readonly int Rank;
		public readonly PropPanel BaseProps;
		public readonly Dictionary<PropType, CurveData> Curves;
		public readonly PromoteData Promote;
		public readonly AffixSetData Affix;

		public WeaponData(JObject data) {
			Id = data["id"].AsInt();
			var icon = data["icon"].AsString();
			Icon = icon.Replace("UI_EquipIcon_", "");
			Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
			WeaponType = WeaponTypeExt.FromConfigName(data["weaponType"].AsString());
			Rank = data["rankLevel"].AsInt();

			BaseProps = new PropPanel();
			Curves = new Dictionary<PropType, CurveData>();
			var list = data["weaponProp"].AsArray();
			foreach (JObject item in list) {
				var propTypeStr = item["propType"].GetString();
				if (propTypeStr == null) {
					continue;
				}
				var propType = PropTypeExt.FromConfigName(propTypeStr);
				var value = item["initValue"].OptDouble();
				var curveType = item["type"].AsString();
				var curve = CoreEngine.Ins.Upgrade.Curves[curveType];
				BaseProps += propType.By(value);
				Curves[propType] = curve;
			}

			var promoteId = data["weaponPromoteId"].AsInt();
			Promote = CoreEngine.Ins.Upgrade.Promotes[promoteId];

			var affixs = data["skillAffix"].AsArray();
			foreach (JToken item in affixs) {
				var id = item.AsInt();
				if (id == 0) {
					continue;
				}
				if (Affix == null) {
					Affix = CoreEngine.Ins.Affix.Affixs[id];
				} else {
					throw new Exception($"Weapon {Id} {Name} has more than one affix!");
				}
			}
		}

		public WeaponInfo NewInfo() {
			return new WeaponInfo(this);
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
