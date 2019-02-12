using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SetUp.Model
{
    public class ClassModel
    {
        public String TypeOfClass { get; set; }
        public String Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public String ClassName { get; set; }
        public String WhichWeek { get; set; }
        public String TargetGroup { get; set; }
        public String Room { get; set; }
        public String Teacher { get; set; }

        public ClassModel(String day, TimeSpan start, TimeSpan end, String whichWeek, String room, String target, String typeOfClass, String className, String teacher)
        {
            Day = day;
            StartTime = start;
            EndTime = end;
            WhichWeek = whichWeek;
            Room = room;
            TargetGroup = target;
            TypeOfClass = typeOfClass;
            ClassName = className;
            Teacher = teacher;
        }

        public override bool Equals(object obj)
        {
            var item = obj as ClassModel;
            if (obj == null)
                return false;
            return (this.ClassName.Equals(item.ClassName) 
                    && this.TargetGroup.Equals(item.TargetGroup)
                    && this.Day.Equals(item.Day)
                    && this.StartTime.Equals(item.StartTime)
                    && this.WhichWeek.Equals(item.WhichWeek)
                    );
        }

        public override int GetHashCode()
        {
            return ClassName.GetHashCode() ^ TargetGroup.GetHashCode();
        }

        public override string ToString()
        {
            string startTime = StartTime.ToString("c");
            startTime = startTime.Substring(0, startTime.Length - 3);
            string endTime = EndTime.ToString("c");
            endTime = endTime.Substring(0, endTime.Length - 3);
            string timeFormat = (startTime + " - " + endTime);

            String objectStr = TypeOfClass + "," + Day + "," + timeFormat + "," + ClassName + "," + WhichWeek + "," + TargetGroup + "," + Room + "," + Teacher;
            return objectStr;
        }
    }
}
