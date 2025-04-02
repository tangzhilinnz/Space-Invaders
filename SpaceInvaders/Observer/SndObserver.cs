//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    public class SndObserver : ColObserver
    {
        public SndObserver(Sound sound)
        {
            this.pSound = sound;
            Debug.Assert(this.pSound != null);
        }
        public override void Notify()
        {
            //Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            this.pSound.SetVolume(1.0f);
            SoundMan.Play(this.pSound);
        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.SoundObserver;
        }

        // Data
        Sound pSound;
    }
}

// --- End of File ---
