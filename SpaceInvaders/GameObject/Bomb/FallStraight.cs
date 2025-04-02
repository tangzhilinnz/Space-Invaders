//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class FallStraight : FallStrategy
    {
        public FallStraight()
        {
            this.oldPosY = 0.0f;
        }

        public override void Reset(float posY)
        {
            this.oldPosY = posY;
        }

        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
        }

        // Data
        private float oldPosY;
    }
}

// --- End of File ---
