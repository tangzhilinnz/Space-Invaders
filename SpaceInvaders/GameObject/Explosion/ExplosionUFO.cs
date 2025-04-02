using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionUFO : ExplosionCategory
    {
        public ExplosionUFO(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ExplosionCategory.Type.UFO)
        {
            this.x = posX;
            this.y = posY;
        }

        ~ExplosionUFO()
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

        // Data: ---------------

    }
}

// --- End of File ---