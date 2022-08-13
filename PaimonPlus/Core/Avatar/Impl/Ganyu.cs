using System;
using System.Collections.Generic;

namespace PaimonPlus.Core.Avatar.Impl {
    /// <summary>
    /// 甘雨
    /// </summary>
    public class Ganyu : AvatarImpl {
        public override int AvatarId => 10000037;
        public override ElemType ElemType => ElemType.Ice;

        public Ganyu() {
        }

        public override List<SkillOption> GetSkillOptions() {
            return new List<SkillOption>() {
                new GanyuSkillA1(this),
                new GanyuSkillA2(this),
            };
        }

        public class GanyuSkillA1 : SkillOption {
            private Ganyu Ganyu;
            public GanyuSkillA1(Ganyu ganyu) {
                Ganyu = ganyu;
            }

            public override string Name => "重击-蓄力第二段";

            public override ElemType SkillElemType => Ganyu.ElemType;


            public override void Prepare(CalcContext ctx) {
                ctx.SkillType = SkillType.AZ;
                var skillLevel = ctx.Avatar.GetSkillLevel(SkillType.A);
                var skill = Ganyu.AvatarData.SkillDepot.GetSkill(SkillType.A).GetProudSkillData(skillLevel);
                ctx.SkillRate = skill.Params[8];
            }

            public override void Apply(CalcContext ctx) {
            }

        }

        public class GanyuSkillA2 : SkillOption {
            private Ganyu Ganyu;
            public GanyuSkillA2(Ganyu ganyu) {
                Ganyu = ganyu;
            }

            public override string Name => "重击-蓄力范围伤害";

            public override ElemType SkillElemType => Ganyu.ElemType;

            public override void Prepare(CalcContext ctx) {
                ctx.SkillType = SkillType.AZ;
                var skillLevel = ctx.Avatar.GetSkillLevel(SkillType.A);
                var skill = Ganyu.AvatarData.SkillDepot.GetSkill(SkillType.A).GetProudSkillData(skillLevel);
                ctx.SkillRate = skill.Params[9];
            }

            public override void Apply(CalcContext ctx) {
            }
        }
    }
}
