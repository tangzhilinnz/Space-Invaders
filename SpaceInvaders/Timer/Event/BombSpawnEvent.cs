using IrrKlang;
using System;
using System.Diagnostics;

namespace SE456
{
    class BombSpawnEvent : Command
    {
        public BombSpawnEvent(Random pRandom, GameObject _pGrid, GameObject _pUFORoot, GameObject _pBombRoot)
        {
            //this.pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            this.pBombRoot = _pBombRoot;
            Debug.Assert(this.pBombRoot != null);

            this.pUFORoot = _pUFORoot;
            Debug.Assert(this.pUFORoot != null);

            this.pSB_Bombs = SpriteBatchMan.Find(SpriteBatch.Name.Bombs);
            Debug.Assert(this.pSB_Bombs != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;
            Debug.Assert(this.pRandom != null);

            this.pGrid = _pGrid;
            Debug.Assert(this.pGrid != null);
        }

        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);
            //AlienGrid pAlien = (AlienGrid)pGrid;

            int num = pGrid.GetNumChildren();

            if (num > 0)
            {
                int randomValue = pRandom.Next(num);
                int bombType = pRandom.Next(4);
                Component pChild = IteratorForwardComposite.GetChild(pGrid);

                while (randomValue > 0)
                {
                    pChild = IteratorForwardComposite.GetSibling(pChild);
                    randomValue--;
                }

                AlienColumn pColumn = (AlienColumn)pChild;

                float bottomY = pColumn.y - pColumn.poColObj.poColRect.height / 2.0f;
                float bottomX = pColumn.x;

                UFO pUFO = (UFO)IteratorForwardComposite.GetChild(this.pUFORoot);
                if (pUFO != null && pUFO.x > pUFO.dropBombPos && !pUFO.dropOnce)
                {
                    bottomY = pUFO.y;
                    bottomX = pUFO.x;
                    pUFO.dropOnce = true;
                }

                Bomb pBomb = null;
                switch (bombType)
                { 
                    case 0:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombZigZag, new FallZigZag(), bottomX, bottomY);
                        break;
                    case 1:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombCross, new FallCross(), bottomX, bottomY);
                        break;
                    case 2:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombStraight, new FallStraight(), bottomX, bottomY);
                        break;
                    case 3:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombRolling, new FallRolling(), bottomX, bottomY);
                        break;
                    default:
                        Debug.Assert(false);
                        pBomb = null; // Just to avoid compiler warnings
                        break;
                }

                pBomb.ActivateCollisionSprite(this.pSB_Boxes);
                pBomb.ActivateSprite(this.pSB_Bombs);

                //// Attach the missile to the Bomb root
                //GameObject pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
                //Debug.Assert(pBombRoot != null);

                // Add to GameObject Tree - {update and collisions}
                this.pBombRoot.Add(pBomb);

                float curLevel = ShipMan.GetShipLevel();
                float time = (float)pRandom.Next(500, 2000) / 1000.0f;
                TimerEventMan.Add(TimerEvent.Name.BombSpawn,
                    new BombSpawnEvent(this.pRandom, this.pGrid, this.pUFORoot, this.pBombRoot), time);
            }
        }

        private GameObject pBombRoot;
        private GameObject pUFORoot;
        private SpriteBatch pSB_Bombs;
        private SpriteBatch pSB_Boxes;
        private Random pRandom;
        private GameObject pGrid;
    }
}

// --- End of File ---
