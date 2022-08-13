using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 元素类型
    /// </summary>
    public sealed class ElemType {
        public static readonly List<ElemType> All = new();

        public static readonly ElemType Fire = new("Fire", "火", PropType.FireAddHurt);
        public static readonly ElemType Water = new("Water", "水", PropType.WaterAddHurt);
        public static readonly ElemType Wind = new("Wind", "风", PropType.WindAddHurt);
        public static readonly ElemType Elec = new("Elec", "雷", PropType.ElecAddHurt);
        public static readonly ElemType Grass = new("Grass", "草", PropType.GrassAddHurt);
        public static readonly ElemType Ice = new("Ice", "冰", PropType.IceAddHurt);
        public static readonly ElemType Rock = new("Rock", "岩", PropType.RockAddHurt);
        public static readonly ElemType Physical = new("Physical", "物理", PropType.PhysicalAddHurt);

        public readonly string Name;
        public readonly string Desc;

        /// <summary>
        /// 对应的增伤类型
        /// </summary>
        public readonly PropType AddHurtType;

        private ElemType(string name, string desc, PropType addHurtType) {
            All.Add(this);
            Name = name;
            Desc = desc;
            AddHurtType = addHurtType;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
