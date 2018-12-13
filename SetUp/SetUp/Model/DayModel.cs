using System;
using System.Collections.Generic;


namespace SetUp.Model
{
    public class DayModel
    {
        public String DayName { get; set; }
        public List<ClassModel> Classes { get; set; }

        public DayModel(String dayName)
        {
            DayName = dayName;
            Classes = new List<ClassModel>();
        }

        public void AddClassToDay(params ClassModel[] someClasses)
        {
            foreach(ClassModel clas in someClasses)
                Classes.Add(clas);
            Classes.Sort((x, y) => TimeSpan.Compare(x.StartTime, y.StartTime));
        }

    }
}
