using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            registerbtvEnabled = true;
            otpVisibility = false;
        }
        public ICommand btnCommand =>
             new Command<string>((index) =>
             {
                 if(int.Parse(index) == 0)
                 {
                     btnNav = 1;
                     btnTxt = "Send Otp";
                     varificationvisibility = true;
                     numberVisibility = true;
                     titleTxt = "Enter Your Phone Number";
                     egTxt = "e.g: 01xxxxxxxxx";
                     sendAgainVisibility = false;
                     infoVisibility = false;
                     registerbtvEnabled = true;
                     otpfiText = "";
                     otpsecText = "";
                     otpthText = "";
                     otpfrText = "";
                     otpfivText = "";
                 }
                 else
                 {
                     if (btnNav == 1)
                     {
                         btnNav++;
                         titleTxt = "Enter 5 Digit OTP ";
                         numberVisibility = false;
                         egTxt = "";
                         btnTxt = "Varify";
                         sendAgainColor = "gray";
                         sendAgainVisibility = true;
                         varificationvisibility = false;
                         otpVisibility = true;
                         entryText = "";
                     }
                     else if (btnNav == 2)
                     {
                         //send otp to mobile
                         string otp = "12345";
                         if (otpfiText != null && otpsecText != null && otpthText != null && otpfrText != null && otpfivText != null)
                         {
                             if(otpfiText == otp[0].ToString() && otpsecText == otp[1].ToString() && otpthText == otp[2].ToString() && otpfrText == otp[3].ToString() && otpfivText == otp[4].ToString() )
                             {
                                 btnNav++;
                                 errorText = "";
                                 otpVisibility = false;
                                 titleTxt = "Enter Your Information";
                                 infoVisibility = true;
                                 btnTxt = "Register";
                                 sendAgainVisibility = false;
                             }
                             else
                             {
                                 errorText = "OTP doesnt match";
                             }
                             
                         }
                         else
                         {
                             errorText = "OTP can't be empty";
                         }

                     }
                     else if (btnNav == 3)
                     {
                         bool allok = true;
                         if (password != null)
                         {
                             if (!password.Any(char.IsDigit) && password.Length < 6)
                             {
                                 passimgsrc = "wrong.png";
                                 pasMargin = 9;
                                 allok = false;
                                 errorText = "Password length should be 6 and at least contain one digit";
                             }
                             else
                             {
                                 passimgsrc = "right.png";
                                 pasMargin = 10;
                             }
                         }
                         else
                         {
                             passimgsrc = "wrong.png";
                             pasMargin = 9;
                             allok = false;
                         }

                         if (conPassword != null)
                         {
                             if (conPassword != password)
                             {
                                 conpassimgsrc = "wrong.png";
                                 conpasMargin = 9;
                                 allok = false;
                                 errorText = "Password doesn't match";
                             }
                             else
                             {
                                 conpassimgsrc = "right.png";
                                 conpasMargin = 10;
                             }
                         }
                         else
                         {
                             conpassimgsrc = "wrong.png";
                             conpasMargin = 9;
                             allok = false;
                         }


                         if (name == null || name == "")
                         {
                             nameimgsrc = "wrong.png";
                             nameMargin = 9;
                             allok = false;
                             errorText = "Name shouldn't be empty";
                         }
                         else
                         {
                             nameimgsrc = "right.png";
                             nameMargin = 10;
                         }

                         if (city == null || city == "")
                         {
                             cityimgsrc = "wrong.png";
                             cityMargin = 9;
                             allok = false;
                             errorText = "City shouldn't be empty";
                         }
                         else
                         {
                             cityimgsrc = "right.png";
                             cityMargin = 10;
                         }

                         if (allok == true)
                         {
                             registerbtvEnabled = true;
                             Application.Current.MainPage.Navigation.PushAsync(new TakeTuitionView(false));
                         }

                     }
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

        private string entryText1;

        public string entryText { get => entryText1; set => SetProperty(ref entryText1, value); }

        private bool infoVisibility1;

        public bool infoVisibility { get => infoVisibility1; set => SetProperty(ref infoVisibility1, value); }

        private string password1;

        public string password { get => password1; set => SetProperty(ref password1, value); }

        private string conPassword1;

        public string conPassword { get => conPassword1; set => SetProperty(ref conPassword1, value); }

        private string name1;

        public string name { get => name1; set => SetProperty(ref name1, value); }

        private string city1;

        public string city { get => city1; set => SetProperty(ref city1, value); }

        private string passimgsrc1;

        public string passimgsrc { get => passimgsrc1; set => SetProperty(ref passimgsrc1, value); }

        private Thickness nameMargin1;

        public Thickness nameMargin { get => nameMargin1; set => SetProperty(ref nameMargin1, value); }

        private string conpassimgsrc1;

        public string conpassimgsrc { get => conpassimgsrc1; set => SetProperty(ref conpassimgsrc1, value); }

        private Thickness pasMargin1;

        public Thickness pasMargin { get => pasMargin1; set => SetProperty(ref pasMargin1, value); }

        private string nameimgsrc1;

        public string nameimgsrc { get => nameimgsrc1; set => SetProperty(ref nameimgsrc1, value); }

        private Thickness cityMargin1;

        public Thickness cityMargin { get => cityMargin1; set => SetProperty(ref cityMargin1, value); }

        private string cityimgsrc1;

        public string cityimgsrc { get => cityimgsrc1; set => SetProperty(ref cityimgsrc1, value); }

        private Thickness conpasMargin1;

        public Thickness conpasMargin { get => conpasMargin1; set => SetProperty(ref conpasMargin1, value); }

        private bool registerbtvEnabled1;

        public bool registerbtvEnabled { get => registerbtvEnabled1; set => SetProperty(ref registerbtvEnabled1, value); }

        private bool otpVisibility1;

        public bool otpVisibility { get => otpVisibility1; set => SetProperty(ref otpVisibility1, value); }

        private string otpfiText1;

        public string otpfiText { get => otpfiText1; set => SetProperty(ref otpfiText1, value); }

        private string otpsecText1;

        public string otpsecText { get => otpsecText1; set => SetProperty(ref otpsecText1, value); }

        private string otpfrText1;

        public string otpfrText { get => otpfrText1; set => SetProperty(ref otpfrText1, value); }

        private string otpthText1;

        public string otpthText { get => otpthText1; set => SetProperty(ref otpthText1, value); }

        private string errorText1;

        public string errorText { get => errorText1; set => SetProperty(ref errorText1, value); }

        private string otpfivText1;

        public string otpfivText { get => otpfivText1; set => SetProperty(ref otpfivText1, value); }
        #endregion

    }
}
