//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    class ImageNode : SLink
    {
        //------------------------------------
        // Constructors
        //------------------------------------
        public ImageNode(Image _pImage)
            : base()
        {
            Debug.Assert(_pImage != null);
            this.pImage = _pImage;
        }
		//------------------------------------
		// Methods
		//------------------------------------

        //------------------------------------
        // Override
        //------------------------------------
		override public void Wash()
        {
            this.pImage = null;

            base.baseWash();
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   ({0}) node", this.GetHashCode());

            // Data:
            Debug.WriteLine("   pImage: {0} ({1})", this.pImage.GetName(), this.pImage.GetHashCode());

			base.baseDump();
		}
        public override System.Enum GetName()
        {
            return this.pImage.GetName();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Image pImage;
    }
}

// --- End of File ---
