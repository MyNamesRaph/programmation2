using System;

namespace CegepVicto.TechInfo.H2020.P2.DA1933711.JeuxVie
{
    public class Cell
    {
        enum States
        {
            Dead = 0,
            Born = 1,
            Live = 2,
            Dying = 4
        }

        private int state;
        private int livingNeighbours;
        private int nextState;
        private int x;
        private int y;

        public Cell(int state, int x, int y)
        {
            this.state = state;
            this.x = x;
            this.y = y;
        }

        public int State
        {
            get { return state; }
            set { state = value; }
        }
        public int NextState
        {
            get { return nextState; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }
        public int LivingNeighbours
        {
            get { return livingNeighbours; }
            set { livingNeighbours = value; }
        }

        /// <summary>
        /// Calculates the next state of the cell
        /// </summary>
        public void SetNextState()
        {
            //Every cell that does not fill the bottom requirements die
            nextState = (int)States.Dead;

            //Every live cell with 2 or 3 neighbours survives
            if (state == (int)States.Live)
            {
                if (livingNeighbours == 2 || livingNeighbours == 3)
                    nextState = (int)States.Live;
            }
            //Every dead cell with 3 neighbours is born
            if (state == (int)States.Dead)
            {
                if (livingNeighbours == 3)
                    nextState = (int)States.Live;
            }

            
            
        }

    }
}
