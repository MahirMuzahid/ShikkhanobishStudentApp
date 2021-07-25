using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RattingPageView : ContentPage
    {
        public RattingPageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public async Task EndOrBackBtn()
        {

            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Please rate teacher first",
                                  confirmingText: "OK"
                                  );         
        }

        protected override bool OnBackButtonPressed()
        {
            EndOrBackBtn();
            return true;
        }
        public async Task FinalRate()
        {
            using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Please Wait..."))
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/FinalizeTuitionHistory".PostUrlEncodedAsync(new { tuitionID = StaticPageToPassData.lastTuitionHistoryID , ratting = StaticPageToPassData.lastRate, teacherID = StaticPageToPassData.lastTeacherID })
         .ReceiveJson<Response>();
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
                await Application.Current.MainPage.Navigation.PushModalAsync(new TakeTuitionView(false));
                await dialog.DismissAsync();
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            FinalRate();
        }
    }
}