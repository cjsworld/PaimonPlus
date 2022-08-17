using PaimonPlus.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaimonPlus.UI {
    public class AvatarInfoVO : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private AvatarData? avatarData;
        public AvatarData? AvatarData { get => avatarData; set { avatarData = value; OnPropertyChanged(); } }

        private int level = 90;
        public int Level { get => level; set { level = value; OnPropertyChanged(); } }
    }
}
