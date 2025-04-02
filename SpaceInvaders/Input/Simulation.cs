//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class Simulation
    {
        public enum State
        {
            Realtime,
            FixedStep,
            SingleStep,
            Pause
        };

        // singleton access
        private static Simulation privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new Simulation();
            }
        }

        public static void Update(float systemTime)
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            // update input
            pSim.privProcessInput();

            // Time update.
            //      Get the time that has passed.
            //      Feels backwards, but its not, need to see how much time has passed
            pSim.stopWatch_toc = systemTime - pSim.stopWatch_tic;
            pSim.stopWatch_tic = systemTime;

            if (pSim.privGetState() == State.FixedStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
            }
            else if (pSim.privGetState() == State.Realtime)
            {
                pSim.timeStep = pSim.stopWatch_toc;
            }
            else if (pSim.privGetState() == State.SingleStep)
            {
                pSim.timeStep = SIM_SINGLE_TIME_STEP;
                pSim.privSetState(State.Pause);
            }
            else //  pSim->getState() == SimulationEnum::Pause
            {
                pSim.timeStep = 0.0f;
            }

            pSim.totalWatch += pSim.timeStep;

        }


        // --- Simulation controls ------------
        //   S - single step
        //   D - repeat step while holding
        //   G - start simulation fixed step
        //   H - start simulation realtime stepping
        private void privProcessInput()
        {
            // ------------------------------------------------------------------
            //  SIMULATION Controls 
            // ------------------------------------------------------------------
            if (this.isLocked)
            {
                return;
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_G) == true)
            {
                this.privSetState(State.FixedStep);
            }
            else if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_H) == true)
            {
                this.privSetState(State.Realtime);
            }
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_S) && (oldKey == false))
            {
                // Do only once "a single step"
                this.privSetState(State.SingleStep);
            }
            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_D) == true)
            {
                // repeating "a single step"
                this.privSetState(State.SingleStep);
            }

            oldKey = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_S);

        }

        // Get / Set state

        private void privSetState(State simState)
        {
            this.state = simState;
        }
        private State privGetState()
        {
            return this.state;
        }
        public static void SetState(State simState)
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            pSim.privSetState(simState);
        }
        public static State GetState()
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            return pSim.privGetState();
        }
        public static float GetTimeStep()
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);
            return pSim.timeStep;
        }
        public static float GetTotalTime()
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);
            return pSim.totalWatch;
        }

        public static void LockSimulationInput()
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            pSim.isLocked = true;
        }

        public static void UnlockSimulationInput()
        {
            Simulation pSim = Simulation.privGetInstance();
            Debug.Assert(pSim != null);

            pSim.isLocked = false;
        }

        // cannot create without going through singleton
        private Simulation()
        {
            this.state = State.SingleStep;

            this.timeStep = 0.0f;
            this.totalWatch = 0.0f;
            this.stopWatch_tic = 0.0f;
            this.stopWatch_toc = 0.0f;
            this.isLocked = false;
        }

        // ----------------------------------------------
        // data:
        // ----------------------------------------------

        private static Simulation pInstance;

        private State state;

        private float stopWatch_tic;
        private float stopWatch_toc;
        private float totalWatch;
        private float timeStep;

        private const int SIM_NUM_WAKE_CYCLES = 0;
        private const float SIM_SINGLE_TIME_STEP = 0.016666f;

        private static bool oldKey = false;

        private bool isLocked;
    }
}

// --- End of File ---
