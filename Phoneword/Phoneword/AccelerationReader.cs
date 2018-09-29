﻿using System;
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
            Console.WriteLine($"IN REGULAR PROJ: Reading: X: {data.Acceleration.X}, Y: " +
                "{data.Acceleration.Y}, Z: {data.Acceleration.Z}");
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            } catch (FeatureNotSupportedException fns) { }
              catch (Exception ex) { }


        }
    }
}