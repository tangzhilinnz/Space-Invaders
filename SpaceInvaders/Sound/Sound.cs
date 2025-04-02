using System;
using System.Diagnostics;

namespace SE456
{
    public class Sound : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            FastInvader1,
            FastInvader2,
            FastInvader3,
            FastInvader4,
            ShipShot,
            UFOMove,
            AlienKilled,
            UFOKilled,
            ShipKilled,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public Sound()
            : base()
        {
            this.mName = Name.Uninitialized;
            this.pSndSource = null;
            this.volume = 1.0f;
            this.isPlaying = false;
            this.soundInstance = null;
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(Name _name, IrrKlang.ISoundSource _pSndSource)
        {
            Debug.Assert(_pSndSource != null);
            Debug.Assert(_name != Name.Uninitialized);

            this.mName = _name;
            this.pSndSource = _pSndSource;
            this.pSndSource.DefaultVolume = this.volume;
        }

        public void Stop()
        {
            if (this.soundInstance != null && !this.soundInstance.Finished)
            {
                this.soundInstance.Stop();
                this.isPlaying = false;
            }
        }

        public void SetVolume(float _volume)
        {
            Debug.Assert(_volume >= 0.0f && _volume <= 1.0f);
            this.volume = _volume;

            if (this.pSndSource != null)
            {
                this.pSndSource.DefaultVolume = _volume;
            }

            if (this.soundInstance != null)
            {
                this.soundInstance.Volume = _volume;
            }
        }

        public float GetVolume()
        {
            return this.volume;
        }

        public bool IsPlaying()
        {
            return this.isPlaying && (this.soundInstance != null && !this.soundInstance.Finished);
        }

        //------------------------------------
        // Override
        //------------------------------------
        override public Enum GetName()
        {
            return this.mName;
        }

        override public void Wash()
        {
            this.Stop();

            this.mName = Name.Uninitialized;
            this.pSndSource = null;
            this.volume = 1.0f;
            this.isPlaying = false;
            this.soundInstance = null;

            base.baseWash();
        }

        override public void Dump()
        {
            // Using HASH code as unique identifier
            Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());

            if (this.pSndSource == null)
            {
                Debug.WriteLine("      SoundSource: null");
            }
            else
            {
                Debug.WriteLine("      SoundSource: {0}", this.pSndSource.Name);
            }

            Debug.WriteLine("      Volume: {0}", this.volume);
            Debug.WriteLine("      Status: {0}", this.isPlaying ? "Playing" : "Stopped");

            base.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name mName;
        public IrrKlang.ISoundSource pSndSource;
        public IrrKlang.ISound soundInstance;
        public float volume;
        public bool isPlaying;
    }
}

// --- End of File ---
