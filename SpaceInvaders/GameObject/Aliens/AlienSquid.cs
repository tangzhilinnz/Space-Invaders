using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienSquid : AlienCategory
    {
        public AlienSquid(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.SquidAlien, spriteName, posX, posY)
        {
            this.score = 30;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an RedBird
            // Call the appropriate collision reaction            
            other.VisitAlienSquid(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienSquid
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            // Debug.WriteLine("update: {0}", this);
            base.Update();
        }

        // Data: ---------------

    }
}

// --- End of File ---
