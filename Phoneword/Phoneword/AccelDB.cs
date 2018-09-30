using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Phoneword
{
    // Class contains the DB connection + ORM methods
    public class AccelDB
    {
        readonly SQLiteAsyncConnection db;

        public AccelDB(string dbPath)
        {

            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<AccelerationDataPoint>().Wait();
        }
    
        public Task<AccelerationDataPoint> GetItemAsync(int id)
        {
            return db.Table<AccelerationDataPoint>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        // accel is y axis acceleration
        public Task<int> SaveItemAsync(AccelerationDataPoint accelY)
        {
            if (accelY.ID != 0)
            {
                return db.UpdateAsync(accelY);
            }
            else
            {
                return db.InsertAsync(accelY);
            }
        }
    }
}