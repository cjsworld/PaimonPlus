using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物套装配置数据
    /// </summary>
    public class RelicSetData {
        public readonly int Id;
        public readonly string Name;

        /// <summary>
        /// 套装效果需要的件数（比如2件套，4件套）
        /// </summary>
        public readonly int[] SetNeedNum;

        /// <summary>
        /// 套装效果组
        /// <br/>
        /// 2件套->效果等级0，4件套->效果等级1
        /// </summary>
        public readonly AffixSetData AffixSet;

        /// <summary>
        /// 套装各槽位配置数据
        /// </summary>
        public readonly Dictionary<RelicSlotType, RelicSlotData> Slots;

        /// <summary>
        /// 套装可能出现的星级
        /// </summary>
        public readonly List<int> AllRanks = new();


        public RelicSetData(JObject data, Dictionary<int, JObject> relicDict) {
            Id = data["setId"].AsInt();
            AffixSet = CoreEngine.Ins.Affix.Affixs[data["EquipAffixId"].AsInt()];
            Name = AffixSet.Levels[0].Name;
            SetNeedNum = data["setNeedNum"].AsArray().ToArray(e => e.AsInt());
            Slots = new();
            var l = data["containsList"].AsArray();
            foreach (var it in l) {
                var relic = relicDict[it.AsInt()];
                var mainDepotId = relic["mainPropDepotId"].AsInt();
                var slotType = RelicSlotType.GetByMainDepotId(mainDepotId);
                if (slotType == null) {
                    continue;
                }
                var name = CoreEngine.Ins.GetText(relic["nameTextMapHash"].AsLong());
                var desc = CoreEngine.Ins.GetText(relic["descTextMapHash"].AsLong());
                Slots.Add(slotType, new RelicSlotData(this, slotType, name, desc));
            }
        }


        /// <summary>
        /// 创建新的圣遗物信息
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public RelicInfo NewInfo(RelicSlotType slot, int rank) {
            var data = Slots[slot];
            return new RelicInfo(data, rank);
        }


        public override string ToString() {
            return Name;
        }
    }
}
