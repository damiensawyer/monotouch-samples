using System;
using UIKit;
using System.Runtime.InteropServices;
using CoreGraphics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Foundation;


// http://adoptioncurve.net/archives/2012/09/a-simple-uicollectionview-tutorial/
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
		NSString[] data = new NSString[]{new NSString("One"), new NSString("Two")};

		string reuseIdentifier;

		public DamienDataSource(string reuseIdentifier)
		{
			this.reuseIdentifier = reuseIdentifier;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath) {
			var section = indexPath.Section;
			var row = indexPath.Row;

			//var data = this.data[row];

			var result = (UICollectionViewCell)collectionView.DequeueReusableCell(this.reuseIdentifier, indexPath);

			// set properties on view....

			return result;
		}


		public override nint GetItemsCount(UICollectionView collectionView, nint section) {
			return 1000;
		}

		public override nint NumberOfSections(UICollectionView collectionView) {
			return 1;
		}
	}

	public class DamienDelegate : UICollectionViewDelegate
	{
	}


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

	public class DamienCell : UICollectionViewCell
	{
		[Export ("initWithFrame:")] // need to do this... remove it to see why
		public DamienCell (CGRect frame) : base (frame)
		{

			BackgroundView = new UIView{BackgroundColor = UIColor.Orange};

			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};

			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.Blue;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);

			var v = new UIView();
			v.BackgroundColor = UIColor.Red;
			ContentView.AddSubview(v);
		}
	}

}

