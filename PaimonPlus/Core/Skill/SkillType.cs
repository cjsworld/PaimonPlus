using System;
using System.Collections.Generic;


namespace PaimonPlus.Core {
    /// <summary>
    /// 技能类型
    /// </summary>
    public sealed class SkillType {
        public static readonly List<SkillType> All = new();

        public static readonly SkillType A = new(0, "普通攻击");
        public static readonly SkillType AZ = new(-1, "重击");
        public static readonly SkillType E = new(1, "元素战技");
        public static readonly SkillType Shift = new(2, "冲刺");
        public static readonly SkillType Q = new(5, "元素爆发");

        /// <summary>
        /// 触发器ID，可能对应到界面上各种按钮，比如普通，E，Q，冲刺之类
        /// </summary>
        public readonly int TriggerID;
        public readonly string Desc;

        public static SkillType GetByTriggerID(int triggerID) {
            var t = All.Find(e => e.TriggerID == triggerID);
            if (t == null) {
                throw new Exception($"Unknown SkillType trigger id {triggerID}");
            }
            return t;
        }

        private SkillType(int triggerID, string name) {
            All.Add(this);
            TriggerID = triggerID;
            Desc = name;
        }

        public override string ToString() {
            return Desc;
        }
    }
}
