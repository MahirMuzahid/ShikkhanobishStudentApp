using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShikkhanobishStudentApp.View;

namespace ShikkhanobishStudentApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TakeTuitionView(true));

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
