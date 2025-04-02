//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract class InputObserver : SLink
    {
        public enum Name
        {
            CollisionBoxObserver,
            MoveLeftObserver,
            MoveRightObserver,
            ShootObserver,
            Uninitialized
        }

        // define this in concrete
        abstract public void Notify();

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public InputSubject pSubject;
    }
}

// --- End of File ---
