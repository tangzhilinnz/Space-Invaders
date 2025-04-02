using System;
using System.Diagnostics;

namespace SE456
{
    class ExplosionSpawnEvent : Command
    {
        public ExplosionSpawnEvent(ExplosionCategory.Type _type, float _posX, float _posY, int _repeat = 0)
        {
            this.pSB_Explosions = SpriteBatchMan.Find(SpriteBatch.Name.Explosions);
            Debug.Assert(this.pSB_Explosions != null);

            //this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            //Debug.Assert(this.pSB_Boxes != null);

            this.type = _type;
            this.repeat = _repeat;
            this.posX = _posX;
            this.posY = _posY;

            switch (type)
            {
                case ExplosionCategory.Type.Alien:
                    pExplosion = new ExplosionAlien(GameObject.Name.ExplosionAlien, SpriteGame.Name.ExplosionAlien, posX, posY);
                    break;
                case ExplosionCategory.Type.AlienShot:
                    pExplosion = new ExplosionAlienShot(GameObject.Name.ExplosionAlienShot, SpriteGame.Name.ExplosionAlienShot, posX, posY);
                    break;
                case ExplosionCategory.Type.Ship:
                    pExplosion = new ExplosionShip(GameObject.Name.ExplosionShip, SpriteGame.Name.ExplosionShip, posX, posY);
                    break;
                case ExplosionCategory.Type.ShipShot:
                    pExplosion = new ExplosionShipShot(GameObject.Name.ExplosionShipShot, SpriteGame.Name.ExplosionShipShot, posX, posY);
                    break;
                case ExplosionCategory.Type.UFO:
                    pExplosion = new ExplosionUFO(GameObject.Name.ExplosionUFO, SpriteGame.Name.ExplosionUFO, posX, posY);
                    break;
                default:
                    Debug.Assert(false);
                    pExplosion = null; // Just to avoid compiler warnings
                    break;
            }

            //pExplosion.ActivateCollisionSprite(this.pSB_Boxes);
            pExplosion.ActivateSprite(this.pSB_Explosions);
            // must be called here. because GameObjectNodeMan.Update() is before this,  you can't depend on it.
            pExplosion.Update();
            GameObject pExpRoot = GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
            pExpRoot.Add(pExplosion);
        }

        override public void Execute(float deltaTime)
        {
            if (ExplosionCategory.Type.Ship == this.type && this.repeat > 0)
            {
                ((ExplosionShip)pExplosion).UpdateImage();

                this.repeat--;

                TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                    this, deltaTime, 1);

                return;
            }

            // Find the SpriteNode
            Debug.Assert(pExplosion.pSpriteProxy != null);
            SpriteNode pSpriteNode = pExplosion.pSpriteProxy.GetSpriteNode();

            // Remove it from the manager
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            // Remove from GameObjectMan
            GameObjectNodeMan.Remove(pExplosion);

            //this.pExplosion.Remove();
        }

        private SpriteBatch pSB_Explosions;
        //private SpriteBatch pSB_Boxes;
        private GameObject pExplosion;
        private int repeat;
        private ExplosionCategory.Type type;
        private float posX;
        private float posY;
    }
}

// --- End of File ---
