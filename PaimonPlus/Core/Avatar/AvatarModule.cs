using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using XPlugin.Json;

namespace PaimonPlus.Core {
    public class AvatarModule : CoreEngineModule {
        public Dictionary<int, AvatarData> Avatars { get; private set; } = new();

        public Collection<AvatarData> SortedAvatars {
            get {
                return new ObservableCollection<AvatarData>(Avatars.Values.OrderBy(x => {
                    //元素类型正序，星级倒序，id正序
                    return x.ElemType.Index * 10000 + (10 - x.Rank) * 1000 + x.Id % 1000;
                }).AsEnumerable());
            }
        }

        public override void Init() {
            var config = CoreEngine.ReadJArrayConfig("AvatarExcelConfigData");
            foreach (JObject item in config) {
                if (item["useType"].OptString() != "AVATAR_FORMAL") {
                    continue;
                }
                var avatar = new AvatarData(item);
                if (avatar.Id == 10000005/*空*/ || avatar.Id == 10000007/*莹*/) {
                    //暂不支持旅行者
                    continue;
                }
                Avatars.Add(avatar.Id, avatar);
            }

            var implNS = this.GetType().Namespace + ".Avatar.Impl";
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == implNS && t.IsSubclassOf(typeof(AvatarImpl))
                    select t;
            foreach (var t in types) {
                Activator.CreateInstance(t);
            }
        }
    }
}
