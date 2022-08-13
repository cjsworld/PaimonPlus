using System;
using System.Diagnostics;

namespace PaimonPlus.Core {
    /// <summary>
    /// 数值计算环境
    /// </summary>
    public class CalcContext {
        /// <summary>
        /// 角色
        /// </summary>
        public AvatarInfo Avatar { get; set; }

        /// <summary>
        /// 敌人
        /// </summary>
        public MonsterInfo Monster { get; set; }

        /// <summary>
        /// 技能
        /// </summary>
        public SkillOption SkillOption { get; set; }


        /// <summary>
        /// 自身的属性值
        /// </summary>
        public PropPanel MinePanel;

        /// <summary>
        /// 敌人属性值
        /// </summary>
        public PropPanel TargetPanel;

        /// <summary>
        /// 技能类型
        /// </summary>
        public SkillType SkillType;

        /// <summary>
        /// 技能倍率
        /// </summary>
        public double SkillRate;

        /// <summary>
        /// 反应倍率
        /// </summary>
        public double ReactionRate; //TODO


        public double TotalDamage() {
            MinePanel = Avatar.GetTotalPanel();
            //Trace.WriteLine(MinePanel);

            TargetPanel = Monster.Props.ClonePanel();

            SkillOption.Prepare(this); //技能初始化
            Avatar.Relic.Apply(this); //圣遗物效果
            Avatar.Weapon.Data.Impl?.Apply(this); //武器效果
            SkillOption.Apply(this); //技能效果

            //基础乘区（攻击力）
            var damage = MinePanel.TotalATK;

            //倍率区
            damage *= SkillRate;

            //增伤区
            var addHurt = MinePanel[PropType.AddHurt];
            addHurt += MinePanel[SkillOption.SkillElemType.AddHurtType];
            damage *= 1 + addHurt;

            //暴击乘区
            damage *= 1 + MinePanel[PropType.CritHurt];

            //反应区
            if (ReactionRate > 0) {
                var elemMastery = MinePanel[PropType.ElemMastery];
                var r = 2.78f * elemMastery / (elemMastery + 1400);
                damage *= ReactionRate * (1 + r);
            }

            //防御区
            damage *= (Avatar.Level + 100) / (Avatar.Level + 100 + (1 - MinePanel[PropType.IngoreDEF]) * (1 - TargetPanel[PropType.PercentDEF]) * (Monster.Level + 100));

            //抗性区
            var subHurt = TargetPanel[SkillOption.SkillElemType.SubHurtType];
            if (subHurt < 0) {
                damage *= 1 - subHurt / 2;
            } else if (subHurt <= 0.75f) {
                damage *= 1 - subHurt;
            } else {
                damage *= 1 / (1 + 4 * subHurt);
            }

            return damage;
        }
    }
}
