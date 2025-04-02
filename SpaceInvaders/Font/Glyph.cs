//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
	public class Glyph : DLink
	{
		//------------------------------------
		// Enum
		//------------------------------------
		public enum Name
		{
			Consolas36pt,
            SpaceInvaders,

            NullObject,
			Uninitialized
		}

		//------------------------------------
		// Constructor
		//------------------------------------

		public Glyph()
			: base()
		{
			this.mName = Name.Uninitialized;
			this.pTexture = null;
			this.poSubRect = new Azul.Rect();
			Debug.Assert(this.poSubRect != null);
			this.key = 0;
		}

		//------------------------------------
		// Methods
		//------------------------------------

		public void Set(Glyph.Name _name,
						int _key,
						Texture.Name _textName,
						float _x, float _y, float _width, float _height)
		{
			Debug.Assert(this.poSubRect != null);
			this.mName = _name;

			this.pTexture = TextureMan.Find(_textName);
			Debug.Assert(this.pTexture != null);

			this.poSubRect.Set(_x, _y, _width, _height);

			this.key = _key;

		}

		public Azul.Rect GetAzulRect()
		{
			Debug.Assert(this.poSubRect != null);
			return this.poSubRect;
		}

		public Azul.Texture GetAzulTexture()
		{
			Debug.Assert(this.pTexture != null);
			return this.pTexture.GetAzulTexture();
		}

		//------------------------------------
		// Override
		//------------------------------------
		override public Enum GetName()
		{
			return this.mName;
		}

		override public bool Compare(NodeBase pTarget)
		{
			// This is used in ManBase.Find() 
			Debug.Assert(pTarget != null);

			Glyph pDataB = (Glyph)pTarget;

			bool status = false;

			if (this.GetName().GetHashCode() == pDataB.GetName().GetHashCode()
				&& this.key == pDataB.key)
			{
				status = true;
			}

			return status;
		}

		override public void Wash()
		{
			Debug.Assert(this.poSubRect != null);
			this.mName = Name.Uninitialized;
			this.pTexture = null;
			this.poSubRect.Set(0, 0, 1, 1);
			this.key = 0;

			base.baseWash();
		}

		override public void Dump()
		{
			Debug.WriteLine("\t\tname: {0} ({1})", this.GetName(), this.GetHashCode());
			Debug.WriteLine("\t\t\tkey: {0}", this.key);
			if (this.pTexture != null)
			{
				Debug.WriteLine("\t\t   pTexture: {0}", this.pTexture.GetName());
			}
			else
			{
				Debug.WriteLine("\t\t   pTexture: null");
			}
			Debug.WriteLine("\t\t      pRect: {0}, {1}, {2}, {3}", this.poSubRect.x, this.poSubRect.y, this.poSubRect.width, this.poSubRect.height);

			base.baseDump();
		}

		// ------------------------------------
		// Data 
		// ------------------------------------
		public Name mName;
		public int key;
		private Azul.Rect poSubRect;
		private Texture pTexture;
	}
}

// --- End of File ---
