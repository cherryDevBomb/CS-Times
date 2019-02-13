using SetUp.Model;
using System;
using System.IO;
using Xamarin.Forms;

namespace SetUp.View
{
    class EditClassPage : ContentPage
    {
        ClassModel Chosen { get; set; }

        public EditClassPage(ClassModel chosenClass)
        {
            Chosen = chosenClass;
            Padding = new Thickness(0, 8);
            var layout = new StackLayout();

            var exitEdit = new ToolbarItem
            {
                Text = "Cancel",
                Priority = 0
            };
            exitEdit.Clicked += OnExitEditClicked;
            ToolbarItems.Add(exitEdit);

            String text = "";
            if (chosenClass.TypeOfClass == "Curs")
                text = "There are no similar classes";
            else
                text = "Choose from other groups' classes";

            Frame f1 = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["cursColor"],
                BorderColor = (Color)Application.Current.Resources["cursColor"],
                CornerRadius = 4,
                Margin = new Thickness(16, 8),
                Content = new Label
                {
                    Text = text,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    Margin = 0,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                }
            };


            var msg = new Label
            {
                Text = "Custom edit your class",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new CustomEdit(chosenClass));
            };
            msg.GestureRecognizers.Add(tgr);

            Frame f2 = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["seminarColor"],
                BorderColor = (Color)Application.Current.Resources["seminarColor"],
                CornerRadius = 4,
                Margin = new Thickness(16, 8),
                Content = msg,
            };


            var msg2 = new Label
            {
                Text = "Remove this class",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            var tgr2 = new TapGestureRecognizer();
            tgr2.Tapped += OnAlertYesNoClicked;
            msg2.GestureRecognizers.Add(tgr2);

            Frame f3 = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["labColor"],
                BorderColor = (Color)Application.Current.Resources["labColor"],
                CornerRadius = 4,
                Margin = new Thickness(16, 8),
                Content = msg2,
            };

            layout.Children.Add(f1);

            //get all equivalent classes
            foreach (ClassModel c in StudentInfoModel.SortedClasses[chosenClass.ClassName][chosenClass.TypeOfClass])
            {
                if (!c.Equals(chosenClass))
                {
                    var classView = new DetailedClassView(c);

                    var tgr3 = new TapGestureRecognizer();
                    tgr3.Tapped += (s, e) =>
                    {
                        WriteToFile(c);
                    };
                    classView.GestureRecognizers.Add(tgr3);

                    layout.Children.Add(classView);
                }
            }

            layout.Children.Add(f2);
            layout.Children.Add(f3);

            var view = new ScrollView()
            {
                Content = layout
            };
            Content = view;
        }

        async void OnExitEditClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void WriteToFile(ClassModel c)
        {
            String filename = "ReplacedClasses" + StudentInfoModel.Group + StudentInfoModel.Subgroup[1] + ".txt";
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);

            using (var writer = new StreamWriter(filepath))
            {
                writer.WriteLine(c.ToString());
            }
            Navigation.PopModalAsync();
            Application.Current.MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
        }

        void WriteToRemovedFile(ClassModel c)
        {
            String filename = "RemovedClasses" + StudentInfoModel.Group + StudentInfoModel.Subgroup[1] + ".txt";
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);

            using (var writer = new StreamWriter(filepath))
            {
                writer.WriteLine(c.ToString());
            }
            Navigation.PopModalAsync();
            Application.Current.MainPage = new ScheduleNavigationPage(new ScheduleView(StudentInfoModel.YearFormation, StudentInfoModel.Group, StudentInfoModel.Subgroup));
        }

        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("", "Do you really want to remove this class?", "Yes", "No");
            if (answer)
            {
                WriteToFile(Chosen);
            }
        }
    }
}
