using SetUp.Model;
using System;
using Xamarin.Forms;

namespace SetUp.View
{
    class DetailedClassView : ClassView
    {
        public DetailedClassView(ClassModel c) : base(c)
        {
            //group
            var group = new Label
            {
                Text = c.TargetGroup,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            String timeSpecification = c.Day;
            if (c.WhichWeek != "")
            {
                timeSpecification = timeSpecification + " / sapt." + c.WhichWeek;
            } 

            //day
            var day = new Label
            {
                Text = timeSpecification,
                Margin = 0,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };

            Grid line0 = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) }
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            line0.Children.Add(group, 0, 0);

            Frame parent = (Frame)Children[0];
            Grid child = (Grid)(parent).Content;
            ((Grid)child.Children[0]).Children.Add(day, 2, 0);
            Grid line1 = (Grid)child.Children[0];
            Grid line2 = (Grid)child.Children[1];
            Grid line3 = (Grid)child.Children[2];


            Grid lines = new Grid
            {
                RowSpacing = 10,
                RowDefinitions =
                {
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                }
            };
            lines.Children.Add(line0, 0, 0);
            lines.Children.Add(line1, 0, 1);
            lines.Children.Add(line2, 0, 2);
            lines.Children.Add(line3, 0, 3);

            parent.Content = lines;

            //Frame f = new Frame
            //{
            //    Content = lines,
            //    BackgroundColor = Color.Transparent,
            //    BorderColor = MyColor,
            //    CornerRadius = 4,
            //};

            
            //child.RowDefinitions.Insert(0, new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
            
            //((Frame)Children[0]).Content
            //((Grid)Children[1]).Children.Add(day, 2, 0);

            //Children.Add(line);
        }
    }
}
