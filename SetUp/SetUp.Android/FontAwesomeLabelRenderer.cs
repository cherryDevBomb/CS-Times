using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SetUp.Droid;
using SetUp.View.FontIconApp.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FontAwesomeLabel), typeof(FontAwesomeLabelRenderer))]
namespace SetUp.Droid
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class FontAwesomeLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets,
                    "Fonts/" + FontAwesomeLabel.FontAwesomeName + ".otf");
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}