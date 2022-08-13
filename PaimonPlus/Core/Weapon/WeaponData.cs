using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 武器配置数据
    /// </summary>
    public class WeaponData {
        public readonly int Id;
        public readonly string Icon;
        public readonly string Name;

        /// <summary>
        /// 武器类型
        /// </summary>
        public readonly WeaponType WeaponType;

        /// <summary>
        /// 星级
        /// </summary>
        public readonly int Rank;

        /// <summary>
        /// 基础属性
        /// </summary>
        public readonly PropPanel BaseProps;

        /// <summary>
        /// 升级曲线
        /// </summary>
        public readonly Dictionary<PropType, CurveData> Curves;

        /// <summary>
        /// 突破配置
        /// </summary>
        public readonly PromoteData Promote;

        /// <summary>
        /// 效果
        /// </summary>
        public readonly AffixSetData Affix;

        /// <summary>
        /// 武器代码实现
        /// </summary>
        public WeaponImpl? Impl = null;

        public WeaponData(JObject data) {
            Id = data["id"].AsInt();
            var icon = data["icon"].AsString();
            Icon = icon.Replace("UI_EquipIcon_", "");
            Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
            WeaponType = WeaponType.GetByConfigName(data["weaponType"].AsString());
            Rank = data["rankLevel"].AsInt();

            BaseProps = new PropPanel();
            Curves = new Dictionary<PropType, CurveData>();
            var list = data["weaponProp"].AsArray();
            foreach (JObject item in list) {
                var propTypeStr = item["propType"].GetString();
                if (propTypeStr == null) {
                    continue;
                }
                var propType = PropType.GetByConfigName(propTypeStr);
                var value = item["initValue"].OptDouble();
                var curveType = item["type"].AsString();
                var curve = CoreEngine.Ins.Upgrade.Curves[curveType];
                BaseProps += propType.By(value);
                Curves[propType] = curve;
            }

            var promoteId = data["weaponPromoteId"].AsInt();
            Promote = CoreEngine.Ins.Upgrade.Promotes[promoteId];

            var affixs = data["skillAffix"].AsArray();
            foreach (JToken item in affixs) {
                var id = item.AsInt();
                if (id == 0) {
                    continue;
                }
                if (Affix != null) {
                    throw new Exception($"Weapon {Id} {Name} has more than one affix!");
                }
                Affix = CoreEngine.Ins.Affix.Affixs[id];
            }
        }

        public WeaponInfo NewInfo() {
            return new WeaponInfo(this);
        }

        public PropPanel GetBasePanelAt(int level, bool promoted) {
            var panel = BaseProps.ClonePanel();
            foreach (var entry in Curves) {
                panel *= entry.Key.By(entry.Value[level]);
            }
            panel += Promote.GetAddPropsAt(level, promoted);
            return panel;
        }

        public override string ToString() {
            return Name;
        }
    }
}
