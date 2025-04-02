//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class Bomb : BombCategory
    {
        public Bomb(GameObject.Name name, SpriteGame.Name spriteName, FallStrategy _pStrategy, float posX, float posY)
            : base(name, spriteName, posX, posY, BombCategory.Type.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 4.0f;
            this.penetration = 4;

            Debug.Assert(_pStrategy != null);
            this.pStrategy = _pStrategy;

            this.pStrategy.Reset(this.y);

            this.poColObj.pColBoxProxy.SetColor(1, 1, 0);
        }

        public void Reset()
        {
            this.y = 700.0f;
            this.pSpriteProxy.pRealSprite.SwapColor(1.0f, 1.0f, 1.0f);
            this.pStrategy.Reset(this.y);
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
            base.Update();
            this.y -= delta;

            // Strategy
            this.pStrategy.Fall(this);
        }
        public float GetBoundingBoxHeight()
            {
            return this.poColObj.poColRect.height;
            }
        ~Bomb()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBomb(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }
        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.pSpriteProxy != null);

            this.pSpriteProxy.sx *= sx;
            this.pSpriteProxy.sy *= sy;
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Bomb
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        // Data
        public float delta;
        private FallStrategy pStrategy;
        public int penetration;
    }
}

// --- End of File ---
