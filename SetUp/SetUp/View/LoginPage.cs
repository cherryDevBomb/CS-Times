using SetUp.Model;
using SetUp.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SetUp.View
{
    class LoginPage : ContentPage
    {
        private Picker FormationPicker;
        private Picker YearPicker;
        private Picker GroupPicker;
        private Picker SubgroupPicker;
        private Button Login;

        private Dictionary<String, String> GetDictOfCodes()
        {
            Dictionary<String, String> dict = new Dictionary<string, string>
            {
                { "Matematica - linia de studiu romana", "M" },
                { "Informatica - linia de studiu romana", "I" },
                { "Matematica informatica - linia de studiu romana", "MI" },
                { "Matematica informatica - linia de studiu engleza", "MIE" },
                { "Matematica - linia de studiu maghiara", "MM" },
                { "Informatica - linia de studiu maghiara", "IM" },
                { "Matematica informatica - linia de studiu maghiara", "MIM" },
                { "Informatica - in limba engleza", "IE" },
                { "Informatica - in limba germana", "IG" }
            };
            return dict;
        }

        private Dictionary<String, List<String>> GetDictOfYears()
        {
            Dictionary<String, List<String>> dict = new Dictionary<String, List<String>>
            {
                { "Matematica - linia de studiu romana", new List<string> {"1", "2", "3" } },
                { "Informatica - linia de studiu romana", new List<string> {"1", "2", "3" } },
                { "Matematica informatica - linia de studiu romana", new List<string> {"1", "2", "3" } },
                { "Matematica informatica - linia de studiu engleza", new List<string> {"1", "2" } },
                { "Matematica - linia de studiu maghiara", new List<string> {"1", "2", "3" } },
                { "Informatica - linia de studiu maghiara", new List<string> {"1", "2", "3" } },
                { "Matematica informatica - linia de studiu maghiara", new List<string> {"1", "2", "3" } },
                { "Informatica - in limba engleza", new List<string> {"1", "2", "3" } },
                { "Informatica - in limba germana", new List<string> {"1", "2", "3" } }
            };
            return dict;
        }

        public LoginPage()
        {
            Dictionary<String, String> dict = GetDictOfCodes();

            var formations = new List<String>();
            foreach (String key in dict.Keys)
                formations.Add(key);
            FormationPicker = new Picker
            {
                Title = "Specializarea",
                ItemsSource = formations,
                Margin = new Thickness(0, 50, 0, 0),
                TextColor = (Color)Application.Current.Resources["seminarColor"],
            };
            FormationPicker.SelectedIndexChanged += OnFormationsPickerSelectedIndexChanged;

            YearPicker = new Picker
            {
                Title = "Anul de studiu",
                TextColor = (Color)Application.Current.Resources["seminarColor"],
                IsVisible = false
            };
            YearPicker.SelectedIndexChanged += OnYearPickerSelectedIndexChanged;

            GroupPicker = new Picker()
            {
                Title = "Grupa",
                TextColor = (Color)Application.Current.Resources["seminarColor"],
                IsVisible = false

            };
            GroupPicker.SelectedIndexChanged += OnGroupPickerSelectedIndexChanged;

            SubgroupPicker = new Picker()
            {
                Title = "Subgrupa",
                ItemsSource = new List<String>() { "/1", "/2" },
                TextColor = (Color)Application.Current.Resources["seminarColor"],
                IsVisible = false
            };
            SubgroupPicker.SelectedIndexChanged += OnSubgroupPickerSelectedIndexChanged;

            Image logo = new Image
            {
                Source = Device.RuntimePlatform == Device.Android ? "SetUp.Android/Resources/drawable/logo.png" : "SetUp.iOS/Resources/drawable/logo.png",
            };

            Login = new Button
            {
                Text = "Submit",
                Margin = new Thickness(0, 30, 0, 0),
                BackgroundColor = (Color)Application.Current.Resources["cursColor"],
                BorderColor = (Color)Application.Current.Resources["cursColor"],
                BorderWidth = 2,
                CornerRadius = 4,
                IsVisible = false
            };
            Login.Clicked += OnLoginButtonClicked;

            Padding = new Thickness(40, 40);
            var layout = new StackLayout();
            layout.Children.Add(logo);
            layout.Children.Add(FormationPicker);
            layout.Children.Add(YearPicker);
            layout.Children.Add(GroupPicker);
            layout.Children.Add(SubgroupPicker);
            layout.Children.Add(Login);
            Content = layout;
        }

        void OnFormationsPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            GroupPicker.SelectedIndex = -1;
            SubgroupPicker.SelectedIndex = -1;
            GroupPicker.IsVisible = false;
            SubgroupPicker.IsVisible = false;
            Login.IsVisible = false;

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedFormation = (string)picker.ItemsSource[selectedIndex];

                //set YearPicker ItemsSource according to selected formation
                var years = new List<String>();
                foreach (String year in GetDictOfYears()[selectedFormation])
                {
                    years.Add(year);
                }
                YearPicker.ItemsSource = years;
                YearPicker.IsVisible = true;
                StudentInfoModel.YearFormation = GetDictOfCodes()[selectedFormation];
            }
        }

        void OnYearPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            GroupPicker.SelectedIndex = -1;
            SubgroupPicker.SelectedIndex = -1;
            SubgroupPicker.IsVisible = false;
            Login.IsVisible = false;

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedYear = (string)picker.ItemsSource[selectedIndex];
                String lastYearFormation = StudentInfoModel.YearFormation;
                if (char.IsDigit(lastYearFormation[lastYearFormation.Length - 1]))
                {

                    StringBuilder builder = new StringBuilder(lastYearFormation);
                    builder[lastYearFormation.Length - 1] = selectedYear[0];
                    StudentInfoModel.YearFormation = builder.ToString();
                }
                else
                {
                    StudentInfoModel.YearFormation = StudentInfoModel.YearFormation + selectedYear;
                }

                List<String> extractedGroups = DataExtractor.ExtractGroups(StudentInfoModel.YearFormation);
                List<String> groups = new List<String>();
                foreach (String group in extractedGroups)
                {
                    groups.Add(group);
                }
                GroupPicker.ItemsSource = groups;
                GroupPicker.IsVisible = true;
            }
        }

        void OnGroupPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            SubgroupPicker.SelectedIndex = -1;
            Login.IsVisible = false;

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedGroup = (string)picker.ItemsSource[selectedIndex];
                StudentInfoModel.Group = selectedGroup;
            }
            SubgroupPicker.IsVisible = true;
        }

        void OnSubgroupPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedSubgroup = (string)picker.ItemsSource[selectedIndex];
                StudentInfoModel.Subgroup = selectedSubgroup;
                StudentInfoModel.SaveLoginInfo();
                Login.IsVisible = true;
            }
        }

        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
        }
    }
}
