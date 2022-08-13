using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 圣遗物信息
    /// </summary>
    public class RelicInfo {
        /// <summary>
        /// 圣遗物槽位配置数据
        /// </summary>
        public readonly RelicSlotData Slot;

        /// <summary>
        /// 星级配置数据
        /// </summary>
        public readonly RelicRankData RankData;
        
        /// <summary>
        /// 星级
        /// </summary>
        public int Rank { get => RankData.Rank; }

        /// <summary>
        /// 主属性类型
        /// </summary>
        public PropType MainPropType { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 副词条属性
        /// </summary>
        public PropPanel SubProp { get; set; }

        public RelicInfo(RelicSlotData slot, int rank) {
            Slot = slot;
            MainPropType = slot.Type.MainPropTypes[0];
            RankData = CoreEngine.Ins.Relic.Ranks[rank];
            Level = 20;
            SubProp = new PropPanel();
        }

        /// <summary>
        /// 根据等级获取当前主属性数值
        /// </summary>
        public Prop MainProp { get => RankData.GetMainProp(MainPropType, Level); }

        /// <summary>
        /// 增加一个副词条
        /// <br/>
        /// 实际数值会根据配置猜测原始精确值
        /// </summary>
        /// <param name="prop"></param>
        public void AddSubProp(Prop prop) {
            AddSubProp(prop.Type, prop.Value);
        }

        /// <summary>
        /// 增加一个副词条
        /// <br/>
        /// 实际数值会根据配置猜测原始精确值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void AddSubProp(PropType type, double value) {
            var rankData = CoreEngine.Ins.Relic.Ranks[Rank];
            SubProp[type] = rankData.SubProps[type].CalcPreciseValue(value);
        }
    }
}
