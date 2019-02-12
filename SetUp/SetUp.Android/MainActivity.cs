using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;

namespace SetUp.Droid
{
    [Activity(Label = "CS Times", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);


            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Internet) == (int)Permission.Granted)
            //{
            //    // We have permission, go ahead and use the camera.
            //}
            //else
            //{
            //    // Camera permission is not granted. If necessary display rationale & request.
            //    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Internet }, 1);
            //}



            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }


    }
}

