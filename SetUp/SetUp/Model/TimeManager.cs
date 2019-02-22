using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SetUp.Model
{
    class TimeManager
    {
        public static int Semester { get; set; }
        public static int WeekNr { get; set; }
        public static int AcademicWeekNr { get; set; }
        public static DateTime Today { get; set; }

        private static readonly List<DateTime> FreeDays = new List<DateTime>
        {
            new DateTime(1900, 5, 1),
            new DateTime(1900, 6, 1),
            new DateTime(1900, 11, 30),
            new DateTime(1900, 12, 1)
        };


        public static void SetCurrentTimes()
        {
            Today = DateTime.Now;
            WeekNr = GetWeekNr(Today);

            Semester = GetSemester(WeekNr);
            AcademicWeekNr = GetAcademicWeekNr(WeekNr);
        }


        public static int GetSemester(int weekNr)
        {
            if (weekNr <= 21)
                return 1;
            else
                return 2;
        }

        public static int GetWeekNr(DateTime now)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int weekNr = 0;
            int weeksPassed = 0;

            if (now.Month > 9)
            {
                DateTime start = new DateTime(now.Year, 10, 1);
                int startWeek = cal.GetWeekOfYear(start, dfi.CalendarWeekRule, DayOfWeek.Monday);
                int thisWeek = cal.GetWeekOfYear(now, dfi.CalendarWeekRule, DayOfWeek.Monday);
                weeksPassed = thisWeek - startWeek + 1;
                weekNr = weeksPassed;
            }
            else
            {
                DateTime start = new DateTime(now.Year - 1, 10, 1);
                DateTime yearEnd = new DateTime(now.Year - 1, 12, 31);
                DateTime newYear = new DateTime(now.Year, 1, 1);
                int startWeek = cal.GetWeekOfYear(start, dfi.CalendarWeekRule, DayOfWeek.Monday);
                int lastWeek = cal.GetWeekOfYear(yearEnd, dfi.CalendarWeekRule, DayOfWeek.Monday);
                weeksPassed = lastWeek - startWeek + 1;
                weekNr = weeksPassed + cal.GetWeekOfYear(now, dfi.CalendarWeekRule, DayOfWeek.Monday);
                if (newYear.DayOfWeek != DayOfWeek.Monday)
                    weekNr--;
            }

            int dayNr = (int)now.DayOfWeek;
            if (dayNr == 0 || dayNr == 6)
                weekNr++;

            return weekNr;
        }

        public static int GetAcademicWeekNr(int astronomicWeekNr)
        {
            Semester = GetSemester(astronomicWeekNr);

            int academicWeekNr = 0;

            if (Semester == 1)
            {
                if (astronomicWeekNr <= 12)
                    academicWeekNr = astronomicWeekNr;
                else if (astronomicWeekNr == 13 || astronomicWeekNr == 14)
                    academicWeekNr = -1; //christmas
                else if (astronomicWeekNr == 15 || astronomicWeekNr == 16)
                    academicWeekNr = astronomicWeekNr - 2;
                else
                    academicWeekNr = -1; //sesiune
            }
            else
            {
                if (astronomicWeekNr <= 29)
                {
                    academicWeekNr = astronomicWeekNr - 21;
                }
                else if (astronomicWeekNr == 30)
                {
                    academicWeekNr = -1;
                }
                else if (astronomicWeekNr <= 36)
                {
                    academicWeekNr = astronomicWeekNr - 22;
                }
                else
                {
                    academicWeekNr = -1;
                }
            }
            return academicWeekNr;
        }

        public static List<DateTime> GetDates(int weekNr)
        {
            int diff = 0;
            if (weekNr == WeekNr)
                diff = 0;
            else if (weekNr == WeekNr + 1)
                diff = 7;

            var monday = Today.AddDays(-(int)Today.DayOfWeek + (int)DayOfWeek.Monday + diff);
            var tuesday = Today.AddDays(-(int)Today.DayOfWeek + (int)DayOfWeek.Tuesday + diff);
            var wednesday = Today.AddDays(-(int)Today.DayOfWeek + (int)DayOfWeek.Wednesday + diff);
            var thursday = Today.AddDays(-(int)Today.DayOfWeek + (int)DayOfWeek.Thursday + diff);
            var friday = Today.AddDays(-(int)Today.DayOfWeek + (int)DayOfWeek.Friday + diff);

            List<DateTime> dates = new List<DateTime>
            {
                monday,
                tuesday,
                wednesday,
                thursday,
                friday
            };

            return dates;
        }

        public static bool IsFreeDay(DateTime date)
        {
            foreach (DateTime freeDay in FreeDays)
            {
                if (freeDay.Month == date.Month && freeDay.Day == date.Day)
                    return true;
            }
            return false;
        }
    }
}
