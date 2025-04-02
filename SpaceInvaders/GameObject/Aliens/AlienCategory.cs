using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Squid,
            Octopus,
            Crab,

            Column,
            Grid,

            Unitialized
        }

        protected AlienCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y)
            : base(gameName, spriteName, _x, _y)
        {

        }

        public override void Update()
        {
            if (this.y < 220.0f + 5.0f)
            {
                this.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            }

            // Go to first child
            base.Update();
        }
    }
}

// --- End of File ---