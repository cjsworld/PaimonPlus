using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物模块
    /// </summary>
    public class RelicModule : CoreEngineModule {
        /// <summary>
        /// 圣遗物星级配置
        /// </summary>
        public readonly Dictionary<int, RelicRankData> Ranks = new();

        /// <summary>
        /// 圣遗物套装配置
        /// </summary>
        public readonly Dictionary<int, RelicSetData> Sets = new();

        public override void Init() {
            var config = CoreEngine.ReadJArrayConfig("ReliquaryMainPropExcelConfigData");
            foreach (JObject item in config) {
                var depotId = item["propDepotId"].OptInt();
                var slot = RelicSlotType.GetByMainDepotId(depotId);
                if (slot == null) {
                    continue;
                }
                var propType = PropType.GetByConfigName(item["propType"].AsString());
                slot.MainPropTypes.Add(propType);
            }

            config = CoreEngine.ReadJArrayConfig("ReliquaryLevelExcelConfigData");
            foreach (JObject item in config) {
                var rank = item["rank"].OptInt();
                if (rank < 4) {
                    continue; //不考虑4星以下圣遗物
                }
                if (!Ranks.TryGetValue(rank, out RelicRankData? rankData)) {
                    rankData = new(rank);
                    Ranks.Add(rank, rankData);
                }
                rankData.AddMainPropData(item);
            }

            config = CoreEngine.ReadJArrayConfig("ReliquaryAffixExcelConfigData");
            foreach (JObject item in config) {
                var depotId = item["depotId"].AsInt();
                if (depotId != 501 && depotId != 401) {
                    continue; //不考虑4星以下圣遗物
                }
                var rank = depotId / 100;
                if (!Ranks.TryGetValue(rank, out RelicRankData? rankData)) {
                    rankData = new(rank);
                    Ranks.Add(rank, rankData);
                }
                rankData.AddSubPropData(item);
            }

            var dict = new Dictionary<int, JObject>();
            config = CoreEngine.ReadJArrayConfig("ReliquaryExcelConfigData");
            foreach (JObject item in config) {
                var id = item["id"].AsInt();
                dict[id] = item;
            }

            config = CoreEngine.ReadJArrayConfig("ReliquarySetExcelConfigData");
            foreach (JObject item in config) {
                if (!item["EquipAffixId"].IsNumber) {
                    continue;
                }
                var set = new RelicSetData(item, dict);
                Sets.Add(set.Id, set);
            }

            config = CoreEngine.ReadJArrayConfig("ReliquaryCodexExcelConfigData");
            foreach (JObject item in config) {
                var id = item["suitId"].AsInt();
                var level = item["level"].AsInt();
                Sets[id].AllRanks.Add(level);
            }
        }
    }
}
