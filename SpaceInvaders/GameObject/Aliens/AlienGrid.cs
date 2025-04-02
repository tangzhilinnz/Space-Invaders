using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienGrid : Composite
    {
        public AlienGrid()
            : base()
        {
            this.name = Name.AlienGrid;
            this.poColObj.pColBoxProxy.SetColor(0.0f, 0.0f, 1.0f);

            this.directionX = 1.0f;
            this.deltaX = 4.0f;
            this.deltaY = -20.0f;

            this.collisionFlag = false;
            this.moveYFlag = false;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGrid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs AlienGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            //Debug.WriteLine("update: {0}", this);
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public void MoveGridX()
        {
            if (!this.collisionFlag || this.moveYFlag)
            {
                IteratorForwardComposite pFor = new IteratorForwardComposite(this);

                Component pNode = pFor.First();
                while (!pFor.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.x += this.deltaX * this.directionX;

                    pNode = pFor.Next();
                }

                this.collisionFlag = false;
                this.moveYFlag = false;
            }
        }

        public void MoveGridY()
        {
            if (this.collisionFlag && !this.moveYFlag)
            {
                IteratorForwardComposite pFor = new IteratorForwardComposite(this);

                Component pNode = pFor.First();
                while (!pFor.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.y += this.deltaY;

                    pNode = pFor.Next();
                }

                this.moveYFlag = true;
                this.ReverseDeltaX();
            }
        }

        public float GetDelta()
        {
            return this.deltaX;
        }

        public void SetDeltaX(float inDelta)
        {
            this.deltaX = inDelta;
        }

        public void SetDeltaY(float inDelta)
        {
            this.deltaY = inDelta;
        }

        public void SetCollisionFlag()
        {
            this.collisionFlag = true;
        }

        public void ReverseDeltaX()
        {
            this.directionX *= -1.0f;
        }

        public void ResetDirectionX()
        {
            this.directionX = 1.0f;
        }

        public int GetNumAliens()
        {
            int count = 0;

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                AlienColumn pColumn = (AlienColumn)pIt.Current();
                Debug.Assert(pColumn != null);
                count += pColumn.GetNumChildren();
            }
            return count;
        }

        // Data: ---------------
        private float deltaX;
        private float deltaY;
        private float directionX;
        private bool collisionFlag;
        private bool moveYFlag;
    }
}

// --- End of File ---
