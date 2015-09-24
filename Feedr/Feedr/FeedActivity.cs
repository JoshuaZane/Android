
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
using Android.Graphics.Drawables;
using System.IO;
using Android.Graphics;

namespace Feedr
{
	[Activity (Label = "FeedActivity")]			
	public class FeedActivity : Activity
	{
		ParseHandler objParse = ParseHandler.Default;
		ListView FeedList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			FeedList = FindViewById<ListView> (Resource.Id.Feedlist);
			LoadFeeds ();
		}

		private async void LoadFeeds()
		{
			List<Post> postlist = await objParse.GetAllPosts ();
			FeedList.Adapter = new DataAdapter (this, postlist);
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			menu.Add ("Add a Post");
			return base.OnPrepareOptionsMenu (menu);
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString ();

			switch (itemTitle)
			{
			case "Add a Post":
				StartActivity(typeof(AddPost));
					break;
			}

			return base.OnOptionsItemSelected (item);
		}

	}
}

