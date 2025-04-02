//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SE456
{
    class GlobalTimer
    {

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GlobalTimer()
        {
            this.mCurrTime = 0.0f;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------

        public static void Update(float time)
        {
            GlobalTimer pTimer = GlobalTimer.privGetInstance();
            pTimer.mCurrTime = time;
        }

        public static float GetTime()
        {
            GlobalTimer pTimer = GlobalTimer.privGetInstance();
            return pTimer.mCurrTime;
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GlobalTimer privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GlobalTimer();
            }

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static GlobalTimer pInstance = null;
        protected float mCurrTime;
    }
}

// --- End of File ---
