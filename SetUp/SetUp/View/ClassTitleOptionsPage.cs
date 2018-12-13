﻿using SetUp.Model;
using SetUp.Repository;
using SetUp.View.FontIconApp.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SetUp.View
{
    class ClassTitleOptionsPage : ContentPage
    {
        private readonly List<Color> colors = new List<Color>() { Color.FromHex("5CDB95"), Color.FromHex("3AAFA9"), Color.FromHex("3B945E") };
        private static int ColorIndex = 0;
        private Color MyColor;
        
        public ClassTitleOptionsPage()
        {
            Title = "Choose class";
            Padding = new Thickness(0, 8);
            var layout = new StackLayout();

            var exitEdit = new ToolbarItem
            {
                Text = "Cancel",
                Priority = 0
            };
            exitEdit.Clicked += OnExitEditClicked;
            ToolbarItems.Add(exitEdit);

            Dictionary<String, Dictionary<String, List<ClassModel>>>.KeyCollection options = StudentInfoModel.SortedClasses.Keys;

            foreach (String title in options)
            {
                layout.Children.Add(GetOptionView(title));
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


        private AbsoluteLayout GetOptionView(String className)
        {
            var optionView = new AbsoluteLayout
            {
                Padding = new Thickness(5, 5),
                Margin = new Thickness(16, 8)
            };
            AbsoluteLayout.SetLayoutBounds(optionView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(optionView, AbsoluteLayoutFlags.All);

            MyColor = colors[ColorIndex++ % 3];

            Frame f = new Frame
            {
                BackgroundColor = Color.Transparent,
                BorderColor = MyColor,
                CornerRadius = 4,
            };
            optionView.Children.Add(f, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            //class name
            var classNameLabel = new Label
            {
                Text = className,
                TextColor = MyColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };


            //edit icon
            var editIcon = new FontAwesomeLabel
            {
                Text = FontIconApp.UserControls.Icon.FAPencil,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = MyColor
            };

            Grid line = new Grid
            {
                Margin = new Thickness(20, 20, 10, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.85, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) },
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            line.Children.Add(classNameLabel, 0, 0);
            line.Children.Add(editIcon, 1, 0);
            AbsoluteLayout.SetLayoutBounds(line, new Rectangle(0, 0, 1, .7));
            AbsoluteLayout.SetLayoutFlags(line, AbsoluteLayoutFlags.All);

            optionView.Children.Add(line);

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new TypeOfClassOptionsPage(className));
            };
            optionView.GestureRecognizers.Add(tgr);

            return optionView;
        }
    }
}
