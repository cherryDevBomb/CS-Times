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

        public DayView(DayModel dayModel, int date)
        {
            DayObj = dayModel;
            Title = DayObj.DayName.Substring(0, 2).ToUpper() + "\n" + ConvertDate(date); 
            Padding = new Thickness(0, 8);

            var layout = new StackLayout();

            //add class views to day view
            foreach (ClassModel clas in DayObj.Classes)
                layout.Children.Add(new ClassView(clas));

            var view = new ScrollView()
            {
                Content = layout
            };
            Content = view;
        }

    }
}
