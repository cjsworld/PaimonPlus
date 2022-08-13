﻿using System;
using System.Collections.Generic;

namespace PaimonPlus.Core {
    public abstract class AvatarImpl {
        public abstract int AvatarId { get; }
        public abstract ElemType ElemType { get; }

        public readonly AvatarData AvatarData;

        public AvatarImpl() {
            AvatarData = CoreEngine.Ins.Avatar.Avatars[AvatarId];
            AvatarData.ElemType = ElemType;
            AvatarData.Impl = this;
        }

        public abstract List<SkillOption> GetSkillOptions();
    }
}
