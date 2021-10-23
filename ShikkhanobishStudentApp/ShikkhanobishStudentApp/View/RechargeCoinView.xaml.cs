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
	public partial class RechargeCoinView : ContentPage
	{
		public RechargeCoinView ()
        {
			InitializeComponent ();
		}
		private void MaterialButton_Clicked_1(object sender, EventArgs e)
		{
            //refer
            Application.Current.MainPage.Navigation.PushAsync(new ReferralView());
            //Navigation.PushAsync(new CustomTransitionNavPage(new ReferralView()));
        }
	}
}