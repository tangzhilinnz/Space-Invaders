using System;
using System.Diagnostics;

namespace SE456
{
    class SceneOverCmd : Command
    {
        public SceneOverCmd()
        {
            this.isGameOver = false;
        }

        public override void Execute(float deltaTime)
        {
            if (!isGameOver)
            {
                TimerEventMan.PauseUpdate(100.0f, 0);
                Font pLivesFont = FontMan.Find(Font.Name.Lives);
                Debug.Assert(pLivesFont != null);
                pLivesFont.UpdateMessage("0");
                ShipMan.UpdateHIScoreFont();

                GameObject pReserveShips = GameObjectNodeMan.Find(GameObject.Name.ReserveShips);
                Ship pReserveShip = (Ship)IteratorForwardComposite.GetChild(pReserveShips);

                while (pReserveShip != null)
                {
                    GameObject pTmp = (GameObject)IteratorForwardComposite.GetSibling(pReserveShip);

                    Debug.Assert(pReserveShip.pSpriteProxy != null);
                    SpriteNode pSpriteNode = pReserveShip.pSpriteProxy.GetSpriteNode();
                    Debug.Assert(pSpriteNode != null);
                    SpriteBatchMan.Remove(pSpriteNode);
                    GameObjectNodeMan.Remove(pReserveShip);

                    pReserveShip = (Ship)pTmp;
                }

                this.isGameOver = true;
                TimerEventMan.Add(TimerEvent.Name.SceneOver, this, 2.5f, 1);
                TimedCharacterFactory.Install("G  A  M  E        O  V  E  R", 0.5f, 0.03f, 255, 650, 1.0f, 0.0f, 0.0f);

                UFORoot pUFORoot = (UFORoot)GameObjectNodeMan.Find(GameObject.Name.UFORoot);
                Debug.Assert(pUFORoot != null);
                UFO pUFO = (UFO)IteratorForwardComposite.GetChild(pUFORoot);
                if (pUFO != null)
                {
                    pUFO.SetDelta(0.0f);
                    TimerEventMan.Remove(TimerEventMan.Find(TimerEvent.Name.UFOMoveSnd, 1), 1);
                }
            }
            else
            {
                ShipMan.ResetShipLevel(1);
                SceneContextMan.SetState(SceneContext.Scene.Over);
            }
        }

        private bool isGameOver;
    }
}

// --- End of File ---
