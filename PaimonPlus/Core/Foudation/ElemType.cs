namespace PaimonPlus.Core {
	public sealed class ElemType {
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
		public readonly PropType AddHurtType;

		private ElemType(string name, string desc, PropType addHurtType) {
			Name = name;
			Desc = desc;
			AddHurtType = addHurtType;
		}

		public override string ToString() {
			return Name;
		}
	}
}
