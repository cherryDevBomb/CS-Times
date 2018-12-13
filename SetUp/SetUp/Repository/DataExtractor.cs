using System;
using System.Collections.Generic;
using System.Text;
using SetUp.Model;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Linq;


namespace SetUp.Repository
{
    class DataExtractor
    {
        public static List<ClassModel> allClasses;

        public static String GetSourceFromHtml(String urlAdress)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAdress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    String data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();

                    return data;
                }
            }
            catch (WebException) { }

            return null;
        }

        public static List<String> ParseTable(String html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            List<String> data = new List<String>();

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in table.SelectNodes("//tr"))
                {
                    String entity = "";
                    foreach (HtmlNode cell in row.SelectNodes("th|td"))
                    {
                        entity = entity + cell.InnerText + ",";
                    }
                    data.Add(entity);
                }
            }
            return data;
        }

        public static List<String> ExtractGroups(String formation)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                String html = GetSourceFromHtml(ScheduleConstructor.GetURL(formation));
                doc.LoadHtml(html);
            }
            catch (Exception) { }

            List<String> groups = new List<String>();
            Boolean firstHeader = true;

            foreach (HtmlNode group in doc.DocumentNode.SelectNodes("//h1"))
            {
                if (!firstHeader)
                {
                    String[] headerFound = group.InnerText.Split(' ');
                    String groupNumber = headerFound[1];
                    groups.Add(groupNumber);
                }
                else
                    firstHeader = false;
            }
            return groups;
        }


        public static List<ClassModel> ExtractClasses(String url)
        {
            List<String> data = new List<String>();
            try {
                data = ParseTable(GetSourceFromHtml(url));
            }
            catch (Exception)
            {

            }
            
            List <ClassModel> classes = new List<ClassModel>();

            foreach (String line in data.Skip(1))
            {            

                String[] elems = line.Split(',');
                if (elems[0] == "Ziua") continue;

                String day = elems[0];

                String t = elems[1];
                String[] times = t.Split('-');
                int start = Int32.Parse(times[0]);
                int end = Int32.Parse(times[1]);

                String whichWeek = elems[2];
                if (whichWeek == "&nbsp;") whichWeek = "";
                else if (whichWeek == "sapt. 1") whichWeek = "1";
                else if (whichWeek == "sapt. 2") whichWeek = "2";

                string where = elems[3];
                string whoFrequents = elems[4];
                string type = elems[5];
                string title = elems[6];
                string profname = elems[7];
               
                ClassModel c = new ClassModel(day, new TimeSpan(start, 0, 0), new TimeSpan(end, 0, 0), whichWeek, where, whoFrequents, type, title, profname);
                if (!(classes.Contains(c)))
                    classes.Add(c);
            }

            //apply custom edits
            try
            {
                String filename = "CustomEditedClasses" + StudentInfoModel.Group + StudentInfoModel.Subgroup[1] + ".txt";
                var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
                List<ClassModel> readFromFile = new List<ClassModel>();

                using (var reader = new StreamReader(filepath))
                {
                    String line = reader.ReadLine();
                    String[] elems = line.Split(',');

                    String t = elems[2];
                    String[] times = t.Split('-');
                    int start = Int32.Parse((times[0].Split(':'))[0]);
                    int end = Int32.Parse((times[1].Split(':'))[0]);

                    ClassModel newClass = new ClassModel(elems[1], new TimeSpan(start, 0, 0), new TimeSpan(end, 0, 0), elems[4], elems[6], elems[5], elems[0], elems[3], elems[7]);
                    readFromFile.Add(newClass);
                    System.Diagnostics.Debug.WriteLine(line);
                }

                foreach (ClassModel c in classes)
                {
                    foreach (ClassModel newC in readFromFile)
                    {
                        if (c.ClassName == newC.ClassName && c.TargetGroup == newC.TargetGroup && c.TypeOfClass == newC.TypeOfClass)
                        {
                            c.Day = newC.Day;
                            c.StartTime = newC.StartTime;
                            c.EndTime = newC.EndTime;
                        }
                    }
                }
            }
            catch (Exception) {}

            //replace interchanged classes
            try
            {
                String filename = "ReplacedClasses" + StudentInfoModel.Group + StudentInfoModel.Subgroup[1] + ".txt";
                var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);

                List<ClassModel> readFromFile = new List<ClassModel>();

                using (var reader = new StreamReader(filepath))
                {
                    String line = reader.ReadLine();
                    String[] elems = line.Split(',');
                    String t = elems[2];
                    String[] times = t.Split('-');
                    int start = Int32.Parse((times[0].Split(':'))[0]);
                    int end = Int32.Parse((times[1].Split(':'))[0]);

                    ClassModel newClass = new ClassModel(elems[1], new TimeSpan(start, 0, 0), new TimeSpan(end, 0, 0), elems[4], elems[6], elems[5], elems[0], elems[3], elems[7]);
                    readFromFile.Add(newClass);
                    System.Diagnostics.Debug.WriteLine(line);
                }

                foreach (ClassModel c in classes)
                {
                    foreach (ClassModel newC in readFromFile)
                    {
                        if (c.ClassName == newC.ClassName && c.TypeOfClass == newC.TypeOfClass &&
                            (c.TargetGroup == StudentInfoModel.YearFormation || c.TargetGroup == StudentInfoModel.Group || c.TargetGroup == (StudentInfoModel.Group + StudentInfoModel.Subgroup)))
                        {
                            c.TargetGroup = c.TargetGroup + " abandonen";
                        }
                        if (c.ClassName == newC.ClassName && c.TypeOfClass == newC.TypeOfClass && c.Day == newC.Day && c.StartTime == newC.StartTime)
                        {
                            if (c.TypeOfClass == "Seminar")
                                c.TargetGroup = StudentInfoModel.Group;
                            else if (c.TypeOfClass == "Laborator")
                                c.TargetGroup = StudentInfoModel.Group + StudentInfoModel.Subgroup;
                        }
                    }
                }
            }
            catch (Exception) { }

            allClasses = classes;

            //save classes in StudentInfoModel
            Dictionary<string, Dictionary<string, List<ClassModel>>> sortedClasses = new Dictionary<string, Dictionary<string, List<ClassModel>>>();

            foreach (ClassModel item in allClasses)
            {
                if (!sortedClasses.ContainsKey(item.ClassName))     //add new class
                {
                    sortedClasses.Add(item.ClassName, new Dictionary<string, List<ClassModel>>());
                }
                if (!sortedClasses[item.ClassName].ContainsKey(item.TypeOfClass))   //add new type of class
                {
                    sortedClasses[item.ClassName].Add(item.TypeOfClass, new List<ClassModel>());
                }
                if (!sortedClasses[item.ClassName][item.TypeOfClass].Contains(item))    //add new class instance
                {
                    sortedClasses[item.ClassName][item.TypeOfClass].Add(item);
                }
            }
            StudentInfoModel.SortedClasses = sortedClasses;

            return classes;
        }
    }
}
