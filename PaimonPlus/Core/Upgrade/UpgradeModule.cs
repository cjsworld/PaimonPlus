using System.Collections.Generic;
using XPlugin.Json;

namespace PaimonPlus.Core {
    /// <summary>
    /// 升级模块
    /// </summary>
    public class UpgradeModule : CoreEngineModule {
        /// <summary>
        /// 等级曲线
        /// </summary>
        public readonly Dictionary<string, CurveData> Curves = new();

        /// <summary>
        /// 突破配置
        /// </summary>
        public readonly Dictionary<int, PromoteData> Promotes = new();

        public override void Init() {
            ReadCurves("Avatar");
            ReadCurves("Weapon");
            ReadPromotes("Avatar");
            ReadPromotes("Weapon");
        }

        private void ReadCurves(string name) {
            var config = CoreEngine.ReadJArrayConfig($"{name}CurveExcelConfigData");
            foreach (JObject item in config) {
                var level = item["level"].AsInt();
                var list = item["curveInfos"].AsArray();
                foreach (JObject info in list) {
                    var type = info["type"].AsString();
                    var value = info["value"].AsDouble();
                    if (!Curves.TryGetValue(type, out CurveData? curve)) {
                        curve = new CurveData(type);
                        Curves.Add(type, curve);
                    }
                    curve.AddLevel(level, value);
                }
            }
        }

        private void ReadPromotes(string name) {
            var config = CoreEngine.ReadJArrayConfig($"{name}PromoteExcelConfigData");
            foreach (JObject item in config) {
                var id = item[$"{name.ToLower()}PromoteId"].AsInt();
                if (!Promotes.TryGetValue(id, out PromoteData? promote)) {
                    promote = new PromoteData(id);
                    Promotes.Add(id, promote);
                }
                promote.AddLevel(item);
            }
        }
    }
}
