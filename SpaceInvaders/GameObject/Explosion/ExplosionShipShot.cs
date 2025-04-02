using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionShipShot : ExplosionCategory
    {
        public ExplosionShipShot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ExplosionCategory.Type.ShipShot)
        {
            this.x = posX;
            this.y = posY;
        }

        ~ExplosionShipShot()
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
