using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// 获取当前的所有套装属性
        /// </summary>
        /// <returns></returns>
        public List<AffixData> GetSetAffix() {
            var list = new List<AffixData>();
            var dict = new Dictionary<RelicSetData, int>();
            foreach (var relic in Relics.Values) {
                var set = relic.Slot.Set;
                if (!dict.TryGetValue(set, out var c)) {
                    c = 0;
                }
                c += 1;
                dict[set] = c;
                for (var i = 0; i < set.SetNeedNum.Length; i++) {
                    var num = set.SetNeedNum[i];
                    if (num == c) {
                        list.Add(set.AffixSet.Levels[i]);
                        break;
                    } else if (num > c) {
                        break;
                    }
                }
            }
            return list;
        }

        public void Apply(CalcContext ctx) {
            var list = GetSetAffix();
            foreach (var affix in list) {
                ctx.MinePanel += affix.AddProps;
            }
        }
    }
}
