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

        public bool tuitionHisChanged { get => tuitionHisChanged1; set => SetProperty(ref tuitionHisChanged1, value); }

        private bool paymentHistoryChaged1;

        public bool paymentHistoryChaged { get => paymentHistoryChaged1; set => SetProperty(ref paymentHistoryChaged1, value); }


        #endregion
    }
}
