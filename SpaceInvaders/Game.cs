//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;


namespace SE456
{
    public class SpaceInvaders : Azul.Game
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Final Project");
            this.SetWidthHeight(672, 768);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Setup Managers - once here
            //---------------------------------------------------------------------------------------------------------

            Simulation.Create();
            TextureMan.Create();
            ImageMan.Create();
            SpriteGameMan.Create();

            //---------------------------------------------------------------------------------------------------------
            // REWORK - SpriteBatchMan
            //    It's a singleton... but it holds an actual instance pointer to the SpriteBatchMan instance
            //    this way scene can own their unique sprite batch man independent of other scenes
            //
            // SpriteBatchMan.Create(3, 1); 
            //                    
            // Once you understand this... you have to do this for many other systems
            //---------------------------------------------------------------------------------------------------------

            SpriteBatchMan.Create();
            TimerEventMan.Create();
            SpriteBoxMan.Create();
            SpriteGameProxyMan.Create();
            SpriteBoxProxyMan.Create();
            GameObjectNodeMan.Create();
            ColPairMan.Create();
            GlyphMan.Create();
            FontMan.Create();
            GhostMan.Create();
            SoundMan.Create();

            //pSceneContext = new SceneContext();
            ShipMan.Create();
            SceneContextMan.Create();
            SceneContextMan.Set(new SceneContext());

            this.ResetTime();

        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {

            GlobalTimer.Update(this.GetTime());

            // Update the scene
            SceneContextMan.Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            SceneContextMan.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            SceneContextMan.Destroy();
            SoundMan.Destroy();
            GhostMan.Destroy();
            FontMan.Destroy();
            GlyphMan.Destroy();
            ColPairMan.Destroy();
            GameObjectNodeMan.Destroy();
            SpriteBoxProxyMan.Destroy();
            SpriteGameProxyMan.Destroy();
            TimerEventMan.Destroy();
            SpriteBoxMan.Destroy();
            SpriteBatchMan.Destroy();
            SpriteGameMan.Destroy();
            ImageMan.Destroy();
            TextureMan.Destroy();
        }

        public override void DisplayHeader()
        {
            Console.Write(this.Header());
        }

        public override void DisplayFooter()
        {
            Console.Write(this.Footer());
        }

    }
}

// --- End of File ---
