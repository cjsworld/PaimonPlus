namespace PaimonPlus.Core {
	public class Prop {
		public PropType Type { get; set; }
		public double Value { get; set; }

		public Prop(PropType type, double value) {
			Type = type;
			Value = value;
		}

		public static implicit operator double(Prop prop) { return prop.Value; }

		public override string ToString() {
			return $"{Type}: {Value}";
		}
	}
}
