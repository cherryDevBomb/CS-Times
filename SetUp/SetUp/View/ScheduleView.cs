using SetUp.Model;
using SetUp.Repository;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SetUp.View
{
    class ScheduleView : TabbedPage
    {
        private readonly Color BackgroundClr = Color.FromHex("#17252A");
        public ScheduleModel ScheduleObj { get; set; }
        private int WeekNumber { get; set; }


        //constructor that creates Schedule of current week
        public ScheduleView(String formation, String group, String subgroup)
        {
            int weekNr = ScheduleConstructor.GetCurrentWeekNumber();

            int dayNr = GetDayIndex();
            if (dayNr > 4)
            {
                dayNr = 0;
                weekNr++;
            }
            WeekNumber = weekNr;

            List<int> dates = GetDateOfMonday(weekNr);
            ScheduleObj = ScheduleConstructor.GetSchedule(formation, group, subgroup, weekNr);
            BarBackgroundColor = BackgroundClr;
            int i = 0;
            foreach (DayModel day in ScheduleObj.Days)
                Children.Add(new DayView(day, dates[i++]));
            
            CurrentPage = Children[dayNr];
        }


        //constructor that creates schedule of week specified by parameter
        public ScheduleView(String formation, String group, String subgroup, int weekNr)
        {
            int dayNr = GetDayIndex();
            if (dayNr > 4)
            {
                weekNr++;
            }

            List<int> dates = GetDateOfMonday(weekNr);
            ScheduleObj = ScheduleConstructor.GetSchedule(formation, group, subgroup, weekNr);
            BarBackgroundColor = BackgroundClr;
            int i = 0;
            foreach (DayModel day in ScheduleObj.Days)
                Children.Add(new DayView(day, dates[i++]));            
        }

        private int GetDayIndex()
        {
            DateTime today = DateTime.Now;
            int dayNr = today.DayOfWeek - DayOfWeek.Monday;
            return dayNr;
        }

        private List<int> GetDateOfMonday(int weekNr)
        {
            int diff = 0;
            if (weekNr == ScheduleConstructor.GetCurrentWeekNumber())
                diff = 0;
            else if (weekNr == ScheduleConstructor.GetCurrentWeekNumber() + 1)
                diff = 7;

            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday + diff);
            var tuesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Tuesday + diff);
            var wednesday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Wednesday + diff);
            var thursday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Thursday + diff);
            var friday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Friday + diff);

            List<int> dates = new List<int>
            {
                monday.Day,
                tuesday.Day,
                wednesday.Day,
                thursday.Day,
                friday.Day
            };

            return dates;
        }
    }
}
