using System;
using System.Collections.Generic;
using System.Diagnostics;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class AvatarModule : CoreEngineModule {
		public readonly Dictionary<int, AvatarData> Avatars = new();

		public override void Init() {
			var config = CoreEngine.ReadJArrayConfig("AvatarExcelConfigData");
			foreach (JObject item in config) {
				if (item["useType"].OptString() != "AVATAR_FORMAL") {
					continue;
				}
				var avatar = new AvatarData(item);
				Avatars.Add(avatar.Id, avatar);
			}
		}
	}
}
