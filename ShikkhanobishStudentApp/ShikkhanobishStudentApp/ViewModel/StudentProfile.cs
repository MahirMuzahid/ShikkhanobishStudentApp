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
    public class StudentProfile: BaseViewModel, INotifyPropertyChanged
    {
        #region Methods
        public StudentProfile()
        {
            name = StaticPageToPassData.thisStudentInfo.name;
            phonenumber = StaticPageToPassData.thisStudentInfo.phonenumber;
            coinSpent = ""+StaticPageToPassData.thisStudentInfo.totalSpent;
            totalTuition = "" + StaticPageToPassData.thisStudentInfo.totalTuitionTime;
            tuitionHisChanged = true;
            paymentHistoryChaged = false;
            hisVisibility = true;
            payVisiblity = false;

        }
        public ICommand closeWindow =>
            new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            });

        private void changepass()
        {
        }
        private void changepn()
        {
        }
        private void changeNmae()
        {
        }
        #endregion

        #region Binding
        private string name1;

        public string name { get => name1; set => SetProperty(ref name1, value); }

        private string phonenumber1;

        public string phonenumber { get => phonenumber1; set => SetProperty(ref phonenumber1, value); }

        private Command changepnCommand1;

        public ICommand changepnCommand
        {
            get
            {
                if (changepnCommand1 == null)
                {
                    changepnCommand1 = new Command(changepn);
                }

                return changepnCommand1;
            }
        }

        

        private Command changepassCommand1;

        public ICommand changepassCommand
        {
            get
            {
                if (changepassCommand1 == null)
                {
                    changepassCommand1 = new Command(changepass);
                }

                return changepassCommand1;
            }
        }

        private string coinSpent1;

        public string coinSpent { get => coinSpent1; set => SetProperty(ref coinSpent1, value); }

        private string totalTuition1;

        public string totalTuition { get => totalTuition1; set => SetProperty(ref totalTuition1, value); }

        private bool tuitionHisChanged1;

        public bool tuitionHisChanged { get { return tuitionHisChanged1; } set { tuitionHisChanged1 = value;
                if(tuitionHisChanged == true)
                {
                    paymentHistoryChaged = false;
                    hisVisibility = true;
                    payVisiblity = false;
                }
                
                OnPropertyChanged(); } }

        private bool paymentHistoryChaged1;

        public bool paymentHistoryChaged { get { return paymentHistoryChaged1; } set { paymentHistoryChaged1 = value;
                if (paymentHistoryChaged == true)
                {
                    tuitionHisChanged = false;
                    hisVisibility = false;
                    payVisiblity = true;
                }
                OnPropertyChanged(); } }

        private Command changeNmaeCommand1;

        public ICommand changeNmaeCommand
        {
            get
            {
                if (changeNmaeCommand1 == null)
                {
                    changeNmaeCommand1 = new Command(changeNmae);
                }

                return changeNmaeCommand1;
            }
        }

       

        private bool payVisiblity1;

        public bool payVisiblity { get => payVisiblity1; set => SetProperty(ref payVisiblity1, value); }

        private bool hisVisibility1;

        public bool hisVisibility { get => hisVisibility1; set => SetProperty(ref hisVisibility1, value); }

        #endregion
    }
}
