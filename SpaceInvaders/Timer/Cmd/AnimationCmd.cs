//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SE456
{
    class AnimationCmd : Command
    {
        public AnimationCmd(SpriteGame.Name spriteName, GameObject _pGrid)
        {
            // initialized the sprite animation is attached to
            this.pSprite = SpriteGameMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // need to keep iterator for state
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);

            this.pGrid = _pGrid;
        }

        public void Attach(Image.Name imageName)
        {
            // Get the image
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageNode pImageHolder = new ImageNode(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            this.poSLinkMan.AddToFront(pImageHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }

        public override void Execute(float deltaTime)
        {
            AlienGrid pAlienGrid = (AlienGrid)this.pGrid;
            int curNum = pAlienGrid.GetNumAliens();

            float newDeltaTime = 0.05f + (0.7f - 0.05f) * curNum / 55;

            if (curNum == 0)
            {
                return;
            }

            // Wrap if at end of iteration list
            if (this.pIt.IsDone())
            {
                this.pIt.First();
            }

           // Debug.WriteLine("<--- trig");
            // Get the image
            ImageNode pImageNode = (ImageNode)this.pIt.Current();
            Debug.Assert(pImageNode != null);

            // advance for next iteration
            this.pIt.Next();

            // change image
            this.pSprite.SwapImage(pImageNode.pImage);

            // Add itself back to timer
            TimerEventMan.Add(TimerEvent.Name.AliensAnimation, this, newDeltaTime);
        }

        // Data: ---------------
        private SpriteGame pSprite;
        private SLinkMan poSLinkMan;
        private Iterator pIt;
        private GameObject pGrid;
    }

}

// --- End of File ---
