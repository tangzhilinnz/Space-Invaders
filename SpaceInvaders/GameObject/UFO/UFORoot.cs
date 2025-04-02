using System;
using System.Diagnostics;

namespace SE456
{
    public class UFORoot : Composite
    {
        public UFORoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColBoxProxy.SetColor(1, 1, 1);
        }

        ~UFORoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFORoot(this);
        }


        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs BombRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            if (pGameObj != null)
            {
                ColPair.Collide(pGameObj, this);
            }
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs BombRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);

            if (pGameObj != null)
            {
                ColPair.Collide(m, pGameObj);
            }
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }



        // Data: ---------------


    }
}

// --- End of File ---
