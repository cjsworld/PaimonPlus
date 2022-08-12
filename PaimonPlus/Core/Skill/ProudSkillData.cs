using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class ProudSkillData {
		public readonly int Id;
		public readonly int Level;
		public readonly int SkillType;
		public readonly string Name;
		public readonly string Desc;
		public readonly string OpenConfig;
		public readonly PropPanel AddProps;
		public readonly string[] ParamDesc;
		public readonly double[] Params;

		public ProudSkillData(JObject data) {
			Id = data["proudSkillId"].AsInt();
			Level = data["level"].AsInt();
			SkillType = data["proudSkillType"].AsInt();
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
			ParamDesc = data["paramDescList"].AsArray().ToArray(e => CoreEngine.Ins.GetText(e.AsLong()));
			Params = data["paramList"].AsArray().ToArray(e => e.AsDouble());
		}

		override public string ToString() {
			return Name;
		}
	}
}
