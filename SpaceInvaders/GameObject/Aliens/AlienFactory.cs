using System;
using System.Diagnostics;

namespace SE456
{
    class AlienFactory
    {
        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pSpriteBoxBatch = SpriteBatchMan.Find(boxSpriteBatchName);
            Debug.Assert(this.pSpriteBoxBatch != null);
        }

        public GameObject Create(AlienCategory.Type type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case AlienCategory.Type.Octopus:
                    // LTN - GameObjectNodeMan
                    pGameObj = new AlienOctopus(SpriteGame.Name.OctopusAlien, posX, posY);
                    break;
                case AlienCategory.Type.Crab:
                    // LTN - GameObjectNodeMan
                    pGameObj = new AlienCrab(SpriteGame.Name.CrabAlien, posX, posY);
                    break;
                case AlienCategory.Type.Squid:
                    // LTN - GameObjectNodeMan
                    pGameObj = new AlienSquid(SpriteGame.Name.SquidAlien, posX, posY);
                    break;
                case AlienCategory.Type.Grid:
                    // LTN - GameObjectNodeMan
                    pGameObj = new AlienGrid();
                    break;
                case AlienCategory.Type.Column:
                    // LTN - GameObjectNodeMan
                    pGameObj = new AlienColumn();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            //Debug.Assert(pGameObj != null);
            //GameObjectNodeMan.Attach(pGameObj);

            // Attached to Group
            pGameObj.pSpriteProxy.SetColor(0.9f, 0.9f, 0.9f);
            pGameObj.ActivateSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pSpriteBoxBatch);

            return pGameObj;
        }

        // Data: ---------------------

        SpriteBatch pSpriteBatch;
        SpriteBatch pSpriteBoxBatch;
    }
}

// --- End of File ---
