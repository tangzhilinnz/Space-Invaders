//-----------------------------------------------------------------------------
// Copyright 2024, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;

namespace SE456
{
    public class SLinkIterator : Iterator
    {
        public SLinkIterator(  )
            : base()
        {
            this.pStart = null;

            this.privInitialize();
        }

        public void Reset( SLink pHead )
        {
            this.pStart = pHead;

            this.privInitialize();
        }
        override public NodeBase Next()
        {
            SLink pLink = (SLink)this.pCurr;

            if (pLink != null)
            {
                pLink = pLink.pNext;
                this.pCurr = (NodeBase)pLink;
            }

            if (pLink == null)
            {
                bDone = true;
            }

            return (NodeBase)pLink;

        }
        override public bool IsDone()
        {
            return bDone;
        }

        override public NodeBase First()
        {
            this.privInitialize();

            return this.pFirst;
        }

        override public NodeBase Current()
        {
            return this.pCurr;
        }

        private void privInitialize()
        {
            this.pFirst = this.pStart;
            this.pCurr = this.pStart;

            if (this.pStart == null)
            {
                this.bDone = true;
            }
            else
            {
                this.bDone = false;
            }
        }

        // ------------------------
        // Data:
        // ------------------------
        NodeBase pStart;
        NodeBase pFirst;
        NodeBase pCurr;
        bool bDone;
    }
}

// --- End of File ---

