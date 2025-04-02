//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class Texture : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Aliens,
            Stitch,
            Birds,
            HotPink,

            PacMan,
            Consolas36pt,
            SpaceInvaders,

            NullObject,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Texture()
        : base()
        {
            // Do the create and load
            this.poAzulTexture = new Azul.Texture();
            Debug.Assert(this.poAzulTexture != null);

            this.mName = Texture.Name.Uninitialized;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name _name, string _pTextureName)
        {
            Debug.Assert(_pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);
            Debug.Assert(_name != Texture.Name.Uninitialized);

            this.poAzulTexture.Set(_pTextureName, Azul.Filter.Filter_Default);
            this.mName = _name;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.poAzulTexture;
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
            // reset with a default texture
            Debug.Assert(this.poAzulTexture != null);
            this.poAzulTexture.Set("HotPink.t.azul", Azul.Filter.Filter_Default);

            this.mName = Name.Uninitialized;

            base.baseWash();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());
            if (this.poAzulTexture == null)
            {
                Debug.WriteLine("      poAzulTexture: null ");
            }
            else
            {
                Debug.WriteLine("      poAzulTexture: ({0}) ", this.poAzulTexture.GetHashCode());
			}

			base.baseDump();
		}

        //------------------------------------
        // Data
        //------------------------------------
        public Name mName;
        public Azul.Texture poAzulTexture;
    }
}

// --- End of File ---
