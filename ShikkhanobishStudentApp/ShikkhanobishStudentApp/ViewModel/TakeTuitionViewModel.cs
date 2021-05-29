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

            public ICommand selectInsCommand =>
            new Command<string>((index) =>
            {
                popUpVisibility = true;

                if(int.Parse(index) == 0)
                {
                    searchName = "Select Institution";
                    SearchableVisibility = false;
                }
                else if (int.Parse(index) == 1)
                {
                    searchName = "Select Class";
                    SearchableVisibility = false;
                }
                else if (int.Parse(index) == 2)
                {
                    searchName = "";

                    searchPlaceholder = "Search Subject";
                    SearchableVisibility = true;
                }
                else if (int.Parse(index) == 3)
                {
                    searchName = "";

                    searchPlaceholder = "Search Chapter";
                    SearchableVisibility = true;
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


        
        private ObservableCollection<InstitutionName> _insNameList = new ObservableCollection<InstitutionName>();
        public ObservableCollection<InstitutionName> insNameList { get => _insNameList; set => SetProperty(ref _insNameList, value); }
        

        private string searchName1;

        public string searchName { get => searchName1; set => SetProperty(ref searchName1, value); }

        private bool searchableVisibility;

        public bool SearchableVisibility { get => searchableVisibility; set => SetProperty(ref searchableVisibility, value); }

        private string searchPlaceholder1;

        public string searchPlaceholder { get => searchPlaceholder1; set => SetProperty(ref searchPlaceholder1, value); }

        #endregion
    }
}
