using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    class SndMoveCmd : Command
    {
        public SndMoveCmd(Sound _pSnd0,
                          Sound _pSnd1,
                          Sound _pSnd2,
                          Sound _pSnd3,
                          GameObject _pGrid)
        {
            this.pSnd0 = _pSnd0;
            this.pSnd1 = _pSnd1;
            this.pSnd2 = _pSnd2;
            this.pSnd3 = _pSnd3;
            Debug.Assert(this.pSnd0 != null);
            Debug.Assert(this.pSnd1 != null);
            Debug.Assert(this.pSnd2 != null);
            Debug.Assert(this.pSnd3 != null);

            this.pSnd0.SetVolume(1.0f);
            this.pSnd1.SetVolume(1.0f);
            this.pSnd2.SetVolume(1.0f);
            this.pSnd3.SetVolume(1.0f);

            this.count = 0;
            this.pGrid = _pGrid;
        }

        public override void Execute(float deltaTime)
        {
            AlienGrid pAlienGrid = (AlienGrid)this.pGrid;
            int curNum = pAlienGrid.GetNumAliens();

            float newDeltaTime = 0.05f + (0.7f - 0.05f) * curNum / 55;

            if (curNum == 0)
            {
                return;
            }

            switch(this.count)
            {
                case 0:
                    SoundMan.Play(this.pSnd0);
                    break;
                case 1:
                    SoundMan.Play(this.pSnd1);
                    break;
                case 2:
                    SoundMan.Play(this.pSnd2);
                    break;
                case 3:
                    SoundMan.Play(this.pSnd3);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            this.count++;

            if (this.count > 3)
            {
                this.count = 0;
            }

            // Add itself back to timer
            TimerEventMan.Add(TimerEvent.Name.SndMove, this, newDeltaTime);
        }

        // Data: ---------------
        Sound pSnd0;
        Sound pSnd1;
        Sound pSnd2;
        Sound pSnd3;
        private GameObject pGrid;
        int count;
    }
}

// --- End of File ---
