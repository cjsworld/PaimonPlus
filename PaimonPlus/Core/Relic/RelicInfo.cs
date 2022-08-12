using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
	public class RelicInfo {
		public readonly RelicSlotData Slot;
		public readonly RelicRankData RankData;
		public int Rank { get => RankData.Rank; }
		
		public PropType MainPropType { get; set; }
		public int Level { get; set; }
		public PropPanel SubProp { get; set; }

		public RelicInfo(RelicSlotData slot, int rank) {
			Slot = slot;
			MainPropType = slot.Type.MainPropTypes[0];
			RankData = CoreEngine.Ins.Relic.Ranks[rank];
			Level = 20;
			SubProp = new PropPanel();
		}

		public Prop MainProp { get => RankData.MainProp(MainPropType, Level); }

		public void AddSubProp(Prop prop) {
			AddSubProp(prop.Type, prop.Value);
		}

		public void AddSubProp(PropType type, double value) {
			var rankData = CoreEngine.Ins.Relic.Ranks[Rank];
			SubProp[type] = rankData.SubProps[type].CalcPreciseValue(value);
		}
	}
}
