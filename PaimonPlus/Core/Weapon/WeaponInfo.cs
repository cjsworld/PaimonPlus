using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
	public class WeaponInfo {
		public readonly WeaponData Data;
		public int Level { get; set; }
		public bool Promoted { get; set; }
		public int Refine { get; set; }

		public WeaponInfo(WeaponData data) {
			Data = data;
			Level = 90;
			Promoted = false;
			Refine = 1;
		}

		public void SetLevel(int level, bool promoted) {
			Level = level;
			Promoted = promoted;
		}

		public PropPanel GetBasePanel() {
			var panel = Data.GetBasePanelAt(Level, Promoted);
			var affix = GetAffix();
			panel += affix.AddProps;
			return panel;
		}

		public AffixData GetAffix() {
			return Data.Affix.Levels[Refine];
		}
	}
}
