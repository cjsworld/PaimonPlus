using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 效果组配置数据
    /// 包含一组效果，按等级索引，等级从0开始
    /// </summary>
    public class AffixSetData {
        public readonly int Id;
        public Dictionary<int, AffixData> Levels = new();

        public AffixSetData(int id) {
            Id = id;
        }

        public void AddAffix(JObject data) {
            var affix = new AffixData(data);
            Levels.Add(affix.Level, affix);
        }

        public override string ToString() {
            if (Levels.Count == 0) {
                return "";
            } else {
                return Levels[0].Name;
            }
        }
    }
}
