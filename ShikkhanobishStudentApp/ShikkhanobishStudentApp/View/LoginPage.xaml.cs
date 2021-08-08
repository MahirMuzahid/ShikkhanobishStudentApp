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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        public async Task LoginStudent()
        {
            loginbtn.IsEnabled = false;
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Checking..."))
                {
                    errortxt.TextColor = Color.White;
                    if (pn.Text != null && pass.Text != null)
                    {
                        Student thistudent = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/LoginStudent".PostUrlEncodedAsync(new { phonenumber = pn.Text, password = pass.Text })
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

        private void loginbtn_Clicked(object sender, EventArgs e)
        {
            LoginStudent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}