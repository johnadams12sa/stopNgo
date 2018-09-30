using System;
using System.Collections.Generic;
using System.Text;

namespace Phoneword
{
    public class AccelEvent
    {
        public double time { get; set; }
        public double accelY { get; set; }

        public AccelEvent(double time, double accel)
        {
            this.time = time;
            accelY = accel;
        }
    }
}
