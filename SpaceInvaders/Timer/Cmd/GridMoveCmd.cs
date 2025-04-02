using System;
using System.Diagnostics;
using System.Windows;

namespace SE456
{
    class GridMoveCmd : Command
    {
        public GridMoveCmd(GameObject _pGrid)
        {
            Debug.Assert(_pGrid != null);
            this.pGrid = _pGrid;
            //this.count = 0;
        }

        public override void Execute(float deltaTime)
        {
            AlienGrid pAlienGrid = (AlienGrid)this.pGrid;
            int curNum = pAlienGrid.GetNumAliens();

            float newDeltaTime = 0.05f + (0.7f - 0.05f) * curNum / 55;
            float newDeltaX = 16.0f - 12.0f * curNum / 55;

            //Debug.WriteLine("c:{0} x:{1} t:{2}", curNum, newDeltaX, newDeltaTime);

            if (curNum == 0)
            {
                return;
            }

            pAlienGrid.MoveGridX();
            pAlienGrid.MoveGridY();

            pAlienGrid.SetDeltaX(newDeltaX);

            // Add itself back to timer
            TimerEventMan.Add(TimerEvent.Name.GridMove, this, newDeltaTime);
        }

        // Data: ---------------
        private GameObject pGrid;
        //private int count;
    }
}

// --- End of File ---
