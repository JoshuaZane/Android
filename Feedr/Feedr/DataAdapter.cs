using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;
using Parse;
using System.Collections.Generic;


namespace Feedr
{

	public class DataAdapter : BaseAdapter<Post> {

		List<Post> items;
		string itemLink;

		Activity context;
		public DataAdapter(Activity context, List<Post> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Post this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.Feed_item, null);

			view.FindViewById<TextView>(Resource.Id.name).Text = item.ParseUser.Username;
			view.FindViewById<TextView>(Resource.Id.timestamp).Text = item.CreatedAt.ToString();
			view.FindViewById<TextView> (Resource.Id.Description).Text = item.Description.ToString ();
			var pic = item.ParseUser.Get<ParseFile> ("ProfilePic");
			view.FindViewById<ImageView>(Resource.Id.profilePic).SetImageBitmap (GetImageBitmapFromUrl(pic.Url.AbsoluteUri));

			var pic2 = item.Image;
			view.FindViewById<ImageView> (Resource.Id.feedImage1).SetImageBitmap (GetImageBitmapFromUrl(pic2.Url.AbsoluteUri));
			return view;
		}
		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;
			if (!(url == "null"))
				using (var webClient = new WebClient ())
				{
					var imageBytes = webClient.DownloadData (url);
					if (imageBytes != null && imageBytes.Length > 0)
					{
						imageBitmap = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length);
					}
				}
			return imageBitmap;
		}

	}
}
