//
// Sample showing the core Element-based API to create a dialog
//
using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SettingsBundle
{
	public class SettingsViewController : DialogViewController
	{		
		
		public SettingsViewController(): base(null,false)
		{
			Root = CreateRoot();
		}
		
		BooleanElement airplaneModeElement;
		EntryElement userNameElement;
		EntryElement passwordElement;
		EntryElement privateElement;
		RootElement CreateRoot ()
		{
			airplaneModeElement = new BooleanElement ("Airplane Mode", false);
			airplaneModeElement.ValueChanged += delegate {
				AirplaneMode = airplaneModeElement.Value;
			};
			userNameElement = new EntryElement ("Login", "Your login name", "");
			userNameElement.Changed += delegate {
				UserName = userNameElement.Value;
			};
			passwordElement = new EntryElement ("Password", "Your password", "", true);
			passwordElement.Changed += delegate {
				Password = passwordElement.Value;
			};
			privateElement = new EntryElement("Private","Not in public settings","");
			privateElement.Changed += delegate {
				PrivateString = privateElement.Value;
			};
			return new RootElement ("Settings") {
				new Section (){
					airplaneModeElement,
					},
				new Section () {
					userNameElement,
					passwordElement,
				},
				new Section(){
					privateElement
				}
			};		
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			UpdateSettings();
		}
		
		public void UpdateSettings()
		{
			prefs.Synchronize();
			airplaneModeElement.Value = AirplaneMode;
			userNameElement.Value = UserName;
			passwordElement.Value = Password;
			privateElement.Value = PrivateString;
			
			this.TableView.ReloadData();
		}
		
		NSUserDefaults prefs =  NSUserDefaults.StandardUserDefaults ;
		public bool AirplaneMode
		{
			get {return prefs.BoolForKey("AirplaneMode");}
			set {
				prefs.SetBool(value,"AirplaneMode");
				prefs.Synchronize();
			}
		}
		
		public string UserName
		{
			get {return prefs.StringForKey("Username");}
			set {
				prefs.SetString(value,"Username");
				prefs.Synchronize();
			}
		}
		public string Password
		{
			get {return prefs.StringForKey("Password");}
			set {
				prefs.SetString(value,"Password");
				prefs.Synchronize();
			}
		}
		
		
		public string PrivateString
		{
			get {return prefs.StringForKey("PrivateString");}
			set {
				prefs.SetString(value,"PrivateString");
				prefs.Synchronize();
			}
		}
		
	}
}
