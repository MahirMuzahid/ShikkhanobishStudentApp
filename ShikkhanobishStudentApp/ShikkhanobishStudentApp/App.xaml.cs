using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShikkhanobishStudentApp.View;
using Xamarin.Essentials;
using System.Threading.Tasks;
using ShikkhanobishStudentApp.Model;
using Flurl.Http;

namespace ShikkhanobishStudentApp
{
    public partial class App : Application
    {
        NetworkAccess current = Connectivity.NetworkAccess;
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            getInfo();
        }
        public async Task getInfo ()
        {
            var pn = await SecureStorage.GetAsync("phonenumber");
            var pass = await SecureStorage.GetAsync("passowrd");
            if(pn == null && pass == null)
            {
                MainPage = new TakeTuitionView(true);
            }
            else
            {
                StaticPageToPassData.thisstPass = pass;
                StaticPageToPassData.thisStPh = pn;              
                MainPage =  new TakeTuitionView(false);
            }
            
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
