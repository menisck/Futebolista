using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;

namespace Futebol.iOS
{
	public partial class Futebol_iOSViewController : UITableViewController
	{
		public Futebol_iOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public static async Task<List<Team>> GetAll () 
		{
			var query = ParseObject.GetQuery ("Teams").OrderBy ("Name");

			var ie = await query.FindAsync ();

			var list = new List<Team>();

			foreach (var item in ie) {
				list.Add(new Team(item ["Name"].ToString (), item.Get<Parse.ParseFile>("Shield")));
			}

			return list;
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			table = new UITableView (View.Bounds);
			//string[] tableItems = new string[] {"Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers"};
			var tableItems = await GetAll ();
			table.Source = new TableSource(tableItems);
			Add (table);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

