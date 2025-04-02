//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class GhostMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public GhostMan(int reserveNum = 3, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   // <--- Kick the can (delegate)
        {
            GhostMan.psActiceGSTMan = null;

            // initialize derived data here
            this.poNodeCompare = new GameObjectNode();
            this.poGameObj = new GameObjectNull();
            this.poNodeCompare.pGameObj = this.poGameObj;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GhostMan();
            }
        }
        public static void Destroy()
        {
            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GhostMan pMan = GhostMan.psActiceGSTMan;
            //GhostMan pMan = GhostMan.privGetInstance();
            GameObjectNode pNode = (GameObjectNode)pMan.baseAdd();

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObjectNode Find(GameObject.Name name)
        {
            GhostMan pMan = GhostMan.psActiceGSTMan;
            //GhostMan pMan = GhostMan.privGetInstance();

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            Debug.Assert(pMan.poNodeCompare.pGameObj != null);

            pMan.poNodeCompare.pGameObj.name = name;

            GameObjectNode pData = (GameObjectNode)pMan.baseFind(pMan.poNodeCompare);

            // OK to return null
            return pData;
        }


        public static void Remove(GameObjectNode pNode)
        {
            Debug.Assert(pNode != null);

            GhostMan pMan = GhostMan.psActiceGSTMan;
            //GhostMan pMan = GhostMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(pNode);
        }
    
        public static void Dump()
        {
            GhostMan pMan = GhostMan.psActiceGSTMan;
            //GhostMan pMan = GhostMan.privGetInstance();

            pMan.baseDump();
        }

        public static void SetActive(GhostMan pGSTMan)
        {
            GhostMan pMan = GhostMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pGSTMan != null);
            GhostMan.psActiceGSTMan = pGSTMan;
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GhostMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new GameObjectNode();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly GameObjectNode poNodeCompare;
        private readonly GameObjectNull poGameObj;
        private static GhostMan pInstance = null;
        private static GhostMan psActiceGSTMan = null;

    }
}

// --- End of File ---
