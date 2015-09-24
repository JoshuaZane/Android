
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Parse;
using Android.Graphics.Drawables;
using System.IO;
using Android.Graphics;

namespace Feedr
{
	[Activity (Label = "RegisterUser")]			
	public class RegisterUser : Activity
	{
		Button btnLoginScreen, btnRegister;
		EditText txtUsername, txtEmail, txtPassword;
		ParseHandler objParse = ParseHandler.Default;
		ImageButton btnProfilePic;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Register);

			InitControls ();
		}
		private void InitControls()
		{
			txtUsername = FindViewById<EditText> (Resource.Id.REGtxtUsername);
			txtEmail = FindViewById<EditText> (Resource.Id.REGtxtEmail);
			txtPassword = FindViewById<EditText> (Resource.Id.REGtxtPassword);
			btnRegister = FindViewById<Button> (Resource.Id.REGbtnRegister);
			btnLoginScreen = FindViewById<Button> (Resource.Id.REGbtnLogin);
			btnProfilePic = FindViewById<ImageButton> (Resource.Id.REGbtnProfilePic);

			btnRegister.Click += delegate {
				RegisterNewUser ();
			};
			//btnLoginScreen.Click += btnLoginScreen_Clicked;

			btnProfilePic.Click += btnProfileImage_Clicked;
		}
		void btnProfileImage_Clicked(object sender, EventArgs e)
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/jpeg");
			imageIntent.SetAction (Intent.ActionGetContent);
			StartActivityForResult (Intent.CreateChooser (imageIntent, "Select photo"), 0);
		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if ((resultCode == Result.Ok) && (data != null))
			{
				btnProfilePic.SetImageURI(data.Data);
			}
		}
		public byte[] GetProfilePicInBytes()
		{
			var fetchDrawable = btnProfilePic.Drawable;
			BitmapDrawable bitmapDrawable = (BitmapDrawable)fetchDrawable;
			var bitmap = bitmapDrawable.Bitmap;

			byte[] bitmapData;
			using (var stream = new MemoryStream ()) {
				bitmap.Compress (Bitmap.CompressFormat.Jpeg, 100, stream);
				bitmapData = stream.ToArray ();
			}
			return bitmapData;
		}
		private async void RegisterNewUser()
		{
			Toast.MakeText (this, "Please wait ...", ToastLength.Short).Show ();

			var result = await objParse.CheckIfUsernameExists (txtUsername.Text);

			if (result == true) {
				Toast.MakeText (this, "Username already exists", ToastLength.Long).Show ();
			} else {
				await objParse.CreateUserAsync (txtUsername.Text, txtEmail.Text, txtPassword.Text, GetProfilePicInBytes());
				Toast.MakeText (this, "Account Successfully Created", ToastLength.Short).Show ();
				Toast.MakeText (this, "Please Login again", ToastLength.Short).Show ();
				ClearAll ();
				StartActivity (typeof(MainActivity));
			}
		}
		void ClearAll()
		{
			txtUsername.Text = "";
			txtEmail.Text = "";
			txtPassword.Text = "";
		}
	}
}

