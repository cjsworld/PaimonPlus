using System;
using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 角色配置数据
    /// </summary>
    public class AvatarData {
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
        public readonly string QualityType;

        /// <summary>
        /// 基础属性
        /// </summary>
        public readonly PropPanel BaseProps;

        /// <summary>
        /// 等级成长曲线
        /// </summary>
        public readonly Dictionary<PropType, CurveData> Curves;

        /// <summary>
        /// 突破配置
        /// </summary>
        public readonly PromoteData Promote;

        /// <summary>
        /// 技能配置
        /// </summary>
        public readonly SkillDepotData SkillDepot;


        public AvatarData(JObject data) {
            Id = data["id"].AsInt();
            var icon = data["iconName"].AsString();
            Icon = icon.Replace("UI_AvatarIcon_", "");
            Name = CoreEngine.Ins.GetText(data["nameTextMapHash"].AsLong());
            WeaponType = WeaponType.GetByConfigName(data["weaponType"].AsString());
            QualityType = data["qualityType"].AsString();
            BaseProps = new PropPanel(
                PropType.BaseHP.By(data["hpBase"].AsDouble()),
                PropType.BaseATK.By(data["attackBase"].AsDouble()),
                PropType.BaseDEF.By(data["defenseBase"].AsDouble()),
                PropType.ChargeRate.By(1),
                PropType.CritRate.By(data["critical"].AsDouble()),
                PropType.CritHurt.By(data["criticalHurt"].AsDouble())
            );

            Curves = new Dictionary<PropType, CurveData>();
            var curves = data["propGrowCurves"].AsArray();
            foreach (JObject item in curves) {
                var propType = PropType.GetByConfigName(item["type"].AsString());
                var curveType = item["growCurve"].AsString();
                var curve = CoreEngine.Ins.Upgrade.Curves[curveType];
                Curves[propType] = curve;
            }

            var promoteId = data["avatarPromoteId"].AsInt();
            Promote = CoreEngine.Ins.Upgrade.Promotes[promoteId];

            var depotId = data["skillDepotId"].AsInt();
            SkillDepot = CoreEngine.Ins.Skill.SkillDepots[depotId];
        }

        /// <summary>
        /// 创建新的角色信息
        /// </summary>
        /// <returns></returns>
        public AvatarInfo NewInfo() {
            return new AvatarInfo(this);
        }

        /// <summary>
        /// 获得无武器、圣遗物时的基础面板
        /// </summary>
        /// <param name="level"></param>
        /// <param name="promoted"></param>
        /// <returns></returns>
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
