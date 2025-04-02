//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class Image : SLink
	{
		//------------------------------------
		// Enum
		//------------------------------------
		public enum Name
		{
			RedBird,
			YellowBird,
			GreenBird,
			WhiteBird,
            BlueBird,

            Wall,
            Ship,
            Missile,

            OctopusAAlien,
            OctopusBAlien,
            CrabAAlien,
            CrabBAlien,
            SquidAAlien,
            SquidBAlien,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            BombZigZag_A,
            BombZigZag_B,
            BombZigZag_C,
            BombZigZag_D,
            BombCross_A,
            BombCross_B,
            BombCross_C,
            BombCross_D,
            BombStraight_A,
            BombStraight_B,
            BombStraight_C,
            BombStraight_D,

            ExplosionShipA,
            ExplosionShipB,
            ExplosionShipShot,
            ExplosionAlien,
            ExplosionAlienShot,
            ExplosionUFO,
            UFO,

            NullObject,
			HotPink,
			Uninitialized
		}

		//------------------------------------
		// Constructors
		//------------------------------------
		public Image()
			: base()
		{
			this.mName = Name.Uninitialized;
			this.pTexture = null;

			this.poRect = new Azul.Rect();
			Debug.Assert(this.poRect != null);
		}

		//------------------------------------
		// Methods
		//------------------------------------
		public void Set(Name _name,
						Texture _pSrcTexture,
						float _x,
						float _y,
						float _w,
						float _h)
		{
			Debug.Assert(_pSrcTexture != null);
			Debug.Assert(_name != Image.Name.Uninitialized);

			this.pTexture = _pSrcTexture;

			// Remember the allocation was already made in constructor
			// so don't remove... replace the data
			Debug.Assert(this.poRect != null);
			this.poRect.Set(_x, _y, _w, _h);

			this.mName = _name;
		}
        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            return this.poRect;
        }

		//------------------------------------
		// Override
		//------------------------------------
		override public Enum GetName()
		{
			return this.mName;
		}

		override public void Wash()
		{
			Debug.Assert(this.poRect != null);
			this.poRect.Clear();

			this.mName = Name.Uninitialized;
			this.pTexture = null;

			base.baseWash();
		}

		override public void Dump()
		{
			// we are using HASH code as its unique identifier 
			Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());
			if (this.pTexture == null)
			{
				Debug.WriteLine("      Texture: null");
			}
			else
			{
				Debug.WriteLine("      Texture: {0} ({1})", this.pTexture.GetName(), this.pTexture.GetHashCode());
			}
			Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

			base.baseDump();
		}

		//------------------------------------
		// Data
		//------------------------------------
		public Name mName;
		public Azul.Rect poRect;
		public Texture pTexture;
	}
}

// --- End of File ---
