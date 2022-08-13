using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物槽位数据
    /// </summary>
    public class RelicSlotData {
        /// <summary>
        /// 套装
        /// </summary>
        public readonly RelicSetData Set;

        /// <summary>
        /// 槽位类型
        /// </summary>
        public readonly RelicSlotType Type;

        public readonly string Name;
        public readonly string Desc;

        public RelicSlotData(RelicSetData set, RelicSlotType slot, string name, string desc) {
            Set = set;
            Type = slot;
            Name = name;
            Desc = desc;
        }

        public override string ToString() {
            return Name;
        }
    }
}
