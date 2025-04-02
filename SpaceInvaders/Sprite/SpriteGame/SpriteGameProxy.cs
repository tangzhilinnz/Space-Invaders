//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System.Diagnostics;

namespace SE456
{
    public class SpriteGameProxy : SpriteBase
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Proxy,
            Compare,
            NullObject,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public SpriteGameProxy()
        : base()   // <--- Delegate (kick the can)
        {
            this.Wash();
        }

        protected SpriteGameProxy(SpriteGameProxy.Name _name)
        : base()
        {
            this.Wash();
            this.name = _name;
        }


        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(SpriteGame.Name _name)
        {
            this.name = SpriteGameProxy.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.r = 1.0f;
            this.g = 1.0f;
            this.b = 1.0f;

            this.pRealSprite = SpriteGameMan.Find(_name);
            Debug.Assert(this.pRealSprite != null);
        }
        private void privPushToReal()
        {
            // push the data from proxy to Real GameSprite
            Debug.Assert(this.pRealSprite != null);

            this.pRealSprite.x = this.x;
            this.pRealSprite.y = this.y;
            this.pRealSprite.sx = this.sx;
            this.pRealSprite.sy = this.sy;

            this.pRealSprite.SwapColor(this.r, this.g, this.b);
        }

        public void SetColor(float red, float green, float blue)
        {
            this.r = red;
            this.g = green;
            this.b = blue;
        }

        //------------------------------------
        // Override
        //------------------------------------
        override public void Wash()
        {
            this.name = SpriteGameProxy.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

            this.sx = 1.0f;
            this.sy = 1.0f;             

            this.pRealSprite = null;

            base.baseWash();
        }
        public override void Render()
        {
            // move the values over to Real GameSprite
            this.privPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pRealSprite.Update();
            this.pRealSprite.Render();
        }
        
        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.privPushToReal();
            this.pRealSprite.Update();
        }


        public override bool Compare(NodeBase pNodeBaseB)
        {  // YES override this... its different

            // This is used in baseFind() 
            Debug.Assert(pNodeBaseB != null);
            SpriteGameProxy pNodeB = (SpriteGameProxy)pNodeBaseB;

            bool status = false;

            Debug.Assert(this.pRealSprite != null);
            Debug.Assert(pNodeB.pRealSprite != null);

            // Why doesn't GetName() work without GetHashCode?
            // Debug.WriteLine("cmp {0} {1} \n", this.GetName().GetHashCode(), pNodeBaseB.GetName().GetHashCode());
            if (this.pRealSprite.GetName().GetHashCode() == pNodeB.pRealSprite.GetName().GetHashCode())
            {
                status = true;
            }

            return status;
        }
        public override System.Enum GetName()
        {
            return this.name;
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            // Data:
            if (pRealSprite != null)
            {
                Debug.WriteLine("       Sprite:{0} ({1})", this.pRealSprite.GetName(), this.pRealSprite.GetHashCode());
            }
            else
            {
                Debug.WriteLine("       Sprite: null");
            }
            Debug.WriteLine("        (x,y): {0},{1}", this.x, this.y);

			base.baseDump();
		}

        //------------------------------------
        // Data
        //------------------------------------
        public Name name;
        public float x;
        public float y;
		public float sx;
        public float sy;
        public float r;
        public float g;
        public float b;
        public SpriteGame pRealSprite;
    }
}

// --- End of File ---

