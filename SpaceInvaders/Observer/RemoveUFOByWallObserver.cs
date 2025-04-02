using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveUFOByWallObserver : ColObserver
    {
        public RemoveUFOByWallObserver()
        {
            this.pUFO = null;
        }
        public RemoveUFOByWallObserver(RemoveUFOByWallObserver m)
        {
            this.pUFO = m.pUFO;
        }
        public override void Notify()
        {
            UFORoot pUFORoot = (UFORoot)this.pSubject.pObjA;

            this.pUFO = (UFO)IteratorForwardComposite.GetChild(pUFORoot);
            Debug.Assert(this.pUFO != null);

            if (this.pUFO.bMarkForDeath == false)
            {
                this.pUFO.bMarkForDeath = true;
                //   Delay
                RemoveUFOByWallObserver pObserver = new RemoveUFOByWallObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            this.pUFO.Remove();
            TimerEvent pUFOMoveSnd = TimerEventMan.Find(TimerEvent.Name.UFOMoveSnd, 1);
            Debug.Assert(pUFOMoveSnd != null);
            TimerEventMan.Remove(pUFOMoveSnd, 1);
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveUFOByWallObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pUFO;
    }
}

// --- End of File ---

