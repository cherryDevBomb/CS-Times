using SetUp.Model;
using SetUp.View.FontIconApp.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SetUp.View
{
    class ClassView : StackLayout
    {
        //private readonly Color CursColor = Color.FromHex("5CDB95");
        //private readonly Color SeminarColor = Color.FromHex("3AAFA9");
        //private readonly Color LabColor = Color.FromHex("3B945E");
        public Color MyColor { get; set; }
        private ClassModel ClassObj { get; set; }

        public ClassView(ClassModel model)
        {
            ClassObj = model;
            
            Padding = new Thickness(5, 5);
            Margin = new Thickness(16, 8, 16, 0);

            String classIconText = "";
            if (model.TypeOfClass == "Curs")
            {
                MyColor = (Color)Application.Current.Resources["cursColor"];
                classIconText = "C";
            }
            else if (model.TypeOfClass == "Seminar")
            {
                MyColor = (Color)Application.Current.Resources["seminarColor"];
                classIconText = "S";
            }
            if (model.TypeOfClass == "Laborator")
            {
                MyColor = (Color)Application.Current.Resources["labColor"];
                classIconText = "L";
            }

            //type of class icon
            Frame classIcon = new Frame
            {
                CornerRadius = 30,
                HeightRequest = 25,
                WidthRequest = 25,
                BorderColor = MyColor,
                BackgroundColor = MyColor,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Margin = 0,
                Padding = 0,
                Content = new Label
                {
                    Text = classIconText,
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                }
            };

            string startTime = ClassObj.StartTime.ToString("c");
            startTime = startTime.Substring(0, startTime.Length - 3);
            string endTime = ClassObj.EndTime.ToString("c");
            endTime = endTime.Substring(0, endTime.Length - 3);
            string timeFormat = (startTime + " - " + endTime);


            //time icon
            var timeIcon = new FontAwesomeLabel
            {
                Text = Icon.FAClockO,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = MyColor
            };


            //time value
            var time = new Label
            {
                Text = timeFormat,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };


            //class title
            var className = new Label
            {
                Text = ClassObj.ClassName,
                TextColor = MyColor,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            //location icon
            var locationIcon = new FontAwesomeLabel
            {
                Text = Icon.FALocationArrow,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = MyColor
            };


            //location value
            var room = new Label
            {
                Text = ClassObj.Room,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };


            //teacher icon
            var teacherIcon = new FontAwesomeLabel
            {
                Text = Icon.FAUser,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = MyColor
            };


            //teacher
            var teacher = new Label
            {
                Text = ClassObj.Teacher,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };



            Grid line1 = new Grid
            {
                //Margin = new Thickness(20, 20, 10, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.50, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.40, GridUnitType.Star) }
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            line1.Children.Add(timeIcon, 0, 0);
            line1.Children.Add(time, 1, 0);


            Grid line2 = new Grid
            {
                //Margin = new Thickness(20, 0, 10, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.9, GridUnitType.Star) },
                },
                RowSpacing = 0,
                ColumnSpacing = 0
            };

            line2.Children.Add(classIcon, 0, 0);
            line2.Children.Add(className, 1, 0);


            Grid line3 = new Grid
            {
                //Margin = new Thickness(20, 0, 10, 10),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.05, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.55, GridUnitType.Star) }
                }
            };
            line3.Children.Add(locationIcon, 0, 0);
            line3.Children.Add(room, 1, 0);
            line3.Children.Add(teacherIcon, 2, 0);
            line3.Children.Add(teacher, 3, 0);


            Grid lines = new Grid
            {
                RowSpacing = 10,
                RowDefinitions =
                {
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                }
            };
            lines.Children.Add(line1, 0, 0);
            lines.Children.Add(line2, 0, 1);
            lines.Children.Add(line3, 0, 2);

            Frame f = new Frame
            {
                Content = lines,
                BackgroundColor = Color.Transparent,
                BorderColor = MyColor,
                CornerRadius = 4,
            };
            Children.Add(f);
        }
    }
}
