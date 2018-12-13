using SetUp.Model;
using SetUp.View.FontIconApp.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SetUp.View
{
    class ClassView : AbsoluteLayout
    {
        private readonly Color CursColor = Color.FromHex("5CDB95");
        private readonly Color SeminarColor = Color.FromHex("3AAFA9");
        private readonly Color LabColor = Color.FromHex("3B945E");
        public Color MyColor { get; set; }
        private ClassModel ClassObj { get; set; }

        public ClassView(ClassModel model)
        {
            ClassObj = model;

            Padding = new Thickness(5, 5);
            SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
            SetLayoutFlags(this, AbsoluteLayoutFlags.All);
            Margin = new Thickness(16, 8, 16, 0);

            String classIconText = "";
            if (model.TypeOfClass == "Curs")
            {
                MyColor = CursColor;
                classIconText = "C";
            }
            else if (model.TypeOfClass == "Seminar")
            {
                MyColor = SeminarColor;
                classIconText = "S";
            }
            if (model.TypeOfClass == "Laborator")
            {
                MyColor = LabColor;
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


            Frame f = new Frame
            {
                BackgroundColor = Color.Transparent,
                BorderColor = MyColor,
                CornerRadius = 4,
            };
            Children.Add(f, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);


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
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };


            //settings icon
            var settingsIcon = new FontAwesomeLabel
            {
                Text = Icon.FAPencil,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = MyColor
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
                Margin = new Thickness(20, 20, 10, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.55, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.35, GridUnitType.Star) }
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            line1.Children.Add(timeIcon, 0, 0);
            line1.Children.Add(time, 1, 0);
            SetLayoutBounds(line1, new Rectangle(0, 0, 1, .3));
            SetLayoutFlags(line1, AbsoluteLayoutFlags.All);


            Grid line2 = new Grid
            {
                Margin = new Thickness(20, 20, 10, 0),
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
            SetLayoutBounds(line2, new Rectangle(0, .3, 1, .3));
            SetLayoutFlags(line2, AbsoluteLayoutFlags.All);


            Grid line3 = new Grid
            {
                Margin = new Thickness(20, 20, 10, 0),
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
            SetLayoutBounds(line3, new Rectangle(0, .65, 1, .35));
            SetLayoutFlags(line3, AbsoluteLayoutFlags.All);


            Children.Add(line1);
            Children.Add(line2);
            Children.Add(line3);

        }

    }
}
