//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SE456
{
    public class SceneOver : SceneState
    {
        public SceneOver()
        {
            this.Initialize();
            this.Type = SceneContext.Scene.Over;
            this.pre1Key = false;
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

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            //---------------------------------------------------------------------------------------------------------
            // Font
            //---------------------------------------------------------------------------------------------------------

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
            pFont = FontMan.Add(Font.Name.Player2_Score, SpriteBatch.Name.Texts, "", Glyph.Name.SpaceInvaders, 557, 700, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "P  U  S  H", Glyph.Name.SpaceInvaders, 300, 500, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.StaticText, SpriteBatch.Name.Texts, "1        O  R        2  P  L  A  Y  E  R  S        B  U  T  T  O  N",
                Glyph.Name.SpaceInvaders, 140, 450, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "C  R  E  D  I  T", Glyph.Name.SpaceInvaders, 475, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "0  0", Glyph.Name.SpaceInvaders, 612, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "P  R  E  S  S        1", Glyph.Name.SpaceInvaders, 25, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.0f);
        }
        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            bool Curr1 = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_1);

            if (Curr1 && !this.pre1Key)
            {
                SceneContextMan.SetState(SceneContext.Scene.Select);
            }
            this.pre1Key = Curr1;


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
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);
            GameObjectNodeMan.SetActive(this.poGameObjectNodeMan);
            GhostMan.SetActive(this.poGhostManMan);

            ShipMan.UpdateHIScoreFont();
            ShipMan.UpdateScoreFont();

            InputMan.LockInput();
            Simulation.LockSimulationInput();
        }
        public override void Leaving()
        {
            // update SpriteBatchMan()
            this.TimeAtPause = TimerEventMan.GetCurrTime();
        }
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;
        public GameObjectNodeMan poGameObjectNodeMan;
        public GhostMan poGhostManMan;
        private bool pre1Key;
    }
}

// --- End of File ---
