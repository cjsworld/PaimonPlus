using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
	public class AvatarInfo {
		public readonly AvatarData Data;
		public int Level { get; set; }
		public bool Promoted { get; set; }

		/** 命之座 */
		public int Constellation { get; set; }

		public WeaponInfo? Weapon { get; set; }

		public RelicPanel Relic { get; set; }

		public AvatarInfo(AvatarData data) {
			Data = data;
			Level = 90;
			Promoted = false;
			Constellation = 0;
			Relic = new RelicPanel();
		}

		public void SetLevel(int level, bool promoted) {
			Level = level;
			Promoted = promoted;
		}

		public PropPanel GetBasePanel() {
			var panel = Data.GetBasePanelAt(Level, Promoted);
			if (Weapon != null) {
				panel += Weapon.GetBasePanel();
			}
			return panel;
		}

		public PropPanel GetTotalPanel() {
			var panel = GetBasePanel();
			panel += Relic.GetPanel();

			//TODO

			//冰套2件套
			panel += PropType.IceAddHurt.By(0.15000000596046448);

			//阿莫斯
			panel += PropType.AddHurt.By(0.11999999731779099);
			panel += PropType.AddHurt.By(0.079999998211860657 * 5);

			//大招
			//panel += PropType.AddHurt.By(0.20);

			return panel;
		}
	}
}
