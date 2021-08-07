using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResgisterView : ContentPage
    {
        public ResgisterView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            StaticPageToPassData.RegisteredPhonenNumber = fi.Text + sec.Text + th.Text + fr.Text + fiv.Text + si.Text + sev.Text + ei.Text + ni.Text;
            sendsms();
        }
        public async Task sendsms()
        {
            Random rd = new Random();
            string number = "01" + StaticPageToPassData.RegisteredPhonenNumber;
            int code = rd.Next(1000,9999);
            string msg = "Shikkhanobish Verification Code: " + code;
            var regRes = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/SendSmsAsync"
        .PostUrlEncodedAsync(new { msg = msg, number = number })
        .ReceiveJson<SendSms>();
            StaticPageToPassData.otpcode = code.ToString();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //fi.Text = ""; sec.Text = ""; th.Text = ""; fr.Text = ""; fiv.Text = ""; si.Text = ""; sev.Text = ""; ei.Text = ""; ni.Text = ""; 
            first.Text = ""; second.Text = ""; third.Text = ""; forth.Text = ""; fifth.Text = "";
        }
    }
}