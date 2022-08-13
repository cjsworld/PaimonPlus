using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PaimonPlus.Core {
    /// <summary>
    /// 属性数值类型
    /// </summary>
    public sealed class PropType {
        public static readonly List<PropType> All = new();

        public static readonly PropType BaseHP = new("BaseHP", "基础生命值", false, "FIGHT_PROP_BASE_HP");
        public static readonly PropType PercentHP = new("PercentHP", "生命值百分比", true, "FIGHT_PROP_HP_PERCENT");
        public static readonly PropType HP = new("HP", "生命值", false, "FIGHT_PROP_HP");

        public static readonly PropType BaseATK = new("BaseATK", "攻击力", false, "FIGHT_PROP_BASE_ATTACK");
        public static readonly PropType PercentATK = new("PercentATK", "攻击力百分比", true, "FIGHT_PROP_ATTACK_PERCENT");
        public static readonly PropType ATK = new("ATK", "攻击力", false, "FIGHT_PROP_ATTACK");

        public static readonly PropType BaseDEF = new("BaseDEF", "基础防御力", false, "FIGHT_PROP_BASE_DEFENSE");
        public static readonly PropType PercentDEF = new("PercentDEF", "防御力百分比", true, "FIGHT_PROP_DEFENSE_PERCENT");
        public static readonly PropType DEF = new("DEF", "防御力", false, "FIGHT_PROP_DEFENSE");

        public static readonly PropType IngoreDEF = new("PercentSubDEF", "无视防御力", true, null);

        public static readonly PropType ElemMastery = new("ElemMastery", "元素精通", false, "FIGHT_PROP_ELEMENT_MASTERY");

        public static readonly PropType CritRate = new("CritRate", "暴击率", true, "FIGHT_PROP_CRITICAL");
        public static readonly PropType CritHurt = new("CritHurt", "暴击伤害", true, "FIGHT_PROP_CRITICAL_HURT");

        public static readonly PropType ChargeRate = new("ChargeRate", "元素充能", true, "FIGHT_PROP_CHARGE_EFFICIENCY");

        public static readonly PropType AddHurt = new("AddHurt", "伤害加成", true, "FIGHT_PROP_ADD_HURT");
        public static readonly PropType FireAddHurt = new("FireAddHurt", "火元素伤害加成", true, "FIGHT_PROP_FIRE_ADD_HURT");
        public static readonly PropType WaterAddHurt = new("WaterAddHurt", "水元素伤害加成", true, "FIGHT_PROP_WATER_ADD_HURT");
        public static readonly PropType WindAddHurt = new("WindAddHurt", "风元素伤害加成", true, "FIGHT_PROP_WIND_ADD_HURT");
        public static readonly PropType ElecAddHurt = new("ElecAddHurt", "雷元素伤害加成", true, "FIGHT_PROP_ELEC_ADD_HURT");
        public static readonly PropType GrassAddHurt = new("GrassAddHurt", "草元素伤害加成", true, "FIGHT_PROP_GRASS_ADD_HURT");
        public static readonly PropType IceAddHurt = new("IceAddHurt", "冰元素伤害加成", true, "FIGHT_PROP_ICE_ADD_HURT");
        public static readonly PropType RockAddHurt = new("RockAddHurt", "岩元素伤害加成", true, "FIGHT_PROP_ROCK_ADD_HURT");
        public static readonly PropType PhysicalAddHurt = new("PhysicalAddHurt", "物理伤害加成", true, "FIGHT_PROP_PHYSICAL_ADD_HURT");


        public static readonly PropType SubHurt = new("AddHurt", "伤害减免", true, "FIGHT_PROP_SUB_HURT");
        public static readonly PropType FireSubHurt = new("FireAddHurt", "火元素抗性", true, "FIGHT_PROP_FIRE_SUB_HURT");
        public static readonly PropType WaterSubHurt = new("WaterAddHurt", "水元素抗性", true, "FIGHT_PROP_WATER_SUB_HURT");
        public static readonly PropType WindSubHurt = new("WindAddHurt", "风元素抗性", true, "FIGHT_PROP_WIND_SUB_HURT");
        public static readonly PropType ElecSubHurt = new("ElecAddHurt", "雷元素抗性", true, "FIGHT_PROP_ELEC_SUB_HURT");
        public static readonly PropType GrassSubHurt = new("GrassAddHurt", "草元素抗性", true, "FIGHT_PROP_GRASS_SUB_HURT");
        public static readonly PropType IceSubHurt = new("IceAddHurt", "冰元素抗性", true, "FIGHT_PROP_ICE_SUB_HURT");
        public static readonly PropType RockSubHurt = new("RockAddHurt", "岩元素抗性", true, "FIGHT_PROP_ROCK_SUB_HURT");
        public static readonly PropType PhysicalSubHurt = new("PhysicalAddHurt", "物理抗性", true, "FIGHT_PROP_PHYSICAL_SUB_HURT");

        public static readonly PropType HealAdd = new("HealAdd", "治疗加成", true, "FIGHT_PROP_HEAL_ADD");
        public static readonly PropType HealedAdd = new("HealedAdd", "受治疗加成", true, "FIGHT_PROP_HEALED_ADD");
        public static readonly PropType ShieldUp = new("ShieldUp", "护盾强效", true, "FIGHT_PROP_SHIELD_COST_MINUS_RATIO");

        public static readonly PropType Unknown = new("Unknown", "未知", false, null);

        public readonly string Name;
        public readonly string Desc;
        public readonly bool IsPercent;

        /// <summary>
        /// 配置文件中的名字
        /// </summary>
        public readonly string? ConfigName;


        public static PropType GetByConfigName(string configName) {
            var t = All.Find(e => e.ConfigName == configName);
            if (t == null) {
                Trace.WriteLine($"Unknown prop type: {configName}");
                return Unknown;
            }
            return t;
        }

        private PropType(string name, string desc, bool isPercent, string? configName) {
            All.Add(this);
            Name = name;
            Desc = desc;
            IsPercent = isPercent;
            ConfigName = configName;
        }

        public Prop By(double value) {
            return new Prop(this, value);
        }

        public override string ToString() {
            return Desc;
        }
    }
}
