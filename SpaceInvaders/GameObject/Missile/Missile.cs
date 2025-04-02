//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class Missile : MissileCategory
    {
        public Missile(SpriteGame.Name spriteName, float posX, float posY)
            : base(GameObject.Name.Missile, spriteName, posX, posY)
        {
            this.x = posX;
            this.y = posY;
            
            this.delta = 9.0f;
			
			this.poColObj.pColBoxProxy.SetColor(1, 1, 0);
            this.penetration = 2;
            this.pSpriteProxy.SetColor(0.9f, 0.9f, 0.9f);
        }
		public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 9.0f;

            base.Resurrect();
            this.poColObj.pColBoxProxy.SetColor(1, 1, 0);
            this.penetration = 2;
        }

        public override void Update()
        {
            if (this.y < 220.0f + 5.0f)
            {
                this.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            }
            else if (this.y < 600.0f)
            {
                this.pSpriteProxy.SetColor(0.9f, 0.9f, 0.9f);
            }
            else
            {
                this.pSpriteProxy.SetColor(1.0f, 0.0f, 0.0f);
            }

            this.y += delta;
            base.Update();
        }

        ~Missile()
        {

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

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        // Data -------------------------------------
        public float delta;
        public int penetration;
    }
}

// --- End of File ---
