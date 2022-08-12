using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class RelicSetData {
		public readonly int Id;
		public readonly string Name;
		public readonly int[] SetNeedNum;
		public readonly AffixSetData AffixSet;
		public readonly Dictionary<RelicSlotType, RelicSlotData> Slots;
		public readonly List<int> AllRanks = new();


		public RelicSetData(JObject data, Dictionary<int, JObject> relicDict) {
			Id = data["setId"].AsInt();
			AffixSet = CoreEngine.Ins.Affix.Affixs[data["EquipAffixId"].AsInt()];
			Name = AffixSet.Levels[0].Name;
			SetNeedNum = data["setNeedNum"].AsArray().ToArray(e => e.AsInt());
			Slots = new();
			var l = data["containsList"].AsArray();
			foreach (var it in l) {
				var relic = relicDict[it.AsInt()];
				var mainDepotId = relic["mainPropDepotId"].AsInt();
				var slotType = RelicSlotType.GetByMainDepotId(mainDepotId);
				if (slotType == null) {
					continue;
				}
				var name = CoreEngine.Ins.GetText(relic["nameTextMapHash"].AsLong());
				var desc = CoreEngine.Ins.GetText(relic["descTextMapHash"].AsLong());
				Slots.Add(slotType, new RelicSlotData(this, slotType, name, desc));
			}
		}

		public RelicInfo NewInfo(RelicSlotType slot, int rank) {
			var data = Slots[slot];
			return new RelicInfo(data, rank);
		}


		public override string ToString() {
			return Name;
		}
	}
}
