using Xamarin.Forms;
using SetUp.View;

using AToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using System.Threading.Tasks;
using SetUp.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScheduleNavigationPage), typeof(NavigationPageRendererDroid))]
#pragma warning disable CS0618 // Type or member is obsolete
public class NavigationPageRendererDroid : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer // APPCOMP
{
    public AToolbar toolbar;
    public Activity context;


    //protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
    //{
    //    base.OnElementChanged(e);

    //    //context = (Activity)Xamarin.Forms.Forms.Context;
    //    //toolbar = context.FindViewById<Android.Support.V7.Widget.Toolbar>(SetUp.Droid.Resource.Id.action_bar);
    //    //toolbar.SetNavigationIcon(SetUp.Droid.Resource.Drawable.logo);


    //    //var actionBar = ((Activity)Context).ActionBar;
    //    //actionBar.SetIcon(SetUp.Droid.Resource.Drawable.logo);
    //}

    protected override Task<bool> OnPushAsync(Page view, bool animated)
    {
        var retVal = base.OnPushAsync(view, animated);

        context = (Activity)Xamarin.Forms.Forms.Context;
        toolbar = context.FindViewById<Android.Support.V7.Widget.Toolbar>(SetUp.Droid.Resource.Id.toolbar);


        if (toolbar != null)
        {
            if (toolbar.NavigationIcon != null)
            {
                toolbar.NavigationIcon = null;
                //toolbar.NavigationIcon = Android.Support.V4.Content.ContextCompat.GetDrawable(context, Resource.Drawable.baseline_arrow_back_24);
                //toolbar.SetNavigationIcon(Resource.Drawable.Back);
            }
        }
        return retVal;
    }
}
#pragma warning restore CS0618 // Type or member is obsolete