using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class RegisterViewModel : BaseViewModel, INotifyPropertyChanged
    {
        int btnNav;
        public RegisterViewModel()
        {
            btnNav = 1;
            btnTxt = "Send Otp";
            varificationvisibility = true;
            numberVisibility = true;
            titleTxt = "Enter Your Phone Number";
            egTxt = "e.g: 01xxxxxxxxx";
            sendAgainVisibility = false;
            infoVisibility = false;
        }
        public ICommand btnCommand =>
             new Command(() =>
             {
                 if(btnNav == 1)
                 {
                     btnNav++;
                     titleTxt = "Enter 5 Digit OTP ";
                     numberVisibility = false;
                     egTxt = "";
                     btnTxt = "Varify";
                     sendAgainColor = "gray";
                     sendAgainVisibility = true;
                 }
                 else if(btnNav == 2)
                 {
                     varificationvisibility = false;
                     titleTxt = "Enter Your Information";
                     infoVisibility = true;
                     btnTxt = "Register";
                     sendAgainVisibility = false;
                 }
             });
         public ICommand goBack =>
             new Command(() =>
             {
                 Application.Current.MainPage.Navigation.PopAsync();
             });

        #region Binding 
        private bool numberVisibility1;

        public bool numberVisibility { get => numberVisibility1; set => SetProperty(ref numberVisibility1, value); }
        private bool varificationvisibility1;

        public bool varificationvisibility { get => varificationvisibility1; set => SetProperty(ref varificationvisibility1, value); }

        private string btnTxt1;

        public string btnTxt { get => btnTxt1; set => SetProperty(ref btnTxt1, value); }

        private string titleTxt1;

        public string titleTxt { get => titleTxt1; set => SetProperty(ref titleTxt1, value); }

        private string egTxt1;

        public string egTxt { get => egTxt1; set => SetProperty(ref egTxt1, value); }

        private bool sendAgainVisibility1;

        public bool sendAgainVisibility { get => sendAgainVisibility1; set => SetProperty(ref sendAgainVisibility1, value); }

        private string sendAgainColor1;

        public string sendAgainColor { get => sendAgainColor1; set => SetProperty(ref sendAgainColor1, value); }

        private bool infoVisibility1;

        public bool infoVisibility { get => infoVisibility1; set => SetProperty(ref infoVisibility1, value); }
        #endregion

    }
}
