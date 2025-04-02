using System;
using System.Diagnostics;

namespace SE456
{
    class SpriteBoxProxyMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SpriteBoxProxyMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            // initialize derived data here
            SpriteBoxProxyMan.psSpriteBoxProxyCompare = new SpriteBoxProxy();
            SpriteBoxProxyMan.psSpriteBoxProxyCompare.pRealBox = new SpriteBoxNull();
            SpriteBoxProxyMan.psSpriteBoxProxyCompare.pRealBox.mName = SpriteBox.Name.Compare;
            SpriteBoxProxyMan.psSpriteBoxProxyCompare.name = SpriteBoxProxy.Name.Compare;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 0, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new SpriteBoxProxyMan(reserveNum, reserveGrow);
            }

            // Add a SpriteProxyNull
            SpriteBoxProxyMan.Add(SpriteBox.Name.NullObject);
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                SpriteBoxProxyMan.DumpStats();
            }
        }
        public static SpriteBoxProxy Add(SpriteBox.Name name)
        {
            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            SpriteBoxProxy pNode = (SpriteBoxProxy)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }
        public static void Remove(SpriteBoxProxy pSprite)
        {
            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSprite != null);
            pMan.baseRemove(pSprite);
        }
        public static SpriteBoxProxy Find(SpriteBox.Name name)
        {
            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            SpriteBoxProxyMan.psSpriteBoxProxyCompare.pRealBox.mName = name;

            SpriteBoxProxy pData = (SpriteBoxProxy)pMan.baseFind(SpriteBoxProxyMan.psSpriteBoxProxyCompare);
            return pData;
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ SpriteBoxProxy Man: ------");

            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ SpriteBoxProxy Man: ------");

            SpriteBoxProxyMan pMan = SpriteBoxProxyMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new SpriteBoxProxy();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static SpriteBoxProxyMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static SpriteBoxProxy psSpriteBoxProxyCompare;
        private static SpriteBoxProxyMan psInstance = null;

    }
}

// --- End of File ---
