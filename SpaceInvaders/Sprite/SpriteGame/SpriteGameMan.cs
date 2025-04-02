//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	class SpriteGameMan : ManBase
	{
		//----------------------------------------------------------------------
		// Constructor
		//----------------------------------------------------------------------
		private SpriteGameMan(int _reserveNum, int _reserveGrow)
				: base(new DLinkMan(), new DLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
		{
			// initialize derived data here
			SpriteGameMan.psSpriteCompare = new SpriteGame();
			Debug.Assert(SpriteGameMan.psSpriteCompare != null);
		}

		//----------------------------------------------------------------------
		// Static Methods
		//----------------------------------------------------------------------
		public static void Create(int _reserveNum = 8, int _reserveGrow = 1)
		{
			// make sure values are ressonable 
			Debug.Assert(_reserveNum >= 0);
			Debug.Assert(_reserveGrow > 0);

			// initialize the singleton here
			Debug.Assert(SpriteGameMan.psInstance == null);

			// Do the initialization
			if (SpriteGameMan.psInstance == null)
			{
				SpriteGameMan.psInstance = new SpriteGameMan(_reserveNum, _reserveGrow);
			}

			// Null sprite added to manager
			SpriteGameMan.Add(SpriteGame.Name.NullObject, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectShieldColumn, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectShieldGrid, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectShieldRoot, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectBombRoot, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectMissileGroup, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectExplosionRoot, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.NullObjectUFORoot, Image.Name.NullObject, 0.0f, 0.0f, 0.0f, 0.0f);
        }
		public static void Destroy(bool bPrintEnable = false)
		{
			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Do something clever here
			// track peak number of active nodes
			// print stats on destroy
			// invalidate the singleton
			if (bPrintEnable)
			{
				SpriteGameMan.DumpStats();
			}
		}
		public static SpriteGame Add(SpriteGame.Name _SpriteName,
									Image.Name _ImageName,
									float _x,
									float _y,
									float _w,
									float _h,
									Azul.Color _pColor = null)
		{
			Debug.Assert(_SpriteName != SpriteGame.Name.Uninitialized);
			Debug.Assert(_ImageName != Image.Name.Uninitialized);

			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			Image pImage = ImageMan.Find(_ImageName);
			Debug.Assert(pImage != null);

			SpriteGame pSprite = (SpriteGame)pMan.baseAdd();
			Debug.Assert(pSprite != null);

			// Initialize the data
			pSprite.Set(_SpriteName, pImage, _x, _y, _w, _h, _pColor);
			return pSprite;
		}
		public static SpriteGame Find(SpriteGame.Name _name)
		{
			Debug.Assert(_name != SpriteGame.Name.Uninitialized);

			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Compare functions only compares two Sprites

			// So:  Use the Compare Sprite - as a reference
			//      use in the Compare() function
			SpriteGameMan.psSpriteCompare.mName = _name;

			SpriteGame pData = (SpriteGame)pMan.baseFind(SpriteGameMan.psSpriteCompare);
			return pData;
		}
		public static void Remove(SpriteGame _pSprite)
		{
			Debug.Assert(_pSprite != null);

			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseRemove(_pSprite);
		}
		public static void Dump()
		{
			Debug.WriteLine("\n   ------ Sprite Man: ------");

			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDump();

		}
		public static void DumpStats()
		{
			Debug.WriteLine("\n   ------ Sprite Man: ------");

			SpriteGameMan pMan = SpriteGameMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDumpStats();

			Debug.WriteLine("   ------------\n");
		}

		//------------------------------------
		// Override Abstract methods
		//------------------------------------
		override protected NodeBase derivedCreateNode()
		{
			NodeBase pNodeBase = new SpriteGame();
			Debug.Assert(pNodeBase != null);

			return pNodeBase;
		}

		//------------------------------------
		// Private methods
		//------------------------------------
		private static SpriteGameMan privGetInstance()
		{
			// Safety - this forces users to call Create() first
			// before using class
			Debug.Assert(psInstance != null);

			return psInstance;
		}

		//------------------------------------
		// Data: unique data for this manager 
		//------------------------------------
		private static SpriteGame psSpriteCompare;
		private static SpriteGameMan psInstance = null;

	}
}

// --- End of File ---
