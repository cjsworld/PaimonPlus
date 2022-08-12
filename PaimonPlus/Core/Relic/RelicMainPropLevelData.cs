using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class RelicMainPropLevelData {
		public readonly int Level;
		private readonly PropPanel AddProps;

		public RelicMainPropLevelData(JObject data) {
			Level = data["level"].OptInt();
			AddProps = new();
			var list = data["addProps"].AsArray();
			foreach (JObject prop in list) {
				var propType = PropTypeExt.FromConfigName(prop["propType"].AsString());
				AddProps[propType] = prop["value"].OptDouble();
			}
		}

		public Prop GetProp(PropType type) {
			return AddProps.GetProp(type);
		}
	}
}
