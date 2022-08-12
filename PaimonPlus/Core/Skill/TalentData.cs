using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class TalentData {
		public readonly int Id;
		public readonly string Name;
		public readonly string Desc;
		public readonly string OpenConfig;
		public readonly PropPanel AddProps;
		public readonly double[] Params;

		public TalentData(JObject data) {
			Id = data["talentId"].AsInt();
			Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
			Desc = CoreEngine.Ins.GetText(data["descTextMapHash"].AsLong());
			OpenConfig = data["openConfig"].AsString();
			AddProps = new();
			var l = data["addProps"].AsArray();
			foreach (JObject it in l) {
				if (it.Count == 0) {
					continue;
				}
				var propType = PropTypeExt.FromConfigName(it["propType"].AsString());
				AddProps[propType] = it["value"].OptDouble();
			}
			Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
		}
	}
}
