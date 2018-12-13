using SetUp.Model;
using SetUp.Repository;
using System;
using Xamarin.Forms;

namespace SetUp.View
{
    public class ScheduleNavigationPage : NavigationPage
    {
        private int CurrentWeek = ScheduleConstructor.GetCurrentWeekNumber();

        public ScheduleNavigationPage(Page p) : base(p)
        {
            BarBackgroundColor = Color.FromHex("#17252A");
            AddNextButton();
            AddSecondaryMenuItems();

            int dayNr = (int)DateTime.Now.DayOfWeek;
            if (dayNr == 0 || dayNr == 6)
                CurrentWeek++;
        }

        private void AddSecondaryMenuItems()
        {
            var editOption = new ToolbarItem
            {
                Text = "Edit classes",
                Order = ToolbarItemOrder.Secondary
            };
            editOption.Clicked += OnEditClicked;
            var logOutOption = new ToolbarItem
            {
                Text = "Log out",
                Order = ToolbarItemOrder.Secondary
            };
            logOutOption.Clicked += OnLogoutClicked;
            ToolbarItems.Add(editOption);
            ToolbarItems.Add(logOutOption);
        }

        private void AddNextButton()
        {
            var nextLabel = new ToolbarItem
            {
                Text = "Saptamana  " + CurrentWeek,
                Priority = 0
            };
            var nextPageBtn = new ToolbarItem
            {
                Icon = Device.RuntimePlatform == Device.Android ? "SetUp.Android/Resources/drawable-hdpi/baseline_hdr_weak_white_48.png"
                                                                : "SetUp.iOS/Resources/drawable/baseline_hdr_strong_white_48.png",
                Priority = 1,
            };

            nextPageBtn.Clicked += OnNextPageButtonClicked;
            ToolbarItems.Add(nextPageBtn);
            ToolbarItems.Add(nextLabel);
        }

        private void AddPrevButton()
        {
            int nextWeekNr = CurrentWeek + 1;
            var prevLabel = new ToolbarItem
            {
                Text = "Saptamana  " + nextWeekNr,
                Priority = 0
            };
            var prevPageBtn = new ToolbarItem
            {
                Icon = Device.RuntimePlatform == Device.Android ? "SetUp.Android/Resources/drawable-hdpi/baseline_hdr_strong_white_48.png" 
                                                                : "SetUp.iOS/Resources/drawable/baseline_hdr_strong_white_48.png",
                Priority = 1
            };
            prevPageBtn.Clicked += OnPrevPageButtonClicked;
            ToolbarItems.Add(prevPageBtn);
            ToolbarItems.Add(prevLabel);
        }

        async void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup, CurrentWeek + 1));
            ToolbarItems.Clear();
            AddPrevButton();
            AddSecondaryMenuItems();
        }

        async void OnPrevPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            ToolbarItems.Clear();
            AddNextButton();
            AddSecondaryMenuItems();
        }

        async void OnEditClicked(object sender, EventArgs e)
        {
            var updateSchedulePage = new NavigationPage(new ClassTitleOptionsPage())
            {
                BarBackgroundColor = Color.FromHex("#17252A")
            };
            await Navigation.PushModalAsync(updateSchedulePage);

        }

        async void OnLogoutClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Are you sure you want to log out?", "", "Yes", "No");
            if (confirm)
                Application.Current.MainPage = new LoginPage();
        }
    }
}
