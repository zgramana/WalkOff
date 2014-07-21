// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace WalkOff
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel _activity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView _confidence { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel _label { get; set; }

		[Action ("_reset_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void _reset_TouchUpInside (UIButton sender);

		[Action ("UIButton4_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton4_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (_activity != null) {
				_activity.Dispose ();
				_activity = null;
			}
			if (_confidence != null) {
				_confidence.Dispose ();
				_confidence = null;
			}
			if (_label != null) {
				_label.Dispose ();
				_label = null;
			}
		}
	}
}
