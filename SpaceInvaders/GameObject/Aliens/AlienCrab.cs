using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienCrab : AlienCategory
    {
        public AlienCrab(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.CrabAlien, spriteName, posX, posY)
        {
            this.score = 20;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GreenBird
            // Call the appropriate collision reaction            
            other.VisitAlienCrab(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienCrab
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            //   Debug.WriteLine("update: {0}", this);
            base.Update();
        }

        // Data: ---------------

    }
}

// --- End of File ---
