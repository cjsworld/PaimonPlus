using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaimonPlus.Core {
    public abstract class WeaponImpl {
        public abstract int WeaponId { get; }

        public readonly WeaponData WeaponData;
        
        public WeaponImpl() {
            WeaponData = CoreEngine.Ins.Weapon.Weapons[WeaponId];
            WeaponData.Impl = this;
        }

        public abstract void Apply(CalcContext ctx);
    }
}
