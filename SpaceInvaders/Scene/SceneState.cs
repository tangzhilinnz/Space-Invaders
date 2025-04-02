//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public abstract class SceneState
    {
        public SceneState()
        {
            this.TimeAtPause = TimerEventMan.GetCurrTime();
        }
        public abstract void Initialize();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        public abstract void Entering();
        public abstract void Leaving();

        public float TimeAtPause;
        public SceneContext.Scene Type;
    }
}

// --- End of File ---
