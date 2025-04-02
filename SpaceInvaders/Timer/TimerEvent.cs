//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    //---------------------------------------------------------------
    public class TimerEvent : DLink
    {
        //-----------------------------------------------------------
        // Enum
        //-----------------------------------------------------------
        public enum Name
        {
            Sample1,
            Sample2,
            RepeatSample,

            AliensAnimation,
            AliensAnimation1,
            AliensAnimation2,
            AliensAnimation3,
            Animation,
            GridMove,
            SndMove,
            UFOMoveSnd,
            BombFallingSnd,

            BombSpawn,
            ExplosionSpawn,
            UFOScoreSpawn,
            UFOSpawn,

            SceneOver,
            NextLevel,

            ShipResurrection,

            TimedCharacter,

            Uninitialized
        }

        //-----------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------
        public TimerEvent()
            : base()
        {
            this.name = TimerEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        //------------------------------------------------------------
        // Methods
        //------------------------------------------------------------

        public void Set(TimerEvent.Name _eventName, 
                        Command _pCommand, 
                        float _deltaTimeToTrigger)
        {
            Debug.Assert(_pCommand != null);

            this.name = _eventName;
            this.pCommand = _pCommand;
            this.deltaTime = _deltaTimeToTrigger;

            // set the trigger time
            this.triggerTime = TimerEventMan.GetCurrTime() + _deltaTimeToTrigger;
        }
        public void Process()
        {
            // make sure the command is valid
            Debug.Assert(this.pCommand != null);
            // fire off command
            this.pCommand.Execute(deltaTime);
        }

		//------------------------------------
		// Override
		//------------------------------------
		public override System.Enum GetName()
        {
            return this.name;
        }
		override public void Wash()
		{
			this.name = TimerEvent.Name.Uninitialized;
			this.pCommand = null;
			this.triggerTime = 0.0f;
			this.deltaTime = 0.0f;

            base.baseWash();
		}
		override public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());

            // Data:
            Debug.WriteLine("      Command: {0}", this.pCommand);
            Debug.WriteLine("   Event Name: {0}", this.name);
            Debug.WriteLine(" Trigger Time: {0}", this.triggerTime);
            Debug.WriteLine("   Delta Time: {0}", this.deltaTime);

            base.baseDump();
        }

        override public bool LessThanByPriority(NodeBase _pNodeBaseB)
        {
            TimerEvent pNodeBaseB = (TimerEvent)_pNodeBaseB;
            return this.triggerTime < pNodeBaseB.triggerTime;
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;
    }
}

// --- End of File ---
