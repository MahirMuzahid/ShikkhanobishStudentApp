using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class StartPageViewModel: BaseViewModel
    {
        public StartPageViewModel()
        {
            getLoggedUser();
        }
        public async Task getLoggedUser()
        {
            var pn = await SecureStorage.GetAsync("phonenumber");
            var pass = await SecureStorage.GetAsync("passowrd");

            if (pn != null && pass != null)
            {
                List<Student> allStudent = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getStudent".GetJsonAsync<List<Student>>();
                for (int i = 0; i < allStudent.Count; i++)
                {
                    if (pn == allStudent[i].phonenumber && pass == allStudent[i].password)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new TakeTuitionView(false));
                        break;
                    }

                }

            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new TakeTuitionView(true));
            }
        }
    }
}
