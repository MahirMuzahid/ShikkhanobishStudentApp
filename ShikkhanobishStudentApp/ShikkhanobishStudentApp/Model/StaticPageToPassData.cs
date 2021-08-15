﻿using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShikkhanobishStudentApp.Model
{
    public class StaticPageToPassData
    {
        public static string RegisteredPhonenNumber;
        public static string thisStPh;
        public static string thisstPass;
        public static string otpcode;
        public static bool isLoginOK;

        public static Student thisStudentInfo;
        public static bool isFromLogin;
        public static bool isInBackground;
        public static string lastTuitionHistoryID { get; set; }
        public static int lastRate { get; set; }
        public static int lastTeacherID { get; set; }
        public static int reportIndex { get; set; }
        public static string reportDes { get; set; }
        public static PerMinPassModel perMinCall { get; set; }
        public static StudentPaymentHistory thispayment { get; set; }
        public static favouriteTeacher selectedPopupFavTeacher { get; set; }
        public static string LastPaymentRequestID {get;set;}
        public static int GenarateNewID()
        {
            Random rnd = new Random();
            int newID = rnd.Next(10000000, 99999999);
            return newID;
        }
        public static async Task GetStudent()
        {
            thisStudentInfo = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getStudentWithID".PostUrlEncodedAsync(new { studentID = thisStudentInfo.studentID })
  .ReceiveJson<Student>();
        }
        public static async Task OnStart()
        {
            isInBackground = false;
        }
        public static async Task OnPause()
        {
            isInBackground = true;
        }
    }
}
