using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            getInfo();
        }
        public async Task getInfo()
        {
            var currentAppVersion = VersionTracking.CurrentBuild;

            var currentRealVersion = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getAppVersion".GetJsonAsync<AppVersion>();

            if (int.Parse(currentAppVersion) < currentRealVersion.studentAtVersion)
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "New Version Is Available! Please download latest version to use Shikkhanobish Teacher App..."))
                {
                    await Task.Delay(3000);
                    await Browser.OpenAsync("https://play.google.com/store/apps/details?id=com.shikkhanobish.shikkhanobishstudentapp");
                }
            }
            else
            {
                var pn = await SecureStorage.GetAsync("phonenumber");
                var pass = await SecureStorage.GetAsync("passowrd");
                if (pn == null && pass == null)
                {
                    await Task.Delay(1000);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    StaticPageToPassData.thisstPass = pass;
                    StaticPageToPassData.thisStPh = pn;
                    StaticPageToPassData.isFromLogin = false;
                    StaticPageToPassData.thisStudentInfo = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/LoginStudent".PostUrlEncodedAsync(new { phonenumber = StaticPageToPassData.thisStPh, password = StaticPageToPassData.thisstPass })
                  .ReceiveJson<Student>();
                    Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuitionView(false));
                }

            }
            
            

        }
    }
}