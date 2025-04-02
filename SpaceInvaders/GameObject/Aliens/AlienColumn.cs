using System;
using System.Diagnostics;

namespace SE456
{
    public class AlienColumn : Composite
    {
        public AlienColumn()
            : base()
        {
            this.name = Name.AlienColumn;
            this.poColObj.pColBoxProxy.SetColor(0.0f, 1.0f, 0.0f);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs AlienColumn
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            // Debug.WriteLine("update: {0}", this);
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("");
            Debug.WriteLine("Column:");

            // walk through the list and render
            Iterator pIt = this.poDLinkMan.GetIterator();
            Debug.Assert(pIt != null);

           for(pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                GameObject pNode = (GameObject)pIt.Current();
                Debug.Assert(pNode != null);

                pNode.Dump();
            }
        }

        // LTN- AlienColumn class
        private static SpriteGameProxyNull psSpriteGameProxyNull = new SpriteGameProxyNull();
    }
}

// --- End of File ---
