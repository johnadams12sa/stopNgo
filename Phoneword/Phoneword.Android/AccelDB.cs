using Android.OS;
using System.IO;

namespace Phoneword.Droid
{
    public interface AccelDB
    {
        void Add(float accelerationX, float accelerationY, float accelerationZ);
    }
}