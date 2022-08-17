using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 元素类型
    /// </summary>
    public sealed class ElemType {
        public static readonly List<ElemType> All = new();

        public static readonly ElemType Fire = new(0, "Fire", "火", 104111, PropType.FireAddHurt, PropType.FireSubHurt);
        public static readonly ElemType Water = new(1, "Water", "水", 104121, PropType.WaterAddHurt, PropType.WaterSubHurt);
        public static readonly ElemType Wind = new(2, "Wind", "风", 104151, PropType.WindAddHurt, PropType.WindSubHurt);
        public static readonly ElemType Elec = new(3, "Elec", "雷", 104141, PropType.ElecAddHurt, PropType.ElecSubHurt);
        public static readonly ElemType Grass = new(4, "Grass", "草", 104131/*推测*/, PropType.GrassAddHurt, PropType.GrassSubHurt);
        public static readonly ElemType Ice = new(5, "Ice", "冰", 104161, PropType.IceAddHurt, PropType.IceSubHurt);
        public static readonly ElemType Rock = new(6, "Rock", "岩", 104171, PropType.RockAddHurt, PropType.RockSubHurt);
        public static readonly ElemType Physical = new(7, "Physical", "物理", null, PropType.PhysicalAddHurt, PropType.PhysicalSubHurt);

        public readonly int Index;
        public readonly string Name;
        public readonly string Desc;

        public readonly int? MatId;

        /// <summary>
        /// 对应的增伤类型
        /// </summary>
        public readonly PropType AddHurtType;


        /// <summary>
        /// 对应的增伤类型
        /// </summary>
        public readonly PropType SubHurtType;

        public static ElemType GetByMatId(int? matId) {
            if (matId == null) {
                return Physical;
            }
            return All.Find(e => e.MatId == matId) ?? Physical;
        }

        private ElemType(int index, string name, string desc, int? matId, PropType addHurtType, PropType subHurtType) {
            All.Add(this);
            Index = index;
            Name = name;
            Desc = desc;
            MatId = matId;
            AddHurtType = addHurtType;
            SubHurtType = subHurtType;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
