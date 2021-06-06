﻿using System;
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
            NavigationPage.SetHasNavigationBar(this, false);
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            cpimg.Opacity = .3;
            rclbl.TextColor = Color.FromHex("#C9C9C9");
            coingrid.IsVisible = true;
            coingrid.TranslationX = width;
            coingrid.Opacity = 0;
            //var displayheight = DeviceDisplay.MainDisplayInfo.Height;
            //colorFrame.TranslationY = -(displayheight - displayheight / (9.2 / 3.2));
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            coingrid.IsVisible = true;           
            ttlbl.TextColor = Color.FromHex("#C9C9C9");
            rclbl.TextColor = Color.Black;
            cpimg.Opacity = 1;
            ttimg.Opacity = .3;
            coingrid.FadeTo(1,1500, Easing.CubicOut);
            await coingrid.TranslateTo(0, 0, 1000, Easing.CubicOut);
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;    
            
            ttlbl.TextColor = Color.Black;
            ttimg.Opacity = 1;
            cpimg.Opacity = .3;           
            rclbl.TextColor = Color.FromHex("#C9C9C9");
            coingrid.FadeTo(0, 500, Easing.SinIn);
            coingrid.Opacity = 0;
            await coingrid.TranslateTo(width, 0, 1000, Easing.SinIn);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {            
            Application.Current.MainPage.Navigation.PushAsync(new StudentProfile());
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {

            loginView.TranslateTo(0, 0, 1500, Easing.CubicOut);
            loginView.FadeTo(1, 1000, Easing.CubicOut);
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            loginView.TranslateTo(0, -1000, 1500, Easing.CubicIn);
            loginView.FadeTo(0, 1200, Easing.CubicIn);
            loginView.Opacity = 0;
        }
    }
}