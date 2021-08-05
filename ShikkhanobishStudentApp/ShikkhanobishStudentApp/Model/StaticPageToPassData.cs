using System;
using System.Collections.Generic;
using System.Text;

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
        public static string lastTuitionHistoryID { get; set; }
        public static int lastRate { get; set; }
        public static int lastTeacherID { get; set; }
        public static int reportIndex { get; set; }
        public static string reportDes { get; set; }
        public static PerMinPassModel perMinCall { get; set; }
        public static StudentPaymentHistory thispayment { get; set; }
        public static favouriteTeacher selectedPopupFavTeacher { get; set; }
        public static int GenarateNewID()
        {
            Random rnd = new Random();
            int newID = rnd.Next(10000000, 99999999);
            return newID;
        }
    }
}
