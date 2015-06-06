using System;
using UIKit;
using System.Runtime.InteropServices;
using CoreGraphics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Foundation;


// http://adoptioncurve.net/archives/2012/09/a-simple-uicollectionview-tutorial/
using System.Linq;


namespace SimpleCollectionView
{
	
	/* This is my first attempt at a custom Collection View. Some notes
	 *  - I have made a custom UIViewController. You don't need to. there is a built in UICollectionViewController which does a lot of stuff for you. 
	 * 
	 */
	  
	public class DamienCollectionView : UICollectionView
	{
		string reuseIdentifier = "chickenlips";
		public DamienCollectionView(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
		{
			
			this.RegisterClassForCell(typeof(DamienCell), this.reuseIdentifier);
			this.DataSource = new DamienDataSource(this.reuseIdentifier);
		}
	}

	// An object that adopts the UICollectionViewDataSource protocol is responsible for providing the data and views required by a collection view
	public class DamienDataSource : UICollectionViewDataSource
	{

		string reuseIdentifier;

		DateTime[] data;

		public DamienDataSource(string reuseIdentifier)
		{
			this.data = Enumerable.Range(0, 100).Select(x => DateTime.Now.AddDays(x)).ToArray();
			this.reuseIdentifier = reuseIdentifier;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath) {
			var section = indexPath.Section;
			var row = indexPath.Row;

			//var data = this.data[row];

			var cell = (DamienCell)collectionView.DequeueReusableCell(this.reuseIdentifier, indexPath);
			var item = this.data[row];
			cell.Text = item.ToString("d");


			return cell;
		}


		public override nint GetItemsCount(UICollectionView collectionView, nint section) {
			return this.data.Length;
		}

		public override nint NumberOfSections(UICollectionView collectionView) {
			return 1;
		}
	}


	public class DamienCell : UICollectionViewCell
	{
		public string Text { set{ this.textView.Text = value;		}}
		private UITextView textView { get; set; }
		[Export ("initWithFrame:")] // need to do this... remove it to see why
		public DamienCell (CGRect frame) : base (frame)
		{

			BackgroundView = new UIView{BackgroundColor = UIColor.Orange};

			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};
			
			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.Red;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);
			
			this.textView = new UITextView(ContentView.Frame);
			this.textView.BackgroundColor = UIColor.Blue;
			this.textView.TextColor = UIColor.White;
			ContentView.AddSubview(this.textView);
		}
	}

	public class DamienDelegate : UICollectionViewDelegate
	{
	}


	//[Obsolete("not needed", true)]
	public class DamienViewController : UIViewController
	{
		public DamienViewController()
		{
		}

		public override void ViewDidLoad() {

			var layout = new UICollectionViewFlowLayout();
			layout.ScrollDirection = UICollectionViewScrollDirection.Vertical;

			layout.ItemSize = new CGSize(100, 100); // should match cell


			var myView = new DamienCollectionView(this.View.Bounds, layout );

			this.View.Add(myView);
				
		}
	}


}

