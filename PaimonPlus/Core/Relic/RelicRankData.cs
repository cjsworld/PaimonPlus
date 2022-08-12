using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class RelicRankData {
		public readonly int Rank;
		public readonly Dictionary<int, RelicMainPropLevelData> MainProps = new();
		public readonly Dictionary<PropType, RelicSubPropData> SubProps = new();

		public RelicRankData(int rank) {
			Rank = rank;
		}

		public void AddMainPropData(JObject data) {
			var main = new RelicMainPropLevelData(data);
			MainProps.Add(main.Level - 1, main);
		}

		public void AddSubPropData(JObject data) {
			var propType = PropTypeExt.FromConfigName(data["propType"].AsString());
			var value = data["propValue"].AsDouble();
			if (!SubProps.TryGetValue(propType, out RelicSubPropData? sub)) {
				sub = new RelicSubPropData(propType);
				SubProps.Add(propType, sub);
			}
			sub.AddValue(value);
		}

		public Prop MainProp(PropType propType, int level) {
			return MainProps[level].GetProp(propType);
		}

		public Prop SubProp(PropType propType, double value) {
			return propType.By(SubProps[propType].CalcPreciseValue(value));
		}
	}
}
