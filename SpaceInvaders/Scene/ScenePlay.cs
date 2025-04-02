using IrrKlang;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Windows.Shapes;
using static SE456.SceneContext;

namespace SE456
{
    public class ScenePlay : SceneState
    {
        readonly Random pRandom = new Random();
        Sound pSndVader0, pSndVader1, pSndVader2, pSndVader3, pUFOMove, pShipShot, pShipKilled;
        GameObject pUFORoot, pBombRoot, pAlienGrid, pShieldRoot, pMissileGroup, pShipRoot;

        public ScenePlay()
        {
            this.Initialize();
            this.Type = SceneContext.Scene.Play;
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

            this.poColPairMan = new ColPairMan(3, 1);
            ColPairMan.SetActive(this.poColPairMan);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts, 210);
            SpriteBatch pSB_Alien = SpriteBatchMan.Add(SpriteBatch.Name.Alien_Batch, 50);
            SpriteBatch pSB_Box = SpriteBatchMan.Add(SpriteBatch.Name.SpriteBox_Batch, 100);
            pSB_Box.Disable();
            SpriteBatch pSB_Shields = SpriteBatchMan.Add(SpriteBatch.Name.Shields, 80);
            SpriteBatch pSB_Bombs = SpriteBatchMan.Add(SpriteBatch.Name.Bombs, 90);
            SpriteBatch pSB_Explosions = SpriteBatchMan.Add(SpriteBatch.Name.Explosions, 95);
            SpriteBatch pSB_UFO = SpriteBatchMan.Add(SpriteBatch.Name.UFO, 96);
            SpriteBatch pSB_Missiles = SpriteBatchMan.Add(SpriteBatch.Name.Missiles, 99);
            SpriteBatch pSB_Ships = SpriteBatchMan.Add(SpriteBatch.Name.Ships, 120);

            //------------------------------------------------------
            // Sound
            //------------------------------------------------------
            this.pSndVader0 = SoundMan.Find(Sound.Name.FastInvader1);
            this.pSndVader1 = SoundMan.Find(Sound.Name.FastInvader2);
            this.pSndVader2 = SoundMan.Find(Sound.Name.FastInvader3);
            this.pSndVader3 = SoundMan.Find(Sound.Name.FastInvader4);
            this.pShipShot = SoundMan.Find(Sound.Name.ShipShot);
            this.pUFOMove = SoundMan.Find(Sound.Name.UFOMove);

            Sound pAlienKilled = SoundMan.Find(Sound.Name.AlienKilled);
            Sound pUFOKilled = SoundMan.Find(Sound.Name.UFOKilled);
            this.pShipKilled = SoundMan.Find(Sound.Name.ShipKilled);

            Debug.Assert(this.pSndVader0 != null);
            Debug.Assert(this.pSndVader1 != null);
            Debug.Assert(this.pSndVader2 != null);
            Debug.Assert(this.pSndVader3 != null);
            Debug.Assert(this.pShipShot != null);
            Debug.Assert(this.pUFOMove != null);

            Debug.Assert(pAlienKilled != null);
            Debug.Assert(pUFOKilled != null);
            Debug.Assert(this.pShipKilled != null);

            //---------------------------------------------------------------------------------------------------------
            // Input
            //---------------------------------------------------------------------------------------------------------
            InputSubject pInputSubject;

            pInputSubject = InputMan.GetCollisionBoxSubject();
            pInputSubject.Attach(new CollisionBoxObserver());

            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

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
            pFont = FontMan.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "3", Glyph.Name.SpaceInvaders, 25, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "C  R  E  D  I  T", Glyph.Name.SpaceInvaders, 475, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);
            pFont = FontMan.Add(Font.Name.Credit, SpriteBatch.Name.Texts, "0  0", Glyph.Name.SpaceInvaders, 612, 30, 3.0f, 3.0f);
            pFont.SetColor(0.9f, 0.9f, 0.9f);

            // --- UFO score ---
            Font pUFOScore = FontMan.Add(Font.Name.UFOScore, SpriteBatch.Name.Texts, "", Glyph.Name.SpaceInvaders, 0.0f, 0.0f, 3.0f, 3.0f);
            pUFOScore.SetColor(1.0f, 0f, 0f);

