using System;
using System.Diagnostics;

namespace SE456
{
    class NextLevelCmd : Command
    {
        public NextLevelCmd()
        {
            this.isNextLevel = false;
        }

        public override void Execute(float deltaTime)
        {
            if (this.isNextLevel)
            {
                ShipMan.ScaleShipLevel();
                SceneContextMan.SetState(SceneContext.Scene.Play);
            }
            else
            {
                TimerEventMan.PauseUpdate(100.0f, 0);
                InputMan.LockInput();

                this.isNextLevel = true;
                TimerEventMan.Add(TimerEvent.Name.NextLevel, this, 2.5f, 1);
            }
        }

        private bool isNextLevel;
    }
}

// --- End of File ---
