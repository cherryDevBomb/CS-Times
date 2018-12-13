using System.Collections.Generic;

namespace SetUp.Model
{ 
    public class ScheduleModel
    {
        public List<DayModel> Days { get; set; }

        public ScheduleModel()
        {
            Days = new List<DayModel>();
        }

        public void AddDayToWeek(params DayModel[] someDays)
        {
            foreach(DayModel day in someDays)
                Days.Add(day);
        }
    }
}
