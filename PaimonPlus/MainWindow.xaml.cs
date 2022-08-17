using PaimonPlus.Core;
using PaimonPlus.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaimonPlus {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private AvatarInfoVO? avatarInfo;
        public AvatarInfoVO? AvatarInfo { get => avatarInfo; set { avatarInfo = value; OnPropertyChanged(); } }

        public MainWindow() {
            CoreEngine.Ins.Init();
            AvatarInfo = new();
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            TestGanyu();
        }


        private void TestGanyuBasePanel() {
            var ganyu = CoreEngine.Ins.Avatar.Avatars[10000037];
            Trace.WriteLine(ganyu.GetBasePanelAt(1, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(20, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(20, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(40, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(40, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(50, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(50, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(60, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(60, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(70, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(70, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(80, false));
            Trace.WriteLine(ganyu.GetBasePanelAt(80, true));
            Trace.WriteLine(ganyu.GetBasePanelAt(90, false));
        }

        private void TestRelicSubProp() {
            var relicRank = CoreEngine.Ins.Relic.Ranks[5];
            Trace.WriteLine(relicRank.SubProps[PropType.CritHurt].CalcPreciseValue(0.326));
            //Trace.WriteLine(relicRank.SubProps[PropType.CritHurt].CalcPreciseValue(0.312));
            //Trace.WriteLine(relicRank.SubProps[PropType.CritHurt].CalcPreciseValue(0.326));
            //Trace.WriteLine(relicRank.SubProps[PropType.DEF].CalcPreciseValue(19));
        }

        private void TestGanyu() {
            var ganyu = CoreEngine.Ins.Avatar.Avatars[10000037];
            var info = ganyu.NewInfo();
            var weapon = CoreEngine.Ins.Weapon.Weapons[15502];
            info.Weapon = weapon.NewInfo();

            var relicSet = CoreEngine.Ins.Relic.Sets[14001];//冰套
            RelicInfo relic;
            //花
            relic = relicSet.NewInfo(RelicSlotType.Flower, 5);
            relic.AddSubProp(PropType.DEF, 19);
            relic.AddSubProp(PropType.PercentATK, 0.152);
            relic.AddSubProp(PropType.ATK, 37);
            relic.AddSubProp(PropType.CritHurt, 0.202);
            info.Relic.PutRelic(relic);

            //羽毛
            relic = relicSet.NewInfo(RelicSlotType.Leather, 5);
            relic.AddSubProp(PropType.PercentATK, 0.105);
            relic.AddSubProp(PropType.CritHurt, 0.326);
            relic.AddSubProp(PropType.CritRate, 0.039);
            relic.AddSubProp(PropType.HP, 269);
            info.Relic.PutRelic(relic);

            //沙漏
            relic = relicSet.NewInfo(RelicSlotType.Sand, 5);
            relic.MainPropType = PropType.PercentATK;
            relic.AddSubProp(PropType.CritRate, 0.097);
            relic.AddSubProp(PropType.ChargeRate, 0.091);
            relic.AddSubProp(PropType.ATK, 35);
            relic.AddSubProp(PropType.CritHurt, 0.148);
            info.Relic.PutRelic(relic);

            //杯子
            relic = relicSet.NewInfo(RelicSlotType.Cup, 5);
            relic.MainPropType = PropType.IceAddHurt;
            relic.AddSubProp(PropType.PercentDEF, 0.117);
            relic.AddSubProp(PropType.CritRate, 0.031);
            relic.AddSubProp(PropType.PercentHP, 0.122);
            relic.AddSubProp(PropType.CritHurt, 0.187);
            info.Relic.PutRelic(relic);

            //头
            relic = relicSet.NewInfo(RelicSlotType.Cap, 5);
            relic.MainPropType = PropType.CritHurt;
            relic.AddSubProp(PropType.DEF, 37);
            relic.AddSubProp(PropType.HP, 538);
            relic.AddSubProp(PropType.ATK, 33);
            relic.AddSubProp(PropType.CritRate, 0.066);
            info.Relic.PutRelic(relic);

            var panel = info.GetTotalPanel();

            //Trace.WriteLine(panel);

            Trace.WriteLine($"名称: {ganyu.Name}");
            Trace.WriteLine($"生命值: {panel.TotalHP}");
            Trace.WriteLine($"攻击力: {panel.TotalATK}");
            Trace.WriteLine($"防御力: {panel.TotalDEF}");
            Trace.WriteLine($"暴击率: {panel[PropType.CritRate]}");
            Trace.WriteLine($"暴击伤害: {panel[PropType.CritHurt]}");
            Trace.WriteLine($"元素充能效率: {panel[PropType.ChargeRate]}");
            Trace.WriteLine($"冰元素伤害加成: {panel[PropType.IceAddHurt]}");

            //HP: 16582
            //ATK: 2508
            //DEF: 759
            //CritRate: 28.3%
            //CritHurt: 236.9%
            //ChargeRate: 109.1%
            //IceAddHurt: 61.6%
            Trace.WriteLine("===============");

            var monster = new MonsterInfo();
            monster.Level = 88;
            monster.Props += PropType.IceSubHurt.By(0.10000000149011612);
            //monster.Props += PropType.IceSubHurt.By(-0.15); //TODO 甘雨1命效果

            var ctx = new CalcContext() {
                Avatar = info,
                Monster = monster,
                SkillOption = ganyu.Impl.GetSkillOptions()[0], //重击二段范围伤害
            };
            var damage = ctx.TotalDamage();
            Trace.WriteLine($"暴击伤害值: {damage}");
        }

        private void TestAyakaPanel() {
            //var ayaka = CoreEngine.Ins.Avatar.Avatars[10000002];
            //info = ayaka.NewInfo();
            //weapon = CoreEngine.Ins.Weapon.Weapons[11509];
            //info.Weapon = weapon.NewInfo();
            //panel = info.GetBasePanel();
            //Trace.WriteLine(panel);
        }
    }
}
