using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Phoneword
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrackingPage : ContentPage
	{
        private AccelerationReader ar;

		public TrackingPage ()
		{
			InitializeComponent ();
            ar = new AccelerationReader();
            ar.ToggleAccelerometer();
            AccelDB db = App.Database;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            float accelY = data.Acceleration.Y;
            AccelDisplay.Text = accelY.ToString("00.00");
            if (Math.Abs(accelY) > 0.100)
            {
                AccelerationDataPoint point = new AccelerationDataPoint();
                point.time = new DateTime();
                point.accelY = accelY;
                db.SaveItemAsync(point);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Accelerometer.Start(SensorSpeed.UI);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.Stop();

        }

    }
}