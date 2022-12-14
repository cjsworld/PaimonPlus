using System;
using System.Collections.Generic;


namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物槽位类型
    /// </summary>
    public sealed class RelicSlotType {
        public static readonly List<RelicSlotType> All = new();
        public static readonly RelicSlotType Flower = new(0, "生之花", 4000);
        public static readonly RelicSlotType Leather = new(1, "死之羽", 2000);
        public static readonly RelicSlotType Sand = new(2, "时之沙", 1000);
        public static readonly RelicSlotType Cup = new(3, "空之杯", 5000);
        public static readonly RelicSlotType Cap = new(4, "理之冠", 3000);

        /// <summary>
        /// 序号
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// 描述
        /// </summary>
        public readonly string Desc;

        /// <summary>
        /// 主属性词条集合id
        /// </summary>
        public readonly int MainDepotId;

        /// <summary>
        /// 可能出现的主属性词条类型
        /// </summary>
        public readonly List<PropType> MainPropTypes = new();


        public static RelicSlotType GetByIndex(int index) {
            var t = All.Find(e => e.Index == index);
            if (t == null) {
                throw new Exception($"Unknown RelicSlot index {index}");
            }
            return t;
        }

        public static RelicSlotType? GetByMainDepotId(int depotId) {
            return All.Find(e => e.MainDepotId == depotId);
        }

        private RelicSlotType(int triggerID, string desc, int mainDepotId) {
            All.Add(this);
            Index = triggerID;
            Desc = desc;
            MainDepotId = mainDepotId;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
