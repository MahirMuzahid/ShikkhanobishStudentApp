using Flurl.Http;
using ShikkhanobishStudentApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReferralView : ContentPage
    {
        public Student thisStudent = new Student();
        List<ReferralTable> allrefarerral = new List<ReferralTable>();
        ReferralTable thisref = new ReferralTable();
        public ReferralView()
        {
            InitializeComponent();
            GetAllStudent();
        }
        public async Task GetAllStudent()
        {
            submitbtn.IsEnabled = false;
            var allStudent = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getStudent".GetJsonAsync<List<Student>>();
            allrefarerral = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getRefferalTable".GetJsonAsync<List<ReferralTable>>();
            bool isGive = false;
            for (int i = 0; i < allrefarerral.Count; i++)
            {
                if(allrefarerral[i].studentID == StaticPageToPassData.thisStudentInfo.studentID)
                {
                    thisref = allrefarerral[i];
                    rfrCode.Text = allrefarerral[i].referralID;
                    if(allrefarerral[i].referredStudentID == 0)
                    {
                        used.Text = "N/A";                       
                    }
                    else
                    {
                       
                        for(int j = 0; j < allStudent.Count; j++)
                        {
                            if(allStudent[j].studentID == allrefarerral[i].referredStudentID)
                            {
                                used.Text = "" + allStudent[j].name;
                                break;
                            }
                        }
                        
                    }
                    break;
                }
                
                if (allrefarerral[i].referredStudentID == StaticPageToPassData.thisStudentInfo.studentID)
                {
                    isGive = true;                 
                    for (int j = 0; j < allStudent.Count; j++)
                    {
                        if (allStudent[j].studentID == allrefarerral[i].studentID)
                        {
                            used.Text = "" + allStudent[j].name;
                            break;
                        }
                    }
                }

                
            }
            if (!isGive)
            {
                give.Text = "N/A";
                submitbtn.IsEnabled = true;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            ShowPopUp();
        }
        public async Task ShowPopUp()
        {
            await MaterialDialog.Instance.AlertAsync(message: "রেফার কর তোমার বন্ধুকে। তুমি এবং তোমার বন্ধু জিতে নাও ৫ মিনিট ফ্রি টিউশন! তোমার বন্ধুকে শিক্ষানবিশ একাউন্টে রেফার অপশনে গিয়ে উপরের নাম্বারটি \"Enter Referral Code\" এ পেস্ট করতে বল এবং দুইজনেই জিতে নাও ৫ মিনিট করে ফ্রি টিউশন!");
        }
        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            CheckRefferID();
        }
        public async Task CheckRefferID()
        {
            bool ok = true;
            int index = 0;
            if (codetxt.Text != null && codetxt.Text != "")
            {
                for (int i = 0; i < allrefarerral.Count; i++)
                {
                    if (codetxt.Text == allrefarerral[i].referralID.ToString())
                    {
                        if (allrefarerral[i].studentID == StaticPageToPassData.thisStudentInfo.studentID || allrefarerral[i].referredStudentID == StaticPageToPassData.thisStudentInfo.studentID)
                        {
                            ok =  false;
                            index = 1;
                            break;
                        }
                    }
                    if(i == allrefarerral.Count - 1)
                    {
                        index = 2;
                        ok = false;
                    }
                }
            }
            if (ok)
            {
                var res = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getRefferalTable".PostUrlEncodedAsync(new { referredStudentID = thisref.referredStudentID, referralID  = thisref.referralID, studentID  = thisref .studentID}).ReceiveJson<Response>();
            }
            else
            {
                if(index == 1)
                {
                    await MaterialDialog.Instance.AlertAsync(message: "You can not use this referral code!");
                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Code doesn't match!");
                }
                
            }
        }
    }
}