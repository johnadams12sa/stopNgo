using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Phoneword
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrackingPage : ContentPage
	{
        AccelerationReader ar = new AccelerationReader();
        AccelDB db = App.Database;
		public TrackingPage ()
		{   
			InitializeComponent ();
            ar.ToggleAccelerometer();
            AccelDisplay.Text = ar.accelY.ToString("00.000");
		}
        protected override void OnAppearing()
        {
            float f = ar.accelY;
            AccelDisplay.Text = f.ToString("00.0000");
        }

    }
}