using System;
using System.Diagnostics;

namespace SE456
{
    public class FreezeSceneObserver : ColObserver
    {
        public FreezeSceneObserver(bool _isInstantDeath = false)
        {
            this.isInstantDeath = _isInstantDeath;
        }
        override public void Notify()
        {
            GameObject pReserveShips = GameObjectNodeMan.Find(GameObject.Name.ReserveShips);
            if (pReserveShips.GetNumChildren() > 0 && !this.isInstantDeath)
            {
                TimerEventMan.Add(TimerEvent.Name.ShipResurrection, new ShipResurrectionCmd(), 0.25f, 1);
            }
            else
            {
                TimerEventMan.Add(TimerEvent.Name.SceneOver, new SceneOverCmd(), 0.25f, 1);
                TimerEventMan.Remove(TimerEventMan.Find(TimerEvent.Name.NextLevel, 1), 1);
            }
        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.FreezeSceneObserver;
        }

        private bool isInstantDeath;
    }
}

// --- End of File ---
