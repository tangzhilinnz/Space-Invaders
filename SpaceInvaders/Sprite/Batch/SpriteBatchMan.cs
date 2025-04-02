//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteBatchMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteBatchMan(int _reserveNum  = 1, int _reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
        {
            SpriteBatchMan.psActiveSBMan = null;
		
            // initialize derived data here
            SpriteBatchMan.psSpriteBatchCompare = new SpriteBatch();
			Debug.Assert(SpriteBatchMan.psSpriteBatchCompare != null);
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(SpriteBatchMan.psInstance == null);

            // Do the initialization
            if (SpriteBatchMan.psInstance == null)
            {
                SpriteBatchMan.psInstance = new SpriteBatchMan();
            }
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteBatchMan.DumpStats();
            }
        }

        public static SpriteBatch Add(SpriteBatch.Name _name, int _priority = -1, int _reserveNum = 3, int _reserveGrow = 1)
        {
            Debug.Assert(_name != SpriteBatch.Name.Uninitialized);

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.baseAdd(false);
            Debug.Assert(pSpriteBatch != null);

            // Initialize the data
            pSpriteBatch.Set(_name, _reserveNum, _reserveGrow);
            pSpriteBatch.SetPriority(_priority);
            pMan.baseInsertWithPriority(pSpriteBatch);
            return pSpriteBatch;
        }
		public static void SetActive(SpriteBatchMan pSBMan)
		{
			SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
			Debug.Assert(pMan != null);

			Debug.Assert(pSBMan != null);
			SpriteBatchMan.psActiveSBMan = pSBMan;
		}
        public static void Draw()
        {
            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // iterate through the nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // Downcast (its OK - homogeneous list)
                // Assumes someone before here called update() on each sprite
                SpriteBatch pSpriteBatch = (SpriteBatch)pIt.Current();
                if (pSpriteBatch.IsEnabled())
                {
                    pSpriteBatch.GetSpriteNodeMan().Draw();
                }
            }
        }
        public static SpriteBatch Find(SpriteBatch.Name _name)
        {
			Debug.Assert(_name != SpriteBatch.Name.Uninitialized);

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two SpriteBatchs

            // So:  Use the Compare SpriteBatch - as a reference
            //      use in the Compare() function
            SpriteBatchMan.psSpriteBatchCompare.mName = _name;

            SpriteBatch pData = (SpriteBatch)pMan.baseFind(SpriteBatchMan.psSpriteBatchCompare);
            return pData;
        }

        public static void SetPriority(SpriteBatch.Name _name, int _priority)
        {
            SpriteBatch pSpriteBatch = SpriteBatchMan.Find(_name);
            Debug.Assert(pSpriteBatch != null); // Make sure pSpriteBatch is found out from poActive
            pSpriteBatch.SetPriority(_priority);

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseReinsertWithPriority(pSpriteBatch);
        }
        public static void Remove(SpriteBatch _pSpriteBatch)
        {
			Debug.Assert(_pSpriteBatch != null);

            SpriteBatchMan pMan = SpriteBatchMan.psActiveSBMan;
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(_pSpriteBatch);
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeMan pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBatch Man: ------");

            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteBatch();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteBatchMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteBatch psSpriteBatchCompare;
        private static SpriteBatchMan psInstance = null;
        private static SpriteBatchMan psActiveSBMan = null;
    }
}

// --- End of File ---
