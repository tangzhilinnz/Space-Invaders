//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    class DelayedObjectMan
    {
        static public void Attach(ColObserver pObserver)
        {
            Debug.Assert(pObserver != null);
            DelayedObjectMan pDelayMan = DelayedObjectMan.privGetInstance();

            pDelayMan.poSLinkMan.AddToFront(pObserver);
        }

        static public void Process()
        {
            DelayedObjectMan pDelayMan = DelayedObjectMan.privGetInstance();
            Iterator pIt = pDelayMan.poSLinkMan.GetIterator();

            ColObserver pNode = null;
            for (pIt.First();!pIt.IsDone();pIt.Next())
            {
                pNode = (ColObserver)pIt.Current();
                Debug.Assert(pNode != null);
                pNode.Execute();
            }

            // remove
            pNode = (ColObserver)pIt.First();
            while (!pIt.IsDone())
            {
                ColObserver pTmp = pNode;
                pNode = (ColObserver)pIt.Next();

                // remove
                pDelayMan.poSLinkMan.Remove(pTmp);
            }
        }
        private DelayedObjectMan()
        {
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);
        }

        private static DelayedObjectMan privGetInstance()
        {
            // Do the initialization
            if (instance == null)
            {
                instance = new DelayedObjectMan();
            }

            // Safety - this forces users to call create first
            Debug.Assert(instance != null);

            return instance;
        }

        // -------------------------------------------
        // Data: 
        // -------------------------------------------

        private SLinkMan poSLinkMan;
        private static DelayedObjectMan instance = null;
    }
}

// --- End of File ---
