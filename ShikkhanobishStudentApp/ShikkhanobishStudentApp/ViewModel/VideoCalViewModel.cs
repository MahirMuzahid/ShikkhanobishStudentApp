
using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Vonage;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class VideoCalViewModel: BaseViewModel, INotifyPropertyChanged
    {

        #region Methods
        bool TimerContinue;
        int timerSecCounter,timerMinCounter, totalCostCount;
        bool isSafeTiemAvailable;
        bool isLastMin;
        bool isBalanceOver;
        RealTimeApiMethods realtimeapi = new RealTimeApiMethods();
        CostClass Allcost = new CostClass();

        public VideoCalViewModel()
        {
            isBalanceOver = false;
            isLastMin = false;
            TimerContinue = true;
            timeColor = Color.LightSeaGreen;
            timerSecCounter = 15;
            timerMinCounter = 0;
            totalCostCount = 0;
            totaolCost = totalCostCount + "";
            isSafeTiemAvailable = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if(isSafeTiemAvailable)
                {
                    timerSecCounter--;
                    time = timerMinCounter + " : " + timerSecCounter+"";
                    if(timerSecCounter == 0 & timerMinCounter == 0 && isSafeTiemAvailable)
                    {
                        SendApiCall();
                        timeColor = Color.White;
                        isSafeTiemAvailable = false;
                    }
                }
                else
                {
                    if(timerSecCounter == 59)
                    {                      
                        timerSecCounter = -1;
                        timerMinCounter++;
                        SendApiCall();
                    }
                    timerSecCounter++;
                    time = timerMinCounter + " : " + timerSecCounter + "";
                }
                
                return TimerContinue;
            });
            
        }
        public async Task GetAllCost()
        {
            Allcost = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetCost".GetJsonAsync<CostClass>();
        }
        public async Task SendApiCall()
        {
            if(timerMinCounter == 0)
            {
                await GetAllCost();
            }
            if (!isBalanceOver)
            {
                int cost = 0;
                PerMinPassModel perminCall = StaticPageToPassData.perMinCall;
                int time = timerMinCounter + 1;
                if (perminCall.firstChoiceID == "101")
                {
                    totalCostCount = totalCostCount + Allcost.SchoolCost;
                    cost = Allcost.SchoolCost;
                    totaolCost = totalCostCount + "";
                }
                if (perminCall.firstChoiceID == "102")
                {
                    totalCostCount = totalCostCount + Allcost.CollegeCost;
                    cost = Allcost.CollegeCost;
                    totaolCost = totalCostCount + "";
                }
                StaticPageToPassData.lastTuitionHistoryID = perminCall.sessionID;
                StaticPageToPassData.lastTeacherID = perminCall.teacherID;
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/PerMinPassCall".PostUrlEncodedAsync(new
                {
                    studentID = perminCall.studentID,
                    teacherID = perminCall.teacherID,
                    time = time,
                    sessionID = perminCall.sessionID,
                    firstChoiceID = perminCall.firstChoiceID,
                    secondChoiceID = perminCall.secondChoiceID,
                    thirdChoiceID = perminCall.thirdChoiceID,
                    forthChoiceID = perminCall.forthChoiceID,
                    firstChoiceName = perminCall.firstChoiceName,
                    secondChoiceName = perminCall.secondChoiceName,
                    thirdChoiceName = perminCall.thirdChoiceName,
                    forthChoiceName = perminCall.forthChoiceName
                })
         .ReceiveJson<PerMinCallResponse>();
                string SendTimeAndCostInfo = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/SendTimeAndCostInfo?&teacherID=" + perminCall.teacherID + "&studentID=" + perminCall.studentID + "&time=" + time + "&earned=" + res.earned;
                await realtimeapi.ExecuteRealTimeApi(SendTimeAndCostInfo);
                if (isLastMin)
                {
                    var result = await MaterialDialog.Instance.ConfirmAsync(message: "You do not have enough balance to continue after 1 minuite. Call will cut autometicly after 1 minuite",
                                      confirmingText: "Ok");
                    string lastMinCall = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/LastMinAlert?&teacherID=" + perminCall.teacherID + "&studentID=" + perminCall.studentID + "&isLastMin=" + true;
                    await realtimeapi.ExecuteRealTimeApi(lastMinCall);
                }
                var student = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getStudentWithID".PostUrlEncodedAsync(new { studentID = StaticPageToPassData.thisStudentInfo.studentID })
    .ReceiveJson<Student>();
                if (student.freemin == 0 && student.coin < cost)
                {
                    isBalanceOver = true;
                }
                if (student.freemin == 0 && student.coin < cost * 2)
                {
                    isLastMin = true;
                }
            }
            else
            {
                using (var dialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Insufficient Balance To Continue Call..."))
                {
                    Task.Delay(1000);
                    Application.Current.MainPage.Navigation.PushModalAsync(new RattingPageView());
                    string cutUrlCall = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/CutVideoCall?&teacherID=" + StaticPageToPassData.lastTeacherID + "&studentID=" + StaticPageToPassData.thisStudentInfo.studentID + "&isCut=" + true;
                    await realtimeapi.ExecuteRealTimeApi(cutUrlCall);
                    CrossVonage.Current.EndSession();                   
                }
                   
            }

           

        }
        public async Task EndOrBackBtn()
        {
            var result = await MaterialDialog.Instance.ConfirmAsync(message: "Do you want to cut the call?",
                                    confirmingText: "Yes",
                                    dismissiveText: "No");           
            if (result == true)
            {
                string cutUrlCall = "https://shikkhanobishrealtimeapi.shikkhanobish.com/api/ShikkhanobishSignalR/CutVideoCall?&teacherID=" + StaticPageToPassData.lastTeacherID + "&studentID=" + StaticPageToPassData.thisStudentInfo.studentID + "&isCut=" + true;
                realtimeapi.ExecuteRealTimeApi(cutUrlCall);
                TimerContinue = false;
                CrossVonage.Current.EndSession();
                //CrossVonage.Current.MessageReceived -= OnMessageReceived;
               Application.Current.MainPage.Navigation.PushModalAsync(new RattingPageView());
            }


        }
        private void PerformEndCall()
        {
            EndOrBackBtn();
        }
        public ICommand goRattingPage =>
            new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushModalAsync(new RattingPageView());
            });


        #endregion

        #region Binding
        private string time1;

        public string time { get => time1; set => SetProperty(ref time1, value); }

        private string totaolCost1;

        public string totaolCost { get => totaolCost1; set => SetProperty(ref totaolCost1, value); }

        private Color timeColor1;

        public Color timeColor { get => timeColor1; set => SetProperty(ref timeColor1, value); }

        private Command endCall;

        public ICommand EndCall
        {
            get
            {
                if (endCall == null)
                {
                    endCall = new Command(PerformEndCall);
                }

                return endCall;
            }
        }

        
        #endregion
    }
}
