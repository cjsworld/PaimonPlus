using System;
using System.Collections.Generic;


namespace PaimonPlus.Core {
	public class RelicPanel {
		public readonly Dictionary<RelicSlotType, RelicInfo> Relics = new();

		public void PutRelic(RelicInfo relic) {
			Relics[relic.Slot.Type] = relic;
		}

		public PropPanel GetPanel() {
			var panel = new PropPanel();
			foreach (var relic in Relics.Values) {
				panel += relic.MainProp;
				panel += relic.SubProp;
			}
			return panel;
		}
	}
}
