using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Foundation;

namespace Futebol.iOS
{
	public class TableSource : UITableViewSource
	{
		List<Team> tableItems;
		string cellIdentifier = "TableCell";

		public TableSource (List<Team> items)
		{
			tableItems = items;
		}

		#region implemented abstract members of UITableViewSource

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);

			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			cell.TextLabel.Text = tableItems[indexPath.Row].Name;

			var imageName = tableItems [indexPath.Row].Shield.Name;
			var path = Environment.CurrentDirectory + "/shields/";

			if (!Directory.Exists (path))
				Directory.CreateDirectory (path);

			if (!File.Exists(path + imageName)) {
				var wc = new WebClient ();
				var image = wc.DownloadData (tableItems [indexPath.Row].Shield.Url);

				File.WriteAllBytes (path + imageName, image);
			}

			cell.ImageView.Image = UIImage.FromFile (path + imageName);
			//cell.ImageView.Image = UIImage.LoadFromData (imageData);

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}

		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
				new UIAlertView("DetailDisclosureButton Touched" 
					, tableItems[indexPath.Row].Name, null, "OK", null).Show();
		}

		#endregion
	}

	public class Team 
	{
		public Team (string name, Parse.ParseFile shield)
		{
			Name = name;
			Shield = shield;
		}

		public string Name { get; set; }
		public Parse.ParseFile Shield { get; set; }
	}
}

