using SetUp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SetUp.Repository
{
    static class ScheduleConstructor
    {
        public static String GetURL(String formation)
        {
            String urlBuilder = "http://www.cs.ubbcluj.ro/files/orar/";
            DateTime currentDate = DateTime.Now;
            String year = "";
            int month = currentDate.Month;
            if (month > 9 || month < 2)
            {
                year = currentDate.Year.ToString();
                year = year + "-1";
            }
            else
            {
                var yearNr = currentDate.Year;
                yearNr = yearNr - 1;
                year = yearNr.ToString();
            }
            urlBuilder = urlBuilder + year + "/tabelar/" + formation + ".html";
            return urlBuilder;
        }

        public static int GetCurrentWeekNumber()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            DateTime start = DateTime.Now;
            DateTime now = DateTime.Now;

            int weeksPassed = 0;

            if (now.Month > 9)
            {
                start = new DateTime(now.Year, 10, 1);
                int startWeek = cal.GetWeekOfYear(start, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                int thisWeek = cal.GetWeekOfYear(now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                weeksPassed = thisWeek - startWeek + 1;
            }
            else if (now.Month < 2)
            {
                DateTime newYear = new DateTime(now.Year, 1, 1);
                weeksPassed = 12 + cal.GetWeekOfYear(newYear, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - 1;
            }
            else if (now.Month >= 2 && now.Month <= 9)
            {
                start = new DateTime(now.Year, 2, 25);
                int startWeek = cal.GetWeekOfYear(start, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                int thisWeek = cal.GetWeekOfYear(now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                weeksPassed = thisWeek - startWeek + 1;
                if (weeksPassed > 9)
                {
                    weeksPassed--;
                }

            }

            return weeksPassed;
        }

        public static ScheduleModel GetSchedule(String yearFormation, String group, String subgroup, int weekNr)
        {

            String url = GetURL(yearFormation);
            List<ClassModel> classes = DataExtractor.ExtractClasses(url);

            int nrOfWeek = weekNr % 2;
            if (nrOfWeek == 0) nrOfWeek = 2;

            List<DayModel> days = new List<DayModel>
            {
                new DayModel("Luni"),
                new DayModel("Marti"),
                new DayModel("Miercuri"),
                new DayModel("Joi"),
                new DayModel("Vineri")
            };

            foreach (ClassModel c in classes)
            {
                if (c.TargetGroup == yearFormation || c.TargetGroup == group || c.TargetGroup == (group+subgroup))
                {
                    if (c.WhichWeek == "" || c.WhichWeek == nrOfWeek.ToString())
                    {
                        foreach (DayModel day in days)
                        {
                            if (day.DayName == c.Day)
                                day.AddClassToDay(c);
                        }
                    }
                }
            }

            ScheduleModel schedule = new ScheduleModel();
            foreach(DayModel day in days)
            {
                schedule.AddDayToWeek(day);
            }
            return schedule;
        }
    }
}
