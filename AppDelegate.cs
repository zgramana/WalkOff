using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;

namespace WalkOff
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window {
            get;
            set;
        }

        public override void FinishedLaunching (UIApplication application)
        {
            var textAttr = UINavigationBar.Appearance.GetTitleTextAttributes();
            textAttr.Font = UIFont.FromName("Helvetica Neue-bold", 16.0f);
            textAttr.TextColor = UIColor.White;
            textAttr.TextShadowColor = UIColor.Clear;
            UINavigationBar.Appearance.SetTitleTextAttributes(textAttr);

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }

        // This method is invoked when the application is about to move from active to inactive state.
        // OpenGL applications should use this method to pause.
        public override void OnResignActivation (UIApplication application)
        {
        }
        
        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground (UIApplication application)
        {
        }
         
        // This method is called as part of the transiton from background to active state.
//        public override async void WillEnterForeground (UIApplication application)
//        {
//            var controller = Window.RootViewController.ChildViewControllers[0] as MainViewController;
//            var steps = await controller.RefreshSteps();
//            // do domething with steps...
//            Debugger.Log(0, "Results", steps.ToString());
//            controller.StepCount = steps;
//        }
        
        // This method is called when the application is about to terminate. Save data, if needed.
        public override void WillTerminate (UIApplication application)
        {
        }
    }
}

