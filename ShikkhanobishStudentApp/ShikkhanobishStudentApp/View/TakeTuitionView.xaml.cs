using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShikkhanobishStudentApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakeTuitionView : ContentPage
    {
        public TakeTuitionView()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;

            coingrid.IsVisible = true;
            coingrid.TranslationX = width;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            coingrid.IsVisible = true;
            await coingrid.TranslateTo(0, 0,1000,Easing.CubicOut);
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            await coingrid.TranslateTo(width, 0, 1000, Easing.SinIn);
        }
    }
}