//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class Command
    {
        // define this in concrete
        abstract public void Execute(float deltaTime);
    }
}

// --- End of File ---
