﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PokeDex.ViewModel;
using System.Diagnostics;
using PokeDex.Http;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PokeDex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PokemonViewModel pokeVM;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            AppViewBackButtonVisibility.Collapsed;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

            
            Frame.Navigate(typeof(AboutPage));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.Exit();
        }

        private void All_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GetAll), pokeVM);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pokeVM = (PokemonViewModel)e.Parameter;
        }
    }
}
