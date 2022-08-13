using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物装备面板
    /// </summary>
    public class RelicPanel {
        /// <summary>
        /// 已装备的圣遗物
        /// </summary>
        public readonly Dictionary<RelicSlotType, RelicInfo> Relics = new();

        /// <summary>
        /// 装备圣遗物
        /// </summary>
        /// <param name="relic"></param>
        public void PutRelic(RelicInfo relic) {
            Relics[relic.Slot.Type] = relic;
        }

        /// <summary>
        /// 获取当前已装备所有圣遗物的属性总和
        /// </summary>
        /// <returns></returns>
        public PropPanel GetPanel() {
            var panel = new PropPanel();
            foreach (var relic in Relics.Values) {
                panel += relic.MainProp;
                panel += relic.SubProp;
            }
            return panel;
        }
    }
}
