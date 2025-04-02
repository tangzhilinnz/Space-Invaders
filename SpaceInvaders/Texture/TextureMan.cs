//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class TextureMan : ManBase
	{
		//----------------------------------------------------------------------
		// Constructor
		//----------------------------------------------------------------------
		private TextureMan(int _reserveNum, int _reserveGrow)
				: base(new SLinkMan(), new SLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
		{
			// initialize derived data here
			TextureMan.psTextureCompare = new Texture();
			Debug.Assert(TextureMan.psTextureCompare != null);

		}

		//----------------------------------------------------------------------
		// Static Methods
		//----------------------------------------------------------------------
		public static void Create(int _reserveNum = 3, int _reserveGrow = 1)
		{
			// make sure values are ressonable 
			Debug.Assert(_reserveNum >= 0);
			Debug.Assert(_reserveGrow > 0);

			// initialize the singleton here
			Debug.Assert(TextureMan.psInstance == null);

			// Do the initialization
			if (psInstance == null)
			{
				TextureMan.psInstance = new TextureMan(_reserveNum, _reserveGrow);
			}

            // Preload these
			TextureMan.Add(Texture.Name.HotPink, "HotPink.t.azul");
            TextureMan.Add(Texture.Name.NullObject, "HotPink.t.azul");
		}
		public static void Destroy(bool bPrintEnable = false)
		{
			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Do something clever here
			// track peak number of active nodes
			// print stats on destroy
			// invalidate the singleton
			if (bPrintEnable)
			{
				TextureMan.DumpStats();
			}
		}

		public static Texture Add(Texture.Name _name, string _pTextureName)
		{
			Debug.Assert(!string.IsNullOrEmpty(_pTextureName));
			Debug.Assert(_name != Texture.Name.Uninitialized);

			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			Texture pTexture = (Texture)pMan.baseAdd();
			Debug.Assert(pTexture != null);

			// Initialize the data
			pTexture.Set(_name, _pTextureName);
			return pTexture;
		}

		public static Texture Find(Texture.Name _name)
		{
			Debug.Assert(_name != Texture.Name.Uninitialized);

			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			// Compare functions only compares two Textures

			// So:  Use the Compare Texture - as a reference
			//      use in the Compare() function
			TextureMan.psTextureCompare.mName = _name;

			Texture pData = (Texture)pMan.baseFind(TextureMan.psTextureCompare);
			return pData;
		}
		public static void Remove(Texture _pTexture)
		{
			Debug.Assert(_pTexture != null);

			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseRemove(_pTexture);
		}
		public static void Dump()
		{
			Debug.WriteLine("\n   ------ Texture Man: ------");

			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDump();
		}
		public static void DumpStats()
		{
			Debug.WriteLine("\n   ------ Texture Man: ------");

			TextureMan pMan = TextureMan.privGetInstance();
			Debug.Assert(pMan != null);

			pMan.baseDumpStats();

			Debug.WriteLine("   ------------\n");
		}

		//----------------------------------------------------------------------
		// Override Abstract methods
		//----------------------------------------------------------------------
		override protected NodeBase derivedCreateNode()
		{
			// compile time polymorphism
			NodeBase pNodeBase = new Texture();
			Debug.Assert(pNodeBase != null);

			return pNodeBase;
		}

		//------------------------------------
		// Private methods
		//------------------------------------
		private static TextureMan privGetInstance()
		{
			// Safety - this forces users to call Create() first
			// before using class
			Debug.Assert(psInstance != null);

			return psInstance;
		}

		//------------------------------------
		// Data: unique data for this manager 
		//------------------------------------
		private static Texture psTextureCompare;
		private static TextureMan psInstance = null;
	}
}

// --- End of File ---
