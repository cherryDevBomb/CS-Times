using Xamarin.Forms;


namespace SetUp.View
{
    class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            Padding = new Thickness(8, 8);
            Content = new StackLayout
            {
                Children =
                {
                    new Image
                    {
                        Source = Device.RuntimePlatform == Device.Android ? "SetUp.Android/Resources/drawable/outline_error_outline_black_18dp.png"
                                                                  : "SetUp.iOS/Resources/drawable/outline_error_outline_black_18dp.png",
                        Margin = new Thickness(0, 50)
                    },

                    new Label
                    {
                        Text = "Error loading. Check your connection or try again later",
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                    }
                }
            };
        }
    }
}
