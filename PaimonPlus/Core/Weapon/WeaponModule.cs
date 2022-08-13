using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 武器模块
    /// </summary>
    public class WeaponModule : CoreEngineModule {
        public readonly Dictionary<int, WeaponData> Weapons = new();

        public override void Init() {
            var config = CoreEngine.ReadJArrayConfig("WeaponExcelConfigData");
            foreach (JObject item in config) {
                var weapon = new WeaponData(item);
                Weapons.Add(weapon.Id, weapon);
            }

            var implNS = this.GetType().Namespace + ".Weapon.Impl";
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == implNS && t.IsSubclassOf(typeof(WeaponImpl))
                        select t;
            foreach (var t in types) {
                Activator.CreateInstance(t);
            }
        }
    }
}
