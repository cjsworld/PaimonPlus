using PaimonPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaimonPlus {
	public class DamageFormula {
		//public StatusPanel Panel { get; set; }
		//public float SkillRate { get; set; }
		//public float ExtraDamage { get; set; }
		//public float ReactionRate { get; set; }
		//public int MineLevel { get; set; }
		//public float DecreaseDEF { get; set; }
		//public int TargetLevel { get; set; }
		//public float TargetElemResistance { get; set; }


		//public float TotalDamage() {
		//	//基础乘区（攻击力）
		//	float damage = Panel.ATK;
		//	//倍率区
		//	damage *= SkillRate;
		//	//增伤区
		//	damage *= (1 + ExtraDamage);
		//	//暴击乘区
		//	damage *= (1 + Panel.CritDamage);
		//	//反应区
		//	if (ReactionRate > 0) {
		//		float r = 2.78f * Panel.ElemMastery / (Panel.ElemMastery + 1400);
		//		damage *= ReactionRate * (1 + r);
		//	}
		//	//防御区
		//	damage *= (MineLevel + 100) / (MineLevel + 100 + (1 - DecreaseDEF) * (TargetLevel + 100));
		//	//抗性区
		//	if (TargetElemResistance < 0) {
		//		damage *= (1 - TargetElemResistance / 2);
		//	} else if (TargetElemResistance <= 0.75f) {
		//		damage *= (1 - TargetElemResistance);
		//	} else {
		//		damage *= (1 / (1 + 4 * TargetElemResistance));
		//	}
		//	return damage;
		//}
	}
}
