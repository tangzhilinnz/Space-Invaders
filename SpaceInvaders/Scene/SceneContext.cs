//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;

namespace SE456
{
    public class SceneContext
    {
        public enum Scene
        {
            Select,
            Play,
            Over
        }
        public SceneContext()
        {
            // reserve the states
            this.poSceneSelect = new SceneSelect();
            this.poScenePlay = new ScenePlay();
            this.poSceneOver = new SceneOver();

            // initialize to the select state
            this.pSceneState = this.poSceneSelect;
            this.pSceneState.Entering();
        }

        public SceneState GetState()
        {
            return this.pSceneState;
        }
        public void SetState(Scene eScene)
        {
            switch (eScene)
            {
                case Scene.Select:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poSceneSelect;
                    this.pSceneState.Entering();
                    break;

                case Scene.Play:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poScenePlay;
                    this.pSceneState.Entering();
                    break;

                case Scene.Over:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poSceneOver;
                    this.pSceneState.Entering();
                    break;
            }
        }


        // ----------------------------------------------------
        // Data: 
        // -------------------------------------------o---------
        SceneState pSceneState;
        SceneSelect poSceneSelect;
        SceneOver poSceneOver;
        ScenePlay poScenePlay;

    }
}

// --- End of File ---
