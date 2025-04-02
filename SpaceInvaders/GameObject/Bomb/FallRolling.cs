using System;
using System.Diagnostics;

namespace SE456
{
    class FallRolling : FallStrategy
    {
        public FallRolling()
        {
            this.oldPosY = 0.0f;

            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // need to keep iterator for state
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);

            // Get the image
            Image pImage_A = ImageMan.Find(Image.Name.BombStraight_A);
            Image pImage_B = ImageMan.Find(Image.Name.BombStraight_B);
            Image pImage_C = ImageMan.Find(Image.Name.BombStraight_C);
            Image pImage_D = ImageMan.Find(Image.Name.BombStraight_D);
            Debug.Assert(pImage_A != null);
            Debug.Assert(pImage_B != null);
            Debug.Assert(pImage_C != null);
            Debug.Assert(pImage_D != null);

            // Create a new holder
            ImageNode pImageHolder = new ImageNode(pImage_D);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);
            pImageHolder = new ImageNode(pImage_C);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);
            pImageHolder = new ImageNode(pImage_B);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);
            pImageHolder = new ImageNode(pImage_A);
            Debug.Assert(pImageHolder != null);
            this.poSLinkMan.AddToFront(pImageHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }

        public override void Reset(float posY)
        {
            this.oldPosY = posY;
        }

        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);

            float targetY = oldPosY - 1.0f * pBomb.GetBoundingBoxHeight();

            if (pBomb.y < targetY)
            {
                //pBomb.MultiplyScale(-1.0f, 1.0f);

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
                pBomb.pSpriteProxy.pRealSprite.SwapImage(pImageNode.pImage);

                oldPosY = targetY;
            }
        }

        // Data
        private float oldPosY;
        private SLinkMan poSLinkMan;
        private Iterator pIt;
    }
}

// --- End of File ---