            Simulation.SetState(Simulation.State.Realtime);

            //--------------------------------------------------------
            // UFO
            //--------------------------------------------------------
            this.pUFORoot = new UFORoot(GameObject.Name.UFORoot, SpriteGame.Name.NullObjectUFORoot, 0.0f, 0.0f);
            this.pUFORoot.ActivateCollisionSprite(pSB_Box);
            this.pUFORoot.ActivateSprite(pSB_UFO);
            GameObjectNodeMan.Attach(this.pUFORoot);

            //--------------------------------------------------------
            // Bomb
            //--------------------------------------------------------
            this.pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObjectBombRoot, 0.0f, 0.0f);
            this.pBombRoot.ActivateCollisionSprite(pSB_Box);
            this.pBombRoot.ActivateSprite(pSB_Bombs);
            GameObjectNodeMan.Attach(this.pBombRoot);

            //--------------------------------------------------------
            // Explosion
            //--------------------------------------------------------
            ExplosionRoot pExplosionRoot = new ExplosionRoot(GameObject.Name.ExplosionRoot, SpriteGame.Name.NullObjectExplosionRoot, 0.0f, 0.0f);
            pExplosionRoot.ActivateSprite(pSB_Explosions);
            //pExplosionRoot.ActivateCollisionSprite(pSB_Box);
            GameObjectNodeMan.Attach(pExplosionRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------
            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, -10.0f, -10.0f);
            pWallGroup.ActivateCollisionSprite(pSB_Box);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, (672 - 12), 350, 24, 580);
            pWallRight.ActivateCollisionSprite(pSB_Box);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, 12, 350, 24, 580);
            pWallLeft.ActivateCollisionSprite(pSB_Box);

            WallTop pWalltop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, 672 / 2, 716, 672, 103);
            pWalltop.ActivateCollisionSprite(pSB_Box);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.Wall, 672 / 2, 50, 672, 3);
            pWallBottom.ActivateCollisionSprite(pSB_Box);
            pWallBottom.ActivateSprite(pSB_Texts);

            // Add to the composite the children
            pWallGroup.Add(pWallBottom);
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWalltop);

            GameObjectNodeMan.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Bumper
            //---------------------------------------------------------------------------------------------------------
            BumperRoot pBumperRoot = new BumperRoot(GameObject.Name.BumperRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateSprite(pSB_Box);

            BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, SpriteGame.Name.NullObject, 647, 100, 50, 100);
            pBumperRight.ActivateCollisionSprite(pSB_Box);

            BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, SpriteGame.Name.NullObject, 25, 100, 50, 100);
            pBumperLeft.ActivateCollisionSprite(pSB_Box);

            // Add to the composite the children
            pBumperRoot.Add(pBumperRight);
            pBumperRoot.Add(pBumperLeft);

            GameObjectNodeMan.Attach(pBumperRoot);

            //-------------------------------------------------------
            // Shield 
            //-------------------------------------------------------
            this.pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObjectShieldRoot, 0.0f, 0.0f);
            this.pShieldRoot.ActivateCollisionSprite(pSB_Box);
            this.pShieldRoot.ActivateSprite(pSB_Shields);
            this.pShieldRoot.SetCollisionColor(0.0f, 0.0f, 1.0f);
            GameObjectNodeMan.Attach(this.pShieldRoot);

            //-------------------------------------------------------
            // Missile
            //-------------------------------------------------------
            this.pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, SpriteGame.Name.NullObjectMissileGroup, -10.0f, -10.0f);
            this.pMissileGroup.ActivateSprite(pSB_Missiles);
            this.pMissileGroup.ActivateCollisionSprite(pSB_Box);
            GameObjectNodeMan.Attach(this.pMissileGroup);

            //-------------------------------------------------------
            // Ship
            //-------------------------------------------------------
            this.pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            this.pShipRoot.ActivateSprite(pSB_Ships);
            this.pShipRoot.ActivateCollisionSprite(pSB_Box);
            GameObjectNodeMan.Attach(this.pShipRoot);

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------
            AlienFactory SIF = new AlienFactory(SpriteBatch.Name.Alien_Batch, SpriteBatch.Name.SpriteBox_Batch);

            this.pAlienGrid = (AlienGrid)SIF.Create(AlienCategory.Type.Grid);
            GameObjectNodeMan.Attach(this.pAlienGrid);

            //-----------------------------------------------------------------
            // ColPair 
            //-----------------------------------------------------------------
            // associate in a collision pair
            ColPair pColPair;

            // Missile vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, this.pMissileGroup, this.pShieldRoot);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new ShipMissileBrickObserver());
            pColPair.Attach(new RemoveBrickObserver());
            //pColPair.Attach(new ShipReadyObserver());

            // Alien vs Wall
            pColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, this.pAlienGrid, pWallGroup);
            Debug.Assert(pColPair != null);
            pColPair.Attach(new GridObserver());

            // Missile vs Wall
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, this.pMissileGroup, pWallGroup);
            Debug.Assert(pColPair != null);
            // Missile Wall a collision pair
            pColPair.Attach(new RemoveMissileObserver());
            pColPair.Attach(new ShipReadyObserver());

            // Bomb vs Bottom
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, this.pBombRoot, pWallGroup);
            pColPair.Attach(new RemoveBombObserver());

            // Bomb vs Shield
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Shield, this.pBombRoot, this.pShieldRoot);
            pColPair.Attach(new RemoveBrickObserver());
            pColPair.Attach(new BombBrickObserver());

            // Bumper vs Ship
            pColPair = ColPairMan.Add(ColPair.Name.Bumper_Ship, pBumperRoot, this.pShipRoot);
            pColPair.Attach(new ShipMoveObserver());

            // Missile vs Alien
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Alien, this.pMissileGroup, this.pAlienGrid);
            pColPair.Attach(new AddScoreObserver());
            pColPair.Attach(new RemoveMissileObserver(false));
            pColPair.Attach(new RemoveAlienObserver());
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(pAlienKilled));

            // Shiled vs Alien
            pColPair = ColPairMan.Add(ColPair.Name.Alien_Shiled, this.pAlienGrid, this.pShieldRoot);
            pColPair.Attach(new RemoveBrickObserver());

            // Missile vs Bomb
            pColPair = ColPairMan.Add(ColPair.Name.Missile_Bomb, this.pMissileGroup, this.pBombRoot);
            pColPair.Attach(new RemoveBombObserver(true));
            pColPair.Attach(new RemoveMissileObserver(false));
            pColPair.Attach(new ShipReadyObserver());

            // Missile vs UFO
            pColPair = ColPairMan.Add(ColPair.Name.Missile_UFO, this.pMissileGroup, this.pUFORoot);
            pColPair.Attach(new AddScoreObserver());
            pColPair.Attach(new RemoveUFOByMissileObserver());
            pColPair.Attach(new RemoveMissileObserver(false));
            pColPair.Attach(new ShipReadyObserver());
            pColPair.Attach(new SndObserver(pUFOKilled));

            // UFO vs Wall
            pColPair = ColPairMan.Add(ColPair.Name.UFO_WALL, this.pUFORoot, pWallGroup);
            pColPair.Attach(new RemoveUFOByWallObserver());

            // Bomb vs Ship
            pColPair = ColPairMan.Add(ColPair.Name.Bomb_Ship, this.pBombRoot, this.pShipRoot);
            pColPair.Attach(new RemoveBombByShipObserver());
            pColPair.Attach(new SndObserver(this.pShipKilled));
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new FreezeSceneObserver());

            // Alien vs Ship
            pColPair = ColPairMan.Add(ColPair.Name.Alien_Ship, this.pAlienGrid, this.pShipRoot);
            pColPair.Attach(new SndObserver(this.pShipKilled));
            pColPair.Attach(new RemoveShipObserver());
            pColPair.Attach(new FreezeSceneObserver(true));

        }


        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {   
                SoundMan.Update();
                InputMan.Update();
                float tatolTime = Simulation.GetTotalTime();
                TimerEventMan.Update(tatolTime, 0);
                TimerEventMan.Update(tatolTime, 1);
                GameObjectNodeMan.Update();
                ColPairMan.Process();
                DelayedObjectMan.Process();
            }
        }
        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public void InitDynamicScene()
        {
            int curLevel = ShipMan.GetShipLevel();
            if (curLevel == 1)
            {
                ShipMan.GenShip();
                ShipMan.ResetScore(0);
                ShipMan.UpdateHIScoreFont();
                ShipMan.UpdateScoreFont();
            }
            else
            {
                ShipMan.UpdateScoreFont();
            }

            ShipMan.GetShip().x = 75;
            ShipMan.GetShip().y = 100;

            Font pFont = FontMan.Find(Font.Name.Lives);
            pFont.UpdateMessage(ShipMan.GetLife().ToString());

            //-------------------------------------------------------
            // Shield 
            //-------------------------------------------------------
            this.pShieldRoot = ShieldFactory.CreateSingleShield();

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------
            AlienFactory SIF = new AlienFactory(SpriteBatch.Name.Alien_Batch, SpriteBatch.Name.SpriteBox_Batch);

            for (int i = 0; i < 11; i++)
            {
                AlienColumn pColumn = (AlienColumn)SIF.Create(AlienCategory.Type.Column);
                this.pAlienGrid.Add(pColumn);

                pColumn.Add(SIF.Create(AlienCategory.Type.Squid, 86.0f + i * 50.0f, Math.Max(400.0f, 600.0f - (curLevel - 1) * 50.0f)));
                pColumn.Add(SIF.Create(AlienCategory.Type.Crab, 86.0f + i * 50.0f, Math.Max(350.0f, 550.0f - (curLevel - 1) * 50.0f)));
                pColumn.Add(SIF.Create(AlienCategory.Type.Crab, 86.0f + i * 50.0f, Math.Max(300.0f, 500.0f- (curLevel - 1) * 50.0f)));
                pColumn.Add(SIF.Create(AlienCategory.Type.Octopus, 86.0f + i * 50.0f, Math.Max(250.0f, 450.0f - (curLevel - 1) * 50.0f)));
                pColumn.Add(SIF.Create(AlienCategory.Type.Octopus, 86.0f + i * 50.0f, Math.Max(200.0f, 400.0f - (curLevel - 1) * 50.0f)));
            }

            //---------------------------------------------------------------------------------------------------------
            // Set Timer
            //---------------------------------------------------------------------------------------------------------
            // Create an animation sprite
            AnimationCmd pAnimOctopus = new AnimationCmd(SpriteGame.Name.OctopusAlien, this.pAlienGrid);
            AnimationCmd pAnimCrab = new AnimationCmd(SpriteGame.Name.CrabAlien, this.pAlienGrid);
            AnimationCmd pAnimSquid = new AnimationCmd(SpriteGame.Name.SquidAlien, this.pAlienGrid);
            GridMoveCmd pGridMove = new GridMoveCmd(this.pAlienGrid);

            // attach several images to cycle
            pAnimOctopus.Attach(Image.Name.OctopusBAlien);
            pAnimOctopus.Attach(Image.Name.OctopusAAlien);

            pAnimCrab.Attach(Image.Name.CrabBAlien);
            pAnimCrab.Attach(Image.Name.CrabAAlien);

            pAnimSquid.Attach(Image.Name.SquidBAlien);
            pAnimSquid.Attach(Image.Name.SquidAAlien);

            SndMoveCmd pSndMove = new SndMoveCmd(pSndVader0, pSndVader1, pSndVader2, pSndVader3, this.pAlienGrid);

            // add AnimationSprite to timer
            TimerEventMan.Add(TimerEvent.Name.AliensAnimation, pAnimOctopus, 0.7f);
            TimerEventMan.Add(TimerEvent.Name.AliensAnimation, pAnimCrab, 0.7f);
            TimerEventMan.Add(TimerEvent.Name.AliensAnimation, pAnimSquid, 0.7f);
            TimerEventMan.Add(TimerEvent.Name.GridMove, pGridMove, 0.7f);
            TimerEventMan.Add(TimerEvent.Name.SndMove, pSndMove, 0.7f);

            for (int i = 0; i < 1; i++)
            {
                float time = (float)pRandom.Next(1500, 2000) / 1000.0f;
                //Debug.WriteLine("set--->time: {0} ", time);
                TimerEventMan.Add(TimerEvent.Name.BombSpawn,
                    new BombSpawnEvent(pRandom, this.pAlienGrid, this.pUFORoot, this.pBombRoot), time);
            }

            float intervals = (float)pRandom.Next(18, 25);
            TimerEventMan.Add(TimerEvent.Name.UFOSpawn,
                new UFOSpawnEvent(this.pRandom, this.pUFORoot, this.pUFOMove), intervals, 1);
        }

        public override void Entering()
        {
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);
            GameObjectNodeMan.SetActive(this.poGameObjectNodeMan);
            GhostMan.SetActive(this.poGhostManMan);
            ColPairMan.SetActive(this.poColPairMan);

            SpriteBatch pSpriteMan = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            Debug.Assert(pSpriteMan != null);
            pSpriteMan.Disable();

            this.InitDynamicScene();

            InputMan.UnlockInput();
            Simulation.LockSimulationInput();

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;

            TimerEventMan.PauseUpdate(delta, 0);
            TimerEventMan.PauseUpdate(delta, 1);
        }
        public override void Leaving()
        {
            // Delete remaining game objects
            GameObject pColumn = (GameObject)IteratorForwardComposite.GetChild(this.pAlienGrid);
            while (pColumn != null)
            {
                GameObject pAlien = (GameObject)IteratorForwardComposite.GetChild(pColumn);
                while (pAlien != null)
                {
                    GameObject pTmp = (GameObject)IteratorForwardComposite.GetSibling(pAlien);
                    pAlien.Remove();
                    pAlien = pTmp;
                }

                GameObject pColTmp = (GameObject)IteratorForwardComposite.GetSibling(pColumn);
                pColumn.Remove();
                pColumn = pColTmp;
            }
            ((AlienGrid)this.pAlienGrid).ResetDirectionX();


            UFO pUFO = (UFO)IteratorForwardComposite.GetChild(this.pUFORoot);
            if (pUFO != null)
            {
                pUFO.Remove();
            }

            Bomb pBomb = (Bomb)IteratorForwardComposite.GetChild(this.pBombRoot);
            if (pBomb != null)
            {
                pBomb.Remove();
            }

            Missile pMissile = (Missile)IteratorForwardComposite.GetChild(this.pMissileGroup);
            if (pMissile != null)
            {
                pMissile.Remove();
            }

            GameObject pShieldGrid = (GameObject)IteratorForwardComposite.GetChild(this.pShieldRoot);
            while (pShieldGrid != null)
            {
                GameObject pShiledCol = (GameObject)IteratorForwardComposite.GetChild(pShieldGrid);
                while (pShiledCol != null)
                {
                    GameObject pShiledBrick = (GameObject)IteratorForwardComposite.GetChild(pShiledCol);

                    while (pShiledBrick != null)
                    {
                        GameObject pShiledBrickTmp = (GameObject)IteratorForwardComposite.GetSibling(pShiledBrick);
                        pShiledBrick.Remove();
                        pShiledBrick = pShiledBrickTmp;
                    }

                    GameObject pShiledColTmp = (GameObject)IteratorForwardComposite.GetSibling(pShiledCol);
                    pShiledCol.Remove();
                    pShiledCol = pShiledColTmp;
                }

                GameObject pShieldGridTmp = (GameObject)IteratorForwardComposite.GetSibling(pShieldGrid);
                pShieldGrid.Remove();
                pShieldGrid = pShieldGridTmp;
            }

            // Delete timer evnets
            TimerEventMan.RemoveAllEvents(0);
            TimerEventMan.RemoveAllEvents(1);

            Font pFont = FontMan.Find(Font.Name.TimedCharacter);
            if (pFont != null) FontMan.Remove(pFont);

            pFont = FontMan.Find(Font.Name.UFOScore);
            pFont.UpdateMessage("");

            // Need a better way to do this
            this.TimeAtPause = GlobalTimer.GetTime();
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;
        public ColPairMan poColPairMan;
        public GameObjectNodeMan poGameObjectNodeMan;
        public GhostMan poGhostManMan;
    }
}

// --- End of File ---
