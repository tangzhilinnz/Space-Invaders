//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class SpriteGame : SpriteBase
	{
		//------------------------------------
		// Enum
		//------------------------------------
		public enum Name
		{
            OctopusAlien,
            CrabAlien,
            SquidAlien,
			Wall,
            Ship,
            Missile,
            
			BombStraight,
            BombZigZag,
            BombCross,
            BombRolling,

            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            ExplosionShip,
            ExplosionShipShot,
            ExplosionAlien,
            ExplosionAlienShot,
            ExplosionUFO,
            UFO,

            Compare,
			NullObject,
            NullObjectShieldColumn,
            NullObjectShieldGrid,
            NullObjectShieldRoot,
            NullObjectBombRoot,
            NullObjectMissileGroup,
            NullObjectExplosionRoot,
            NullObjectUFORoot,
            Uninitialized
		}

		//------------------------------------
		// Constructors
		//------------------------------------

		public SpriteGame()
		: base()   // <--- Delegate (kick the can)
		{
			this.x = 0.0f;
			this.y = 0.0f;
			this.sx = 1.0f;
			this.sy = 1.0f;
			this.angle = 0.0f;

			this.mName = Name.Uninitialized;
			this.pImage = null;

			this.poColor = new Azul.Color();
			Debug.Assert(this.poColor != null);

			this.poAzulSprite = new Azul.Sprite();
			Debug.Assert(this.poAzulSprite != null);

			// Temp instead of dynamically calling each time
			this.poRect = new Azul.Rect();
			Debug.Assert(this.poRect != null);

		}

		//------------------------------------
		// Methods
		//------------------------------------
		public override void Update()
		{
			this.poAzulSprite.x = this.x;
			this.poAzulSprite.y = this.y;
			this.poAzulSprite.sx = this.sx;
			this.poAzulSprite.sy = this.sy;
			this.poAzulSprite.angle = this.angle;

			this.poAzulSprite.Update();
		}
		public override void Render()
		{
			this.poAzulSprite.Render();
		}
		public void Set(Name _name, Image _pImage, float _x, float _y, float _w, float _h, Azul.Color pColor)
		{
			Debug.Assert(_name != SpriteGame.Name.Uninitialized);
			Debug.Assert(_pImage != null);
			Debug.Assert(this.poAzulSprite != null);
			Debug.Assert(this.poColor != null);

			this.pImage = _pImage;
			this.mName = _name;

			if (pColor == null)
			{
				this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
			}
			else
			{
				this.poColor.Set(pColor);
			}

			Debug.Assert(this.pImage.pTexture != null);
			this.poRect.Set(_x, _y, _w, _h);
			this.poAzulSprite.Swap(this.pImage.pTexture.poAzulTexture, this.pImage.poRect, this.poRect, this.poColor);
			this.poAzulSprite.Update();

			this.x = poAzulSprite.x;
			this.y = poAzulSprite.y;
			this.sx = poAzulSprite.sx;
			this.sy = poAzulSprite.sy;
			this.angle = poAzulSprite.angle;
		}

		public void SwapColor(Azul.Color _pColor)
		{
			Debug.Assert(_pColor != null);
			Debug.Assert(this.poColor != null);
			Debug.Assert(this.poAzulSprite != null);
			this.poColor.Set(_pColor);
			this.poAzulSprite.SwapColor(_pColor);
		}
		public void SwapColor(float red, float green, float blue, float alpha = 1.0f)
		{
			Debug.Assert(this.poColor != null);
			Debug.Assert(this.poAzulSprite != null);
			this.poColor.Set(red, green, blue, alpha);
			this.poAzulSprite.SwapColor(this.poColor);
		}
		public void SwapImage(Image pNewImage)
		{
			Debug.Assert(this.poAzulSprite != null);
			Debug.Assert(pNewImage != null);
			this.pImage = pNewImage;

			this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
			this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
		}

		public Azul.Rect GetRect()
		{
			Debug.Assert(this.poRect != null);
			return this.poRect;
		}

		//------------------------------------
		// Override
		//------------------------------------
		public override Enum GetName()
		{
			return this.mName;
		}
		override public void Wash()
		{
			Debug.Assert(this.poColor != null);
			Debug.Assert(this.poAzulSprite != null);

			this.x = 0.0f;
			this.y = 0.0f;
			this.sx = 1.0f;
			this.sy = 1.0f;
			this.angle = 0.0f;

			this.mName = Name.Uninitialized;
			this.pImage = null;

			this.poColor.Set(1.0f, 1.0f, 1.0f, 1.0f);

			Image pImageTmp = ImageMan.Find(Image.Name.HotPink);
			Debug.Assert(pImageTmp != null);
			Debug.Assert(pImageTmp.pTexture != null);

			this.poRect.Set(0.0f, 0.0f, 1.0f, 1.0f);
			this.poAzulSprite.Swap(pImageTmp.pTexture.poAzulTexture,
									pImageTmp.poRect,
									this.poRect,
									this.poColor);
			this.poAzulSprite.Update();

			base.baseWash();
		}
		override public void Dump()
		{
			// we are using HASH code as its unique identifier 
			Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());
			Debug.WriteLine("             Image: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());
			Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
			Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
			Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
			Debug.WriteLine("           (angle): {0}", this.angle);

			base.baseDump();
		}

		//------------------------------------
		// Data
		//------------------------------------
		public float x;
		public float y;
		public float sx;
		public float sy;
		public float angle;

		public Name mName;
		public Image pImage;
		public Azul.Color poColor;
		private Azul.Sprite poAzulSprite;
		private Azul.Rect poRect;
	}
}

// --- End of File ---

