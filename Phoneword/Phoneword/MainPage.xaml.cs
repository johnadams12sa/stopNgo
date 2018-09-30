using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
    
        public MainPage()
        {
            InitializeComponent();;
        }

        void OnTracking(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrackingPage());
        }

        void History(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }

        void Statistics(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Statistics());
        }

        //void OnTranslate(object sender, EventArgs e)
        //{
        //    translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
        //    if (!string.IsNullOrWhiteSpace(translatedNumber))
        //    {
        //        callButton.IsEnabled = true;
        //        callButton.Text = "Call " + translatedNumber;
        //    }
        //    else
        //    {
        //        callButton.IsEnabled = false;
        //        callButton.Text = "Call";
        //    }
        //}

        //async void OnCall(object sender, EventArgs e)
        //{
        //    if (await this.DisplayAlert(
        //            "Dial a Number",
        //            "Would you like to call " + translatedNumber + "?",
        //            "Yes",
        //            "No"))
        //    {
        //        var dialer = DependencyService.Get<IDialer>();
        //        if (dialer != null)
        //            dialer.Dial(translatedNumber);
        //    }
        //}
    }
}