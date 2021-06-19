using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : ContentPage
    {
        public StudentProfile()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            List<int> a = new List<int>();
            a.Add(0);
            a.Add(1);
            a.Add(2);
            a.Add(3);
            a.Add(4);
            a.Add(5);
            hislist.ItemsSource = a;
        }
    }
}