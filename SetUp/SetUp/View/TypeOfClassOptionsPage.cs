using SetUp.Model;
using System;
using Xamarin.Forms;

namespace SetUp.View
{
    class TypeOfClassOptionsPage : ContentPage 
    {
        public TypeOfClassOptionsPage(String className)
        {
            Padding = new Thickness(0, 8);
            var layout = new StackLayout();

            var exitEdit = new ToolbarItem
            {
                Text = "Cancel",
                Priority = 0
            };
            exitEdit.Clicked += OnExitEditClicked;
            ToolbarItems.Add(exitEdit);

            foreach (String type in StudentInfoModel.SortedClasses[className].Keys)
            {
                foreach (ClassModel c in StudentInfoModel.SortedClasses[className][type])
                {
                    if (c.TargetGroup == StudentInfoModel.YearFormation || 
                        c.TargetGroup == StudentInfoModel.Group ||
                        c.TargetGroup == (StudentInfoModel.Group + StudentInfoModel.Subgroup))
                    {
                        var classView = new ClassView(c);

                        var tgr = new TapGestureRecognizer();
                        tgr.Tapped += (s, e) =>
                        {
                            Navigation.PushAsync(new EditClassPage(c));
                        };
                        classView.GestureRecognizers.Add(tgr);

                        layout.Children.Add(classView);
                    }
                }
            }

            var view = new ScrollView()
            {
                Content = layout
            };
            Content = view;
        }

        async void OnExitEditClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
