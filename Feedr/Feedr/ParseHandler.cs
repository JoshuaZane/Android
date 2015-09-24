
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
using Parse;
using System.Threading.Tasks;

namespace Feedr
{
	public class ParseHandler
	{
		static ParseHandler toDoServiceInstance = new ParseHandler ();
		public static ParseHandler Default { get { return toDoServiceInstance; } }
		public List<Post> Items { get; private set; }

		private ParseHandler() { }

		public async Task CreateUserAsync(string username, string email, string password,byte[] profilepic)
		{
			if (username != "" && email != "" && password != "" && profilepic != null)
			{
				ParseFile file = new ParseFile ("avatar.jpeg", profilepic);
				await file.SaveAsync ();

				var user = new ParseUser () {
					Username = username,
					Password = password, 
					Email = email,
				};

				user ["ProfilePic"] = file;
				await user.SignUpAsync();
			}
		}
		public async Task<Boolean> CheckIfUsernameExists (string username)
		{
			var query = ParseUser.Query;
			var queryresult = await query.WhereEqualTo ("username", username).FindAsync ();

			if (queryresult.ToList ().Count > 0) {
				return true;
			} else {
				return false;
			}
		}
		public async Task<Boolean> Login (string username,string password)
		{

			try {
				await ParseUser.LogInAsync (username, password);
				return true;
			} catch (Exception e) {
				Console.WriteLine ("Login Failed:" + e.Message);
				return false;
			}
		}
		public async Task<Boolean> AddPost(string Description, byte[] Postpic)
		{
			try 
			{
				ParseFile file = new ParseFile("postpic.jpg", Postpic);
				await file.SaveAsync();

				ParseObject Post = new ParseObject("Post");
				Post["Description"] = Description;
				Post["Image"] = file;
				Post["User"] = ParseUser.CurrentUser;

				await Post.SaveAsync();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine ("Error:" + e.Message);
				return false;
			}
		}
		public async Task<List<Post>> GetAllPosts()
		{
			var query = ParseObject.GetQuery ("Post");
			var result = await query.FindAsync ();

			var PostList = new List<Post> ();

			foreach (var obj in result)
			{
				Post tempobj = new Post ();

				tempobj.ObjectId = obj.ObjectId;
				tempobj.CreatedAt = obj.CreatedAt;
				tempobj.UpdatedAt = obj.UpdatedAt;

				ParseUser usrobj = obj.Get<ParseUser> ("User");
				tempobj.ParseUser = await usrobj.FetchIfNeededAsync ();

				tempobj.Image = obj.Get<ParseFile> ("Image");
				tempobj.Description = Convert.ToString (obj ["Description"]);

				PostList.Add (tempobj);
			}
			return PostList;

		}
		public ParseUser GetCurrentUserInstance()
		{
			return ParseUser.CurrentUser;
		}
	}
}
