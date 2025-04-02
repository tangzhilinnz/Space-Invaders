using System.Diagnostics;

namespace SE456
{
    public class SpriteBoxProxy : SpriteBase
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            Proxy,
            Compare,
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE (OOO- tm)
        public SpriteBoxProxy()
        : base()   // <--- Delegate (kick the can)
        {
            this.Wash();
        }

        protected SpriteBoxProxy(SpriteBoxProxy.Name _name)
        : base()
        {
            this.Wash();
            this.name = _name;
        }


        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(SpriteBox.Name _name)
        {
            this.name = SpriteBoxProxy.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.red = 0.0f;
            this.green = 0.0f;
            this.blue = 0.0f;

            this.pRealBox = SpriteBoxMan.Find(_name);
            Debug.Assert(this.pRealBox != null);
        }

        public void SetColor(float red, float green, float blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public void SetRect(ColRect colRect)
        {
            Debug.Assert(colRect != null);
            this.pColRect = colRect;
        }

        private void privPushToReal()
        {
            // push the data from proxy to Real GameBox
            Debug.Assert(this.pRealBox != null);

            this.pRealBox.x = this.x;
            this.pRealBox.y = this.y;
            this.pRealBox.SetColor(this.red, this.green, this.blue);
            this.pRealBox.SwapRect(this.pColRect);
        }

        //------------------------------------
        // Override
        //------------------------------------
        override public void Wash()
        {
            this.name = SpriteBoxProxy.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.red = 1.0f;
            this.green = 1.0f;
            this.blue = 1.0f;

            this.pRealBox = null;

            base.baseWash();
        }
        public override void Render()
        {
            // move the values over to Real GameSprite
            this.privPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pRealBox.Update();
            this.pRealBox.Render();
        }

        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.privPushToReal();
            this.pRealBox.Update();
        }

        public override bool Compare(NodeBase pNodeBaseB)
        {
            Debug.Assert(pNodeBaseB != null);
            SpriteBoxProxy pNodeB = (SpriteBoxProxy)pNodeBaseB;

            bool status = false;

            Debug.Assert(this.pRealBox != null);
            Debug.Assert(pNodeB.pRealBox != null);

            if (this.pRealBox.GetName().GetHashCode() == pNodeB.pRealBox.GetName().GetHashCode())
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
            if (pRealBox != null)
            {
                Debug.WriteLine("       Sprite:{0} ({1})", this.pRealBox.GetName(), this.pRealBox.GetHashCode());
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
        public float red;
        public float green;
        public float blue;
        public SpriteBox pRealBox;
        public ColRect pColRect;
    }
}

// --- End of File ---
