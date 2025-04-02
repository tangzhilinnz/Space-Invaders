using System;
using System.Diagnostics;

namespace SE456
{
    class AddScoreObserver : ColObserver
    {
        public AddScoreObserver()
        {
            this.pTarget = null;
        }

        public override void Notify()
        {
            // Delete alien
            this.pTarget = this.pSubject.pObjB;
            Debug.Assert(this.pTarget != null);

            ShipMan.UpdateScore(this.pTarget.score);
            int curScore = ShipMan.GetScore();

            ShipMan.UpdateScoreFont();

            int HI_score = ShipMan.GetHIScore();
            if (curScore > HI_score)
            {
                ShipMan.SetHIScore(curScore);
            }
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.AddScoreObserver;
        }

        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pTarget;
    }
}

// --- End of File ---
