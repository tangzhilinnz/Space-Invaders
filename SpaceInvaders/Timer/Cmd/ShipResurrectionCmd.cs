using System;
using System.Diagnostics;

namespace SE456
{
    class ShipResurrectionCmd : Command
    {
        public ShipResurrectionCmd()
        {
            this.isResurrected = false;
        }

        public override void Execute(float deltaTime)
        {
            if (this.isResurrected)
            {
                ShipMan.ActivateShip();
                InputMan.UnlockInput();
            }
            else
            {
                TimerEventMan.PauseUpdate(2.5f, 0);

                GameObject pReserveShips = GameObjectNodeMan.Find(GameObject.Name.ReserveShips);
                Ship pReserveShip = (Ship)IteratorForwardComposite.GetChild(pReserveShips);
                Debug.Assert(pReserveShip != null);

                Debug.Assert(pReserveShip.pSpriteProxy != null);
                SpriteNode pSpriteNode = pReserveShip.pSpriteProxy.GetSpriteNode();
                Debug.Assert(pSpriteNode != null);
                SpriteBatchMan.Remove(pSpriteNode);
                GameObjectNodeMan.Remove(pReserveShip);

                int remainLives = pReserveShips.GetNumChildren() + 1;

                Font pLivesFont = FontMan.Find(Font.Name.Lives);
                Debug.Assert(pLivesFont != null);
                pLivesFont.UpdateMessage(remainLives.ToString());

                this.isResurrected = true;
                TimerEventMan.Add(TimerEvent.Name.ShipResurrection, this, 3.5f, 1);
            }
        }

        private bool isResurrected;
    }
}

// --- End of File ---