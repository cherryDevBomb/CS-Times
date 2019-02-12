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
            String year = "";
            DateTime currentDate = TimeManager.Today;     //DateTime.Now;
            if (TimeManager.Semester == 1)
            { 
                if (currentDate.Month > 9)
                {
                    year = currentDate.Year.ToString() + "-1";
                }
                else
                {
                    year = (currentDate.Year - 1).ToString() + "-1";
                }
            }
            else
            {
                year = (currentDate.Year - 1).ToString() + "-2";
            }
            urlBuilder = urlBuilder + year + "/tabelar/" + formation + ".html";
            return urlBuilder;
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
