//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class DLinkIterator : Iterator
    {
        public DLinkIterator(  )
            : base()
        {
            this.pStart = null;
            this.bDeleteMe = false;

            this.privInitialize();
        }

        public void Reset( DLink pHead )
        {
            this.pStart = pHead;

            this.privInitialize();
        }
        override public NodeBase Next()
        {
            NodeBase pTmp = null;

            if (this.bDeleteMe == false)
            {
            DLink pLink = (DLink)this.pCurr;

            if (pLink != null)
            {
                pLink = pLink.pNext;
                this.pCurr = (NodeBase)pLink;
            }

            if (pLink == null)
            {
                bDone = true;
            }

                pTmp = (NodeBase)pLink;
            }
            else
            {
                this.bDeleteMe = false;
                // do nothing... it Next() was called in the earse()
            }

            return pTmp;

        }

        // -------------------------------------------------------------------
        // Erase() - Remove a node while iterating
        // -------------------------------------------------------------------
        override public void Erase(ManBase pMan)
        {
            // need to have the manager update its stats, etc...
            Debug.Assert(pMan != null);

            // the one to be deleted
            NodeBase pTmp = this.Current();
            if (pTmp == null) return;
            Debug.Assert(pTmp != null);

            // increment the iterator
            this.Next();
            // signal to skip Next() since it was incremented here
            this.bDeleteMe = true;

            // actual removal
            pMan.baseRemove(pTmp);
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

        private void privInitialize( )
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
        bool bDeleteMe;
        bool bDone;
    }
}

// --- End of File ---

