//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;


namespace SE456
{
    class InputSubject
    {
        public InputSubject()
        {
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);
        }

        public void Attach(InputObserver pObserver)
        {
            // protection
            Debug.Assert(pObserver != null);

            pObserver.pSubject = this;

            // Attach it to the Animation Sprite ( Push to front )
            this.poSLinkMan.AddToFront(pObserver);
        }


        public void Notify()
        {
            Iterator pIt = this.poSLinkMan.GetIterator();

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                InputObserver pObserver = (InputObserver)pIt.Current();
                Debug.Assert(pObserver != null);

                pObserver.Notify();
            }

        }

        public void Detach()
        {
        }


        // Data: ------------------------
        private SLinkMan poSLinkMan;



    }
}

// --- End of File ---
