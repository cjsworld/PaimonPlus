using System;

namespace PaimonPlus.Core {
	public enum WeaponType {
		/** 单手剑 */
		Sword,
		/** 双手剑 */
		Claymore,
		/** 长枪 */
		Pole,
		/** 法器 */
		Catalyst,
		/** 弓 */
		Bow,
	}

	public static class WeaponTypeExt {
		public static WeaponType FromConfigName(string configName) {
			return configName switch {
				"WEAPON_SWORD_ONE_HAND" => WeaponType.Sword,
				"WEAPON_CLAYMORE" => WeaponType.Claymore,
				"WEAPON_POLE" => WeaponType.Pole,
				"WEAPON_CATALYST" => WeaponType.Catalyst,
				"WEAPON_BOW" => WeaponType.Bow,
				_ => throw new Exception($"Unknown weapon type: {configName}"),
			};
		}
	}
}
