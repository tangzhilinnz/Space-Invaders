using System;
using System.Diagnostics;

namespace SE456
{
    class UFOSpawnEvent : Command
    {
        public UFOSpawnEvent(Random pRandom, GameObject _pUFORoot, Sound _pShotSnd)
        {
            this.pUFORoot = _pUFORoot;
            Debug.Assert(this.pUFORoot != null);

            this.pSB_UFO = SpriteBatchMan.Find(SpriteBatch.Name.UFO);
            Debug.Assert(this.pSB_UFO != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;
            Debug.Assert(this.pRandom != null);

            this.pShotSnd = _pShotSnd;
            Debug.Assert(this.pShotSnd != null);
            this.pShotSnd.SetVolume(1.0f);
        }

        override public void Execute(float deltaTime)
        {
            GameObject pAlienGrid = GameObjectNodeMan.Find(GameObject.Name.AlienGrid);
            Debug.Assert(pAlienGrid != null);
            GameObject pShipRoot = GameObjectNodeMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);
            GameObject pShip = (GameObject)IteratorForwardComposite.GetChild(pShipRoot);

            if (((AlienGrid)pAlienGrid).GetNumAliens() < 20 || pShip == null)
            {
                return;
            }

            int curLevel = ShipMan.GetShipLevel();
            float intervals = (float)pRandom.Next(18, 25);

            int randomScore = pRandom.Next(1, 5) * 100;
            float dropBombPos = (float)pRandom.Next(100, 400);

            UFO pUFO = new UFO(GameObject.Name.UFO, SpriteGame.Name.UFO, 0.0f, 645.0f, dropBombPos, randomScore);

            pUFO.ActivateCollisionSprite(this.pSB_Boxes);
            pUFO.ActivateSprite(this.pSB_UFO);

            // Add to GameObject Tree - {update and collisions}
            this.pUFORoot.Add(pUFO);

            TimerEventMan.Add(TimerEvent.Name.UFOMoveSnd, new UFOMoveCmd(this.pShotSnd), 0.2f, 1);
            TimerEventMan.Add(TimerEvent.Name.UFOSpawn, new UFOSpawnEvent(this.pRandom, this.pUFORoot, this.pShotSnd), intervals, 1);
        }

        private GameObject pUFORoot;
        private SpriteBatch pSB_UFO;
        private SpriteBatch pSB_Boxes;
        private Random pRandom;
        private Sound pShotSnd;
    }
}

// --- End of File ---
