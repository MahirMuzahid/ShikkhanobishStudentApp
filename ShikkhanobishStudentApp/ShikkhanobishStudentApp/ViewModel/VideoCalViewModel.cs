using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class VideoCalViewModel: BaseViewModel, INotifyPropertyChanged
    {

        #region Methods
        bool TimerContinue;
        int timerSecCounter,timerMinCounter, totalCostCount;
        bool isSafeTiemAvailable;
        public VideoCalViewModel()
        {
            TimerContinue = true;
            timeColor = Color.LightSeaGreen;
            timerSecCounter = 10;
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
                        timeColor = Color.White;
                        isSafeTiemAvailable = false;
                    }
                }
                else
                {
                    if(timerSecCounter == 59)
                    {
                        SendApiCall();
                        timerSecCounter = -1;
                        timerMinCounter++;
                    }
                    timerSecCounter++;
                    time = timerMinCounter + " : " + timerSecCounter + "";
                }
                
                return TimerContinue;
            });
        }
        public async Task SendApiCall()
        {
            
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
        #endregion
    }
}
