//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class FallStrategy
    {
        abstract public void Fall(Bomb pBomb);
        abstract public void Reset(float posY);

    }
}

// --- End of File ---
