
namespace SetUp.View
{
    using Xamarin.Forms;

    namespace FontIconApp.UserControls
    {
        public class FontAwesomeLabel : Label
        {
            public static readonly string FontAwesomeName = "FontAwesome";

            public FontAwesomeLabel()
            {
                FontFamily = FontAwesomeName;
            }
        }

        public static class Icon
        {
            public static readonly string FAClockO = "\uf017";  //time icon
            public static readonly string FAPencil = "\uf040";  //edit icon
            public static readonly string FALocationArrow = "\uf124"; //location 
            public static readonly string FACopyright = "\uf1f9";
            public static string FAUser = "\uf007";             //teacher icon
            public static string FAArrowRight = "\uf061";   //right arrow
        }
    }
}
