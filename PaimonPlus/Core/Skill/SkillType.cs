using System;
using System.Collections.Generic;


namespace PaimonPlus.Core {
	public sealed class SkillType {
		public static readonly List<SkillType> All = new();
		public static readonly SkillType A = new(0, "普通攻击");
		public static readonly SkillType E = new(1, "元素战技");
		public static readonly SkillType Shift = new(2, "冲刺");
		public static readonly SkillType Q = new(5, "元素爆发");

		public readonly int TriggerID;
		public readonly string Name;

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
			Name = name;
		}
	}
}
