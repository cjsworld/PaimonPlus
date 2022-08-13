using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 武器类型
    /// </summary>
    public sealed class WeaponType {
        public static readonly List<WeaponType> All = new();

        public static readonly WeaponType Sword = new("Sword", "单手剑", "WEAPON_SWORD_ONE_HAND");
        public static readonly WeaponType Claymore = new("Claymore", "双手剑", "WEAPON_CLAYMORE");
        public static readonly WeaponType Pole = new("Pole", "长枪", "WEAPON_POLE");
        public static readonly WeaponType Catalyst = new("Catalyst", "法器", "WEAPON_CATALYST");
        public static readonly WeaponType Bow = new("Bow", "弓", "WEAPON_BOW");

        public readonly string Name;
        public readonly string Desc;
        public readonly string ConfigName;

        public static WeaponType GetByConfigName(string configName) {
            var t = All.Find(e => e.ConfigName == configName);
            if (t == null) {
                throw new Exception($"Unknown weapon type: {configName}");
            }
            return t;
        }

        public WeaponType(string name, string desc, string configName) {
            All.Add(this);
            Name = name;
            Desc = desc;
            ConfigName = configName;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
