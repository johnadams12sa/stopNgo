using SQLite;
using System;

namespace Phoneword
{
    public class AccelerationDataPoint
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime time { get; set; }
        public float accelY { get; set; }
        public int sec { get
            {
                return time.Second;
            } }
    }
}
