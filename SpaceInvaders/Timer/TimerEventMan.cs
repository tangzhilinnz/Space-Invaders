//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class TimerEventMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public TimerEventMan(int reserveNum = 1, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   
        {
            // initialize derived data here
            this.poNodeCompare = new TimerEvent();
            this.mCurrTime = 0.0f;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(TimerEventMan.pInstance == null);

            Debug.Assert(TimerEventMan.pInstanceExp == null);

            // Do the initialization
            if (TimerEventMan.pInstance == null)
            {
                TimerEventMan.pInstance = new TimerEventMan();
            }
            if (TimerEventMan.pInstanceExp == null)
            {
                TimerEventMan.pInstanceExp = new TimerEventMan();
            }
        }
        public static void Destroy(bool bPrintEnable = false)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance(0);
            Debug.Assert(pMan != null);

            if (bPrintEnable)
            {
                TimerEventMan.DumpStats(0);
            }

            pMan = TimerEventMan.privGetInstance(1);
            Debug.Assert(pMan != null);
            if (bPrintEnable)
            {
                TimerEventMan.DumpStats(1);
            }
        }

        public static TimerEvent Add(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger, int i = 0)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            TimerEvent pNode = (TimerEvent)pMan.baseAdd(false);
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            pMan.baseInsertWithPriority(pNode);
            return pNode;
        }

        public static TimerEvent Find(TimerEvent.Name name, int i = 0)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            TimerEvent pData = (TimerEvent)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(TimerEvent pImage, int i = 0)
        {
            //Debug.Assert(pImage != null);
            if (pImage == null)
            {
                return;
            }

            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            pMan.baseRemove(pImage);
            Iterator pIt = pMan.baseGetIterator();
            pIt.First();
        }

        public static void RemoveAllEvents(int i = 0)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            // walk through the list and execute
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                // remove from list
                pIt.Erase(pMan);

            }
        }

        public static void Dump(int i = 0)
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats(int i = 0)
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }

        public static void PauseUpdate(float delta, int i = 0)
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            // walk the list
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // Update the times
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                TimerEvent pEvent = (TimerEvent)pIt.Current();
                pEvent.triggerTime += delta;
            }
        }

        public static void Update(float totalTime, int i = 0)
        {
            // Debug.WriteLine("Time: {0}", totalTime);
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk through the list and execute
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            TimerEvent pNode = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted then its an early out
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pNode = (TimerEvent)pIt.Current();
                if (pMan.mCurrTime >= pNode.triggerTime)
                {
                    // call it
                    pNode.Process();

                    // remove from list
                    pIt.Erase(pMan);
                }
                else
                {
                    break;
                }
            }

        }

        public static float GetCurrTime(int i = 0)
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance(i);
            Debug.Assert(pMan != null);

            // return time
            return pMan.mCurrTime;
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerEventMan privGetInstance(int i = 0)
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);
            Debug.Assert(i == 0 || i == 1);

            if (i == 0)
                return TimerEventMan.pInstance;
            else
                return TimerEventMan.pInstanceExp;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new TimerEvent();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly TimerEvent poNodeCompare;
        private static TimerEventMan pInstance = null;

        private static TimerEventMan pInstanceExp = null;

        protected float mCurrTime;
    }
}

// --- End of File ---

