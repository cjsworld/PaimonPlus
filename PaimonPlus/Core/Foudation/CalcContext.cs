using System;
using System.Collections.Generic;
using System.Diagnostics;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class CalcContext {
		public AvatarInfo Avatar { get; set; }

		public ElemType SkillElemType { get; set; }
		public double SkillRate { get; set; }
		public double ReactionRate { get; set; }
		public int MineLevel { get; set; }
		public double DecreaseDEF { get; set; }
		public int TargetLevel { get; set; }
		public double TargetElemResistance { get; set; }


		public double TotalDamage() {
			var panel = Avatar.GetTotalPanel();

			var atk = panel[PropType.BaseATK];
			atk *= 1 + panel[PropType.PercentATK];
			atk += panel[PropType.ATK];

			//基础乘区（攻击力）
			var damage = atk;

			//倍率区
			damage *= SkillRate;

			//增伤区
			var addHurt = panel[PropType.AddHurt];
			addHurt += panel[SkillElemType.AddHurtType];
			damage *= 1 + addHurt;

			//暴击乘区
			damage *= 1 + panel[PropType.CritHurt];

			//反应区
			if (ReactionRate > 0) {
				var elemMastery = panel[PropType.ElemMastery];
				var r = 2.78f * elemMastery / (elemMastery + 1400);
				damage *= ReactionRate * (1 + r);
			}

			//防御区
			damage *= (MineLevel + 100) / (MineLevel + 100 + (1 - DecreaseDEF) * (TargetLevel + 100));

			//抗性区
			if (TargetElemResistance < 0) {
				damage *= 1 - TargetElemResistance / 2;
			} else if (TargetElemResistance <= 0.75f) {
				damage *= 1 - TargetElemResistance;
			} else {
				damage *= 1 / (1 + 4 * TargetElemResistance);
			}

			return damage;
		}
	}
}
