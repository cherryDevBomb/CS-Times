using Plugin.Connectivity;
using SetUp.Model;
using SetUp.View;
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

            if (CrossConnectivity.Current.IsConnected)
            {
                //if (!StudentInfoModel.GetLoginInfo())
                //    MainPage = new LoginPage();
                //else
                //    MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));

                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new ErrorPage(); 
            }

            //StudentInfoModel.YearFormation = "IG2";
            //StudentInfoModel.Group = "721";
            //StudentInfoModel.Subgroup = "/2";
            //MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
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
