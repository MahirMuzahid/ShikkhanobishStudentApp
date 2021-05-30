using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using ShikkhanobishStudentApp.Model;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class TakeTuitionViewModel : BaseViewModel, INotifyPropertyChanged
    {


        #region Methods
        public TakeTuitionViewModel()
        {
            popUpVisibility = false;
            InsListPopulate();
            ClsListPopulate();
            SubListPopulate();
            ChpListPopulate();
            SelectedInsName = "Not Selected";
            SelectedClassName = "Not Selected";
            SelectedSubjectName = "Not Selected";
            SelectedChapterName = "Not Selected";

            seletedCountTextVisibility = false;
            CLseletedCountTextVisibility = false;
            SubseletedCountTextVisibility = false;
            ChpseletedCountTextVisibility = false;

            activebtn = false;

            remainword = "Remain 300 Words";
        }          
        public void InsListPopulate()
       {
            ObservableCollection<InstitutionName> insnList = new ObservableCollection<InstitutionName>();
            for(int i = 0; i < 3; i++)
            {
                InstitutionName insn = new InstitutionName();
                insn.InsName = "School";
                insn.studentCount = 50;
                insn.tuitionRequest = 300;
                insn.avgratting = 4.5f;
                insn.type = 0;
                insnList.Add(insn);
            }          
            backUpInsName = insnList;
       }
        public void ClsListPopulate()
        {
            ObservableCollection<InstitutionName> insnList = new ObservableCollection<InstitutionName>();
            for (int i = 0; i < 3; i++)
            {
                InstitutionName insn = new InstitutionName();
                insn.InsName = "Class 9";
                insn.studentCount = 10;
                insn.tuitionRequest = 150;
                insn.avgratting = 4f;
                insn.type = 1;
                insnList.Add(insn);
            }
            backUpclsName = insnList;
        }
        public void SubListPopulate()
        {
            ObservableCollection<InstitutionName> insnList = new ObservableCollection<InstitutionName>();
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                InstitutionName insn = new InstitutionName();
                char randomChar = (char)rnd.Next('a', 'z');
                char randomChar2 = (char)rnd.Next('a', 'z');
                char randomChar3 = (char)rnd.Next('a', 'z');
                char randomChar4 = (char)rnd.Next('a', 'z');
                insn.InsName = randomChar.ToString() + randomChar2.ToString() + randomChar3.ToString() + randomChar4.ToString();
                insn.studentCount = 45;
                insn.tuitionRequest =200;
                insn.avgratting = 4.2f;
                insn.type = 2;
                insnList.Add(insn);
            }
            backUpsubName = insnList;
        }
        public void ChpListPopulate()
        {
            ObservableCollection<InstitutionName> insnList = new ObservableCollection<InstitutionName>();
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                InstitutionName insn = new InstitutionName();
                char randomChar = (char)rnd.Next('a', 'z');
                char randomChar2 = (char)rnd.Next('a', 'z');
                char randomChar3 = (char)rnd.Next('a', 'z');
                char randomChar4 = (char)rnd.Next('a', 'z');
                insn.InsName = randomChar.ToString() + randomChar2.ToString() + randomChar3.ToString() + randomChar4.ToString();
                insn.studentCount = 25;
                insn.tuitionRequest = 250;
                insn.avgratting = 3.9f;
                insn.type = 3;
                insnList.Add(insn);
            }
            backUpchpName = insnList;
        }
        public ICommand selectInsCommand =>
            new Command<string>((index) =>
            {
                popUpVisibility = true;

                if(int.Parse(index) == 0)
                {
                    thisList = backUpInsName;
                    insNameList = backUpInsName;
                    searchName = "Select Institution";
                    SearchableVisibility = false;
                }
                else if (int.Parse(index) == 1)
                {
                    thisList = backUpclsName;
                    insNameList = backUpclsName;
                    searchName = "Select Class";
                    SearchableVisibility = false;
                }
                else if (int.Parse(index) == 2)
                {
                    thisList = backUpsubName;
                    insNameList = backUpsubName;
                    searchName = "";
                    searchPlaceholder = "Search Subject";
                    SearchableVisibility = true;
                }
                else if (int.Parse(index) == 3)
                {
                    thisList = backUpchpName;
                    insNameList = backUpchpName;
                    searchName = "";
                    searchPlaceholder = "Search Chapter";
                    SearchableVisibility = true;
                }

            });
        public ICommand SelectedItem =>
             new Command<InstitutionName>((intName) =>
             {

                 popUpVisibility = false;
                 if (intName.type == 0)
                 {
                     seletedCountTextVisibility = true;
                     SelectedInsName = intName.InsName;
                     SCount = intName.studentCount;
                     TRequest = intName.tuitionRequest;
                     avgratting = intName.avgratting;

                 }
                 else if (intName.type == 1)
                 {
                     CLseletedCountTextVisibility = true;
                     SelectedClassName = intName.InsName;
                     CLSCount = intName.studentCount;
                     CLTRequest = intName.tuitionRequest;
                     CLavgratting = intName.avgratting;
                 }
                 else if (intName.type == 2)
                 {
                     SubseletedCountTextVisibility = true;
                     SelectedSubjectName = intName.InsName;
                     SubSCount = intName.studentCount;
                     SubTRequest = intName.tuitionRequest;
                     Subavgratting = intName.avgratting;
                 }
                 else if (intName.type == 3)
                 {
                     ChpseletedCountTextVisibility = true;
                     SelectedChapterName = intName.InsName;
                     ChpSCount = intName.studentCount;
                     ChpTRequest = intName.tuitionRequest;
                     Chpavgratting = intName.avgratting;

                 }
                 CheckEverythign();

                 searchText = "";
             });
        public void CheckEverythign()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if(seletedCountTextVisibility& CLseletedCountTextVisibility& SubseletedCountTextVisibility& ChpseletedCountTextVisibility && detailTxt.Length <101)
                {
                    activebtn = true;
                }
               
            });
        }
        private void PerformClosePopUp()
        {
            popUpVisibility = false;
        }
        public void SearchControl()
        {
            string SearchedText = searchText1;
            if (SearchedText == null || SearchedText == "")
            {
                insNameList = thisList;
            }
            else
            {
                ObservableCollection<InstitutionName> insnListNew = new ObservableCollection<InstitutionName>();
                for (int i = 0; i < thisList.Count; i++)
                {
                    string thisins = thisList[i].InsName.ToLower();
                    SearchedText = SearchedText.ToLower();
                    bool allMatched = true;
                    for(int j = 0; j < SearchedText.Length; j++)
                    {
                        if (thisins[j] != SearchedText[j])
                        {
                            allMatched = false;
                            break;
                        }
                    }
                    if(allMatched)
                    {
                        insnListNew.Add(thisList[i]);
                    }
                }
                insNameList = insnListNew;
            }
        }

        public void countWord()
        {
            if (detailTxt.Length > 300)
            {
                remainword = "Extra " + (300 - detailTxt.Length)*(-1) + " Words";
                remainColopr = "Red";
            }
            else
            {
                remainword = "Remain " + (300 - detailTxt.Length) + " Words";
                remainColopr = "Gray";
                CheckEverythign();
            }
            


        }
        #endregion


        #region Binding Garbage

        public ObservableCollection<InstitutionName> backUpInsName { get; set; }
        public ObservableCollection<InstitutionName> backUpclsName { get; set; }
        public ObservableCollection<InstitutionName> backUpsubName { get; set; }
        public ObservableCollection<InstitutionName> backUpchpName { get; set; }
        public ObservableCollection<InstitutionName> thisList { get; set; }
        private bool popUpVisibility1;

        public bool popUpVisibility { get => popUpVisibility1; set => SetProperty(ref popUpVisibility1, value); }
        
        private ObservableCollection<InstitutionName> _insNameList = new ObservableCollection<InstitutionName>();
        public ObservableCollection<InstitutionName> insNameList { get => _insNameList; set => SetProperty(ref _insNameList, value); }
        

        private string searchName1;

        public string searchName { get => searchName1; set => SetProperty(ref searchName1, value); }

        private bool searchableVisibility;

        public bool SearchableVisibility { get => searchableVisibility; set => SetProperty(ref searchableVisibility, value); }

        private string searchPlaceholder1;

        public string searchPlaceholder { get => searchPlaceholder1; set => SetProperty(ref searchPlaceholder1, value); }

        private string selectedInsName;

        public string SelectedInsName { get => selectedInsName; set => SetProperty(ref selectedInsName, value); }

        private string selectedClassName;

        public string SelectedClassName { get => selectedClassName; set => SetProperty(ref selectedClassName, value); }

        private string selectedSubjectName;

        public string SelectedSubjectName { get => selectedSubjectName; set => SetProperty(ref selectedSubjectName, value); }

        private string selectedChapterName;

        public string SelectedChapterName { get => selectedChapterName; set => SetProperty(ref selectedChapterName, value); }

        private string searchText1;

        public string searchText { get { return searchText1; } set { searchText1 = value; SearchControl(); OnPropertyChanged(); } }

        private ICommand closePopUp;

        public ICommand ClosePopUp
        {
            get
            {
                if (closePopUp == null)
                {
                    closePopUp = new Command(PerformClosePopUp);
                }

                return closePopUp;
            }
        }

        private int sCount;

        public int SCount { get => sCount; set => SetProperty(ref sCount, value); }

        private int tRequest;

        public int TRequest { get => tRequest; set => SetProperty(ref tRequest, value); }

        private float avgratting1;

        public float avgratting { get => avgratting1; set => SetProperty(ref avgratting1, value); }

        private int cLSCount;

        public int CLSCount { get => cLSCount; set => SetProperty(ref cLSCount, value); }

        private int cLTRequest;

        public int CLTRequest { get => cLTRequest; set => SetProperty(ref cLTRequest, value); }

        private float cLavgratting;

        public float CLavgratting { get => cLavgratting; set => SetProperty(ref cLavgratting, value); }

        private int subSCount;

        public int SubSCount { get => subSCount; set => SetProperty(ref subSCount, value); }

        private int subTRequest;

        public int SubTRequest { get => subTRequest; set => SetProperty(ref subTRequest, value); }

        private float subavgratting;

        public float Subavgratting { get => subavgratting; set => SetProperty(ref subavgratting, value); }

        private int chpSCount;

        public int ChpSCount { get => chpSCount; set => SetProperty(ref chpSCount, value); }

        private int chpTRequest;

        public int ChpTRequest { get => chpTRequest; set => SetProperty(ref chpTRequest, value); }

        private float chpavgratting;

        public float Chpavgratting { get => chpavgratting; set => SetProperty(ref chpavgratting, value); }

        private bool seletedCountTextVisibility1;

        public bool seletedCountTextVisibility { get => seletedCountTextVisibility1; set => SetProperty(ref seletedCountTextVisibility1, value); }

        private bool cLseletedCountTextVisibility;

        public bool CLseletedCountTextVisibility { get => cLseletedCountTextVisibility; set => SetProperty(ref cLseletedCountTextVisibility, value); }

        private bool subseletedCountTextVisibility;

        public bool SubseletedCountTextVisibility { get => subseletedCountTextVisibility; set => SetProperty(ref subseletedCountTextVisibility, value); }

        private bool chpseletedCountTextVisibility;

        public bool ChpseletedCountTextVisibility { get => chpseletedCountTextVisibility; set => SetProperty(ref chpseletedCountTextVisibility, value); }


        private bool inactivebtn;

        public bool Inactivebtn { get => inactivebtn; set => SetProperty(ref inactivebtn, value); }

        private bool activebtn1;

        public bool activebtn { get => activebtn1; set => SetProperty(ref activebtn1, value); }

        private string detailTxt1;
        public string detailTxt { get { return detailTxt1; } set { detailTxt1 = value; countWord(); OnPropertyChanged(); } }

        private string remainword1;
        public string remainword { get { return remainword1; } set { remainword1 = value;  OnPropertyChanged(); } }

        private string remainColopr1;
        public string remainColopr { get { return remainColopr1; } set { remainColopr1 = value; OnPropertyChanged(); } }


        #endregion
    }
}
