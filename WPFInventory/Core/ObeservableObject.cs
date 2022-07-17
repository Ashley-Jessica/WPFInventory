using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFInventory.Core
{
    internal class ObeservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}