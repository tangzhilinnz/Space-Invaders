using System;
using System.Diagnostics;

namespace SE456
{
    public class ExplosionRoot : Composite
    {
        public ExplosionRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColBoxProxy.SetColor(1, 1, 1);

            this.name = name;
        }
        ~ExplosionRoot()
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Explosion: Do nothing
        }

        public override void Update()
        {
            base.Update();
        }


        // Data: ---------------


    }
}// --- End of File ---
