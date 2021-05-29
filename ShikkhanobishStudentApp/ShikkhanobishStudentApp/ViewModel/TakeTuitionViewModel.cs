using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using ShikkhanobishStudentApp.Model;
using System.Collections.ObjectModel;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class TakeTuitionViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<InstitutionName> backUpInsName { get; set; }
        public TakeTuitionViewModel()
        {
            popUpVisibility = false;
            ObservableCollection<InstitutionName> insnList = new ObservableCollection<InstitutionName>();
            InstitutionName insn = new InstitutionName();
            insn.InsName = "School";
            insnList.Add(insn);
            InstitutionName insn1 = new InstitutionName();
            insn1.InsName = "College";
            insnList.Add(insn1);
            InstitutionName insn2 = new InstitutionName();
            insn2.InsName = "University";
            insnList.Add(insn2);
            backUpInsName = insnList;
            insNameList = insnList;

        }
        private void selectIns()
        {
            popUpVisibility = true;
        }
           
        private void PerformClosePopup()
        {
            popUpVisibility = false;
        }
        public ICommand searchTextChangedCommand => 
            new Command<string>((searchedText) =>
            {
                if(searchedText == null || searchedText == "")
                {
                    insNameList = backUpInsName;
                }
                else
                {
                    ObservableCollection<InstitutionName> insnListNew = new ObservableCollection<InstitutionName>();
                    for (int i = 0; i < backUpInsName.Count; i++)
                    {
                        string thisins = backUpInsName[i].InsName.ToLower();
                        searchedText = searchedText.ToLower(); ;
                        if (thisins[0] == searchedText[0])
                        {
                            insnListNew.Add(backUpInsName[i]);
                        }
                    }
                    insNameList = insnListNew;
                }
                
            });

        #region Binding Garbage
        private bool popUpVisibility1;

        public bool popUpVisibility { get => popUpVisibility1; set => SetProperty(ref popUpVisibility1, value); }

        private ICommand closePopup;

        public ICommand ClosePopup
        {
            get
            {
                if (closePopup == null)
                {
                    closePopup = new Command(PerformClosePopup);
                }

                return closePopup;
            }
        }


        private ICommand selectInsCommand1;
        private ObservableCollection<InstitutionName> _insNameList = new ObservableCollection<InstitutionName>();
        public ObservableCollection<InstitutionName> insNameList { get => _insNameList; set => SetProperty(ref _insNameList, value); }
        public ICommand selectInsCommand
        {
            get
            {
                if (selectInsCommand1 == null)
                {
                    selectInsCommand1 = new Command(selectIns);
                }

                return selectInsCommand1;
            }
        }
        
        #endregion
    }
}
