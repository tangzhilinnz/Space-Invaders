using System;
using System.Diagnostics;

namespace SE456
{
    public class UFO : UFOCategory
    {
        public UFO(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float _dropBombPos, int _score)
            : base(name, spriteName, posX, posY, UFOCategory.Type.UFO)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 1.8f;
            this.pSpriteProxy.SetColor(1.0f, 0.0f, 0.0f);
            this.poColObj.pColBoxProxy.SetColor(1, 1, 0);
            this.dropBombPos = _dropBombPos;
            this.score = _score;
            this.dropOnce = false;
        }

        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            base.Update();
            // Now remove it
            base.Remove();
        }

        public override void Update()
        {
            this.x += this.delta;
            base.Update();
        }

        public void SetDelta(float _delta)
        {
            this.delta = _delta;
        }

        ~UFO()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFO(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Bomb
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        // Data
        private float delta;
        public float dropBombPos;
        public bool dropOnce;
    }
}

// --- End of File ---
