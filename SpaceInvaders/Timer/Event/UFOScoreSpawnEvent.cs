using System;
using System.Diagnostics;

namespace SE456
{
    class UFOScoreSpawnEvent : Command
    {
        public UFOScoreSpawnEvent(float _posX, float _posY, float _playTime, int _score = 100)
        {
            this.isPlay = false;
            this.score = _score;
            this.posX = _posX;
            this.posY = _posY;
            this.playTime = _playTime;
        }

        override public void Execute(float deltaTime)
        {
            if (!this.isPlay)
            {
                this.isPlay = true;

                string spacedScore = "";
                string scoreStr = this.score.ToString();
                for (int i = 0; i < scoreStr.Length; i++)
                {
                    spacedScore += scoreStr[i];
                    if (i < scoreStr.Length - 1)
                    {
                        spacedScore += " ";
                    }
                }

                Font pFont = FontMan.Find(Font.Name.UFOScore);
                Debug.Assert(pFont != null);
                pFont.UpdateMessage(spacedScore);
                pFont.poSpriteFont.x = this.posX;
                pFont.poSpriteFont.y = this.posY;

                TimerEventMan.Add(TimerEvent.Name.UFOScoreSpawn, this, playTime, 1);
            }
            else
            {
                Font pFont = FontMan.Find(Font.Name.UFOScore);
                Debug.Assert(pFont != null);
                pFont.UpdateMessage("");
                pFont.poSpriteFont.x = 0.0f;
                pFont.poSpriteFont.y = 0.0f;
            }
        }

        private float score;
        private float posX;
        private float posY;
        private bool isPlay;
        private float playTime;
    }
}

// --- End of File ---