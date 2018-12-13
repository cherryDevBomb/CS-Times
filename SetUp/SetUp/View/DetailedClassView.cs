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

            Grid line = new Grid
            {
                Margin = new Thickness(20, 20, 10, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) }
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            line.Children.Add(group, 0, 0);
            SetLayoutBounds(line, new Rectangle(0, 0, 1, .25));
            SetLayoutFlags(line, AbsoluteLayoutFlags.All);

            SetLayoutBounds(Children[1], new Rectangle(0, 0.25, 1, 0.25));
            SetLayoutBounds(Children[2], new Rectangle(0, 0.5, 1, 0.25));
            SetLayoutBounds(Children[3], new Rectangle(0, 0.75, 1, 0.25));

            ((Grid)Children[1]).Children.Add(day, 2, 0);

            Children.Add(line);
        }
    }
}
