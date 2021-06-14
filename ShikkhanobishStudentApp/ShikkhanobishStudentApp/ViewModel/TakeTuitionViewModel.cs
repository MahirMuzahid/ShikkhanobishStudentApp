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
using ShikkhanobishStudentApp.View;
using ShikkhanobishStudentApp.Server_Connection;
using Flurl.Http;
using System.Threading.Tasks;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class TakeTuitionViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public ServerConnection serverconnection { get; set; }
        ObservableCollection<ClassInfo> AllclsList = new ObservableCollection<ClassInfo>();
        ObservableCollection<UniversityName> AllUNameList = new ObservableCollection<UniversityName>();
        ObservableCollection<Subject> AllsubList = new ObservableCollection<Subject>();
        ObservableCollection<Degree> AlldegreeList = new ObservableCollection<Degree>();
        ObservableCollection<Chapter> AllchpList = new ObservableCollection<Chapter>();
        ObservableCollection<Course> AllCrsList = new ObservableCollection<Course>();
        private int popupFirstIndex;
       

        #region Methods
        public TakeTuitionViewModel()
        {
            homeFirst();
        }
        public void homeFirst()
        {
            popUpVisibility = false;
            SelectedInsName = "Not Selected";
            SelectedClassName = "Not Selected";
            SelectedSubjectName = "Not Selected";
            SelectedChapterName = "Not Selected";

            seletedCountTextVisibility = false;
            CLseletedCountTextVisibility = false;
            SubseletedCountTextVisibility = false;
            ChpseletedCountTextVisibility = false;

            activebtn = false;
            remainColopr = "Black";

            remainword = "Remain 300 Words";
            List<int> ggl = new List<int>();
            ggl.Add(1);
            ggl.Add(2);
            ggl.Add(3);
            ggl.Add(4);
            remainColopr = "#E5E5E5";
            offerList = ggl;
            secTitle = "Class";
            thirdTitle = "Subject";
            forthTitle = "Chapter";
            firstListBtnVisibility = true;
            secondListBtnVisibility = false;
            thirdListBtnVisibility = false;
            forthBtnVisbility = false;
            resultprgs = .1;
            resultvisi = true;
        }
        public bool checkInternet()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Performlogout()
        {
            homeFirst();
        }
        #region populate list
        public async Task InsListPopulate()
        {
            backUpFipName = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getInstitution".GetJsonAsync<ObservableCollection<Institution>>();
        }
        public async Task ClassListPopulate()
        {           
            AllclsList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getClassInfo".GetJsonAsync<ObservableCollection<ClassInfo>>();
            secondListBtnVisibility = true;
        }
        public async Task UNameListPopulate()
        {
            AllUNameList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getUniversityName".GetJsonAsync<ObservableCollection<UniversityName>>();
            secondListBtnVisibility = true;
        }
        public async Task DegreeListPopulate()
        {
            AlldegreeList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getDegree".GetJsonAsync<ObservableCollection<Degree>>();
            
        }
        public async Task SubjectListPopulate()
        {
            AllsubList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getSubject".GetJsonAsync<ObservableCollection<Subject>>();
        }
        public async Task ChapterListPopulate()
        {
            AllchpList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getChapter".GetJsonAsync<ObservableCollection<Chapter>>();
        }
        public async Task CourseListPopulate()
        {
            AllCrsList = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getCourse".GetJsonAsync<ObservableCollection<Course>>();
        }
        #endregion
        //Click in select btn
        public ICommand selectInsCommand =>
            new Command<string>(async(index) =>
            {
                popupList.Clear();
                popUpVisibility = true;
                if (int.Parse(index) == 0)
                {
                    if(backUpFipName == null)
                    {
                        resultvisi = true;
                    }                   
                    searchName = "Select Institution";
                    SearchableVisibility = false;
                    await InsListPopulate();
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertInsTOPupUpList(backUpFipName);
                    resultvisi = false;
                    popupList = convertedList;
                    
                }
                //selecting list for 2nd popup
                else if (int.Parse(index) == 1)
                {

                    if (selectedGlobalIns.institutionID == 101)
                    {
                        resultvisi = true;
                        searchName = "Select Class";
                        ObservableCollection<ClassInfo> popupclsList = new ObservableCollection<ClassInfo>();
                        for (int i = 0; i < AllclsList.Count; i++)
                        {
                            if (AllclsList[i].institutionID == selectedGlobalIns.institutionID)
                            {
                                popupclsList.Add(AllclsList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertClsTOPupUpList(popupclsList);
                        resultvisi = false;
                        popupList = convertedList;
                    }
                    else if (selectedGlobalIns.institutionID == 102)
                    {
                        resultvisi = true;
                        searchName = "Select Class";
                        ObservableCollection<ClassInfo> popupclsList = new ObservableCollection<ClassInfo>();
                        for (int i = 0; i < AllclsList.Count; i++)
                        {
                            if (AllclsList[i].institutionID == selectedGlobalIns.institutionID)
                            {
                                popupclsList.Add(AllclsList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        resultvisi = false;
                        convertedList = ConvertClsTOPupUpList(popupclsList);

                        popupList = convertedList;
                    }
                    else if (selectedGlobalIns.institutionID == 103)
                    {
                        resultvisi = true;
                        searchName = "Select University";
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertUniNameTOPupUpList(AllUNameList);
                        resultvisi = false;
                        popupList = convertedList;
                    }
                    SearchableVisibility = false;
                }
                //selecting list for 3rd popup
                else if (int.Parse(index) == 2)
                {
                    if (thirdTitle == "Subject")
                    {
                        resultvisi = true;
                        searchName = "Select Subject";
                        ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                        for (int i = 0; i < AllsubList.Count; i++)
                        {
                            if (AllsubList[i].classID == selectedGlobalCls.classID)
                            {
                                popupclsList.Add(AllsubList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertSubTOPupUpList(popupclsList);
                        resultvisi = false;
                        popupList = convertedList;
                    }
                    else if (thirdTitle == "Degree")
                    {
                        resultvisi = true;
                        searchName = "Select Degree";
                        ObservableCollection<Degree> popupclsList = new ObservableCollection<Degree>();
                        for (int i = 0; i < AlldegreeList.Count; i++)
                        {
                            if (AlldegreeList[i].uniNameID == selectedGlobalUniName.uniNameID)
                            {
                                popupclsList.Add(AlldegreeList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertDegreeTOPupUpList(popupclsList);
                        resultvisi = false;
                        popupList = convertedList;
                    }

                }
                //selecting list for 4thrd popup
                else if (int.Parse(index) == 3)
                {
                    if (forthTitle == "Chapter")
                    {
                        resultvisi = true;
                        searchName = "Select Chapter";
                        ObservableCollection<Chapter> popupclsList = new ObservableCollection<Chapter>();
                        for (int i = 0; i < AllchpList.Count; i++)
                        {
                            if (AllchpList[i].classID == selectedGlobalCls.classID && AllchpList[i].subjectID == selectedGlobalSub.subjectID)
                            {
                                popupclsList.Add(AllchpList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertChapterTOPupUpList(popupclsList);
                        resultvisi = false;
                        popupList = convertedList;

                    }
                    else if (forthTitle == "Course")
                    {
                        resultvisi = true;
                        searchName = "Select Course";
                        ObservableCollection<Course> popupclsList = new ObservableCollection<Course>();
                        for (int i = 0; i < AllCrsList.Count; i++)
                        {
                            if (AllCrsList[i].uniNameID == selectedGlobalUniName.uniNameID && AllCrsList[i].degreeID == selectedGlobalDgr.degreeID)
                            {
                                popupclsList.Add(AllCrsList[i]);
                            }
                        }
                        ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                        convertedList = ConvertCourseTOPupUpList(popupclsList);
                        resultvisi = false;
                        popupList = convertedList;
                    }

                }
                
            });






        //click in selected item
        public ICommand SelectedItem =>
             new Command<popupList>(async(thisList) =>
             {

                 popUpVisibility = false;
                 
                
                 if (thisList.ListIndex == 1)
                 {
                     if (selectedGlobalCls != null || selectedGlobalUniName != null)
                     {

                         resetList(1);
                     }
                     seletedCountTextVisibility = true;
                     Institution selectedIns = new Institution();
                     for (int i = 0; i < backUpFipName.Count; i++)
                     {
                         if (backUpFipName[i].name == thisList.name)
                         {
                             selectedIns = backUpFipName[i];
                         }
                     }
                     selectedGlobalIns = selectedIns;
                     SelectedInsName = selectedIns.name;
                     TRequest = selectedIns.tuitionRequest;
                     avgratting = selectedIns.avgRatting;
                     if (thisList.name == "College" || thisList.name == "School")
                     {
                         secTitle = "Class";
                         thirdTitle = "Subject";
                         forthTitle = "Chapter";
                         await ClassListPopulate();
                     }
                     else
                     {
                         secTitle = "University";
                         thirdTitle = "Degree";
                         forthTitle = "Course";
                         await UNameListPopulate();
                     }
                     

                 }
                 else if (thisList.ListIndex == 2 || thisList.ListIndex == 3)
                 {
                     if (selectedGlobalSub != null || selectedGlobalDgr != null)
                     {
                         resetList(2);
                     }
                     thirdListBtnVisibility = true;
                     CLseletedCountTextVisibility = true;
                     if (thisList.ListIndex == 2)
                     {
                         ClassInfo selectedList = new ClassInfo();
                         for (int i = 0; i < AllclsList.Count; i++)
                         {
                             if (AllclsList[i].name == thisList.name)
                             {
                                 selectedList = AllclsList[i];
                             }

                         }
                         selectedGlobalCls = selectedList;
                         SelectedClassName = selectedList.name;
                         CLTRequest = selectedList.tuitionRequest;
                         CLavgratting = selectedList.avgRatting;
                         await SubjectListPopulate();
                         thirdListBtnVisibility = true;
                     }
                     else
                     {
                         UniversityName selectedList = new UniversityName();
                         for (int i = 0; i < AllUNameList.Count; i++)
                         {
                             if (AllUNameList[i].name == thisList.name)
                             {
                                 selectedList = AllUNameList[i];
                             }

                         }
                         selectedGlobalUniName = selectedList;
                         SelectedClassName = selectedList.name;
                         CLTRequest = selectedList.tuitionRequest;
                         CLavgratting = selectedList.avgRatting;
                         await DegreeListPopulate();
                         thirdListBtnVisibility = true;
                     }


                 }
                 else if(thisList.ListIndex == 4)
                 {
                     if (selectedGlobalCrs != null || selectedGlobalChp != null)
                     {
                         resetList(3);
                     }
                     SubseletedCountTextVisibility = true;
                     Subject selectedList = new Subject();
                     for (int i = 0; i < AllclsList.Count; i++)
                     {
                         if (AllsubList[i].name == thisList.name)
                         {
                             selectedList = AllsubList[i];
                         }

                     }
                     selectedGlobalSub = selectedList;
                     SelectedSubjectName = selectedList.name;
                     SubTRequest = selectedList.tuitionRequest;
                     Subavgratting = selectedList.avgRatting;
                     forthBtnVisbility = true;
                     await ChapterListPopulate();
                     firstListBtnVisibility = true;

                 }
                 else if (thisList.ListIndex == 5)
                 {
                     if (selectedGlobalCrs != null || selectedGlobalChp != null)
                     {
                         resetList(3);
                     }
                     SubseletedCountTextVisibility = true;
                     Degree selectedList = new Degree();
                     for (int i = 0; i < AlldegreeList.Count; i++)
                     {
                         if (AlldegreeList[i].name == thisList.name)
                         {
                             selectedList = AlldegreeList[i];
                         }

                     }
                     selectedGlobalDgr = selectedList;
                     SelectedSubjectName = selectedList.name;
                     SubTRequest = selectedList.tuitionRequest;
                     Subavgratting = selectedList.avgRatting;
                     forthBtnVisbility = true;
                     await CourseListPopulate();
                     firstListBtnVisibility = true;


                 }
                 else if (thisList.ListIndex == 6)
                 {
                     ChpseletedCountTextVisibility = true;
                     Chapter selectedList = new Chapter();
                     for (int i = 0; i < AllchpList.Count; i++)
                     {
                         if (AllchpList[i].name == thisList.name)
                         {
                             selectedList = AllchpList[i];
                         }

                     }
                     selectedGlobalChp = selectedList;

                     SelectedChapterName = selectedList.name;
                     ChpTRequest = selectedList.tuitionRequest;
                     Chpavgratting = selectedList.avgRatting;
                 }
                 else if (thisList.ListIndex == 7)
                 {
                     ChpseletedCountTextVisibility = true;
                     Course selectedList = new Course();
                     for (int i = 0; i < AllCrsList.Count; i++)
                     {
                         if (AllCrsList[i].name == thisList.name)
                         {
                             selectedList = AllCrsList[i];
                         }

                     }
                     selectedGlobalCrs = selectedList;

                     SelectedChapterName = selectedList.name;
                     ChpTRequest = selectedList.tuitionRequest;
                     Chpavgratting = selectedList.avgRatting;
                 }

                 CheckEverythign();

                 searchText = "";
             });
        #region reset list
        public void resetSecondListlist()
        {
            selectedGlobalCls = null;
            selectedGlobalUniName = null;
            CLseletedCountTextVisibility = false;
            SelectedClassName = "Not Selected";
        }
        public void resetThisrdListlist()
        {
            selectedGlobalSub = null;
            selectedGlobalDgr = null;
            SubseletedCountTextVisibility = false;
            SelectedSubjectName = "Not Selected";
        }
        public void resetForthListlist()
        {
            selectedGlobalChp = null;
            selectedGlobalCrs = null;
            ChpseletedCountTextVisibility = false;
            SelectedChapterName = "Not Selected";
        }
        public void resetList (int i)
        {
            if(i == 1)
            {
                resetSecondListlist();
                resetThisrdListlist();
                resetForthListlist();
                thirdListBtnVisibility = false;
                forthBtnVisbility = false;
            }
            else if (i == 2)
            {
                resetThisrdListlist();
                resetForthListlist();
                forthBtnVisbility = false;
            }
            else if (i == 3)
            {
                resetForthListlist();
            }
        }
        #endregion
        public ICommand CallTeacher =>
             new Command<Institution>((intName) =>
             {
                 Application.Current.MainPage.Navigation.PushAsync(new VideoCallPage());
             });
        public void CheckEverythign()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if(detailTxt != null)
                {
                    if (seletedCountTextVisibility & CLseletedCountTextVisibility & SubseletedCountTextVisibility & ChpseletedCountTextVisibility && detailTxt.Length < 101)
                    {
                        activebtn = true;
                        permincostVisibility = true;
                        perminCostText = "3 coin/min";                        
                    }
                }
                
               
            });
        }
        private void PerformClosePopUp()
        {
            popUpVisibility = false;
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
                remainColopr = "Black";
                CheckEverythign();
            }
            
        }

        public ICommand goRegisterView =>
              new Command(() =>
              {
                  Application.Current.MainPage.Navigation.PushModalAsync(new ResgisterView());
              });


        #region Converter
        public ObservableCollection<popupList> ConvertInsTOPupUpList(ObservableCollection<Institution> insList)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < insList.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = insList[i].name;
                popupobj.totalRequest = insList[i].tuitionRequest;
                popupobj.avgRatting = insList[i].avgRatting;
                popupobj.ListIndex = 1;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
        public ObservableCollection<popupList> ConvertClsTOPupUpList(ObservableCollection<ClassInfo> list)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < list.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = list[i].name;

                popupobj.ListIndex = 2;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
         public ObservableCollection<popupList> ConvertUniNameTOPupUpList(ObservableCollection<UniversityName> list)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < list.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = list[i].name;
                popupobj.totalRequest = list[i].tuitionRequest;
                popupobj.avgRatting = list[i].avgRatting;
                popupobj.ListIndex = 3;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
        public ObservableCollection<popupList> ConvertSubTOPupUpList(ObservableCollection<Subject> insList)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < insList.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = insList[i].name;
                popupobj.totalRequest = insList[i].tuitionRequest;
                popupobj.avgRatting = insList[i].avgRatting;
                popupobj.ListIndex = 4;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }

        public ObservableCollection<popupList> ConvertDegreeTOPupUpList(ObservableCollection<Degree> insList)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < insList.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = insList[i].name;
                popupobj.totalRequest = insList[i].tuitionRequest;
                popupobj.avgRatting = insList[i].avgRatting;
                popupobj.ListIndex =5;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
        public ObservableCollection<popupList> ConvertChapterTOPupUpList(ObservableCollection<Chapter> insList)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < insList.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = insList[i].name;
                popupobj.totalRequest = insList[i].tuitionRequest;
                popupobj.avgRatting = insList[i].avgRatting;
                popupobj.ListIndex = 6;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
        public ObservableCollection<popupList> ConvertCourseTOPupUpList(ObservableCollection<Course> insList)
        {
            ObservableCollection<popupList> popuplist = new ObservableCollection<popupList>();

            for (int i = 0; i < insList.Count; i++)
            {
                popupList popupobj = new popupList();
                popupobj.name = insList[i].name;
                popupobj.totalRequest = insList[i].tuitionRequest;
                popupobj.avgRatting = insList[i].avgRatting;
                popupobj.ListIndex = 7;
                popuplist.Add(popupobj);
            }

            return popuplist;
        }
        #endregion


        #endregion


        #region Binding Garbage
        public Institution selectedGlobalIns { get; set; }
        public ClassInfo selectedGlobalCls { get; set; }
        public UniversityName selectedGlobalUniName { get; set; }
        public Subject selectedGlobalSub { get; set; }
        public Degree selectedGlobalDgr { get; set; }
        public Chapter selectedGlobalChp { get; set; }
        public Course selectedGlobalCrs{ get; set; }
        public ObservableCollection<Institution> backUpFipName { get; set; }
        public ObservableCollection<ClassInfo> backUpScPName { get; set; }
        public ObservableCollection<popupList> backUpthrPName { get; set; }
        public ObservableCollection<popupList> backUpfrPName { get; set; }
        public ObservableCollection<popupList> thisList { get; set; }
        private bool popUpVisibility1;

        public bool popUpVisibility { get => popUpVisibility1; set => SetProperty(ref popUpVisibility1, value); }
        
        private ObservableCollection<popupList> _popupList = new ObservableCollection<popupList>();
        public ObservableCollection<popupList> popupList { get => _popupList; set => SetProperty(ref _popupList, value); }
        

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

        public string searchText { get { return searchText1; } set { searchText1 = value; /*SearchControl();*/ OnPropertyChanged(); } }

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

        private double avgratting1;

        public double avgratting { get => avgratting1; set => SetProperty(ref avgratting1, value); }

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

        private bool permincostVisibility1;

        public bool permincostVisibility { get => permincostVisibility1; set => SetProperty(ref permincostVisibility1, value); }

        private string perminCostText1;

        public string perminCostText { get => perminCostText1; set => SetProperty(ref perminCostText1, value); }
        private string SecondListTitle1;

        public string SecondListTitle { get => SecondListTitle1; set => SetProperty(ref SecondListTitle1, value); }

        private List<int> offerList1 { get; set; }
        public List<int> offerList { get { return offerList1; } set { offerList1 = value; OnPropertyChanged(); } }

        private double tticonopacity1;

        public double tticonopacity { get => tticonopacity1; set => SetProperty(ref tticonopacity1, value); }

        private double rcopacity1;

        public double rcopacity { get => rcopacity1; set => SetProperty(ref rcopacity1, value); }

        private bool secondListBtnVisibility1;

        public bool secondListBtnVisibility { get => secondListBtnVisibility1; set => SetProperty(ref secondListBtnVisibility1, value); }

        private bool firstListBtnVisibility1;

        public bool firstListBtnVisibility { get => firstListBtnVisibility1; set => SetProperty(ref firstListBtnVisibility1, value); }

        private string secTitle1;

        public string secTitle { get => secTitle1; set => SetProperty(ref secTitle1, value); }

        private string thirdTitle1;

        public string thirdTitle { get => thirdTitle1; set => SetProperty(ref thirdTitle1, value); }

        private string forthTitle1;

        public string forthTitle { get => forthTitle1; set => SetProperty(ref forthTitle1, value); }

        private bool thirdListBtnVisibility1;

        public bool thirdListBtnVisibility { get => thirdListBtnVisibility1; set => SetProperty(ref thirdListBtnVisibility1, value); }

        private bool forthBtnVisbility1;

        public bool forthBtnVisbility { get => forthBtnVisbility1; set => SetProperty(ref forthBtnVisbility1, value); }

        private string phoneNumber;

        public string PhoneNumber { get => phoneNumber; set => SetProperty(ref phoneNumber, value); }
        private string Password1;

        public string Password { get => Password1; set => SetProperty(ref Password1, value); }

        private string errortxt1;

        public string errortxt { get => errortxt1; set => SetProperty(ref errortxt1, value); }

        private bool connectivitiVisibility1;

        public bool connectivitiVisibility { get => connectivitiVisibility1; set => SetProperty(ref connectivitiVisibility1, value); }

        private double resultprgs1;

        public double resultprgs { get => resultprgs1; set => SetProperty(ref resultprgs1, value); }

        private bool resultvisi1;

        public bool resultvisi { get => resultvisi1; set => SetProperty(ref resultvisi1, value); }

        private Command logout1;

        public ICommand logout
        {
            get
            {
                if (logout1 == null)
                {
                    logout1 = new Command(Performlogout);
                }

                return logout1;
            }
        }

        
        #endregion
    }
}
