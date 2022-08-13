using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 怪物配置数据
    /// </summary>
    //public class MonsterData {
    //    public readonly int Id;
    //    public readonly string Name;

    //    public readonly string MonsterName;

    //    /// <summary>
    //    /// 怪物基础属性
    //    /// </summary>
    //    public readonly PropPanel BaseProps;

    //    public MonsterData(JObject data, Dictionary<int, JObject> descDict) {
    //        Id = data["id"].AsInt();
    //        MonsterName = data["monsterName"].OptString();
    //        var descId = data["describeId"].OptInt();
    //        if (descId != 0 && descDict.TryGetValue(descId, out var desc)) {
    //            Name = CoreEngine.Ins.GetText(desc["nameTextMapHash"].AsLong());
    //        } else {
    //            Name = "";
    //        }
    //        BaseProps = new PropPanel(
    //            PropType.FireSubHurt.By(data["fireSubHurt"].OptDouble()),
    //            PropType.WaterSubHurt.By(data["waterSubHurt"].OptDouble()),
    //            PropType.WindSubHurt.By(data["windSubHurt"].OptDouble()),
    //            PropType.ElecSubHurt.By(data["elecSubHurt"].OptDouble()),
    //            PropType.GrassSubHurt.By(data["grassSubHurt"].OptDouble()),
    //            PropType.IceSubHurt.By(data["iceSubHurt"].OptDouble()),
    //            PropType.RockSubHurt.By(data["rockSubHurt"].OptDouble()),
    //            PropType.PhysicalSubHurt.By(data["physicalSubHurt"].OptDouble())
    //        );
    //    }

    //    public override string ToString() {
    //        return String.IsNullOrEmpty(Name) ? MonsterName : Name;
    //    }
    //}
}
