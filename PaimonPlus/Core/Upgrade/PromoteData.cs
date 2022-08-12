using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class PromoteData {
		public readonly int Id;
		public readonly List<PromoteLevelData> Levels = new();

		public PromoteData(int id) {
			Id = id;
		}

		public void AddLevel(JObject data) {
			var level = new PromoteLevelData(data);
			Levels.Add(level);
		}

		public PropPanel GetAddPropsAt(int level, bool promoted) {
			for (var i = 0; i < Levels.Count; i++) {
				var data = Levels[i];
				if (level < data.UnlockMaxLevel) {
					return data.AddProps;
				} else if (level == data.UnlockMaxLevel && (i == Levels.Count - 1 || !promoted)) {
					return data.AddProps;
				}
			}
			throw new Exception("PromoteLevel error!");
		}
	}
}
