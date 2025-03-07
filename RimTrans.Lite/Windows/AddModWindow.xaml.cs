﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RimTrans.Lite.Controls;
using Path = System.IO.Path;

namespace RimTrans.Lite.Windows
{
    /// <summary>
    /// Window_AddMod.xaml 的交互逻辑
    /// </summary>
    public partial class AddModWindow : Window
    {
        public AddModWindow()
        {
            InitializeComponent();
            vm.View = this;
        }

        public ModListBoxItem Result { get; set; }

        private void modListBoxInternal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectedMod = (ModListBoxItem)modListBoxInternal.SelectedItem;
        }

        private void modListBoxWorkshop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.SelectedMod = (ModListBoxItem)modListBoxWorkshop.SelectedItem;
        }

        private void searchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var filterText = searchBox.Text.ToLower();

            if (string.IsNullOrEmpty(filterText))
                foreach (ModListBoxItem item in modListBoxInternal.Items)
                    item.Visibility = Visibility.Visible;

            foreach (ModListBoxItem item in modListBoxInternal.Items)
            {
                var modNameContainsFilteredText = item.ModName.ToLower().Contains(filterText);
                var modFolderName = Path.GetFileName(item.ModPath)?.ToLower();
                var modFolderContainsFilteredText = modFolderName?.Contains(filterText) ?? false;
                item.Visibility = modNameContainsFilteredText || modFolderContainsFilteredText
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }
    }
}
