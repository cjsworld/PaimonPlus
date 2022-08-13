using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 元素类型
    /// </summary>
    public sealed class ElemType {
        public static readonly List<ElemType> All = new();

        public static readonly ElemType Fire = new("Fire", "火", PropType.FireAddHurt, PropType.FireSubHurt);
        public static readonly ElemType Water = new("Water", "水", PropType.WaterAddHurt, PropType.WaterSubHurt);
        public static readonly ElemType Wind = new("Wind", "风", PropType.WindAddHurt, PropType.WindSubHurt);
        public static readonly ElemType Elec = new("Elec", "雷", PropType.ElecAddHurt, PropType.ElecSubHurt);
        public static readonly ElemType Grass = new("Grass", "草", PropType.GrassAddHurt, PropType.GrassSubHurt);
        public static readonly ElemType Ice = new("Ice", "冰", PropType.IceAddHurt, PropType.IceSubHurt);
        public static readonly ElemType Rock = new("Rock", "岩", PropType.RockAddHurt, PropType.RockSubHurt);
        public static readonly ElemType Physical = new("Physical", "物理", PropType.PhysicalAddHurt, PropType.PhysicalSubHurt);

        public readonly string Name;
        public readonly string Desc;

        /// <summary>
        /// 对应的增伤类型
        /// </summary>
        public readonly PropType AddHurtType;


        /// <summary>
        /// 对应的增伤类型
        /// </summary>
        public readonly PropType SubHurtType;

        private ElemType(string name, string desc, PropType addHurtType, PropType subHurtType) {
            All.Add(this);
            Name = name;
            Desc = desc;
            AddHurtType = addHurtType;
            SubHurtType = subHurtType;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
