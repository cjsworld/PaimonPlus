using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class WeaponModule : CoreEngineModule {
		public readonly Dictionary<int, WeaponData> Weapons = new();

		public override void Init() {
			var config = CoreEngine.ReadJArrayConfig("WeaponExcelConfigData");
			foreach (JObject item in config) {
				var weapon = new WeaponData(item);
				Weapons.Add(weapon.Id, weapon);
			}
		}
	}
}
