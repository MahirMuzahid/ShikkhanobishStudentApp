using ShikkhanobishStudentApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class RattingPageViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public RattingPageViewModel()
        {
            reportVisibility = false;
            oneStartVisibility = false;
            twoStartVisibility = false;
            threeStartVisibility = false;
            fourStartVisibility = false;
            fiveStartVisibility = false;
            rateBtnEnabled = false;
            reportSubmitEnabled = false;
        }
        private void PerformStartClick(string index)
        {
            rateBtnEnabled = true;
            if (index == "1")
            {
                oneStartVisibility = true;
                twoStartVisibility = false;
                threeStartVisibility = false;
                fourStartVisibility = false;
                fiveStartVisibility = false;
                rattingName = "Unexpected!";
            }
            if (index == "2")
            {
                oneStartVisibility = true;
                twoStartVisibility = true;
                threeStartVisibility = false;
                fourStartVisibility = false;
                fiveStartVisibility = false;
                rattingName = "Room For Improve!";
            }
            if (index == "3")
            {
                oneStartVisibility = true;
                twoStartVisibility = true;
                threeStartVisibility = true;
                fourStartVisibility = false;
                fiveStartVisibility = false;
                rattingName = "Good Teacher!";
            }
            if (index == "4")
            {
                oneStartVisibility = true;
                twoStartVisibility = true;
                threeStartVisibility = true;
                fourStartVisibility = true;
                fiveStartVisibility = false;
                rattingName = "Excelent Teacher!";
            }
            if (index == "5")
            {
                oneStartVisibility = true;
                twoStartVisibility = true;
                threeStartVisibility = true;
                fourStartVisibility = true;
                fiveStartVisibility = true;
                rattingName = "My Favourite Teacher!";
            }
        }
        public void checkReportBtn()
        {
            if(firstChecked || secondChecked || thiedChecked|| forthChecked)
            {
                reportSubmitEnabled = true;
            }
            else
            {
                reportSubmitEnabled = false;
            }
           
        }
        private void PerformpopUpReport()
        {
            reportVisibility = true;
        }
        private void PerformpopOutReport()
        {
            reportVisibility = false;
        }
        #region Binding
        private bool oneStartVisibility1;

        public bool oneStartVisibility { get => oneStartVisibility1; set => SetProperty(ref oneStartVisibility1, value); }

        private bool twoStartVisibility1;

        public bool twoStartVisibility { get => twoStartVisibility1; set => SetProperty(ref twoStartVisibility1, value); }

        private bool threeStartVisibility1;

        public bool threeStartVisibility { get => threeStartVisibility1; set => SetProperty(ref threeStartVisibility1, value); }

        private bool fourStartVisibility1;

        public bool fourStartVisibility { get => fourStartVisibility1; set => SetProperty(ref fourStartVisibility1, value); }

        private bool fiveStartVisibility1;

        public bool fiveStartVisibility { get => fiveStartVisibility1; set => SetProperty(ref fiveStartVisibility1, value); }

        private Command startClick;

        public ICommand StartClick
        {
            get
            {
                if (startClick == null)
                {
                    startClick = new Command<string>(PerformStartClick);
                }

                return startClick;
            }
        }

        private bool rateBtnEnabled1;

        public bool rateBtnEnabled { get => rateBtnEnabled1; set => SetProperty(ref rateBtnEnabled1, value); }

        private string rattingName1;

        public string rattingName { get => rattingName1; set => SetProperty(ref rattingName1, value); }

        private bool reportVisibility1;

        public bool reportVisibility { get => reportVisibility1; set => SetProperty(ref reportVisibility1, value); }

        private Command popOutReport1;

        public ICommand popOutReport
        {
            get
            {
                if (popOutReport1 == null)
                {
                    popOutReport1 = new Command(PerformpopOutReport);
                }

                return popOutReport1;
            }
        }

        

        private Command popUpReport1;

        public ICommand popUpReport
        {
            get
            {
                if (popUpReport1 == null)
                {
                    popUpReport1 = new Command(PerformpopUpReport);
                }

                return popUpReport1;
            }
        }

       

        private bool reportSubmitEnabled1;

        public bool reportSubmitEnabled { get => reportSubmitEnabled1; set => SetProperty(ref reportSubmitEnabled1, value); }

        private bool firstChecked1;

        public bool firstChecked { get => firstChecked1; set { firstChecked1 = value; checkReportBtn(); SetProperty(ref firstChecked1, value); } }

        private bool secondChecked1;

        public bool secondChecked { get => secondChecked1; set { secondChecked1 = value; checkReportBtn(); SetProperty(ref secondChecked1, value); } }

        private bool thiedChecked1;

        public bool thiedChecked { get => thiedChecked1; set { thiedChecked1 = value; checkReportBtn(); SetProperty(ref thiedChecked1, value); } }

        private bool forthChecked1;

        public bool forthChecked { get => forthChecked1; set { forthChecked1 = value; checkReportBtn(); SetProperty(ref forthChecked1, value); } }
        #endregion

    }
}
