using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Feedr
{
	[Activity (Label = "Feedr", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button btnRegister, btnLogin;
		EditText txtUsername, txtPassword;
		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);

			InitControls ();
		}
		public void InitControls ()
		{
			btnRegister = FindViewById <Button> (Resource.Id.LOGbtnRegister);
			btnRegister.Click += delegate {StartActivity(typeof(RegisterUser));};

			btnLogin = FindViewById <Button> (Resource.Id.LOGbtnLogin);
			btnLogin.Click += btnLogin_Clicked;
			txtUsername = FindViewById<EditText> (Resource.Id.LOGusername);
			txtPassword = FindViewById<EditText> (Resource.Id.LOGpassword);
		}
		public async void btnLogin_Clicked(object sender, EventArgs e)
		{
			if (txtUsername.Text != "" && txtPassword.Text != "") {
				var result = await objParse.Login (txtUsername.Text, txtPassword.Text);

				if (result == true) {
					Toast.MakeText (this, "Login Successful", ToastLength.Long).Show ();
					StartActivity (typeof(FeedActivity));
				} else {
					Toast.MakeText (this, "Login Unsuccessful. Please check your username and password", ToastLength.Long).Show ();
				}
			}
		}
	}
}


