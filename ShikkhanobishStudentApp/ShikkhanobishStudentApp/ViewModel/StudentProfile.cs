using ShikkhanobishStudentApp.Model;
using ShikkhanobishStudentApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishStudentApp.ViewModel
{
    public class StudentProfile: BaseViewModel, INotifyPropertyChanged
    {
        #region Methods
        public ICommand closeWindow =>
            new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
            });
        #endregion

        #region Binding
        #endregion
    }
}
