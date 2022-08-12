using System;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class PromoteLevelData {
		public readonly int PromoteLevel;
		public readonly int UnlockMaxLevel;
		public readonly PropPanel AddProps;

		public PromoteLevelData(JObject data) {
			PromoteLevel = data["promoteLevel"].OptInt(1);
			UnlockMaxLevel = data["unlockMaxLevel"].AsInt();
			AddProps = new PropPanel();
			var list = data["addProps"].AsArray();
			foreach (JObject token in list) {
				var type = PropTypeExt.FromConfigName(token["propType"].AsString());
				var value = token["value"].OptDouble();
				if (value > 0) {
					AddProps += type.By(value);
				}
			}
		}
	}
}
