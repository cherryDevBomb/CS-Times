using System;
using System.Collections.Generic;
using System.IO;


namespace SetUp.Model
{
    public static class StudentInfoModel
    {
        public static String YearFormation { get; set; }
        public static String Group { get; set; }
        public static String Subgroup { get; set; }
        public static Dictionary<String, Dictionary<String, List<ClassModel>>> SortedClasses { get; set; }

        public static void SaveLoginInfo()
        {
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LoginCredentials.txt");

            using (var writer = new StreamWriter(filepath))
            {
                String info = YearFormation + "," + Group + "," + Subgroup;
                writer.WriteLine(info);
            }
        }

        public static bool GetLoginInfo()
        {
            try
            {
                var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LoginCredentials.txt");

                using (var reader = new StreamReader(filepath))
                {
                    String line = reader.ReadLine();
                    String[] elems = line.Split(',');
                    YearFormation = elems[0];
                    Group = elems[1];
                    Subgroup = elems[2];                   
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
