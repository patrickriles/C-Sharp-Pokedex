using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokeDex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            GetSettings();
        }

        private void GetSettings()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var assemblyVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            var assemblyName = assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
            var assemblyCopy = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            var assemblyDesc = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;
            var assemblyComp = assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "VERSION: " + assemblyVersion;
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "NAME: " + assemblyName;
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += assemblyCopy;
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "DESCRIPTION: " + assemblyDesc;
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "DEVELOPERS: Bryan Savue, " +
                "Hadley Igoe, & Patrick Riles";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "\n";
            AssemblyText.Text += "COMPANY: " + assemblyComp;


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += About_BackRequested;
        }

        private void About_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            e.Handled = true;
        }
    }
}
