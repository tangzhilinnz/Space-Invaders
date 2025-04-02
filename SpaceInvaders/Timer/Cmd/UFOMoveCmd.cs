using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    class UFOMoveCmd : Command
    {
        public UFOMoveCmd(Sound _pSnd)
        {
            this.pSnd = _pSnd;
            Debug.Assert(this.pSnd != null);
            this.pSnd.SetVolume(1.0f);
        }

        public override void Execute(float deltaTime)
        {
            SoundMan.Play(this.pSnd);
            TimerEventMan.Add(TimerEvent.Name.UFOMoveSnd, this, deltaTime, 1);
        }

        // Data: ---------------
        Sound pSnd;
    }
}

// --- End of File ---
