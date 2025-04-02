//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class IteratorReverseComposite : IteratorCompositeBase
    {
        public IteratorReverseComposite(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            // Horrible HACK need to clean up.. but its late
            IteratorForwardComposite pForward = new IteratorForwardComposite(pStart);

            Component pPrevNode = null;

            for(pForward.First();!pForward.IsDone();pForward.Next())
            {
                Component pNode = pForward.Curr();

                if (pNode != null)
                {
                    pNode.pReverse = pPrevNode;

                    //if (pNode.pReverse != null)
                    //{
                    //    Debug.WriteLine("n:{0} r:{1}", pNode.GetName(), pNode.pReverse.GetName());
                    //}
                    //else
                    //{
                    //    Debug.WriteLine("n:{0} r:null", pNode.GetName());
                    //}
                }

                pPrevNode = pNode;

            }

            this.pRoot = pStart;
            this.pInit = pPrevNode;
            this.pCurr = pPrevNode;
            this.pPrev = null;

        }

        override public Component First()
        {
            Debug.Assert(this.pRoot != null);

            this.pCurr = this.pInit;

            return this.pCurr;
        }
        override public Component Curr()
        {
            return this.pCurr;
        }
        override public Component Next()
        {
            Debug.Assert(this.pCurr != null);

            this.pPrev = this.pCurr;
            this.pCurr = this.pCurr.pReverse;
            return this.pCurr;
        }

        override public bool IsDone()
        {
            return (this.pPrev == this.pRoot);
        }

        private Component pRoot;
        private Component pCurr;
        private Component pPrev;
        private Component pInit;

    }
}

// --- End of File ----
