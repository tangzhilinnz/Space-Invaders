//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class IteratorForwardComposite : IteratorCompositeBase
    {

        public IteratorForwardComposite(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            this.pCurr = pStart;
            this.pRoot = pStart;
        }
        override public Component Curr()
        {
            return this.pCurr;
        }
        override public Component First()
        {
            Debug.Assert(this.pRoot != null);
            Component pNode = this.pRoot;

            Debug.Assert(pNode != null);
            this.pCurr = pNode;

            // Debug.WriteLine("---> {0} ", this.pCurr.GetHashCode());
            return this.pCurr;
        }

        override public Component Next()
        {
            Debug.Assert(this.pCurr != null);

            Component pNode = this.pCurr;

            Component pChild = GetChild(pNode);
            Component pSibling = GetSibling(pNode);
            Component pParent = GetParent(pNode);

            // Start - Depth first iteration
            pNode = this.privNextStep(pNode, pParent, pChild, pSibling);

            this.pCurr = pNode;

            //if (this.pCurr != null)
            //{
            //    Debug.WriteLine("---> {0}", this.pCurr.GetHashCode());
            //}
            //else
            //{
            //    Debug.WriteLine("---> null");
            //}

            return this.pCurr;
        }

        override public bool IsDone()
        {
            return (this.pCurr == null);
        }


        // --------------------------------------------------
        //  Helper functions
        // --------------------------------------------------
        private Component privNextStep(Component pNode, Component pParent, Component pChild, Component pSibling)
        {
            pNode = null;
			
            if (pChild != null)
            {
                pNode = pChild;
            }
            else
            {
                if (pSibling != null)
                {
                    pNode = pSibling;
                }
                else
                {
                    // No more 
                    //       siblings... 
                    //       children...
                    // Go up a level to the parent

                    while (pParent != null)
                    {
                        // Fix to allow partial tree iterator
                        if(pParent == this.pRoot)
                        {
                            pParent = null;
                            pNode = null;
                            break;
                        }

                        pNode = GetSibling(pParent);
                        if (pNode != null)
                        {
                            // Found one
                            break;
                        }
                        else
                        {
                            // Go fish
                            pParent = GetParent(pParent);
                        }
                    }
                }
            }

            return pNode;
        }

        static public Component GetParent(Component pNode)
        {
            Debug.Assert(pNode != null);

            return pNode.pParent;

        }
        static public Component GetChild(Component pNode)
        {
            Debug.Assert(pNode != null);

            Component pChild;

            if (pNode.type == Component.Container.COMPOSITE)
            {
                pChild = ((Composite)pNode).GetHead();
            }
            else
            {
                pChild = null;
            }

            return pChild;
        }
        static public Component GetSibling(Component pNode)
        {
            Debug.Assert(pNode != null);

            return (Component)pNode.pNext;
        }


        // Data 
        private Component pCurr;
        private Component pRoot;

    }
}

// --- End of File ---
