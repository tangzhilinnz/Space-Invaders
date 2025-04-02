//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShipMissileReady : ShipMissileState
    {
        Sound pShotSnd = null;

        public ShipMissileReady()
        {
            this.pShotSnd = SoundMan.Find(Sound.Name.ShipShot);
            Debug.Assert(this.pShotSnd != null);
            this.pShotSnd.SetVolume(1.0f);
        }

        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.MissileState.Flying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();
            pMissile.SetPos(pShip.x, pShip.y + 20);
            this.Handle(pShip);

            SoundMan.Play(this.pShotSnd);
        }

    }
}// --- End of File ---
