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
using Xamarin.Forms.Vonage;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;
using XF.Material.Forms.UI.Dialogs;
using System.Linq;
using Android.Content.Res;

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
        List<favouriteTeacher> thisfavteacher = new List<favouriteTeacher>();
        RealTimeApiMethods realtimeapi = new RealTimeApiMethods();
        CostClass Allcost = new CostClass();
        private int thisSearcherSubId;
        private int thisSelectedFavPopUpTeacher;
        HubConnection _connection = null;
        Teacher SelectedTeacher = new Teacher();
        string url = "https://shikkhanobishrealtimeapi.shikkhanobish.com/ShikkhanobishHub";
        int rechargeCoinAMountInt, rechargeTTakaAmountInt; 
        StudentPaymentHistory thispayment = new StudentPaymentHistory();
        public Voucher thisUsedVoucher { get; set; }
        List<Voucher> allVoucher = new List<Voucher>();
        #region Methods

        public TakeTuitionViewModel()
        {
            homeFirst();
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public async Task homeFirst()
        {
          
            paymentGifGrid = false;
            SucPaymentText = "";
            prmStudentTextVisibility = false;
            acceptTeacherVisibility = false;
            selectedTeacherConnectingVisibility = false;
            prmStudentTextVisibility = false;
            hireteacherEnabled = false;
            groupChoiseVisibility = false;
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
           
            remainColopr = "#E5E5E5";
            secTitle = "Class";
            thirdTitle = "Subject";
            forthTitle = "Chapter";
            firstListBtnVisibility = true;
            secondListBtnVisibility = false;
            thirdListBtnVisibility = false;
            forthBtnVisbility = false;
            resultprgs = .1;
            resultvisi = true;
            rechargeButtonVisibility = false;

            isLoading = true;
            await GetAllCost();
            await GetVoucher();
            await GetPromotImage();
            await GetProMsg();
            await ConnectToRealTimeApiServer();
            await getALlFavTeacher();
            isLoading = false;
            avaiableCoin = StaticPageToPassData.thisStudentInfo.coin+"";
            freeMinText = StaticPageToPassData.thisStudentInfo.freemin + "";
        }
        #region Methods
        public async Task GetAllCost()
        {
            Allcost = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetCost".GetJsonAsync<CostClass>();

        }
        public  async Task GetVoucher()
        {
             allVoucher = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getVoucher".GetJsonAsync<List<Voucher>>();
        }
        private void PerformpopOutRegMsgVisiblility()
        {
            regMsgVisiblity = false; ;
        }
        public async Task GetProMsg()
        {
            var promsg = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetPromotionalMassage".GetJsonAsync<List<PromotionalMassage>>();
            for(int i = 0; i < promsg.Count; i++)
            {
                if(promsg[i].userType == "student")
                {
                    promsgImgSrc = promsg[i].imageSrc;
                    proMsgText = promsg[i].text;
                }
            }
        }
        public async Task GetPromotImage()
        {
            var allPromoImage = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetPromotionalImage".GetJsonAsync<List<PromotionalImage>>();
            offerList = allPromoImage;
        }
        public async Task CalCulateReachrgeCost()
        {
            for(int i = 0; i < allVoucher.Count; i++)
            {
                if (int.Parse(rechargeAmount) == allVoucher[i].amountTaka)
                {
                    if (allVoucher[i].type == 0)
                    {
                        totalRechargeCoin = rechargeAmount;
                        addedCoinamount = " + " + allVoucher[i].getAmount.ToString();
                        thisUsedVoucher = allVoucher[i];
                        rechargeCoinAMountInt = allVoucher[i].getAmount + int.Parse(rechargeAmount);
                        rechargeTTakaAmountInt = int.Parse(rechargeAmount);
                        totalAmount = rechargeAmount + " Taka";
                        freeminInaddCoinScreen = 0 + "";
                        thispayment.addedMin = 0;
                        break;
                    }
                    if (allVoucher[i].type == 1)
                    {
                        totalRechargeCoin = rechargeAmount;
                        addedCoinamount = "";
                        thisUsedVoucher = allVoucher[i];
                        rechargeCoinAMountInt =  int.Parse(rechargeAmount);
                        rechargeTTakaAmountInt = int.Parse(rechargeAmount);
                        totalAmount = rechargeAmount + " Taka";
                        freeminInaddCoinScreen = allVoucher[i].getAmount +  "";
                        thispayment.addedMin = allVoucher[i].getAmount;
                        break;
                    }

                }
                else
                {
                    totalRechargeCoin = rechargeAmount;
                    addedCoinamount = "";
                    thisUsedVoucher = new Voucher();
                    rechargeCoinAMountInt = allVoucher[i].getAmount;
                    rechargeTTakaAmountInt = int.Parse(rechargeAmount);
                    totalAmount = rechargeAmount + " Taka";
                    freeminInaddCoinScreen = 0 + "";
                }
                if (allVoucher[i].type == 0)
                {
                    
                }
                
               
            }
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
        private async Task PerformlogoutAsync()
        {            
            SecureStorage.RemoveAll();
            if (StaticPageToPassData.isFromLogin)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                var existingPages = Application.Current.MainPage.Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Application.Current.MainPage.Navigation.RemovePage(page);
                }
                await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }
            
        }
       

        public void CheckEverythign()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if(detailTxt != null)
                {
                    if (SelectedInsName != "Not Selected" & SelectedClassName != "Not Selected" & SelectedSubjectName != "Not Selected" & SelectedChapterName != "Not Selected"  && detailTxt.Length < 300 && detailTxt != "" && detailTxt != null)
                    {
                        activebtn = true;
                        permincostVisibility = true;
                        if(selectedInsName == "School")
                        {
                            perminCostText = Allcost.SchoolCost+ " coin/min";
                        }
                        if(SelectedInsName == "College")
                        {
                            perminCostText = Allcost.CollegeCost + " coin/min";
                        }                     
                    }
                    else
                    {
                        activebtn = false;
                        permincostVisibility = false;
                    }
                }
                
               
            });
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
        private async Task PerformrechargeCoin()
        {
            isLoading = true;
            await  Task.Delay(1000);
            RechargeerrorTxt = "";
            try
            {

                
                thispayment.studentID = StaticPageToPassData.thisStudentInfo.studentID;
                thispayment.amountTaka = rechargeTTakaAmountInt;
                if (thisUsedVoucher.voucherID != 0)
                {
                    thispayment.isVoucherUsed = 1;
                    thispayment.voucherID = thisUsedVoucher.voucherID;
                    thispayment.amountCoin = rechargeCoinAMountInt;
                }
                else
                {
                    thispayment.isVoucherUsed = 0;
                    thispayment.voucherID = 0;
                    thispayment.amountCoin = rechargeCoinAMountInt;
                }
                thispayment.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                thispayment.name = thisUsedVoucher.name;
                var redirectURL = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/RequestPayment".PostUrlEncodedAsync(new
                {
                    name = StaticPageToPassData.thisStudentInfo.name,
                    amount = int.Parse(rechargeAmount),
                    studentID = StaticPageToPassData.thisStudentInfo.studentID,
                    phonenumber = StaticPageToPassData.thisStudentInfo.phonenumber
                })
   .ReceiveJson<string>();
                await Application.Current.MainPage.Navigation.PushModalAsync(new PaymentView(redirectURL));
            }
            catch (Exception ex)
            {
                RechargeerrorTxt = ex.Message;
            }
            isLoading = false;
        }

       

        #endregion

        #region Favteacher PopUp
        public ICommand ChooseTeacherPopUp =>
             new Command(async () =>
             {
                 chooseTeacherVisibility = true;
                 hireteacherPopupVisibility = true;
                 hireteacherEnabled = false;

                 if (popupfavteacheritemSource != null)
                 {
                     popupfavteacheritemSource.Clear();
                 }
                 popupfavteacheritemSource = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentIDForPopUp".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID, subjectID = thisSearcherSubId })
       .ReceiveJson<List<favouriteTeacher>>();
                 for (int i = 0; i < popupfavteacheritemSource.Count; i++)
                 {
                     popupfavteacheritemSource[i].activeStatus = "Online";
                     popupfavteacheritemSource[i].activeColor = "Green";
                     popupfavteacheritemSource[i].teacherRatting = Math.Round(popupfavteacheritemSource[i].teacherRatting, 2);
                 }
                 if (popupfavteacheritemSource.Count != 0)
                 {
                     choosefavteacherlbl = true;
                     nofavteacherlbl = false;
                 }
                 else
                 {

                     choosefavteacherlbl = false;
                     nofavteacherlbl = true;

                 }

             });
        private void PerformgroupChoice(string index)
        {
            int indexno = int.Parse(index);
            if (indexno == 1)
            {
                if (scChoice == Color.GreenYellow)
                {
                    scChoice = Color.Transparent;
                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID)
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }
                else
                {
                    scChoice = Color.GreenYellow;
                    cmChoice = Color.Transparent;
                    arChoice = Color.Transparent;

                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID && AllsubList[i].groupName == "Science")
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }

            }
            if (indexno == 2)
            {
                if (cmChoice == Color.GreenYellow)
                {
                    cmChoice = Color.Transparent;
                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID)
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }
                else
                {
                    cmChoice = Color.GreenYellow;
                    arChoice = Color.Transparent;
                    scChoice = Color.Transparent;
                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID && AllsubList[i].groupName == "Commerce")
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }

            }
            if (indexno == 3)
            {
                if (arChoice == Color.GreenYellow)
                {
                    arChoice = Color.Transparent;
                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID)
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }
                else
                {
                    arChoice = Color.GreenYellow;
                    scChoice = Color.Transparent;
                    cmChoice = Color.Transparent;
                    ObservableCollection<Subject> popupclsList = new ObservableCollection<Subject>();
                    for (int i = 0; i < AllsubList.Count; i++)
                    {
                        if (AllsubList[i].classID == selectedGlobalCls.classID && AllsubList[i].groupName == "Arts")
                        {
                            popupclsList.Add(AllsubList[i]);
                        }
                    }
                    groupChoiseVisibility = true;
                    ObservableCollection<popupList> convertedList = new ObservableCollection<popupList>();
                    convertedList = ConvertSubTOPupUpList(popupclsList);
                    resultvisi = false;
                    popupList = convertedList;
                }

            }
        }
        private void PerformClosePopUpHireTeacher()
        {
            hireteacherPopupVisibility = false;
        }
        private async Task PerformchooserndTeachercmd()
        {
            if (randonpopupTeacherbtnColor == Color.FromHex("#5098E87F"))
            {
                randonpopupTeacherbtnColor = Color.Transparent;
                hireteacherEnabled = false;
            }
            else
            {
                randonpopupTeacherbtnColor = Color.FromHex("#5098E87F");
                hireteacherEnabled = true;
                List<favouriteTeacher> newfavTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentIDForPopUp".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID, subjectID = thisSearcherSubId })
      .ReceiveJson<List<favouriteTeacher>>();
                for (int i = 0; i < newfavTeacher.Count; i++)
                {
                    newfavTeacher[i].popupfavSelectedbackground = "#Transparent";
                    newfavTeacher[i].teacherRatting = Math.Round(newfavTeacher[i].teacherRatting, 2);
                }
                popupfavteacheritemSource.Clear();
                popupfavteacheritemSource = newfavTeacher;
                thisfavteacher = newfavTeacher;
                hireteacherEnabled = true;
                thisSelectedFavPopUpTeacher = 0;
            }

        }
        private async Task PerformhireTeacherBtnCmdAsync()
        {

            chooseTeacherVisibility = false;
            selectedTeacherConnectingVisibility = true;
            connectingTeachertxt = "Conneting with Shikkhanobish Teacher Server. Please Wait...";
            int teacherisSelected;

            if (thisSelectedFavPopUpTeacher == 0)
            {
                SelectedTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/HireTeacherAsync".PostUrlEncodedAsync(new { subID = thisSearcherSubId })
       .ReceiveJson<Teacher>();
                teacherisSelected = SelectedTeacher.teacherID;
            }
            else
            {
                teacherisSelected = thisSelectedFavPopUpTeacher;
            }

            string uriToCAllTeacher = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/CallSelectedTeacher?&teacherID=" + teacherisSelected + "&des=" + detailTxt + "&cls=" + SelectedClassName + "&sub=" + thisSearcherSubId + "&chapter=" + selectedChapterName + "&cost=" + "3" + "&name=" + StaticPageToPassData.thisStudentInfo.name + "&studentID=" + StaticPageToPassData.thisStudentInfo.studentID;
            await realtimeapi.ExecuteRealTimeApi(uriToCAllTeacher);
        }

        private void PerformcancleTeacherSearch()
        {

            selectedTeacherConnectingVisibility = false;
            chooseTeacherVisibility = true;

        }
        private void PerformacceptTeacherTuition()
        {
            if (!CrossVonage.Current.TryStartSession())
            {
                return;
            }
            PerMinPassModel perminPass = new PerMinPassModel();
            perminPass.studentID = StaticPageToPassData.thisStudentInfo.studentID;
            perminPass.teacherID = thisSelectedFavPopUpTeacher;
            perminPass.time = 0;
            perminPass.sessionID = thisSesionID;
            perminPass.firstChoiceID = firstChoiceID + "";
            perminPass.secondChoiceID = secondChoiceID + "";
            perminPass.thirdChoiceID = thirdChoiceID + "";
            perminPass.forthChoiceID = forthChoiceID + "";
            perminPass.firstChoiceName = SelectedInsName;
            perminPass.secondChoiceName = SelectedClassName;
            perminPass.thirdChoiceName = SelectedSubjectName;
            perminPass.forthChoiceName = selectedChapterName;
            StaticPageToPassData.perMinCall = perminPass;
            Application.Current.MainPage.Navigation.PushModalAsync(new VideoCallPage());
        }
        public async Task requestPermission()
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();
        }
        public async Task ConnectToRealTimeApiServer()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();
            await _connection.StartAsync();


            _connection.Closed += async (s) =>
            {
                await _connection.StartAsync();
            };

            _connection.On<int, int, bool, int, string, string>("SelectedTeacherResponse", async (teacherID, studentID, response, apikey, sessionID, token) =>
            {
                if (teacherID == thisSelectedFavPopUpTeacher && studentID == StaticPageToPassData.thisStudentInfo.studentID)
                {
                    if (response == false)
                    {
                        
                        int teacherisSelected;
                        if (thisSelectedFavPopUpTeacher == 0)
                        {
                            SelectedTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/HireTeacherAsync".PostUrlEncodedAsync(new { subID = thisSearcherSubId })
                   .ReceiveJson<Teacher>();
                            teacherisSelected = SelectedTeacher.teacherID;
                            string uriToCAllTeacher = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/CallSelectedTeacher?&teacherID=" + teacherisSelected + "&des=" + detailTxt + "&cls=" + SelectedClassName + "&sub=" + thisSearcherSubId + "&chapter=" + selectedChapterName + "&cost=" + "3" + "&name=" + StaticPageToPassData.thisStudentInfo.name + "&studentID=" + StaticPageToPassData.thisStudentInfo.studentID;
                            await realtimeapi.ExecuteRealTimeApi(uriToCAllTeacher);
                        }
                        else
                        {
                            connectingTeachertxt = "Teacher is busy. Choose another teacher.";
                            Task.Delay(2000);
                            selectedTeacherConnectingVisibility = false;
                            chooseTeacherVisibility = true;
                            teacherisSelected = thisSelectedFavPopUpTeacher;
                        }

                       
                    }
                    else
                    {
                        connectingTeachertxt = "A teacher has been connected...";
                        thisSesionID = sessionID;
                        acceptTeacherVisibility = true;
                        CrossVonage.Current.ApiKey = apikey + "";
                        CrossVonage.Current.SessionId = sessionID;
                        CrossVonage.Current.UserToken = token;


                    }
                }
            });
            _connection.On<int, bool>("PassActiveStatus", async (teacherID, isActive) =>
            {
                popupfavteacheritemSource.Clear();
                popupfavteacheritemSource = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentIDForPopUp".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID, subjectID = thisSearcherSubId })
   .ReceiveJson<List<favouriteTeacher>>();

                for (int i = 0; i < popupfavteacheritemSource.Count; i++)
                {
                    popupfavteacheritemSource[i].activeStatus = "Online";
                    popupfavteacheritemSource[i].activeColor = "Green";
                    popupfavteacheritemSource[i].teacherRatting = Math.Round(popupfavteacheritemSource[i].teacherRatting, 2);
                }

                if(thisSelectedFavPopUpTeacher == teacherID)
                {
                    hireteacherEnabled = false;
                    randonpopupTeacherbtnColor = Color.FromHex("#5098E87F");
                }
            });
            _connection.On<int, bool, string,string, string, string, string, string>("StudentPaymentStatus", async (studentID, successFullPayment,amount,response, paymentID, trxID, cardID, cardType) =>
            {
                if(studentID == StaticPageToPassData.thisStudentInfo.studentID)
                {
                    if (successFullPayment)
                    {
                        //StaticPageToPassData.LastPaymentRequestID = paymentID;
                        var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/setStudentPaymentHistory".PostUrlEncodedAsync(new { 
                            studentID = StaticPageToPassData.thisStudentInfo.studentID,
                            paymentID = paymentID,
                            date = thispayment.date,
                            trxID = trxID,
                            amountTaka = amount,
                            amountCoin = thispayment.amountCoin,
                            medium = cardType,
                            name = thispayment.name,
                            isVoucherUsed = thispayment.isVoucherUsed,
                            voucherID = thispayment.voucherID,
                            cardID = cardID})
                            .ReceiveJson<Response>();

                        var regRes = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/updateStudent"
                            .PostUrlEncodedAsync(new { 
                             studentID = thispayment.studentID, 
                             phonenumber = StaticPageToPassData.thisStudentInfo.phonenumber, 
                             password = StaticPageToPassData.thisStudentInfo.password,
                             totalSpent = StaticPageToPassData.thisStudentInfo.totalSpent,
                             totalTuitionTime = StaticPageToPassData.thisStudentInfo.totalTuitionTime, 
                             coin = StaticPageToPassData.thisStudentInfo.coin+thispayment.amountCoin, 
                             freemin = StaticPageToPassData.thisStudentInfo.freemin+thispayment.addedMin, 
                             city = StaticPageToPassData.thisStudentInfo.city, 
                             name = StaticPageToPassData.thisStudentInfo.name, 
                             institutionName = "none" })
                             .ReceiveJson<Response>();

                        paymentGifGrid = true;
                        SucPaymentText = "You have successfully added " + amount + " coin in your account. Thank you for staying with us.";
                        await StaticPageToPassData.GetStudent();
                        avaiableCoin = StaticPageToPassData.thisStudentInfo.coin + "";
                        freeMinText = StaticPageToPassData.thisStudentInfo.freemin + "";
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: response,
                                    title: "There is problem");
                    }
                }
            });

        }
        private void PerformpopOUTpaymentGif()
        {
            paymentGifGrid = false;
            SucPaymentText = "";
        }
        #endregion

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

        #region Popup in take tuition
        //Click in select btn
        public ICommand selectInsCommand =>
            new Command<string>(async (index) =>
            {
                popupList.Clear();
                popUpVisibility = true;
                if (int.Parse(index) == 0)
                {
                    if (backUpFipName == null)
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
                        arChoice = Color.Transparent;
                        scChoice = Color.Transparent;
                        cmChoice = Color.Transparent;
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
                        groupChoiseVisibility = true;
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
             new Command<popupList>(async (thisList) =>
             {


                 groupChoiseVisibility = false;
                 popWaitVisiblity = true;
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
                             firstChoiceID = backUpFipName[i].institutionID+"";
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
                     CLseletedCountTextVisibility = true;
                     if (thisList.ListIndex == 2)
                     {
                         ClassInfo selectedList = new ClassInfo();
                         for (int i = 0; i < AllclsList.Count; i++)
                         {
                             if (AllclsList[i].name == thisList.name)
                             {
                                 selectedList = AllclsList[i];
                                 secondChoiceID = AllclsList[i].classID + "";

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
                                 secondChoiceID = AllUNameList[i].uniNameID + "";
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
                 else if (thisList.ListIndex == 4)
                 {
                     if (selectedGlobalCrs != null || selectedGlobalChp != null)
                     {
                         resetList(3);
                     }
                     SubseletedCountTextVisibility = true;
                     Subject selectedList = new Subject();
                     for (int i = 0; i < AllsubList.Count; i++)
                     {
                         if (AllsubList[i].name == thisList.name)
                         {
                             selectedList = AllsubList[i];
                             thisSearcherSubId = AllsubList[i].subjectID;
                             thirdChoiceID = AllsubList[i].subjectID + "";
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
                             thirdChoiceID = AlldegreeList[i].degreeID + "";
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
                             forthChoiceID = AllchpList[i].chapterID + "";
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
                             forthChoiceID = AllCrsList[i].courseID + "";
                         }

                     }
                     selectedGlobalCrs = selectedList;

                     SelectedChapterName = selectedList.name;
                     ChpTRequest = selectedList.tuitionRequest;
                     Chpavgratting = selectedList.avgRatting;
                 }

                 CheckEverythign();
                 popWaitVisiblity = false;
                 popUpVisibility = false;
                 searchText = "";
             });
        private void PerformClosePopUp()
        {
            popUpVisibility = false;
        }
        #endregion

        #region favourite Teacher
        private void favGrid()
        {          
          
        }
        public async Task GetFavouriteTeaacherList()
        {
            thisfavteacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentID".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID })
       .ReceiveJson<List<favouriteTeacher>>();


            for(int i = 0; i < thisfavteacher.Count; i++)
            {
                thisfavteacher[i].popupfavSelectedbackground = "Transparent";
                thisfavteacher[i].teacherRatting = Math.Round(thisfavteacher[i].teacherRatting, 2);
            }
            if (thisfavteacher.Count == 0) {
                prmStudentTextVisibility = true;
            }
            else
            {
                prmStudentTextVisibility = false;
            }
            favteacherItemSource = thisfavteacher;


        }
        public async Task  getALlFavTeacher()
        {
            
           var prm = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getPremiumStudentWithID".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID })
 .ReceiveJson<PremiumStudent>();
            if (prm.studentID == 0)
            {
                prmStudentText = "*";
                studentstatus= "Normal";
                studentstatusColor = Color.Black;
                maxnumteacher= prm.maxNumberofFavouriteTeacher;
                prmStudentTextVisibility = true;
            }
            else
            {
                prmStudentText = "*";
                studentstatus = "Premium";
                studentstatusColor = Color.FromHex("#864AE8");
                maxnumteacher = prm.maxNumberofFavouriteTeacher;
                prmStudentTextVisibility = false;
            }
            await GetFavouriteTeaacherList();

           
        }
        public ICommand RemoveFavTeacher
        {
            get
            {
                return new Command<favouriteTeacher>((favteacher) =>
                {
                    Remove(favteacher);
                });
            }
        }
        public ICommand SeletecFavpopupTeacher
        {
            get
            {
                return new Command<favouriteTeacher>(async (favteacher) =>
                {
                    //List<favouriteTeacher> newfavTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentIDForPopUp".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID, subjectID = thisSearcherSubId })
     // .ReceiveJson<List<favouriteTeacher>>();
                    popupfavteacheritemSource.Clear();
                    popupfavteacheritemSource = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentIDForPopUp".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID, subjectID = thisSearcherSubId })
       .ReceiveJson<List<favouriteTeacher>>();

                    for (int i = 0; i < popupfavteacheritemSource.Count; i++)
                    {
                        popupfavteacheritemSource[i].activeStatus = "Online";
                        popupfavteacheritemSource[i].activeColor = "Green";
                        if (popupfavteacheritemSource[i].teacherID == favteacher.teacherID)
                        {
                            popupfavteacheritemSource[i].popupfavSelectedbackground = "#5098E87F";
                            thisSelectedFavPopUpTeacher = popupfavteacheritemSource[i].teacherID;
                            
                        }
                        else
                        {
                            popupfavteacheritemSource[i].popupfavSelectedbackground = "#Transparent";
                        }
                        popupfavteacheritemSource[i].teacherRatting = Math.Round(popupfavteacheritemSource[i].teacherRatting, 2);
                    }
                   //// popupfavteacheritemSource.Clear();
                    //popupfavteacheritemSource = newfavTeacher;
                    hireteacherEnabled = true;
                    randonpopupTeacherbtnColor = Color.Transparent;
                });
            }
        }
        public async Task SelectFavTeacher()
        {

        }
        public async Task Remove( favouriteTeacher favteacher)
        {
           favteacherItemSource.Clear();
            var res = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/removeFavTeacherWithTeacherID".PostUrlEncodedAsync(new { teacherID = favteacher.teacherID })
     .ReceiveJson<Response>();
            favteacherItemSource =  await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getFavouriteTeacherwithStudentID".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID })
      .ReceiveJson<List<favouriteTeacher>>();
            for (int i = 0; i < favteacherItemSource.Count; i++)
            {
                favteacherItemSource[i].teacherRatting = Math.Round(favteacherItemSource[i].teacherRatting, 2);
            }


        }
        #endregion
       
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
        public void resetList(int i)
        {
            if (i == 1)
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

        private List<PromotionalImage> offerList1 { get; set; }
        public List<PromotionalImage> offerList { get { return offerList1; } set { offerList1 = value; OnPropertyChanged(); } }

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
                    logout1 = new Command(async => PerformlogoutAsync());
                }

                return logout1;
            }
        }

        private string freeMin1;

        public string freeMin { get => freeMin1; set => SetProperty(ref freeMin1, value); }

        private string avaiableCoin1;

        public string avaiableCoin { get => avaiableCoin1; set => SetProperty(ref avaiableCoin1, value); }

        private bool popWaitVisiblity1;

        public bool popWaitVisiblity { get => popWaitVisiblity1; set => SetProperty(ref popWaitVisiblity1, value); }

        private List<favouriteTeacher> favteacherItemSource1;

        public List<favouriteTeacher> favteacherItemSource { get => favteacherItemSource1; set => SetProperty(ref favteacherItemSource1, value); }

        private Command favGridCommand1;

        public ICommand favGridCommand
        {
            get
            {
                if (favGridCommand1 == null)
                {
                    favGridCommand1 = new Command(favGrid);
                }

                return favGridCommand1;
            }
        }

        private string prmStudentText1;

        public string prmStudentText { get => prmStudentText1; set => SetProperty(ref prmStudentText1, value); }

        private string studentstatus1;

        public string studentstatus { get => studentstatus1; set => SetProperty(ref studentstatus1, value); }

        private Color studentstatusColor1;

        public Color studentstatusColor { get => studentstatusColor1; set => SetProperty(ref studentstatusColor1, value); }

        private string maxnumteacher1;

        public string maxnumteacher { get => maxnumteacher1; set => SetProperty(ref maxnumteacher1, value); }

        private bool prmStudentTextVisibility1;

        public bool prmStudentTextVisibility { get => prmStudentTextVisibility1; set => SetProperty(ref prmStudentTextVisibility1, value); }

        private bool groupChoiseVisibility1;

        public bool groupChoiseVisibility { get => groupChoiseVisibility1; set => SetProperty(ref groupChoiseVisibility1, value); }

        private Color scChoice1;

        public Color scChoice { get => scChoice1; set => SetProperty(ref scChoice1, value); }

        private Color cmChoice1;

        public Color cmChoice { get => cmChoice1; set => SetProperty(ref cmChoice1, value); }

        private Color arChoice1;

        public Color arChoice { get => arChoice1; set => SetProperty(ref arChoice1, value); }

        private Command groupChoice1;

        public ICommand groupChoice
        {
            get
            {
                if (groupChoice1 == null)
                {
                    groupChoice1 = new Command<string>(PerformgroupChoice);
                }

                return groupChoice1;
            }
        }

        private bool hireteacherPopupVisibility1;

        public bool hireteacherPopupVisibility { get => hireteacherPopupVisibility1; set => SetProperty(ref hireteacherPopupVisibility1, value); }

        private Command closePopUpHireTeacher;

        public ICommand ClosePopUpHireTeacher
        {
            get
            {
                if (closePopUpHireTeacher == null)
                {
                    closePopUpHireTeacher = new Command(PerformClosePopUpHireTeacher);
                }

                return closePopUpHireTeacher;
            }
        }

        private Command hireTeacherBtnCmd1;

        public ICommand hireTeacherBtnCmd
        {
            get
            {
                if (hireTeacherBtnCmd1 == null)
                {
                    hireTeacherBtnCmd1 = new Command(async => PerformhireTeacherBtnCmdAsync());
                }

                return hireTeacherBtnCmd1;
            }
        }

        
        private Color randonpopupTeacherbtnColor1;

        public Color randonpopupTeacherbtnColor { get => randonpopupTeacherbtnColor1; set => SetProperty(ref randonpopupTeacherbtnColor1, value); }

        private Command chooserndTeachercmd1;

        public ICommand chooserndTeachercmd
        {
            get
            {
                if (chooserndTeachercmd1 == null)
                {
                    chooserndTeachercmd1 = new Command(async => PerformchooserndTeachercmd());
                }

                return chooserndTeachercmd1;
            }
        }

        private bool hireteacherEnabled1;

        public bool hireteacherEnabled { get => hireteacherEnabled1; set => SetProperty(ref hireteacherEnabled1, value); }

        private List<favouriteTeacher> popupfavteacheritemSource1;

        public List<favouriteTeacher> popupfavteacheritemSource { get => popupfavteacheritemSource1; set => SetProperty(ref popupfavteacheritemSource1, value); }

        private bool nofavteacherlbl1;

        public bool nofavteacherlbl { get => nofavteacherlbl1; set => SetProperty(ref nofavteacherlbl1, value); }

        private bool choosefavteacherlbl1;

        public bool choosefavteacherlbl { get => choosefavteacherlbl1; set => SetProperty(ref choosefavteacherlbl1, value); }

        private favouriteTeacher seletectedFavTeacherFrompopUp1;

        public favouriteTeacher seletectedFavTeacherFrompopUp { get { return seletectedFavTeacherFrompopUp1; } set { seletectedFavTeacherFrompopUp1 = value; SetProperty(ref seletectedFavTeacherFrompopUp1, value); } }

        private bool selectedTeacherConnectingVisibility1;

        public bool selectedTeacherConnectingVisibility { get => selectedTeacherConnectingVisibility1; set => SetProperty(ref selectedTeacherConnectingVisibility1, value); }

        private bool chooseTeacherVisibility1;

        public bool chooseTeacherVisibility { get => chooseTeacherVisibility1; set => SetProperty(ref chooseTeacherVisibility1, value); }

        private Command cancleTeacherSearch1;

        public ICommand cancleTeacherSearch
        {
            get
            {
                if (cancleTeacherSearch1 == null)
                {
                    cancleTeacherSearch1 = new Command(PerformcancleTeacherSearch);
                }

                return cancleTeacherSearch1;
            }
        }

        private string connectingTeachertxt1;
        private string thisSesionID;
        private string firstChoiceID;
        private string secondChoiceID;
        private string thirdChoiceID;
        private string forthChoiceID;

        public string connectingTeachertxt { get => connectingTeachertxt1; set => SetProperty(ref connectingTeachertxt1, value); }

        private Command acceptTeacherTuition1;

        public ICommand acceptTeacherTuition
        {
            get
            {
                if (acceptTeacherTuition1 == null)
                {
                    acceptTeacherTuition1 = new Command(PerformacceptTeacherTuition);
                }

                return acceptTeacherTuition1;
            }
        }

        private bool acceptTeacherVisibility1;

        public bool acceptTeacherVisibility { get => acceptTeacherVisibility1; set => SetProperty(ref acceptTeacherVisibility1, value); }

        private Command rechargeCoin1;

        public ICommand rechargeCoin
        {
            get
            {
                if (rechargeCoin1 == null)
                {
                    rechargeCoin1 = new Command(async =>PerformrechargeCoin());
                }

                return rechargeCoin1;
            }
        }

        private WebViewSource paymentSource;

        public WebViewSource PaymentSource { get => paymentSource; set => SetProperty(ref paymentSource, value); }

        private string rechargeAmount1;

        public string rechargeAmount { get => rechargeAmount1; set { rechargeAmount1 = value; if (rechargeAmount != null && rechargeAmount != "" && IsDigitsOnly(rechargeAmount)) { if (int.Parse(rechargeAmount) < 10) { rechargeButtonVisibility = false; totalAmount = ""; totalRechargeCoin = ""; addedCoinamount = ""; } else { rechargeButtonVisibility = true; CalCulateReachrgeCost(); }  } else { rechargeButtonVisibility = false; totalAmount = ""; totalRechargeCoin = ""; addedCoinamount = ""; } SetProperty(ref rechargeAmount1, value); } }

        private bool rechargeButton1;

        public bool rechargeButton { get => rechargeButton1; set => SetProperty(ref rechargeButton1, value); }

        private string rechargeerrorTxt;

        public string RechargeerrorTxt { get => rechargeerrorTxt; set { rechargeerrorTxt = value;  SetProperty(ref rechargeerrorTxt, value); } }

        private bool rechargeButtonVisibility1;

        public bool rechargeButtonVisibility { get => rechargeButtonVisibility1; set => SetProperty(ref rechargeButtonVisibility1, value); }

        private string totalRechargeCoin1;

        public string totalRechargeCoin { get => totalRechargeCoin1; set => SetProperty(ref totalRechargeCoin1, value); }

        private string totalAmount1;

        public string totalAmount { get => totalAmount1; set => SetProperty(ref totalAmount1, value); }

        private string addedCoinamount1;

        public string addedCoinamount { get => addedCoinamount1; set => SetProperty(ref addedCoinamount1, value); }

        private bool regMsgVisiblity1;

        public bool regMsgVisiblity { get => regMsgVisiblity1; set => SetProperty(ref regMsgVisiblity1, value); }

        private ImageSource promsgImgSrc1;

        public ImageSource promsgImgSrc { get => promsgImgSrc1; set => SetProperty(ref promsgImgSrc1, value); }

        private string proMsgText1;

        public string proMsgText { get => proMsgText1; set => SetProperty(ref proMsgText1, value); }

        private Command popOutRegMsgVisiblility1;

        public ICommand popOutRegMsgVisiblility
        {
            get
            {
                if (popOutRegMsgVisiblility1 == null)
                {
                    popOutRegMsgVisiblility1 = new Command(PerformpopOutRegMsgVisiblility);
                }

                return popOutRegMsgVisiblility1;
            }
        }

        private bool paymentGifGrid1;

        public bool paymentGifGrid { get => paymentGifGrid1; set => SetProperty(ref paymentGifGrid1, value); }

        private string sucPaymentText;

        public string SucPaymentText { get => sucPaymentText; set => SetProperty(ref sucPaymentText, value); }

        private Command popOUTpaymentGif1;

        public ICommand popOUTpaymentGif
        {
            get
            {
                if (popOUTpaymentGif1 == null)
                {
                    popOUTpaymentGif1 = new Command(PerformpopOUTpaymentGif);
                }

                return popOUTpaymentGif1;
            }
        }

        private bool isLoading1;

        public bool isLoading { get => isLoading1; set => SetProperty(ref isLoading1, value); }

        private string freeminInaddCoinScreen1;

        public string freeminInaddCoinScreen { get => freeminInaddCoinScreen1; set => SetProperty(ref freeminInaddCoinScreen1, value); }

        private string freeMinText1;

        public string freeMinText { get => freeMinText1; set => SetProperty(ref freeMinText1, value); }












        #endregion
    }
}
