//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;
using static SE456.SceneContext;

namespace SE456
{
    public class ShipMan
    {
        public enum MissileState
        {
            Ready,
            Flying
        }

        public enum MoveState
        {
            MoveRight,
            MoveLeft,
            MoveBoth
        }

        private ShipMan()
        {
            // Store the states
            this.pStateMissileReady = new ShipMissileReady();
            this.pStateMissileFlying = new ShipMissileFlying();

            this.pStateMoveBoth = new ShipMoveBoth();
            this.pStateMoveRight = new ShipMoveRight();
            this.pStateMoveLeft = new ShipMoveLeft();

            // set active
            this.pShip = null;
            this.pMissile = null;
            this.reserveShips = null;
            this.Score = 0;
            this.Hi_Score = 0;
            this.ShipLevel = 1;
        }

        public static void Create()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            //// Stuff to initialize after the instance was created
            //instance.pShip = ActivateShip();
            ////instance.pShip.SetState(ShipMan.MoveState.MoveBoth);
            ////instance.pShip.SetState(ShipMan.MissileState.Ready);
            //ShipMan.CreateReserveShip();
        }

        public static void GenShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);
            pShipMan.pShip = ShipMan.ActivateShip();
            ShipMan.CreateReserveShip();
        }


        private static ShipMan privInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static void CreateReserveShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();

            pShipMan.reserveShips = new ShipRoot(GameObject.Name.ReserveShips, SpriteGame.Name.NullObject, -10.0f, -10.0f);
            GameObjectNodeMan.Attach(pShipMan.reserveShips);

            Ship pReserveShip2 = new Ship(GameObject.Name.Ship, SpriteGame.Name.Ship, 100, 33);
            Ship pReserveShip3 = new Ship(GameObject.Name.Ship, SpriteGame.Name.Ship, 145, 33);
            pReserveShip2.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            pReserveShip3.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);

            // Attached to SpriteBatches
            SpriteBatch pSB_Ships = SpriteBatchMan.Find(SpriteBatch.Name.Ships);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);

            pReserveShip2.ActivateSprite(pSB_Ships);
            pReserveShip3.ActivateSprite(pSB_Ships);
            pShipMan.reserveShips.Add(pReserveShip2);
            pShipMan.reserveShips.Add(pReserveShip3);
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();

            Debug.Assert(pShipMan != null);
              Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static void SetShip(Ship pShip)
        {
            ShipMan pShipMan = ShipMan.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            pShipMan.pShip = pShip;
        }

        public static ShipMissileState GetState(MissileState state)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            ShipMissileState pShipState = null;

            switch (state)
            {
                case ShipMan.MissileState.Ready:
                    pShipState = pShipMan.pStateMissileReady;
                    break;

                case ShipMan.MissileState.Flying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }
        public static ShipMoveState GetState(MoveState state)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            ShipMoveState pShipState = null;

            switch (state)
            {
                case ShipMan.MoveState.MoveBoth:
                    pShipState = pShipMan.pStateMoveBoth;
                    break;

                case ShipMan.MoveState.MoveLeft:
                    pShipState = pShipMan.pStateMoveLeft;
                    break;

                case ShipMan.MoveState.MoveRight:
                    pShipState = pShipMan.pStateMoveRight;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.privInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            // No need to re-calling new()
            Missile pMissile = null;

            GameObjectNode pGameObjNode = GhostMan.Find(GameObject.Name.Missile);
            if (pGameObjNode == null)
            {
                pMissile = new Missile(SpriteGame.Name.Missile, 400, 100);
            }
            else
            { 
                // Recycle it.
                pMissile = (Missile)pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);
               // GhostMan.Dump();
                pMissile.Resurrect(400,100);
            }
            pShipMan.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Missiles = SpriteBatchMan.Find(SpriteBatch.Name.Missiles);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateSprite(pSB_Missiles);

            // Attach the missile to the missile group
            GameObject pMissileGroup = GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        public static Ship ActivateShip()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, SpriteGame.Name.Ship, 75, 100);
            pShip.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            pShipMan.pShip = pShip;

            //// Attach the sprite to the correct sprite batch
            //SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Alien_Batch);
            //pSB_Aliens.Attach(pShip.pSpriteProxy);

            // Attached to SpriteBatches
            SpriteBatch pSB_Ships = SpriteBatchMan.Find(SpriteBatch.Name.Ships);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);

            pShip.ActivateCollisionSprite(pSB_Boxes);
            pShip.ActivateSprite(pSB_Ships);

            // Attach the ship to the ship root
            GameObject pShipRoot = GameObjectNodeMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            pShipMan.pShip.SetState(ShipMan.MoveState.MoveBoth);
            pShipMan.pShip.SetState(ShipMan.MissileState.Ready);

            return pShipMan.pShip;
        }

        static public void ResetScore(int score = 0)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            pShipMan.Score = score;
        }

        static public void UpdateScore(int score)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);
            pShipMan.Score += score;
        }

        public static int GetScore()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);
            return pShipMan.Score;
        }
        static public void SetHIScore(int hiScore = 0)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);
            pShipMan.Hi_Score = hiScore;
        }

        public static int GetHIScore()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);
            return pShipMan.Hi_Score;
        }

        public static void UpdateHIScoreFont()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            int HIscore = ShipMan.GetHIScore();
            Font pFont = FontMan.Find(Font.Name.HI_Score);
            Debug.Assert(pFont != null);

            string spacedScore = "";
            string scoreStr = HIscore.ToString().PadLeft(4, '0');

            for (int i = 0; i < scoreStr.Length; i++)
            {
                spacedScore += scoreStr[i];
                if (i < scoreStr.Length - 1)
                {
                    spacedScore += "  ";
                }
            }

            pFont.UpdateMessage(spacedScore);
        }

        public static void UpdateScoreFont()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            int score = ShipMan.GetScore();
            Font pFont = FontMan.Find(Font.Name.Player1_Score);
            Debug.Assert(pFont != null);

            string spacedScore = "";
            string scoreStr = score.ToString().PadLeft(4, '0');

            for (int i = 0; i < scoreStr.Length; i++)
            {
                spacedScore += scoreStr[i];
                if (i < scoreStr.Length - 1)
                {
                    spacedScore += "  ";
                }
            }

            pFont.UpdateMessage(spacedScore);
        }

        public static void ScaleShipLevel()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.ShipLevel += 1;
        }

        public static void ResetShipLevel(int l = 1)
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            pShipMan.ShipLevel = l;
        }

        public static int GetShipLevel()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            return pShipMan.ShipLevel;
        }

        public static int GetLife()
        {
            ShipMan pShipMan = ShipMan.privInstance();
            Debug.Assert(pShipMan != null);

            return pShipMan.reserveShips.GetNumChildren() + 1;
        }

        // Data: ----------------------------------------------
        private static ShipMan instance = null;
        private int Score;
        private int Hi_Score;

        // Active
        private Ship pShip;
        private ShipRoot reserveShips;
        private Missile pMissile;

        // Reference
        private ShipMissileReady pStateMissileReady;
        private ShipMissileFlying pStateMissileFlying;

        private ShipMoveBoth pStateMoveBoth;
        private ShipMoveRight pStateMoveRight;
        private ShipMoveLeft pStateMoveLeft;

        private int ShipLevel;

    }
}

// --- End of File ---
