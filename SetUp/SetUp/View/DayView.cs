using SetUp.Model;
using System;
using Xamarin.Forms;

namespace SetUp.View
{
    class DayView : ContentPage
    {
        public DayModel DayObj { get; set; }

        private String ConvertDate(int intDate)
        {
            String date = intDate.ToString();
            if (date.Length < 2)
                date = "0" + date;
            return date;
        }

        public DayView(DayModel dayModel, DateTime date, bool free)
        {
            DayObj = dayModel;
            Title = DayObj.DayName.Substring(0, 2).ToUpper() + "\n" + ConvertDate(date.Day); 
            Padding = new Thickness(0, 8);
            var layout = new StackLayout();

            if (!free && !TimeManager.IsFreeDay(date))
            {
                //add class views to day view
                foreach (ClassModel clas in DayObj.Classes)
                    layout.Children.Add(new ClassView(clas));
            }
            else
            {
                layout.Children.Add(new Label
                {
                    Text = "Well deserved rest",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 100),
                });
            }

            var view = new ScrollView()
            {
                Content = layout
            };
            Content = view;
        }
    }
}
