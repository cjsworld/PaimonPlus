using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaimonPlus.Core {
    /// <summary>
    /// 怪物信息
    /// </summary>
    public class MonsterInfo {
        public int Level { get; set; } = 90;
        public PropPanel Props { get; set; } = new PropPanel();
    }
}
