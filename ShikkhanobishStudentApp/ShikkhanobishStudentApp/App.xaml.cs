using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShikkhanobishStudentApp.View;
using Xamarin.Essentials;

namespace ShikkhanobishStudentApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new StartScreen());
        }
        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
