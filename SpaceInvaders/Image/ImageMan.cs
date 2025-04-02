////----------------------------------------------------------------------ImageMan-------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class ImageMan : ManBase
	{
		//----------------------------------------------------------------------
		// Constructor
		//----------------------------------------------------------------------
		private ImageMan(int _reserveNum, int _reserveGrow)
				: base(new SLinkMan(), new SLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
		{
			// initialize derived data here
			ImageMan.psImageCompare = new Image();
			Debug.Assert(ImageMan.psImageCompare != null);
		}

		//----------------------------------------------------------------------
		// Static Methods
		//----------------------------------------------------------------------
		public static void Create(int _reserveNum = 9, int _reserveGrow = 1)
		{
			// make sure values are ressonable 
			Debug.Assert(_reserveNum >= 0);
			Debug.Assert(_reserveGrow > 0);

			// initialize the singleton here
			Debug.Assert(ImageMan.psInstance == null);

			// Do the initialization
			if (ImageMan.psInstance == null)
			{
				ImageMan.psInstance = new ImageMan(_reserveNum, _reserveGrow);
			}

            // Have the manager pre-load these
			ImageMan.Add(Image.Name.HotPink, Texture.Name.HotPink, 0, 0, 128, 128);
            ImageMan.Add(Image.Name.NullObject, Texture.Name.HotPink, 0, 0, 0, 0);
		}
		public static void Destroy(bool bPrintEnable = false)
		{
			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Do something clever here
			// track peak number of active nodes
			// print stats on destroy
			// invalidate the singleton
            if (bPrintEnable)
            {
				ImageMan.DumpStats();
			}
		}
		public static Image Add(Image.Name _name,
								Texture.Name _TextName,
								float _x,
								float _y,
								float _w,
								float _h)
		{
			Debug.Assert(_name != Image.Name.Uninitialized);
			Debug.Assert(_TextName != Texture.Name.Uninitialized);

			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			Texture pTexture = TextureMan.Find(_TextName);
			Debug.Assert(pTexture != null);

			Image pImage = (Image)pMan.baseAdd();
			Debug.Assert(pImage != null);

			// Initialize the data
			pImage.Set(_name, pTexture, _x, _y, _w, _h);
			return pImage;
		}
		public static Image Find(Image.Name _name)
		{
			Debug.Assert(_name != Image.Name.Uninitialized);

			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Compare functions only compares two Images

			// So:  Use the Compare Image - as a reference
			//      use in the Compare() function
			ImageMan.psImageCompare.mName = _name;

			Image pData = (Image)pMan.baseFind(ImageMan.psImageCompare);
			return pData;
		}
		public static void Remove(Image _pImage)
		{
			Debug.Assert(_pImage != null);

			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseRemove(_pImage);
		}
		public static void Dump()
		{
			Debug.WriteLine("\n   ------ Image Man: ------");

			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDump();

		}
		public static void DumpStats()
		{
			Debug.WriteLine("\n   ------ Image Man: ------");

			ImageMan pMan = ImageMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDumpStats();

			Debug.WriteLine("   ------------\n");
		}

		//------------------------------------
		// Override Abstract methods
		//------------------------------------
		override protected NodeBase derivedCreateNode()
		{
			NodeBase pNodeBase = new Image();
			Debug.Assert(pNodeBase != null);

			return pNodeBase;
		}

		//------------------------------------
		// Private methods
		//------------------------------------
		private static ImageMan privGetInstance()
		{
			// Safety - this forces users to call Create() first
			// before using class
			Debug.Assert(psInstance != null);

			return psInstance;
		}

		//------------------------------------
		// Data: unique data for this manager 
		//------------------------------------
		private static Image psImageCompare;
		private static ImageMan psInstance = null;
	}
}

// --- End of File ---
