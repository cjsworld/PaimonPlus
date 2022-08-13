namespace PaimonPlus.Core {
    /// <summary>
    /// 属性数值
    /// </summary>
    public class Prop {
        /// <summary>
        /// 类型
        /// </summary>
        public PropType Type { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
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
