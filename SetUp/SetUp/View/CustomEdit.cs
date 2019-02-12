using SetUp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SetUp.View
{
    class CustomEdit : ContentPage
    {

        private ClassModel ClassMdl { get; set; }
        public CustomEdit(ClassModel c)
        {
            ClassMdl = c;

            Padding = new Thickness(0, 8);
            var layout = new StackLayout()
            {
                Margin = new Thickness(16, 8),
            };

            var exitEdit = new ToolbarItem
            {
                Text = "Cancel",
                Priority = 0
            };
            exitEdit.Clicked += OnExitEditClicked;
            ToolbarItems.Add(exitEdit);

            var item = new DetailedClassView(c);

            List<string> hours = new List<string>();
            for (int i = 8; i <= 20; i++)
            {
                hours.Add(i + ":00");
            }

            var StartTimePicker = new Picker()
            {
                Title = "Start time",
                ItemsSource = hours,
                TextColor = (Color)Application.Current.Resources["seminarColor"],
            };
            StartTimePicker.SelectedIndexChanged += OnStartTimePickerSelectedIndexChanged;

            var DayPicker = new Picker()
            {
                Title = "Day",
                ItemsSource = new List<String>() { "Luni", "Marti", "Miercuri", "Joi", "Vineri" },
                TextColor = (Color)Application.Current.Resources["seminarColor"],
            };
            DayPicker.SelectedIndexChanged += OnDayPickerSelectedIndexChanged;


            var saveButton = new Button
            {
                Text = "Save changes",
                BackgroundColor = (Color)Application.Current.Resources["cursColor"],
                BorderWidth = 2,
                CornerRadius = 4,
                BorderColor = (Color)Application.Current.Resources["cursColor"],
                IsVisible = true,
            };
            saveButton.Clicked += OnSaveButtonClicked;

            layout.Children.Add(StartTimePicker);
            layout.Children.Add(DayPicker);
            layout.Children.Add(saveButton);
            Content = layout;
        }

        async void OnExitEditClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void OnStartTimePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedTime = (string)picker.ItemsSource[selectedIndex];
                String[] elems = selectedTime.Split(':');
                int startTime = Int32.Parse(elems[0]);
                int endTime = startTime + 2;
                ClassMdl.StartTime = new TimeSpan(startTime, 0, 0);
                ClassMdl.EndTime = new TimeSpan(endTime, 0, 0);
            }
        }

        void OnDayPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                String selectedDay = (string)picker.ItemsSource[selectedIndex];
                ClassMdl.Day = selectedDay;
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            WriteToFile();
        }

        void WriteToFile()
        {
            String filename = "CustomEditedClasses" + StudentInfoModel.Group + StudentInfoModel.Subgroup[1] + ".txt";
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);

            using (var writer = new StreamWriter(filepath))
            {
                writer.WriteLine(ClassMdl.ToString());
            }
            Navigation.PopModalAsync();
            Application.Current.MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
        }
    }
}
