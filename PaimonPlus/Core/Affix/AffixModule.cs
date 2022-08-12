using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class AffixModule : CoreEngineModule {
		public readonly Dictionary<int, AffixSetData> Affixs = new();

		public override void Init() {
			var config = CoreEngine.ReadJArrayConfig("EquipAffixExcelConfigData");
			foreach (JObject item in config) {
				var id = item["id"].AsInt();
				if (!Affixs.TryGetValue(id, out AffixSetData? set)) {
					set = new AffixSetData(id);
					Affixs.Add(id, set);
				}
				set.AddAffix(item);
			}
		}
	}
}
