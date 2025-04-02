//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SpriteBoxMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SpriteBoxMan(int _reserveNum, int _reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            SpriteBoxMan.psSpriteBoxCompare = new SpriteBox();
            Debug.Assert(SpriteBoxMan.psSpriteBoxCompare != null);
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
            Debug.Assert(SpriteBoxMan.psInstance == null);

            // Do the initialization
            if (SpriteBoxMan.psInstance == null)
            {
				SpriteBoxMan.psInstance = new SpriteBoxMan(_reserveNum, _reserveGrow);
            }

            SpriteBoxMan.Add(SpriteBox.Name.NullObject, 0, 0, 0, 0);
            SpriteBoxMan.Add(SpriteBox.Name.CollisionBox, 0, 0, 0, 0);
        }
        public static void Destroy( bool bPrintEnable = false )
        {
            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteBoxMan.DumpStats();
            }
        }
        public static SpriteBox Add(SpriteBox.Name _name, 
                                    float _x, 
                                    float _y, 
                                    float _width, 
                                    float _height, 
                                    Azul.Color _pColor = null)
        {
            Debug.Assert(_name != SpriteBox.Name.Uninitialized);

            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            SpriteBox pNode = (SpriteBox)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(_name, _x, _y, _width, _height, _pColor);

            return pNode;
        }
        public static SpriteBox Find(SpriteBox.Name _name)
        {
            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Sprites

            // So:  Use the Compare Sprite - as a reference
            //      use in the Compare() function
            SpriteBoxMan.psSpriteBoxCompare.mName = _name;

            SpriteBox pData = (SpriteBox)pMan.baseFind(SpriteBoxMan.psSpriteBoxCompare);
            return pData;
        }
        public static void Remove(SpriteBox _pSpriteBox)
        {
			Debug.Assert(_pSpriteBox != null);

			SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(_pSpriteBox);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBox Man: ------");

            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBox Man: ------");

            SpriteBoxMan pMan = SpriteBoxMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteBox();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteBoxMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(SpriteBoxMan.psInstance != null);

            return SpriteBoxMan.psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteBox psSpriteBoxCompare;
        private static SpriteBoxMan psInstance = null;

    }
}

// --- End of File ---
