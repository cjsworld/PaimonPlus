using System;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 突破等级配置数据
    /// </summary>
    public class PromoteLevelData {
        /// <summary>
        /// 突破等级
        /// </summary>
        public readonly int PromoteLevel;

        /// <summary>
        /// 解锁等级上限
        /// </summary>
        public readonly int UnlockMaxLevel;
        
        /// <summary>
        /// 增加属性
        /// </summary>
        public readonly PropPanel AddProps;

        public PromoteLevelData(JObject data) {
            PromoteLevel = data["promoteLevel"].OptInt(1);
            UnlockMaxLevel = data["unlockMaxLevel"].AsInt();
            AddProps = new PropPanel();
            var list = data["addProps"].AsArray();
            foreach (JObject token in list) {
                var type = PropType.GetByConfigName(token["propType"].AsString());
                var value = token["value"].OptDouble();
                if (value > 0) {
                    AddProps += type.By(value);
                }
            }
        }
    }
}
