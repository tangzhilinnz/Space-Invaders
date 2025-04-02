using System;
using System.Diagnostics;

namespace SE456
{
    class CollisionBoxObserver : InputObserver
    {
        public override void Notify()
        {
            SpriteBatch pBoxBatch = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            if (pBoxBatch.IsEnabled())
            {
                pBoxBatch.Disable();
            }
            else
            {
                pBoxBatch.Enable();
            }

            //Debug.WriteLine("Collision Box Observer");
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.CollisionBoxObserver;
        }
    }
}

// --- End of File ---
