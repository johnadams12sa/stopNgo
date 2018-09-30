using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Phoneword
{
    class AccelerationReader
    { 
        SensorSpeed speed = SensorSpeed.UI;
        public AccelerationReader()
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            if (Math.Abs(data.Acceleration.Y) > 0.02)
            {
                //TODO: map to database
                
            }
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed); 
            } catch (FeatureNotSupportedException fns) {
                //lmao
            } catch (Exception ex) {
                //lmao 2 the electric bungaloo
            }

        }
    }
}
