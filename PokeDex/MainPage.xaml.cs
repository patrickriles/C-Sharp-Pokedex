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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PokeDex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PokemonViewModel pokeVM;
        APIcaller fetcher;
        

        public MainPage()
        {
            pokeVM = new PokemonViewModel();
            fetcher = new APIcaller();
            this.InitializeComponent();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            fetcher.getData();
            Debug.Write("About");
        }
    }
}