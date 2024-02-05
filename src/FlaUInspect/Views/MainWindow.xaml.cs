﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlaUI.Core;
using FlaUInspect.Core;
using FlaUInspect.ViewModels;
using TextCopy;

namespace FlaUInspect.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainViewModel _vm;
     


        public MainWindow()
        {
            InitializeComponent();
            AppendVersionToTitle();
            Height = 550;
            Width = 700;
            Loaded += MainWindow_Loaded;
            _vm = new MainViewModel();
            ClipboardService.SetText(string.Empty);
            DataContext = _vm;           

        }



        private void AppendVersionToTitle()
        {
            var attr = Assembly.GetEntryAssembly().GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;
            if (attr != null)
            {
                Title += " v" + attr.InformationalVersion;

            }
        }

        
        private void MainWindow_Loaded(object sender, System.EventArgs e)
        {
            if (!_vm.IsInitialized)
            {
                _vm.Initialize(ChoseVersion);
                Loaded -= MainWindow_Loaded;
            }
        }

        private AutomationType ChoseVersion()
        {
            var dlg = new ChooseVersionWindow { Owner = this };
            if (dlg.ShowDialog() != true)
            {
                Close();
            }

            return dlg.SelectedAutomationType;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TreeViewSelectedHandler(object sender, RoutedEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item != null)
            {
                item.BringIntoView();
                e.Handled = true;
            }
        }

        private void CopyXpathButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (!XpathTxtBox.Text.Equals("None"))
            {

                System.Windows.Clipboard.SetText(XpathTxtBox.Text);
               
            }
        }

    }
}
