using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using XPlugin.Json;

namespace PaimonPlus.Core {
	public class CoreEngine {
		public static string ReadTextResource(string filename) {
			var assembly = Assembly.GetExecutingAssembly();
			using var stream = assembly.GetManifestResourceStream("PaimonPlus." + filename);
			if (stream == null) {
				throw new Exception($"缺少配置文件{filename}");
			}
			using var reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}

		public static JObject ReadJObjectConfig(string name) {
			return JObject.Parse(ReadTextResource($"Res.Config.{name}.json"));
		}

		public static JArray ReadJArrayConfig(string name) {
			return JArray.Parse(ReadTextResource($"Res.Config.{name}.json"));
		}

		private static CoreEngine? ins = null;
		public static CoreEngine Ins {
			get {
				ins ??= new CoreEngine();
				return ins;
			}
		}

		internal readonly List<CoreEngineModule> modules = new();
		public readonly UpgradeModule Upgrade = new();
		public readonly AffixModule Affix = new();
		public readonly SkillModule Skill = new();
		public readonly WeaponModule Weapon = new();
		public readonly RelicModule Relic = new();
		public readonly AvatarModule Avatar = new();
		private bool Inited = false;

		public void Init() {
			if (Inited) {
				return;
			}
			Trace.WriteLine($"CoreEngine init started.");
			var watch = new Stopwatch();
			watch.Start();

			TextMap = ReadJObjectConfig("TextMapCHS");

			Upgrade.Init();
			Affix.Init();
			Skill.Init();
			Weapon.Init();
			Relic.Init();
			Avatar.Init();

			TextMap = null;
			Inited = true;

			watch.Stop();
			Trace.WriteLine($"CoreEngine init finished in {watch.ElapsedMilliseconds} ms.");

			GC.Collect();
		}

		private JObject? TextMap = null;
		public string GetText(long hash) {
			if (TextMap is null) {
				return "";
			} else {
				return TextMap[hash.ToString()].OptString();
			}
		}
	}
}
