using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
	public class RelicSlotData {
		public readonly RelicSetData Set;
		public readonly RelicSlotType Type;
		public readonly string Name;
		public readonly string Desc;

		public RelicSlotData(RelicSetData set, RelicSlotType slot, string name, string desc) {
			Set = set;
			Type = slot;
			Name = name;
			Desc = desc;
		}

		public override string ToString() {
			return Name;
		}
	}
}
