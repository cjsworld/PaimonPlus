using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 武器信息
    /// </summary>
    public class WeaponInfo {
        /// <summary>
        /// 武器数据
        /// </summary>
        public readonly WeaponData Data;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否已突破（用于在处于突破等级时区分当前是否已突破）
        /// </summary>
        public bool Promoted { get; set; }

        /// <summary>
        /// 精练等级，初始为1
        /// </summary>
        public int Refine { get; set; }

        public WeaponInfo(WeaponData data) {
            Data = data;
            Level = 90;
            Promoted = false;
            Refine = 1;
        }

        public void SetLevel(int level, bool promoted) {
            Level = level;
            Promoted = promoted;
        }

        /// <summary>
        /// 获取武器带来的面板提升
        /// </summary>
        /// <returns></returns>
        public PropPanel GetBasePanel() {
            var panel = Data.GetBasePanelAt(Level, Promoted);
            var affix = GetAffix();
            panel += affix.AddProps;
            return panel;
        }

        public AffixData GetAffix() {
            return Data.Affix.Levels[Refine - 1];
        }
    }
}
