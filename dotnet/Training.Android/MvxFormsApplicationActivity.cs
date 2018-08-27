﻿//
// MvxFormsApplicationActivity.cs
//
// Author:
//  Jim Borden  <jim.borden@couchbase.com>
//
// Copyright (c) 2016 Couchbase, Inc All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Forms.Droid.Views;
using MvvmCross.Forms.Platform;
using MvvmCross.Platform;
using Training.Core;
using Xamarin.Forms.Platform.Android;
using XLabs.Platform.Device;

namespace Training.Android
{
    /// <summary>
    /// The highest entry point in the MvvmCross application, and the first activity that the user
    /// interacts with
    /// </summary>
    [Activity(Label = "MainActivity", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@android:color/transparent")]
    public class MainActivity
        : MvxFormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            Couchbase.Lite.Support.Droid.Activate(ApplicationContext);

            // Setup the application
            Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(ApplicationContext);
            setup.EnsureInitialized();

            // Register platform specific implementations
            Mvx.RegisterSingleton<IDevice>(() => AndroidDevice.CurrentDevice);
            Mvx.RegisterSingleton<IImageService>(() => new ImageService());

            //Start the application
            var startup = new CoreAppStart();
            var hint = CoreAppStart.CreateHint();
            startup.Start(hint);

            LoadApplication(FormsApplication);
        }
    }
}
