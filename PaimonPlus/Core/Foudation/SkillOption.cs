using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    public abstract class SkillOption {
        public abstract string Name { get; }
        public abstract ElemType SkillElemType { get; }

        public abstract void Prepare(CalcContext ctx);
        public abstract void Apply(CalcContext ctx);
    }
}
