using System;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class AffixData {
		public readonly int Id;
		public readonly string Name;
		public readonly string Desc;
		public readonly int Level;
		public readonly string Config;
		public readonly PropPanel AddProps;
		public readonly double[] Params;

		public AffixData(JObject data) {
			Id = data["affixId"].AsInt();
			Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
			Desc = CoreEngine.Ins.GetText(data["descTextMapHash"].AsLong());
			Level = data["level"].OptInt();
			Config = data["openConfig"].AsString();

			AddProps = new PropPanel();
			var list = data["addProps"].AsArray();
			foreach (JObject item in list) {
				var propTypeStr = item["propType"].GetString();
				if (propTypeStr == null) {
					continue;
				}
				var propType = PropTypeExt.FromConfigName(propTypeStr);
				var value = item["value"].AsDouble();
				AddProps += propType.By(value);
			}
			Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
		}

		public override string ToString() {
			return Name;
		}
	}
}
