using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;
using MonoTouch.CoreMotion;
using System.Threading;
using MonoTouch.CoreFoundation;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WalkOff
{
    public partial class MainViewController : UIViewController
    {
        CMStepCounter _counter;
        CMMotionActivityManager _manager;

        bool _running;
        int _steps = 0;
        int _oldsteps = 0;

        NSDate _lastUpdate;

        readonly UIImage _none;
        readonly UIImage _low;
        readonly UIImage _medium;
        readonly UIImage _full;

        public MainViewController (IntPtr handle) : base (handle)
        {
            // Custom initialization
            _counter = new CMStepCounter();
            _manager = new CMMotionActivityManager();

            _none = UIImage.FromFile("confidence-none.png");
            _low = UIImage.FromFile("confidence-low.png");
            _medium = UIImage.FromFile("confidence-medium.png");
            _full = UIImage.FromFile("confidence-full.png");
        }

        #region View lifecycle

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0.564f, 0.0f, 0.015f);
            NavigationController.NavigationBar.TintColor = UIColor.White;
            NavigationController.NavigationBar.Translucent = false;
        }

        #endregion

        void StartStepCounter (NSOperationQueue queue)
        {
            _counter.StartStepCountingUpdates (queue, 1, (steps, time, err) =>  {
                Debugger.Log((int)SourceLevels.ActivityTracing, "Step Count Update", steps + Environment.NewLine);
                BeginInvokeOnMainThread (() =>  {
                    _lastUpdate = time;
                    _steps = _oldsteps + steps;
                    _label.Text = _steps.ToString ();
                    Debugger.Log((int)SourceLevels.ActivityTracing, "old steps / new steps / total steps", string.Format("{0} / {1} / {2}", _oldsteps, steps, _steps) + Environment.NewLine);
                });
            });
        }

        void StopCounter ()
        {
            _counter.StopStepCountingUpdates ();
        }

        public async Task<int> RefreshSteps ()
        {
            if (!_running)
                return 0;
            var now = NSDate.Now;
            var steps = await _counter.QueryStepCountAsync(_lastUpdate, now, NSOperationQueue.CurrentQueue);
            BeginInvokeOnMainThread(()=>
            {
                _lastUpdate = now;
                _steps = _oldsteps + steps;
                _label.Text = _steps.ToString ();
                Debugger.Log((int)SourceLevels.ActivityTracing, "old steps / new steps / total steps", string.Format("{0} / {1} / {2}", _oldsteps, steps, _steps) + Environment.NewLine);
            });
            return 0;
        }

        partial void UIButton4_TouchUpInside (UIButton sender)
        {
            if (!_running)
            {
                var queue = new NSOperationQueue();
                _manager.StartActivityUpdates(queue, (activity)=>
                {
                    Debugger.Log((int)SourceLevels.ActivityTracing, "ActivityUpdate", activity + Environment.NewLine);
                    string activityText;
                    UIImage confidence = null;
                    var shouldUpdate = false;

                    if (activity.Walking) {
                        activityText = "Walking";
                        shouldUpdate = true;
                    }
                    else if (activity.Running) {
                        activityText = "Running";
                        shouldUpdate = true;
                    }
                    else if (activity.Automotive)
                        activityText = "Driving";
                    else if (activity.Stationary)
                        activityText = "Stationary";
                    else {
                        activityText = "Unknown";
                            shouldUpdate = false;
                    }
                    switch (activity.Confidence)
                    {
                        case CMMotionActivityConfidence.High:
                            confidence = _full;
                            break;
                        case CMMotionActivityConfidence.Medium:
                            confidence = _medium;
                            break;
                        case CMMotionActivityConfidence.Low:
                            confidence = _low;
                            break;
                    }
                    
                    BeginInvokeOnMainThread (() => 
                    {
                        Debugger.Log((int)SourceLevels.ActivityTracing, "Setting Activity Text", activityText + Environment.NewLine);
                        _activity.Text = activityText;
                        _confidence.Image = confidence;
                    });

                    if (shouldUpdate) 
                    {
                        StartStepCounter (NSOperationQueue.CurrentQueue);
                    }
                    else
                    {
                        StopCounter();
                        _oldsteps = _steps;
                    }
                });
                sender.SetTitle("Stop", UIControlState.Normal);
                _running = true;
            } else {
                _manager.StopActivityUpdates();
                _running = false;
                _confidence.Image = _none;
                _activity.Text = "Off";
                sender.SetTitle("Start", UIControlState.Normal);
            }
        }

        partial void _reset_TouchUpInside (UIButton sender)
        {
            _steps = 0;
            _oldsteps = 0;
            _label.Text = "0";
        }
    }
}

