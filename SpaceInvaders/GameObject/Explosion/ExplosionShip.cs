//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionShip : ExplosionCategory
    {
        public ExplosionShip(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ExplosionCategory.Type.Ship)
        {
            this.x = posX;
            this.y = posY;
            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // Get the image
            Image pImage_A = ImageMan.Find(Image.Name.ExplosionShipA);
            Image pImage_B = ImageMan.Find(Image.Name.ExplosionShipB);
            Debug.Assert(pImage_A != null);
            Debug.Assert(pImage_B != null);

            // Create a new holder
            ImageNode pImageHolder = new ImageNode(pImage_B);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);
            pImageHolder = new ImageNode(pImage_A);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }

        ~ExplosionShip()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Explosion: Do nothing
        }


        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public void UpdateImage()
        {
            // Wrap if at end of iteration list
            if (this.pIt.IsDone())
            {
                this.pIt.First();
            }

            ImageNode pImageNode = (ImageNode)this.pIt.Current();
            Debug.Assert(pImageNode != null);

            // advance for next iteration
            this.pIt.Next();

            // change image
            this.pSpriteProxy.pRealSprite.SwapImage(pImageNode.pImage);
        }

        // Data: ---------------
        private SLinkMan poSLinkMan;
        private Iterator pIt;

    }
}// --- End of File ---
