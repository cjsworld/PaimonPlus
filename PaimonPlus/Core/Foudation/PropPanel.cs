using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PaimonPlus.Core {
    /// <summary>
    /// 属性面板
    /// </summary>
    public class PropPanel : ICloneable, IEnumerable<Prop> {
        private readonly Dictionary<PropType, double> Props;

        /// <summary>
        /// 根据指定的属性来创建
        /// </summary>
        /// <param name="props"></param>
        public PropPanel(params Prop[] props) {
            Props = new();
            foreach (var p in props) {
                this[p.Type] = p.Value;
            }
        }

        /// <summary>
        /// 从另一个属性面板复制
        /// </summary>
        /// <param name="panel"></param>
        public PropPanel(PropPanel panel) {
            Props = new Dictionary<PropType, double>(panel.Props);
        }

        /// <summary>
        /// 获取某个类型的值
        /// </summary>
        /// <param name="type"></param>
        /// <returns>如果值不存在，返回0</returns>
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

        /// <summary>
        /// 获取某个类型的值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Prop GetProp(PropType type) {
            return type.By(this[type]);
        }

        /// <summary>
        /// 是否包含某个属性值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasProp(PropType type) {
            return Props.ContainsKey(type);
        }

        /// <summary>
        /// 总生命值
        /// </summary>
        public double TotalHP {
            get {
                var value = this[PropType.BaseHP];
                value *= 1 + this[PropType.PercentHP];
                value += this[PropType.HP];
                return value;
            }
        }

        /// <summary>
        /// 总攻击力
        /// </summary>
        public double TotalATK {
            get {
                var value = this[PropType.BaseATK];
                value *= 1 + this[PropType.PercentATK];
                value += this[PropType.ATK];
                return value;
            }
        }

        /// <summary>
        /// 总防御力
        /// </summary>
        public double TotalDEF {
            get {
                var value = this[PropType.BaseDEF];
                value *= 1 + this[PropType.PercentDEF];
                value += this[PropType.DEF];
                return value;
            }
        }

        /// <summary>
        /// 对当前面板某个特定类型属性，执行数值操作
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public PropPanel Apply(Prop prop, Func<double, double, double> func) {
            this[prop.Type] = func(this[prop.Type], prop.Value);
            return this;
        }

        /// <summary>
        /// 复制一个面板
        /// </summary>
        /// <returns></returns>
        public object Clone() {
            return new PropPanel(this);
        }

        /// <summary>
        /// 复制一个面板
        /// </summary>
        /// <returns></returns>
        public PropPanel ClonePanel() {
            return new PropPanel(this);
        }

        /// <summary>
        /// 加属性
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static PropPanel operator +(PropPanel lhs, Prop rhs) {
            return lhs.Apply(rhs, (l, r) => l + r);
        }

        /// <summary>
        /// 减属性
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static PropPanel operator -(PropPanel lhs, Prop rhs) {
            return lhs.Apply(rhs, (l, r) => l - r);
        }

        /// <summary>
        /// 乘属性
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static PropPanel operator *(PropPanel lhs, Prop rhs) {
            return lhs.Apply(rhs, (l, r) => l * r);
        }

        /// <summary>
        /// 除属性
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static PropPanel operator /(PropPanel lhs, Prop rhs) {
            return lhs.Apply(rhs, (l, r) => l / r);
        }

        /// <summary>
        /// 与另一个属性面板相加
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
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
            foreach (PropType t in PropType.All) {
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

        public IEnumerator<Prop> GetEnumerator() {
            return new PropPanelEnumerator(Props.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }


        private class PropPanelEnumerator : IEnumerator<Prop> {
            private IEnumerator<KeyValuePair<PropType, double>> Nested;

            public PropPanelEnumerator(IEnumerator<KeyValuePair<PropType, double>> nested) {
                Nested = nested;
            }

            public Prop Current {
                get {
                    var entry = Nested.Current;
                    return entry.Key.By(entry.Value);
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                Nested.Dispose();
            }

            public bool MoveNext() {
                return Nested.MoveNext();
            }

            public void Reset() {
                Nested.Reset();
            }
        }
    }
}
