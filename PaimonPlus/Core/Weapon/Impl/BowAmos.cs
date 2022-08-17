using System;
using System.Collections.Generic;

namespace PaimonPlus.Core.Weapon.Impl {
    /// <summary>
    /// 阿莫斯之弓
    /// </summary>
    public class BowAmos : WeaponImpl {
        public override int WeaponId => 15502;

        public override void Apply(CalcContext ctx) {
            var affix = ctx.Avatar.Weapon.GetAffix();
            if (ctx.SkillType == SkillType.A || ctx.SkillType == SkillType.AZ) {
                ctx.MinePanel += PropType.AddHurt.By(affix.Params[0]); //普通攻击和重击造成的伤害提升{0}%
                ctx.MinePanel += PropType.AddHurt.By(affix.Params[1] * 5); //TODO 箭矢发射后每经过0.1秒，伤害还会提升{1}%。至多提升5次
            }
        }
    }
}
