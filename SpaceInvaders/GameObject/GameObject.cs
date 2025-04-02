//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class GameObject : Component
    {
        public enum Name
        {
            BirdGrid,
          
            BirdColumn,
            BirdColumn_0,
            BirdColumn_1,
            BirdColumn_2,

            AlienGrid,
            AlienColumn,
            AlienColumn_0,
            AlienColumn_1,
            AlienColumn_2,
            AlienColumn_3,
            AlienColumn_4,
            AlienColumn_5,
            AlienColumn_6,
            AlienColumn_7,
            AlienColumn_8,
            AlienColumn_9,
            AlienColumn_10,

            OctopusAlien,
            CrabAlien,
            SquidAlien,

            Missile,
            MissileGroup,

            WallGroup,
            WallBottom,
            WallRight,
            WallLeft,
            WallTop,

            BumperRoot,
            BumperRight,
            BumperLeft,

            Ship,
            ShipRoot,
            ReserveShips,

            Bomb,
            BombRoot,

            UFO,
            UFORoot,

            ExplosionRoot,
            ExplosionShip,
            ExplosionShipShot,
            ExplosionAlien,
            ExplosionAlienShot,
            ExplosionUFO,

            ShieldRoot,
            ShieldGrid_0,
            ShieldGrid_1,
            ShieldGrid_2,
            ShieldGrid_3,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldBrick,

            NullObject,
            Uninitialized
        }

        protected GameObject(Component.Container type, 
                                GameObject.Name gameName, 
                                SpriteGame.Name proxyName)
            :base(type)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
			
			this.bMarkForDeath = false;
            this.spriteName = proxyName;
            SpriteGameProxy pProxy = SpriteGameProxyMan.Find(proxyName);
            Debug.Assert(pProxy != null);

            this.pSpriteProxy = pProxy;

            this.poColObj = new ColObject(this.pSpriteProxy);
            Debug.Assert(this.poColObj != null);
            this.score = 0;
        }

        protected GameObject(Component.Container type, 
                                GameObject.Name gameName, 
                                SpriteGame.Name _spriteName, 
                                float _x, 
                                float _y)
            :base(type)
        {
            this.name = gameName;
            this.x = _x;
            this.y = _y;
			
			this.bMarkForDeath = false;
			this.spriteName = _spriteName;
            this.pSpriteProxy = SpriteGameProxyMan.Add(spriteName);

            this.poColObj = new ColObject(this.pSpriteProxy);
            Debug.Assert(this.poColObj != null);
        }
        override public void Resurrect()
        {
            this.bMarkForDeath = false;

            Debug.Assert(this.pSpriteProxy != null);
            Debug.Assert(this.poColObj != null);

            //this.pSpriteProxy = SpriteProxyMan.Add(this.spriteName);

            //// poColObj is still valid... don't renew it
            //// Need more work on this
            //// the new is temporary.. need a "update" to reset ColObject
            //// for now call new
            // this.poColObj = new ColObject(this.pSpriteProxy);

            this.poColObj.Resurrect(this.pSpriteProxy);

            Debug.Assert(this.poColObj != null);
            base.Resurrect();
        }
        ~GameObject()
        {
		
        }
		
		public virtual void Remove()
        {
            // Keenan(delete.A)
            // -----------------------------------------------------------------
            // Very difficult at first... if you are messy, you will pay here!
            // Given a game object....
            // -----------------------------------------------------------------

            // Remove from SpriteBatch

            // Find the SpriteNode
            Debug.Assert(this.pSpriteProxy != null);
            SpriteNode pSpriteNode = this.pSpriteProxy.GetSpriteNode();

            // Remove it from the manager
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColBoxProxy != null);
            pSpriteNode = this.poColObj.pColBoxProxy.GetSpriteNode();

            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            // Remove from GameObjectMan

            GameObjectNodeMan.Remove(this);

            // in the future
            //GhostMan.Add(this);
        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)IteratorForwardComposite.GetChild(pNode);

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.poColObj.poColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    // go to next sibling
                    pNode = (GameObject)IteratorForwardComposite.GetSibling(pNode);
                }

                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;

                //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
		    }
        }

        public virtual void Update()
        {
            Debug.Assert(this.pSpriteProxy != null);
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColBoxProxy != null);

            this.pSpriteProxy.x = this.x;
            this.pSpriteProxy.y = this.y;

            this.poColObj.UpdatePos(this.x, this.y);
            this.poColObj.pColBoxProxy.Update();
        }
        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            pSpriteBatch.Attach(this.poColObj.pColBoxProxy);
        }
        public void ActivateSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pSpriteProxy);
        }
        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColBoxProxy != null);

            this.poColObj.pColBoxProxy.SetColor(red, green, blue);
        }
        override public void Dump()
		{
            // Data:
            Debug.WriteLine("");
            Debug.WriteLine("\tGameObject: --------------");
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pSpriteProxy != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pSpriteProxy.name);
                if (this.pSpriteProxy.pRealSprite == null)
                {
                    Debug.WriteLine("\t\t    pRealSprite: null");
                }
                else
                {
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pSpriteProxy.pRealSprite.GetName());
            }
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

			base.baseDump();
        
		}

        override public Enum GetName()
        {
            return this.name;
        }


        public ColObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        // ------------------------------------
        // Data: 
        // ------------------------------------

        public GameObject.Name name;
        public SpriteGame.Name spriteName;
        public float x;
        public float y;
		
		public bool bMarkForDeath;
		
        public SpriteGameProxy pSpriteProxy;
        public ColObject poColObj;
        public int score;
    }

}

// --- End of File ---
