using System;
using System.Collections.Generic;
using System.Text;

namespace PaimonPlus.Core {
	public class PropPanel : ICloneable {
		private readonly Dictionary<PropType, double> Props;

		public PropPanel(params Prop[] props) {
			Props = new();
			foreach (var p in props) {
				this[p.Type] = p.Value;
			}
		}

		public PropPanel(PropPanel panel) {
			Props = new Dictionary<PropType, double>(panel.Props);
		}

		public double this[PropType type] {
			get {
				if (Props.TryGetValue(type, out var v)) {
					return v;
				} else {
					return 0d;
				}
			}
			set { Props[type] = value; }
		}

		public Prop GetProp(PropType type) {
			return type.By(this[type]);
		}

		public double TotalHP {
			get {
				var value = this[PropType.BaseHP];
				value *= 1 + this[PropType.PercentHP];
				value += this[PropType.HP];
				return value;
			}
		}

		public double TotalATK {
			get {
				var value = this[PropType.BaseATK];
				value *= 1 + this[PropType.PercentATK];
				value += this[PropType.ATK];
				return value;
			}
		}

		public double TotalDEF {
			get {
				var value = this[PropType.BaseDEF];
				value *= 1 + this[PropType.PercentDEF];
				value += this[PropType.DEF];
				return value;
			}
		}

		public PropPanel Apply(Prop prop, Func<double, double, double> func) {
			this[prop.Type] = func(this[prop.Type], prop.Value);
			return this;
		}

		public object Clone() {
			return new PropPanel(this);
		}

		public PropPanel ClonePanel() {
			return new PropPanel(this);
		}

		public static PropPanel operator +(PropPanel lhs, Prop rhs) {
			return lhs.Apply(rhs, (l, r) => l + r);
		}

		public static PropPanel operator -(PropPanel lhs, Prop rhs) {
			return lhs.Apply(rhs, (l, r) => l - r);
		}

		public static PropPanel operator *(PropPanel lhs, Prop rhs) {
			return lhs.Apply(rhs, (l, r) => l * r);
		}

		public static PropPanel operator /(PropPanel lhs, Prop rhs) {
			return lhs.Apply(rhs, (l, r) => l / r);
		}

		public static PropPanel operator +(PropPanel lhs, PropPanel rhs) {
			if (rhs.Props.Count > 0) {
				foreach (var entry in rhs.Props) {
					lhs[entry.Key] = lhs[entry.Key] + entry.Value;
				}
			}
			return lhs;
		}

		override public string ToString() {
			var sb = new StringBuilder();
			foreach (PropType t in Enum.GetValues(typeof(PropType))) {
				if (Props.ContainsKey(t)) {
					if (sb.Length > 0) {
						sb.Append(" | ");
					}
					sb.Append(t.ToString());
					sb.Append(':');
					sb.Append(this[t]);
				}
			}
			return sb.ToString();
		}
	}
}
