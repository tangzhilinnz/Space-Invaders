//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteBatch : DLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            PacMan,
            AngryBirds,
            Texts,
            Boxes,
            UFO,

            ReserveShips_Batch,
            SpriteBox_Batch,
            Alien_Batch,
			Shields,
            Bombs,
            Explosions,
            Missiles,
            Ships,

            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public SpriteBatch()
            : base()
        {
            this.mName = SpriteBatch.Name.Uninitialized;

            this.poSpriteNodeMan = new SpriteNodeMan();
            Debug.Assert(this.poSpriteNodeMan != null);
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(SpriteBatch.Name _name, int _reserveNum = 3, int _reserveGrow = 1)
        {
			Debug.Assert(_name != SpriteBatch.Name.Uninitialized);
            this.mName = _name;
            this.poSpriteNodeMan.Set(_name, _reserveNum, _reserveGrow);
        }

        public void SetName(SpriteBatch.Name _name)
        {
            this.mName = _name;
        }

        public void SetPriority(int _priority = -1)
        {
            mPriority = _priority;
        }

        public SpriteNodeMan GetSpriteNodeMan()
        {
            return this.poSpriteNodeMan;
        }
        //public SpriteNode Attach(SpriteBase _pNode)
        //{
        //    Debug.Assert(_pNode != null);
        //    SpriteNode pNode = this.poSpriteNodeMan.Attach(_pNode);
        //    return pNode;
        //}
        public SpriteNode Attach(GameObject pGameObj)
        {
            Debug.Assert(pGameObj != null);
            SpriteNode pNode = this.poSpriteNodeMan.Attach(pGameObj.pSpriteProxy);


            // Initialize SpriteBatchNode
            pNode.Set(pGameObj.pSpriteProxy, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }

        public SpriteNode Attach(SpriteBase _pNode)
        {
            SpriteNode pNode = this.poSpriteNodeMan.Attach(_pNode);

            // Initialize SpriteBatchNode
            pNode.Set(_pNode, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }

        public void Enable()
        {
            this.bEnable = true;
        }

        public void Disable()
        {
            this.bEnable = false;
        }

        public bool IsEnabled()
        {
            return this.bEnable;
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
            this.mName = Name.Uninitialized;

            base.baseWash();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.GetName(), this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());

			base.baseDump();
		}

        override public bool LessThanByPriority(NodeBase _pNodeBaseB)
        {
            SpriteBatch pNodeBaseB = (SpriteBatch)_pNodeBaseB;
            return this.mPriority < pNodeBaseB.mPriority;
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name mName;
        private readonly SpriteNodeMan poSpriteNodeMan;
        private int mPriority;
        private bool bEnable = true;
    }
}

// --- End of File ---
