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
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeTuitionView : ContentPage
    {
        
        public TakeTuitionView(bool fromLogin)
        {
            InitializeComponent();
            proImage.IsVisible = false;
            if(!fromLogin)
            {
                loginView.Opacity = 0;
                loginView.TranslateTo(0, -1000, 1500, Easing.CubicIn);
                loginView.FadeTo(0, 1200, Easing.CubicIn);
            }
            var current = Connectivity.NetworkAccess;
            FavouriteTeacherGrid.IsVisible = false;
            coingrid.IsVisible = false;
            if (current == NetworkAccess.Internet)
            {
                getAllInfo();
            }
            else
            {
                loginbtn.IsEnabled = false;
                logoutBtn.IsEnabled = false;
                connectivityGrid.IsVisible = true;
                
                ShowSnakeBarError();
            }
           
        }

        public async Task getAllInfo()
        {
            
            connectivityGrid.IsVisible = false;
            NavigationPage.SetHasNavigationBar(this, false);
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            cpimg.Opacity = .3;
            fvimg.Opacity = .3;
            rclbl.TextColor = Color.FromHex("#C9C9C9");
            fvlbl.TextColor = Color.FromHex("#C9C9C9");

            StaticPageToPassData.thisStudentInfo = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/LoginStudent".PostUrlEncodedAsync(new { phonenumber = StaticPageToPassData.thisStPh, password = StaticPageToPassData.thisstPass })
          .ReceiveJson<Student>();
           
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            proImage.IsVisible = true;
        }
        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                logoutBtn.IsEnabled = true;
                loginbtn.IsEnabled = true;
                connectivityGrid.IsVisible = false;
            }
            else
            {
                loginbtn.IsEnabled = false;
                logoutBtn.IsEnabled = false;
                connectivityGrid.IsVisible = true;
                ShowSnakeBarError();
            }
            
        }
        public async Task ShowSnakeBarError()
        {
            var alertDialogConfiguration = new MaterialSnackbarConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ERROR),
                MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
                CornerRadius = 8,
                
                ScrimColor = Color.FromHex("#FFFFFF").MultiplyAlpha(0.32),
                ButtonAllCaps = false

            };

            await MaterialDialog.Instance.SnackbarAsync(message: "No Network Connection Avaiable",
                                        actionButtonText: "Got It",
                                        configuration: alertDialogConfiguration,
                                        msDuration: MaterialSnackbar.DurationIndefinite);


        }
        async private void paymentClicked(object sender, EventArgs e)
        {
            freeMin.Text = "" + StaticPageToPassData.thisStudentInfo.freemin;
            avaiableCoin.Text = "" + StaticPageToPassData.thisStudentInfo.coin;
            coingrid.IsVisible = true;
            FavouriteTeacherGrid.IsVisible = false;
            ttlbl.TextColor = Color.FromHex("#C9C9C9");
            fvlbl.TextColor = Color.FromHex("#C9C9C9");
            rclbl.TextColor = Color.Black;
            cpimg.Opacity = 1;
            ttimg.Opacity = .3;
            fvimg.Opacity = .3;
            
        }

        async private void homeClicked(object sender, EventArgs e)
        {

            FavouriteTeacherGrid.IsVisible = false;
            coingrid.IsVisible = false;
            ttimg.Opacity = 1;
            cpimg.Opacity = .3;
            fvimg.Opacity = .3;
            ttlbl.TextColor = Color.Black;
            rclbl.TextColor = Color.FromHex("#C9C9C9");
            fvlbl.TextColor = Color.FromHex("#C9C9C9");
        }

        private void profileClicked(object sender, EventArgs e)
        {            
            Application.Current.MainPage.Navigation.PushModalAsync(new StudentProfile());
        }
        private void favouriteClicked(object sender, EventArgs e)
        {
            FavouriteTeacherGrid.IsVisible = true;
            coingrid.IsVisible = false;
            ttimg.Opacity = .3;
            cpimg.Opacity = .3;
            fvimg.Opacity = 1;
            ttlbl.TextColor = Color.FromHex("#C9C9C9");
            rclbl.TextColor = Color.FromHex("#C9C9C9");
            fvlbl.TextColor = Color.Black;
           
            GetAllFavTeacher();

        }
        public async Task GetAllFavTeacher()
        {
            
        }
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            loginView.TranslateTo(0, 0, 1500, Easing.CubicOut);
            loginView.FadeTo(1, 1000, Easing.CubicOut);
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            errortxt.Text = "";
            LoginStudent();
        }

        public async Task LoginStudent()
        {
            loginbtn.IsEnabled = false;
            var current = Connectivity.NetworkAccess;
            if ( current == NetworkAccess.Internet)
            {
                using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Checking..."))
                {
                    errortxt.TextColor = Color.White;
                    if (pn.Text != null && pass.Text != null)
                    {
                        Student thistudent = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/LoginStudent".PostUrlEncodedAsync(new {  phonenumber = pn.Text, password = pass.Text })
          .ReceiveJson<Student>();
                        if (pn.Text == thistudent.phonenumber && pass.Text == thistudent.password)
                        {
                            StaticPageToPassData.thisStudentInfo = thistudent;
                            dialog.MessageText = "Loggin In...";
                            if (chkBox.IsChecked)
                            {
                                SecureStorage.SetAsync("phonenumber", pn.Text);
                                SecureStorage.SetAsync("passowrd", pass.Text);
                            }
                            loginView.TranslateTo(0, -1000, 1500, Easing.CubicIn);
                            loginView.FadeTo(0, 1200, Easing.CubicIn);
                            loginView.Opacity = 0;
                            errortxt.Text = "";
                            pn.Text = "";
                            pass.Text = "";
                        }
                        else
                        {
                            pn.HasError = true;
                            pn.ErrorText = "Incorrect Phone Number or Password!";
                            pass.HasError = true;
                        }

                    }
                    else
                    {
                        pn.HasError = true;
                        pn.ErrorText = "Phone Number Or Password can't be empty!";
                        pass.HasError = true;
                    }
                    loginbtn.IsEnabled = true;
                    await dialog.DismissAsync();
                }
                
            }
            else
            {
                errortxt.Text = "No Internet connection";
                errortxt.TextColor = Color.Red;
            }
            loginbtn.IsEnabled = true;
        }

        private void pn_Focused(object sender, FocusEventArgs e)
        {
            pn.HasError = false;
            pn.ErrorText = "";
            pass.HasError = false;
        }

        private void pass_Focused(object sender, FocusEventArgs e)
        {
            pn.HasError = false;
            pn.ErrorText = "";
            pass.HasError = false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPassword());
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                await getAllInfo();
                logoutBtn.IsEnabled = true;
                loginbtn.IsEnabled = true;
                connectivityGrid.IsVisible = false;
                
            }
            else
            {
                logoutBtn.IsEnabled = false;
                loginbtn.IsEnabled = false;
                connectivityGrid.IsVisible = true;
                ShowSnakeBarError();
            }
        }

       
    }
}