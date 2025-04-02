//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class GameObjectNode : DLink
    {

        //------------------------------------
        // Constructor
        //------------------------------------

        public GameObjectNode()
            :base()
        {
            this.Wash();
        }

        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObj = pGameObject;
        }


        //------------------------------------
        // Override
        //------------------------------------
        override public void Wash()
        {
            this.pGameObj = null;

            base.baseWash();
        }

        public override System.Enum GetName()
        {
            System.Enum name;
            if(this.pGameObj == null)
            {
                name = null;
            }
            else
            {
                name = this.pGameObj.GetName();
            }
            return name;
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   GameObjectNode: ({0})", this.GetHashCode());

            // Data:
            if (this.pGameObj != null)
            {
                Debug.WriteLine("      GameObject.name: {0} ({1})", this.pGameObj.GetName(), this.pGameObj.GetHashCode());
            }
            else
            {
                Debug.WriteLine("      GameObject.name: null");
            }

			base.baseDump();
		}

        //----------------------------------
        // Data
        //----------------------------------
        public GameObject pGameObj;
    }
}

// --- End of File ---
