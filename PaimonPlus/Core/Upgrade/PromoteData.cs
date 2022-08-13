using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 突破配置数据
    /// </summary>
    public class PromoteData {
        /// <summary>
        /// 突破配置id
        /// </summary>
        public readonly int Id;

        /// <summary>
        /// 突破等级数据
        /// </summary>
        public readonly List<PromoteLevelData> Levels = new();

        public PromoteData(int id) {
            Id = id;
        }

        public void AddLevel(JObject data) {
            var level = new PromoteLevelData(data);
            Levels.Add(level);
        }

        /// <summary>
        /// 获取某个等级下突破带来的属性提升
        /// </summary>
        /// <param name="level"></param>
        /// <param name="promoted"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
