//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SE456
{
    public class SceneSelect : SceneState
    {
        public SceneSelect()
        {
            this.Initialize();
            this.Type = SceneContext.Scene.Select;
            this.pre2Key = false;
        }

        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);

            this.poGameObjectNodeMan = new GameObjectNodeMan(3, 1);
            GameObjectNodeMan.SetActive(this.poGameObjectNodeMan);

            this.poGhostManMan = new GhostMan(3, 1);
            GhostMan.SetActive(this.poGhostManMan);

            this.pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            this.pSB_Boxes = SpriteBatchMan.Add(SpriteBatch.Name.SpriteBox_Batch);
            this.pSB_Boxes.Disable();


            //-----------------------------------
            // Load the Textures
            //-----------------------------------
            TextureMan.Add(Texture.Name.SpaceInvaders, "SpaceInvaders_ROM.t.azul");
            TextureMan.Add(Texture.Name.Birds, "Birds_N_Shield.t.azul");

            //---------------------------------------------------------------------------------------------------------
            // Create Glyphs
            //---------------------------------------------------------------------------------------------------------
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 65, Texture.Name.SpaceInvaders, 3, 36, 5, 8);   // .A
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 66, Texture.Name.SpaceInvaders, 11, 36, 5, 8);  // .B
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 67, Texture.Name.SpaceInvaders, 19, 36, 5, 8);  // .C
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 68, Texture.Name.SpaceInvaders, 27, 36, 5, 8);  // .D
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 69, Texture.Name.SpaceInvaders, 35, 36, 5, 8);  // .E
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 70, Texture.Name.SpaceInvaders, 43, 36, 5, 8);  // .F
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 71, Texture.Name.SpaceInvaders, 51, 36, 5, 8);  // .G
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 72, Texture.Name.SpaceInvaders, 59, 36, 5, 8);  // .H
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 73, Texture.Name.SpaceInvaders, 67, 36, 5, 8);  // .I
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 74, Texture.Name.SpaceInvaders, 75, 36, 5, 8);  // .J
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 75, Texture.Name.SpaceInvaders, 83, 36, 5, 8);  // .K
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 76, Texture.Name.SpaceInvaders, 91, 36, 5, 8);  // .L
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 77, Texture.Name.SpaceInvaders, 99, 36, 5, 8);  // .M
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 78, Texture.Name.SpaceInvaders, 3, 46, 5, 8);   // .N
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 79, Texture.Name.SpaceInvaders, 11, 46, 5, 8);  // .O
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 80, Texture.Name.SpaceInvaders, 19, 46, 5, 8);  // .P
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 81, Texture.Name.SpaceInvaders, 27, 46, 5, 8);  // .Q
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 82, Texture.Name.SpaceInvaders, 35, 46, 5, 8);  // .R
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 83, Texture.Name.SpaceInvaders, 43, 46, 5, 8);  // .S
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 84, Texture.Name.SpaceInvaders, 51, 46, 5, 8);  // .T
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 85, Texture.Name.SpaceInvaders, 59, 46, 5, 8);  // .U
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 86, Texture.Name.SpaceInvaders, 67, 46, 5, 8);  // .V
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 87, Texture.Name.SpaceInvaders, 75, 46, 5, 8);  // .W
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 88, Texture.Name.SpaceInvaders, 83, 46, 5, 8);  // .X
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 89, Texture.Name.SpaceInvaders, 91, 46, 5, 8);  // .Y
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 90, Texture.Name.SpaceInvaders, 99, 46, 5, 8);  // .Z
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 48, Texture.Name.SpaceInvaders, 3, 56, 5, 8);   // 0
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 49, Texture.Name.SpaceInvaders, 11, 56, 5, 8);  // 1
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 50, Texture.Name.SpaceInvaders, 19, 56, 5, 8);  // 2
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 51, Texture.Name.SpaceInvaders, 27, 56, 5, 8);  // 3
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 52, Texture.Name.SpaceInvaders, 35, 56, 5, 8);  // 4
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 53, Texture.Name.SpaceInvaders, 43, 56, 5, 8);  // 5
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 54, Texture.Name.SpaceInvaders, 51, 56, 5, 8);  // 6
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 55, Texture.Name.SpaceInvaders, 59, 56, 5, 8);  // 7
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 56, Texture.Name.SpaceInvaders, 67, 56, 5, 8);  // 8
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 57, Texture.Name.SpaceInvaders, 75, 56, 5, 8);  // 9
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 60, Texture.Name.SpaceInvaders, 83, 56, 5, 8);  // <
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 62, Texture.Name.SpaceInvaders, 91, 56, 5, 8);  // >
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 32, Texture.Name.SpaceInvaders, 99, 56, 1, 8);  // Space
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 61, Texture.Name.SpaceInvaders, 107, 56, 5, 8); // =
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 42, Texture.Name.SpaceInvaders, 115, 56, 5, 8); // *
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 63, Texture.Name.SpaceInvaders, 123, 56, 5, 8); // ?
            GlyphMan.Add(Glyph.Name.SpaceInvaders, 45, Texture.Name.SpaceInvaders, 131, 56, 5, 8); // -


            //-----------------------------------
            // Create Images
            //-----------------------------------
            // --- SpaceInvaders ---
            ImageMan.Add(Image.Name.OctopusAAlien, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageMan.Add(Image.Name.OctopusBAlien, Texture.Name.SpaceInvaders, 18, 3, 12, 8);
            ImageMan.Add(Image.Name.CrabAAlien, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.CrabBAlien, Texture.Name.SpaceInvaders, 47, 3, 11, 8);
            ImageMan.Add(Image.Name.SquidAAlien, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.SquidBAlien, Texture.Name.SpaceInvaders, 72, 3, 8, 8);

            ImageMan.Add(Image.Name.Missile, Texture.Name.SpaceInvaders, 3, 29, 1, 4);
            ImageMan.Add(Image.Name.Ship, Texture.Name.SpaceInvaders, 3, 14, 13, 8);
            ImageMan.Add(Image.Name.Wall, Texture.Name.Birds, 40, 185, 20, 10);

            ImageMan.Add(Image.Name.Brick, Texture.Name.Birds, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Birds, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Birds, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Birds, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Birds, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Birds, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Birds, 55, 215, 10, 5);

            ImageMan.Add(Image.Name.BombZigZag_A, Texture.Name.SpaceInvaders, 18, 26, 3, 7);
            ImageMan.Add(Image.Name.BombZigZag_B, Texture.Name.SpaceInvaders, 24, 26, 3, 7);
            ImageMan.Add(Image.Name.BombZigZag_C, Texture.Name.SpaceInvaders, 30, 26, 3, 7);
            ImageMan.Add(Image.Name.BombZigZag_D, Texture.Name.SpaceInvaders, 36, 26, 3, 7);
            ImageMan.Add(Image.Name.BombCross_A, Texture.Name.SpaceInvaders, 42, 27, 3, 6);
            ImageMan.Add(Image.Name.BombCross_B, Texture.Name.SpaceInvaders, 48, 27, 3, 6);
            ImageMan.Add(Image.Name.BombCross_C, Texture.Name.SpaceInvaders, 54, 27, 3, 6);
            ImageMan.Add(Image.Name.BombCross_D, Texture.Name.SpaceInvaders, 60, 27, 3, 6);
            ImageMan.Add(Image.Name.BombStraight_A, Texture.Name.SpaceInvaders, 64, 26, 5, 7);
            ImageMan.Add(Image.Name.BombStraight_B, Texture.Name.SpaceInvaders, 70, 26, 3, 7);
            ImageMan.Add(Image.Name.BombStraight_C, Texture.Name.SpaceInvaders, 74, 26, 5, 7);
            ImageMan.Add(Image.Name.BombStraight_D, Texture.Name.SpaceInvaders, 80, 26, 3, 7);

            ImageMan.Add(Image.Name.ExplosionShipA, Texture.Name.SpaceInvaders, 19, 14, 16, 8);
            ImageMan.Add(Image.Name.ExplosionShipB, Texture.Name.SpaceInvaders, 38, 14, 16, 8);
            ImageMan.Add(Image.Name.ExplosionShipShot, Texture.Name.SpaceInvaders, 7, 25, 8, 8);
            ImageMan.Add(Image.Name.ExplosionAlien, Texture.Name.SpaceInvaders, 83, 3, 13, 8);
            ImageMan.Add(Image.Name.ExplosionAlienShot, Texture.Name.SpaceInvaders, 86, 25, 6, 8);
            ImageMan.Add(Image.Name.ExplosionUFO, Texture.Name.SpaceInvaders, 118, 3, 21, 8);

            ImageMan.Add(Image.Name.UFO, Texture.Name.SpaceInvaders, 99, 3, 16, 8);

            //----------------------------------
            // Create Sprites
            //----------------------------------
            // --- SpaceInvaders ---
            SpriteGameMan.Add(SpriteGame.Name.OctopusAlien, Image.Name.OctopusAAlien, 0, 0, 36, 25);
            SpriteGameMan.Add(SpriteGame.Name.CrabAlien, Image.Name.CrabAAlien, 0, 0, 28, 25);
            SpriteGameMan.Add(SpriteGame.Name.SquidAlien, Image.Name.SquidAAlien, 0, 0, 24, 25);
            SpriteGameMan.Add(SpriteGame.Name.Ship, Image.Name.Ship, 0, 0, 39, 24);
            SpriteGameMan.Find(SpriteGame.Name.Ship).SwapColor(0.0f, 1.0f, 0.0f);
            SpriteGameMan.Add(SpriteGame.Name.Missile, Image.Name.Missile, 0, 0, 3, 12);
            SpriteGameMan.Add(SpriteGame.Name.Wall, Image.Name.Wall, 0, 0, 672, 3);
            SpriteGameMan.Add(SpriteGame.Name.BombZigZag, Image.Name.BombZigZag_A, 0, 0, 9, 21);
            SpriteGameMan.Add(SpriteGame.Name.BombStraight, Image.Name.BombStraight_A, 0, 0, 9, 21);
            SpriteGameMan.Add(SpriteGame.Name.BombCross, Image.Name.BombCross_A, 0, 0, 9, 18);
            SpriteGameMan.Add(SpriteGame.Name.BombRolling, Image.Name.BombStraight_A, 0, 0, 9, 18);

            float brickWidth = 10.0f;
            float brickHeight = 5.0f;

            SpriteGameMan.Add(SpriteGame.Name.Brick, Image.Name.Brick, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, brickWidth, brickHeight);
            SpriteGameMan.Add(SpriteGame.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, brickWidth, brickHeight);


            SpriteGameMan.Add(SpriteGame.Name.ExplosionShip, Image.Name.ExplosionShipB, 0, 0, 40, 20);
            SpriteGameMan.Add(SpriteGame.Name.ExplosionShipShot, Image.Name.ExplosionShipShot, 0, 0, 22, 22);
            SpriteGameMan.Add(SpriteGame.Name.ExplosionAlien, Image.Name.ExplosionAlien, 0, 0, 36, 25);
            SpriteGameMan.Add(SpriteGame.Name.ExplosionAlienShot, Image.Name.ExplosionAlienShot, 0, 0, 16, 22);
            SpriteGameMan.Add(SpriteGame.Name.ExplosionUFO, Image.Name.ExplosionUFO, 0, 0, 60, 28);

            SpriteGameMan.Add(SpriteGame.Name.UFO, Image.Name.UFO, 0, 0, 45, 25);

            // --- top fonts ---
            Font pFont;
            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "S  C  O  R  E  <  1  >", Glyph.Name.SpaceInvaders, 26, 740, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "H  I  -  S  C  O  R  E", Glyph.Name.SpaceInvaders, 265, 740, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "S  C  O  R  E  <  2  >", Glyph.Name.SpaceInvaders, 503, 740, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.Player1_Score, SpriteBatch.Name.Texts, "0  0  0  0", Glyph.Name.SpaceInvaders, 52, 700, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.HI_Score, SpriteBatch.Name.Texts, "0  0  0  0", Glyph.Name.SpaceInvaders, 302, 700, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Player2_Score, SpriteBatch.Name.Texts, "0  0  0  0", Glyph.Name.SpaceInvaders, 557, 700, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "P  L  A  Y", Glyph.Name.SpaceInvaders, 320, 580, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "S  P  A  C  E                I  N  V  A  D  E  R  S",
                Glyph.Name.SpaceInvaders, 200, 500, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "*  S  C  O  R  E        A  D  V  A  N  C  E        T  A  B  L  E  *",
                Glyph.Name.SpaceInvaders, 130, 400, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "=    ?        M  Y  S  T  E  R  Y",
                Glyph.Name.SpaceInvaders, 280, 350, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "=    3  0        P  O  I  N  T  S",
                Glyph.Name.SpaceInvaders, 280, 300, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "=    2  0        P  O  I  N  T  S",
                Glyph.Name.SpaceInvaders, 280, 250, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "=    1  0        P  O  I  N  T  S",
                Glyph.Name.SpaceInvaders, 280, 200, 3.0f, 3.0f);
            pFont.SetColor(0.2f, 0.8f, 0.2f);
            pFont = FontMan.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "P  R  E  S  S        2", Glyph.Name.SpaceInvaders, 25, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.0f);
            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "C  R  E  D  I  T", Glyph.Name.SpaceInvaders, 475, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "0  0", Glyph.Name.SpaceInvaders, 612, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            AlienFactory SIF = new AlienFactory(SpriteBatch.Name.Texts, SpriteBatch.Name.SpriteBox_Batch);

            UFO pUFO = new UFO(GameObject.Name.UFO, SpriteGame.Name.UFO, 250.0f, 350.0f, 0.0f, 0);
            pUFO.pSpriteProxy.SetColor(0.9f, 0.9f, 0.9f);
            pUFO.ActivateCollisionSprite(this.pSB_Boxes);
            pUFO.ActivateSprite(this.pSB_Texts);
            pUFO.SetDelta(0.0f);

            AlienColumn pColumn = (AlienColumn)SIF.Create(AlienCategory.Type.Column);
            GameObjectNodeMan.Attach(pColumn);

            pColumn.Add(pUFO);
            pColumn.Add(SIF.Create(AlienCategory.Type.Squid, 250.0f, 300.0f));
            pColumn.Add(SIF.Create(AlienCategory.Type.Crab, 250.0f, 250.0f));
            pColumn.Add(SIF.Create(AlienCategory.Type.Octopus, 250.0f, 200.0f));
        }

        private void LoadOnEntry()
        {
            ShipMan.UpdateHIScoreFont();
        }

        public override void Update(float systemTime)
        {
            bool Curr2 = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_2);

            if (Curr2 && !this.pre2Key)
            {
                SceneContextMan.SetState(SceneContext.Scene.Play);
            }
            this.pre2Key = Curr2;

            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // walk through all objects and push to flyweight
                GameObjectNodeMan.Update();
            }
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);
            GameObjectNodeMan.SetActive(this.poGameObjectNodeMan);
            GhostMan.SetActive(this.poGhostManMan);

            //  FontMan.Dump();
            //  TimerEventMan.Dump();
            this.LoadOnEntry();

            InputMan.LockInput();
            Simulation.LockSimulationInput();

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            TimerEventMan.PauseUpdate(/*0.0f*/delta);
        }
        public override void Leaving()
        {
            this.TimeAtPause = TimerEventMan.GetCurrTime();

    
           // FontMan.RemoveAll();

           // FontMan.Dump();
          //  TimerEventMan.Dump();
            
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;
        public GameObjectNodeMan poGameObjectNodeMan;
        public GhostMan poGhostManMan;
        private SpriteBatch pSB_Texts;
        private SpriteBatch pSB_Boxes;
        private bool pre2Key;
    }
}

// --- End of File ---
