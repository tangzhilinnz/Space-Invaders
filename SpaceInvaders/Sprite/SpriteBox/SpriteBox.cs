//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteBox : SpriteBase
	{
		//------------------------------------
		// Enum
		//------------------------------------
		public enum Name
		{
            Box,
			Box1,
			Box2,
			CollisionBox,

            Compare,
            NullObject,
			Uninitialized
		}

		//------------------------------------
		// Constructors
		//------------------------------------

		public SpriteBox()
		: base()   // <--- Delegate (kick the can)
		{
			this.mName = SpriteBox.Name.Uninitialized;

			Debug.Assert(SpriteBox.psTmpRect != null);
			SpriteBox.psTmpRect.Set(0, 0, 1, 1);

			// Here is the actual new
			this.poLineColor = new Azul.Color(1, 1, 1);
			Debug.Assert(this.poLineColor != null);

			// Here is the actual new
			this.poAzulSpriteBox = new Azul.SpriteBox(psTmpRect, this.poLineColor);
			Debug.Assert(this.poAzulSpriteBox != null);

			this.x = poAzulSpriteBox.x;
			this.y = poAzulSpriteBox.y;
			this.sx = poAzulSpriteBox.sx;
			this.sy = poAzulSpriteBox.sy;
			this.angle = poAzulSpriteBox.angle;
		}


		//------------------------------------
		// Methods
		//------------------------------------
        public override void Update()
		{
			this.poAzulSpriteBox.x = this.x;
			this.poAzulSpriteBox.y = this.y;
			this.poAzulSpriteBox.sx = this.sx;
			this.poAzulSpriteBox.sy = this.sy;
			this.poAzulSpriteBox.angle = this.angle;

			this.poAzulSpriteBox.Update();
		}
        public override void Render()
		{
			this.poAzulSpriteBox.Render();
		}

		public void Set(SpriteBox.Name _name,
						float _x,
						float _y,
						float _width,
						float _height,
						Azul.Color _pLineColor)
		{
			Debug.Assert(_name != SpriteBox.Name.Uninitialized);
			Debug.Assert(this.poAzulSpriteBox != null);
			Debug.Assert(this.poLineColor != null);

			Debug.Assert(SpriteBox.psTmpRect != null);
			SpriteBox.psTmpRect.Set(_x, _y, _width, _height);

			this.mName = _name;

			if (_pLineColor == null)
			{
				this.poLineColor.Set(1, 1, 1);
			}
			else
			{
				this.poLineColor.Set(_pLineColor);
			}

			this.poAzulSpriteBox.Swap(psTmpRect, this.poLineColor);

			this.x = poAzulSpriteBox.x;
			this.y = poAzulSpriteBox.y;
			this.sx = poAzulSpriteBox.sx;
			this.sy = poAzulSpriteBox.sy;
			this.angle = poAzulSpriteBox.angle;
		}

		public void SwapColor(Azul.Color _pColor)
		{
			Debug.Assert(_pColor != null);
			this.poAzulSpriteBox.SwapColor(_pColor);
		}
        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poLineColor != null);
            this.poLineColor.Set(red, green, blue, alpha);
            this.poAzulSpriteBox.SwapColor(this.poLineColor);
        }

        public void SwapRect(Azul.Rect _pRect)
        {
            Debug.Assert(_pRect != null);
            Debug.Assert(SpriteBox.psTmpRect != null);

            SpriteBox.psTmpRect.Set(_pRect);
            this.poAzulSpriteBox.SwapScreenRect(SpriteBox.psTmpRect);
        }

        //------------------------------------
        // Override
        //------------------------------------
        public override System.Enum GetName()
		{
			return this.mName;
		}

		override public void Wash()
		{
			this.mName = SpriteBox.Name.Uninitialized;

			// NOTE:
			// Do not null the poAzulBoxSprite it is created once in Default then reused
			// Do not null the poLineColor it is created once in Default then reused

			this.poLineColor.Set(1, 1, 1);

			this.x = 0.0f;
			this.y = 0.0f;
			this.sx = 1.0f;
			this.sy = 1.0f;
			this.angle = 0.0f;

			base.baseWash();
		}
		override public void Dump()
		{
			// we are using HASH code as its unique identifier 
			Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());
			Debug.WriteLine("      Color(r,b,g): {0},{1},{2} ({3})", this.poLineColor.red, this.poLineColor.green, this.poLineColor.blue, this.poLineColor.GetHashCode());
			Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSpriteBox.GetHashCode());
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
		public Azul.Color poLineColor;
		private Azul.SpriteBox poAzulSpriteBox;

		//------------------------------------------------------------------------
		// Static Data - prevent unecessary "new" in the above methods
		//------------------------------------------------------------------------
		static private Azul.Rect psTmpRect = new Azul.Rect();
	}
}

// --- End of File ---

