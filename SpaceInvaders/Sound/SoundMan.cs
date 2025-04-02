using IrrKlang;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SE456
{
    public class SoundMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private SoundMan(int _reserveNum, int _reserveGrow)
            : base(new SLinkMan(), new SLinkMan(), _reserveNum, _reserveGrow)
        {
            // Initialize the sound engine
            pSndEngine = new IrrKlang.ISoundEngine();
            Debug.Assert(pSndEngine != null);

            // Initialize the sound compare object
            SoundMan.psSoundCompare = new Sound();
            Debug.Assert(SoundMan.psSoundCompare != null);
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int _reserveNum = 10, int _reserveGrow = 1)
        {
            // make sure values are reasonable 
            Debug.Assert(_reserveNum >= 0);
            Debug.Assert(_reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(SoundMan.psInstance == null);

            // Do the initialization
            if (SoundMan.psInstance == null)
            {
                SoundMan.psInstance = new SoundMan(_reserveNum, _reserveGrow);
            }

            // Pre-load the sounds
            SoundMan.Add(Sound.Name.FastInvader1, "fastinvader1.wav");
            SoundMan.Add(Sound.Name.FastInvader2, "fastinvader2.wav");
            SoundMan.Add(Sound.Name.FastInvader3, "fastinvader3.wav");
            SoundMan.Add(Sound.Name.FastInvader4, "fastinvader4.wav");
            SoundMan.Add(Sound.Name.ShipShot, "invaderkilled.wav");
            SoundMan.Add(Sound.Name.UFOMove, "ufo_highpitch.wav");
            SoundMan.Add(Sound.Name.AlienKilled, "shoot.wav");
            SoundMan.Add(Sound.Name.UFOKilled, "ufo_lowpitch.wav");
            SoundMan.Add(Sound.Name.ShipKilled, "explosion.wav");

            // Set initial volume for FastInvader1
            Sound pSound = SoundMan.Find(Sound.Name.ShipShot);
            Debug.Assert(pSound != null);
            SoundMan.Play(pSound, false, 0.0f);
        }

        public static void Destroy(bool bPrintEnable = false)
        {
            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Clean up the sound engine
            if (pMan.pSndEngine != null)
            {
                pMan.pSndEngine.Dispose();
                pMan.pSndEngine = null;
            }

            // print stats on destroy
            if (bPrintEnable)
            {
                SoundMan.DumpStats();
            }

            // invalidate the singleton
            SoundMan.psInstance = null;
        }

        public static Sound Add(Sound.Name _name, string _fileName)
        {
            Debug.Assert(_name != Sound.Name.Uninitialized);
            Debug.Assert(!string.IsNullOrEmpty(_fileName));

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.pSndEngine != null);

            Sound pSound = (Sound)pMan.baseAdd();
            Debug.Assert(pSound != null);

            // Initialize the sound
            IrrKlang.ISoundSource pSource = pMan.pSndEngine.AddSoundSourceFromFile(_fileName);
            Debug.Assert(pSource != null);

            pSound.Set(_name, pSource);
            return pSound;
        }

        public static Sound Find(Sound.Name _name)
        {
            Debug.Assert(_name != Sound.Name.Uninitialized);

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Use the Compare Sound as a reference
            SoundMan.psSoundCompare.mName = _name;

            Sound pData = (Sound)pMan.baseFind(SoundMan.psSoundCompare);
            return pData;
        }

        public static void Remove(Sound _pSound)
        {
            Debug.Assert(_pSound != null);

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(_pSound);
        }

        public static void Dump()
        {
            Debug.WriteLine("\n   ------ Sound Man: ------");

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ Sound Man: ------");

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Sound();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }
        public static void Play(Sound pSound, bool looped = false, float volumn = 0.2f)
        {
            Debug.Assert(pSound != null);
            Debug.Assert(pSound.pSndSource != null);

            SoundMan pMan = SoundMan.privGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pMan.pSndEngine != null);

            pMan.pSndEngine.SoundVolume = volumn;
            pSound.soundInstance = pMan.pSndEngine.Play2D(pSound.pSndSource, looped, false, false);
            pSound.isPlaying = true;
        }

        public static void Update()
        {
            SoundMan pMan = SoundMan.privGetInstance();
            if (pMan != null && pMan.pSndEngine != null)
            {
                // Updates 3D positions and other engine states
                pMan.pSndEngine.Update();
            }
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SoundMan privGetInstance()
        {
            // Safety - this forces users to call Create() first
            Debug.Assert(psInstance != null);

            return psInstance;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private IrrKlang.ISoundEngine pSndEngine;
        private static Sound psSoundCompare;
        private static SoundMan psInstance = null;
    }
}