//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SLinkMan : ListBase
    {
        public SLinkMan()
        {
            // LTN - SLinkMan obj (SLinkMan obj owned by a long term manager class)
            this.poIterator = new SLinkIterator();
            Debug.Assert(this.poIterator != null);

            this.poHead = null;
        }
        override public void AddToFront(NodeBase _pNode)
        {
            // add to front
            Debug.Assert(_pNode != null);

            SLink pNode = (SLink)_pNode;

            // add node to front
            pNode.pNext = poHead;
            poHead = pNode;

            // worst case, pHead was null initially,
            // now we added a node so... this is true
            Debug.Assert(poHead != null);
        }

        public void AddToEnd(NodeBase _pNode)
        {
            // add to front
            Debug.Assert(_pNode != null);
            SLink pNode = (SLink)_pNode;

            // add node
            if (poHead == null)
            {
                // none on list... so add it
                poHead = pNode;
                pNode.pNext = null;
            }
            else
            {
                // spin until end
                SLink pTmp = poHead;
                while (pTmp.pNext != null)
                {
                    pTmp = pTmp.pNext;
                }

                // push to front
                pTmp.pNext = pNode;
                pNode.pNext = null;
            }

            // worst case, pHead was null initially,
            // now we added a node so... this is true
            Debug.Assert(poHead != null);
        }

        override public void Remove(NodeBase _pNode)
        {
            // There should always be something on list
            Debug.Assert(this.poHead != null);
            Debug.Assert(_pNode != null);

            SLink pNode = (SLink)_pNode;

            if (poHead == pNode)
            {
                poHead = poHead.pNext;
            }
            else
            {
                SLink pPrev = poHead;
                while (pPrev.pNext != null && pPrev.pNext != pNode)
                {
                    pPrev = pPrev.pNext;
                }

                if (pPrev.pNext != null)
                {
                    pPrev.pNext = pNode.pNext;
                }
            }

            // remove any lingering links
            // HUGELY important - otherwise its crossed linked 
            pNode.Detach();
        }
        override public NodeBase RemoveFromFront()
        {
            // There should always be something on list
            Debug.Assert(poHead != null);

            // return node
            SLink pNode = poHead;

            // Update head (OK if it points to NULL)
            poHead = poHead.pNext;

            // remove any lingering links
            // HUGELY important - otherwise its crossed linked 
            pNode.Detach();

            return pNode;
        }

        override public Iterator GetIterator()
        {
            Debug.Assert(this.poIterator != null);
            this.poIterator.Reset(this.poHead);

            return this.poIterator;
        }

        // Data:
        public SLink poHead;
        public SLinkIterator poIterator;
    }
}

// --- End of File ---
