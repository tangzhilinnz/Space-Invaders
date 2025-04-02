//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShieldFactory
    {
        private ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pCollisionSpriteBatch = null;
            this.pTree = null;
        }
        private void privSet(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        private void privSetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }
        ~ShieldFactory()
        {
        }
        private GameObject privCreate(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            GameObjectNode pGameObjNode = GhostMan.Find(gameName);
            if (pGameObjNode != null)
            {
                pShield = pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                //GhostMan.Dump();

                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                    case ShieldCategory.Type.LeftTop1:
                    case ShieldCategory.Type.LeftTop0:
                    case ShieldCategory.Type.LeftBottom:
                    case ShieldCategory.Type.RightTop1:
                    case ShieldCategory.Type.RightTop0:
                    case ShieldCategory.Type.RightBottom:
                        ((ShieldBrick)pShield).Resurrect(posX,posY);
                        break;

                    case ShieldCategory.Type.Root:
                        Debug.Assert(false);
                        break;

                    case ShieldCategory.Type.Grid:
                        ((ShieldGrid)pShield).Resurrect(posX, posY);
                        break;

                    case ShieldCategory.Type.Column:
                        ((ShieldColumn)pShield).Resurrect(posX, posY); ;
                        break;

                    default:
                        // something is wrong
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop1, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop0, posX, posY);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftBottom, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop1, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop0, posX, posY);
                    break;

                case ShieldCategory.Type.RightBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightBottom, posX, posY);
                    break;

                case ShieldCategory.Type.Root:
                    Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    pShield = new ShieldGrid(gameName, SpriteGame.Name.NullObjectShieldGrid, posX, posY);
                    break;

                case ShieldCategory.Type.Column:
                    pShield = new ShieldColumn(gameName, SpriteGame.Name.NullObjectShieldColumn, posX, posY);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            }

            // add to the tree
            this.pTree.Add(pShield);

            // Attached to Group
            pShield.ActivateSprite(this.pSpriteBatch);
            pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pShield;
        }

        public static GameObject CreateSingleShield()
        {
            ShieldFactory pFactory = ShieldFactory.privInstance();

            ShieldRoot pShieldRoot = (ShieldRoot)GameObjectNodeMan.Find(GameObject.Name.ShieldRoot);

            if (pShieldRoot == null)
            {
                pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObjectShieldRoot, 0.0f, 0.0f);
                SpriteBatch pSpriteBatch = SpriteBatchMan.Find(SpriteBatch.Name.Shields);
                Debug.Assert(pSpriteBatch != null);

                SpriteBatch pSpriteBoxBatch = SpriteBatchMan.Find(SpriteBatch.Name.SpriteBox_Batch);
                Debug.Assert(pSpriteBoxBatch != null);

                pShieldRoot.ActivateCollisionSprite(pSpriteBoxBatch);
                pShieldRoot.ActivateSprite(pSpriteBatch);
                pShieldRoot.SetCollisionColor(0.0f, 0.0f, 1.0f);
                GameObjectNodeMan.Attach(pShieldRoot);
            }

            pFactory.privSet(SpriteBatch.Name.Shields, SpriteBatch.Name.SpriteBox_Batch, pShieldRoot);

            for (int i = 0; i < 4; i++)
            {
                pFactory.privSetParent(pShieldRoot);
                // create a grid
                GameObject pGrid = pFactory.privCreate(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid_0 + i);

                {
                    float brickWidth = 10.0f;
                    float brickHeight = 5.0f;
                    float start_x = 83.0f + i * 7 * brickWidth + i * 75.33333f;
                    float start_y = 156.0f;
                    float off_x = 0;

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn0 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0);
                    pFactory.privSetParent(pColumn0);

                    pFactory.privCreate(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 1 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 0 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn1 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_1);
                    pFactory.privSetParent(pColumn1);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn2 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_2);
                    pFactory.privSetParent(pColumn2);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn3 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_3);
                    pFactory.privSetParent(pColumn3);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn4 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_4);
                    pFactory.privSetParent(pColumn4);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn5 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_5);
                    pFactory.privSetParent(pColumn5);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);

                    pFactory.privSetParent(pGrid);
                    GameObject pColumn6 = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_6);
                    pFactory.privSetParent(pColumn6);

                    off_x += brickWidth;
                    pFactory.privCreate(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                }
            }

            return pShieldRoot;

        }

        private static ShieldFactory privInstance()
        {
            if(pInstance == null)
            {
                ShieldFactory.pInstance = new ShieldFactory();
            }

            Debug.Assert(pInstance != null);

            return pInstance;
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;

        private static ShieldFactory pInstance = null;
    }
}

// --- End of File ---
