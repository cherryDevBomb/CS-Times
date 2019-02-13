using Android;
using Android.Content.PM;
using Plugin.Connectivity;
using SetUp.Model;
using SetUp.View;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SetUp
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();
            TimeManager.SetCurrentTimes();

            if (CrossConnectivity.Current.IsConnected)
            {
                if (!StudentInfoModel.GetLoginInfo())
                    MainPage = new LoginPage();
                else
                    MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
            }
            else
            {
                MainPage = new ErrorPage();
            } 
        }


        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

        protected override void OnResume ()
		{
			// Handle when your app resumes
		}
    };
}
