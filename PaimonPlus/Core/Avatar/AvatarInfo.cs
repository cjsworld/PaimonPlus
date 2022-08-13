using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    /// <summary>
    /// 角色信息
    /// </summary>
    public class AvatarInfo {
        /// <summary>
        /// 角色配置数据
        /// </summary>
        public readonly AvatarData Data;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否已突破（用于在处于突破等级时区分当前是否已突破）
        /// </summary>
        public bool Promoted { get; set; }

        /// <summary>
        /// 命之座
        /// </summary>
        public int Constellation { get; set; }

        /// <summary>
        /// 武器信息
        /// </summary>
        public WeaponInfo? Weapon { get; set; }

        /// <summary>
        /// 圣遗物信息
        /// </summary>
        public RelicPanel Relic { get; set; }

        public AvatarInfo(AvatarData data) {
            Data = data;
            Level = 90;
            Promoted = false;
            Constellation = 0;
            Relic = new RelicPanel();
        }

        public void SetLevel(int level, bool promoted) {
            Level = level;
            Promoted = promoted;
        }

        /// <summary>
        /// 获取只包含武器的基础面板
        /// </summary>
        /// <returns></returns>
        public PropPanel GetBasePanel() {
            var panel = Data.GetBasePanelAt(Level, Promoted);
            if (Weapon != null) {
                panel += Weapon.GetBasePanel();
            }
            return panel;
        }

        /// <summary>
        /// 获取总面板
        /// </summary>
        /// <returns></returns>
        public PropPanel GetTotalPanel() {
            var panel = GetBasePanel();
            panel += Relic.GetPanel();

            //TODO

            //冰套2件套
            panel += PropType.IceAddHurt.By(0.15000000596046448);

            //阿莫斯
            panel += PropType.AddHurt.By(0.11999999731779099);
            panel += PropType.AddHurt.By(0.079999998211860657 * 5);

            //大招
            //panel += PropType.AddHurt.By(0.20);

            return panel;
        }
    }
}
