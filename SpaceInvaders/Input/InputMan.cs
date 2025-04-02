//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class InputMan
    {
        private InputMan()
        {
            this.poSubjectCollisionBox = new InputSubject();
            this.poSubjectArrowLeft = new InputSubject();
            this.poSubjectArrowRight = new InputSubject();
            this.poSubjectSpace = new InputSubject();

            this.privSpaceKeyPrev = false;

            this.isLocked = false;
        }

        private static InputMan privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputMan();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static InputSubject GetCollisionBoxSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            return pMan.poSubjectCollisionBox;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            return pMan.poSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            return pMan.poSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputMan pMan = InputMan.privGetInstance();
            return pMan.poSubjectSpace;
        }

        public static void Update()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            if (pMan.isLocked)
            {
                return;
            }

            // SpaceKey: (with key history)
            bool spaceKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_SPACE);
            // TKey: (with key history)
            bool tKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_T);

            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.poSubjectSpace.Notify();
            }

            if (tKeyCurr == true && pMan.privTKeyPrev == false)
            {
                pMan.poSubjectCollisionBox.Notify();
            }

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_LEFT) == true)
            {
                pMan.poSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_RIGHT) == true)
            {
                pMan.poSubjectArrowRight.Notify();
            }

            pMan.privSpaceKeyPrev = spaceKeyCurr;
            pMan.privTKeyPrev = tKeyCurr;

        }

        public static void LockInput()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.isLocked = true;
        }

        public static void UnlockInput()
        {
            InputMan pMan = InputMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.isLocked = false;
        }

        // Data: ----------------------------------------------
        private static InputMan pInstance = null;
        private bool privSpaceKeyPrev;
        private bool privTKeyPrev;

        private InputSubject poSubjectCollisionBox;
        private InputSubject poSubjectArrowRight;
        private InputSubject poSubjectArrowLeft;
        private InputSubject poSubjectSpace;

        private bool isLocked;
    }
}

// --- End of File ---
