using SetUp.Model;
using SetUp.Repository;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SetUp.View
{
    class ScheduleView : TabbedPage
    {
        
        public ScheduleModel ScheduleObj { get; set; }


        //constructor that creates Schedule of current week
        public ScheduleView(String formation, String group, String subgroup)
        {

            int weekNr = TimeManager.WeekNr;
            int academicWeekNr = TimeManager.GetAcademicWeekNr(weekNr);

            int dayNr = GetDayIndex();

            if (dayNr == 5 || dayNr == -1) //weekend
            {
                dayNr = 0;
            }

            List<DateTime> dates = TimeManager.GetDates(weekNr);

            ScheduleObj = ScheduleConstructor.GetSchedule(formation, group, subgroup, academicWeekNr);
            BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"];

            //test if not free day
            bool free = false;
            if (academicWeekNr == -1)
                free = true;

            int i = 0;
            foreach (DayModel day in ScheduleObj.Days)
                Children.Add(new DayView(day, dates[i++], free));
            
            CurrentPage = Children[dayNr];
        }


        //constructor that creates schedule of week specified by parameter
        public ScheduleView(String formation, String group, String subgroup, int weekNr)
        {
            int dayNr = GetDayIndex();
            int academicWeekNr = TimeManager.GetAcademicWeekNr(weekNr);

            List<DateTime> dates = TimeManager.GetDates(weekNr);
            ScheduleObj = ScheduleConstructor.GetSchedule(formation, group, subgroup, academicWeekNr);
            BarBackgroundColor = (Color)Application.Current.Resources["barBackgroundColor"];

            //test if not free day
            bool free = false;
            if (academicWeekNr == -1)
                free = true;

            int i = 0;
            foreach (DayModel day in ScheduleObj.Days)
                Children.Add(new DayView(day, dates[i++], free));            
        }

        private int GetDayIndex()
        {
            DateTime today = DateTime.Now;
            int dayNr = today.DayOfWeek - DayOfWeek.Monday;
            return dayNr;
        }
    }
}
