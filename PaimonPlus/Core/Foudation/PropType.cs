using System;
using System.Diagnostics;

namespace PaimonPlus.Core {
	public enum PropType {
		/** 基础生命值 */
		BaseHP,
		/** 生命值百分比 */
		PercentHP,
		/** 生命值 */
		HP,
		/** 攻击力 */
		BaseATK,
		/** 攻击力百分比 */
		PercentATK,
		/** 攻击力 */
		ATK,
		/** 基础防御力 */
		BaseDEF,
		/** 防御力百分比 */
		PercentDEF,
		/** 防御力 */
		DEF,

		/** 元素精通*/
		ElemMastery,

		/** 暴击率 */
		CritRate,
		/** 暴击伤害 */
		CritHurt,

		/** 元素充能 */
		ChargeRate,

		/** 伤害加成 */
		AddHurt,

		/** 火伤加成 */
		FireAddHurt,

		/** 水伤加成 */
		WaterAddHurt,

		/** 风伤加成 */
		WindAddHurt,

		/** 雷伤加成 */
		ElecAddHurt,

		/** 草伤加成 */
		GrassAddHurt,

		/** 冰伤加成 */
		IceAddHurt,

		/** 岩伤加成 */
		RockAddHurt,

		/** 物伤加成 */
		PhysicalAddHurt,

		Unknown
	}

	public static class PropTypeExt {
		public static Prop By(this PropType type, double value) {
			return new Prop(type, value);
		}

		public static PropType FromConfigName(string configName) {
			switch (configName) {
				case "FIGHT_PROP_BASE_HP":
					return PropType.BaseHP;
				case "FIGHT_PROP_HP_PERCENT":
					return PropType.PercentHP;
				case "FIGHT_PROP_HP":
					return PropType.HP;

				case "FIGHT_PROP_BASE_ATTACK":
					return PropType.BaseATK;
				case "FIGHT_PROP_ATTACK_PERCENT":
					return PropType.PercentATK;
				case "FIGHT_PROP_ATTACK":
					return PropType.ATK;

				case "FIGHT_PROP_BASE_DEFENSE":
					return PropType.BaseDEF;
				case "FIGHT_PROP_DEFENSE_PERCENT":
					return PropType.PercentDEF;
				case "FIGHT_PROP_DEFENSE":
					return PropType.DEF;

				case "FIGHT_PROP_CRITICAL":
					return PropType.CritRate;
				case "FIGHT_PROP_CRITICAL_HURT":
					return PropType.CritHurt;

				case "FIGHT_PROP_ELEMENT_MASTERY":
					return PropType.ElemMastery;
				case "FIGHT_PROP_CHARGE_EFFICIENCY":
					return PropType.ChargeRate;

				case "FIGHT_PROP_ADD_HURT": //造成伤害提升
					return PropType.AddHurt;
				case "FIGHT_PROP_FIRE_ADD_HURT":
					return PropType.FireAddHurt;
				case "FIGHT_PROP_WATER_ADD_HURT":
					return PropType.WaterAddHurt;
				case "FIGHT_PROP_WIND_ADD_HURT":
					return PropType.WindAddHurt;
				case "FIGHT_PROP_ELEC_ADD_HURT":
					return PropType.ElecAddHurt;
				case "FIGHT_PROP_GRASS_ADD_HURT":
					return PropType.GrassAddHurt;
				case "FIGHT_PROP_ICE_ADD_HURT":
					return PropType.IceAddHurt;
				case "FIGHT_PROP_ROCK_ADD_HURT":
					return PropType.RockAddHurt;
				case "FIGHT_PROP_PHYSICAL_ADD_HURT":
					return PropType.PhysicalAddHurt;

				case "FIGHT_PROP_SUB_HURT": //抗性
				case "FIGHT_PROP_FIRE_SUB_HURT":
				case "FIGHT_PROP_WATER_SUB_HURT":
				case "FIGHT_PROP_WIND_SUB_HURT":
				case "FIGHT_PROP_ELEC_SUB_HURT":
				case "FIGHT_PROP_ICE_SUB_HURT":
				case "FIGHT_PROP_GRASS_SUB_HURT":
				case "FIGHT_PROP_ROCK_SUB_HURT":
				case "FIGHT_PROP_PHYSICAL_SUB_HURT":

				case "FIGHT_PROP_HEAL_ADD":
				case "FIGHT_PROP_HEALED_ADD":
				case "FIGHT_PROP_SHIELD_COST_MINUS_RATIO"://护盾强效
					return PropType.Unknown;
				default:
					Trace.WriteLine($"Unknown prop type: {configName}");
					return PropType.Unknown;
			}

		}
	}
}
