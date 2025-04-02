//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class SpriteNodeMan : ManBase
	{
		//----------------------------------------------------------------------
		// Constructor
		//----------------------------------------------------------------------
		public SpriteNodeMan(int _reserveNum = 3, int _reserveGrow = 1)
				: base(new DLinkMan(), new DLinkMan(), _reserveNum, _reserveGrow)   // <--- Kick the can (delegate)
		{
		    this.pBackSpriteBatch = null;
		
			// initialize derived data here
			SpriteNodeMan.psSpriteNodeCompare = new SpriteNode();
			Debug.Assert(SpriteNodeMan.psSpriteNodeCompare != null);
		}

		//----------------------------------------------------------------------
		// Methods
		//----------------------------------------------------------------------
        public void Set(SpriteBatch.Name _name, int _reserveNum, int _reserveGrow)
        {
            this.mName = _name;

            Debug.Assert(_reserveNum > 0);
            Debug.Assert(_reserveGrow > 0);

            this.baseSetReserve(_reserveNum, _reserveGrow);
        }

        public SpriteNode Attach(SpriteBase pNode)
        {
            SpriteNode pSpriteNode = (SpriteNode)this.baseAdd();
            Debug.Assert(pSpriteNode != null);

            // Initialize the data
            pSpriteNode.Set(pNode, this);
            return pSpriteNode;
        }

		public void Draw()
		{
			// walk through the list and render
			Iterator pIt = this.baseGetIterator();
			Debug.Assert(pIt != null);

			// iterate through the nodes
			for (pIt.First(); !pIt.IsDone(); pIt.Next())
			{
				// Downcast (its OK - homogeneous list)
				// Assumes someone before here called update() on each sprite
				SpriteNode pNode = (SpriteNode)pIt.Current();
                pNode.pSpriteBase.Render();
			}
		}
		public void Remove(SpriteNode _pSpriteNode)
		{
			Debug.Assert(_pSpriteNode != null);

			this.baseRemove(_pSpriteNode);
		}
		public void Dump()
		{
			Debug.WriteLine("\n   ------ SpriteNode Man: ------");

			this.baseDump();
		}
		public void DumpStats()
		{
			Debug.WriteLine("\n   ------ SpriteNode Man: ------");

			this.baseDumpStats();

			Debug.WriteLine("   ------------\n");
		}

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }
		
		//------------------------------------
		// Override Abstract methods
		//------------------------------------
		override protected NodeBase derivedCreateNode()
		{
			NodeBase pNodeBase = new SpriteNode();
			Debug.Assert(pNodeBase != null);

			return pNodeBase;
		}

		//------------------------------------
		// Private methods
		//------------------------------------

		//------------------------------------
		// Data: unique data for this manager 
		//------------------------------------
		private static SpriteNode psSpriteNodeCompare;
        private SpriteBatch.Name mName;
		
		// Keenan(delete.D)
        private SpriteBatch pBackSpriteBatch;
		
	}
}

// --- End of File ---
