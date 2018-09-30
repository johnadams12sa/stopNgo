using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Phoneword
{
    public partial class App : Application
    {   
        static AccelDB db;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static AccelDB Database
        {
            get
            {
                if (db == null)
                {
                    db = new AccelDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AccelSQLite.db3"));
                }
                return db;
            }
        }

    }
}
