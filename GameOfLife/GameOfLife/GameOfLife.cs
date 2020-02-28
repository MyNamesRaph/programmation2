using System;
using System.Collections.Generic;
using System.Text;

namespace CegepVicto.TechInfo.H2020.P2.DA1933711.JeuxVie
{
    public class GameOfLife
    {
        private static GameOfLife instance;

        enum States
        {
            Dead = 0,
            Born = 1,
            Live = 2,
            Dying = 4
        }
        private string deadStr  = "  ";
        private string bornStr  = "░░";
        private string liveStr  = "██";
        private string dyingStr = "▓▓";
        private bool isInitialized = false;
        private ulong numIterations = 0;


        private Nullable<int> seed;
        private int sizeX;
        private int sizeY;
        private int livingRarity;
        private Cell[,] cellsArray;
        private bool isTransitionEnabled;

        public static GameOfLife GetInstance(Nullable<int> seed, int sizeX,int sizeY, int livingRarity, bool isTransitionEnabled)
        {
            if (instance == null)
                instance = new GameOfLife(seed, sizeX, sizeY, livingRarity, isTransitionEnabled);

            return instance;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed">Seed for the random number generator, set null if you do not want to use a predefined seed</param>
        /// <param name="sizeX">Width of the board; if a negative number is specified, its absolute value will be used</param>
        /// <param name="sizeY">Height of the board; if a negative number is specified, its absolute value will be used</param>
        /// <param name="livingRarity">Number above 0, 1 in X chance of being alive; if a negative number is specified, its absolute value will be used</param>
        private GameOfLife(Nullable<int> seed, int sizeX, int sizeY, int livingRarity, bool isTransitionEnabled)
        {
            this.seed = seed;
            this.sizeX = Math.Abs(sizeX);
            this.sizeY = Math.Abs(sizeY);
            this.livingRarity = Math.Abs(livingRarity);
            this.isTransitionEnabled = isTransitionEnabled;
            cellsArray = new Cell[this.sizeX, this.sizeY];
        }


        /// <summary>
        /// Sets up the random live cells at the start of the game, will use a seed if provided.
        /// Also calculates the next board state
        /// </summary>
        private void SetupStartingBoard()
        {
            Random rnd;
            if (seed == null)
                rnd = new Random();
            else
                rnd = new Random((int)seed);

            for (int x = 0; x < cellsArray.GetLength(0); x++)
            {
                for (int y = 0; y < cellsArray.GetLength(1); y++)
                {
                    int state;
                    if (rnd.Next(0, livingRarity) == 0)
                        state = (int)States.Live;
                    else
                        state = (int)States.Dead;

                    cellsArray[x, y] = new Cell(state, x, y);
                }
            }

            for (int x = 0; x < cellsArray.GetLength(0); x++)
            {
                for (int y = 0; y < cellsArray.GetLength(1); y++)
                {
                    SetCellLivingNeighbours(ref cellsArray[x, y]);
                    cellsArray[x, y].SetNextState();
                }
            }


            isInitialized = true;
        }

        
        /// <summary>
        /// Turns the Cells Array into a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Iteration: " + numIterations.ToString());
            for (int i = 0; i < cellsArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellsArray.GetLength(1); j++)
                {
                    if (cellsArray[i, j].State == (int)States.Born)
                        sb.Append(bornStr);
                    else if (cellsArray[i, j].State == (int)States.Dead)
                        sb.Append(deadStr);
                    else if (cellsArray[i, j].State == (int)States.Dying)
                        sb.Append(dyingStr);
                    else if (cellsArray[i, j].State == (int)States.Live)
                        sb.Append(liveStr);

                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Sets the number of live neighbours that are next to the Cell.
        /// </summary>
        /// <param name="cell">The Cell to update</param>
        public void SetCellLivingNeighbours(ref Cell cell)
        {
            int neighbourCount = 0;
            int x = cell.X;
            int y = cell.Y;

            if (x > 0 && y < cellsArray.GetLength(1)-1)
            {
                if (cellsArray[x - 1, y + 1].State == (int)States.Live)
                    neighbourCount++;
            }
                
            if (y < cellsArray.GetLength(1)-1)
            {
                if (cellsArray[x, y + 1].State == (int)States.Live)
                    neighbourCount++;
            }
                
            if (x < cellsArray.GetLength(0)-1 && y < cellsArray.GetLength(1)-1)
            {
                if (cellsArray[x + 1, y + 1].State == (int)States.Live)
                    neighbourCount++;
            }
             
            if (x > 0)
            {
                if (cellsArray[x - 1, y].State == (int)States.Live)
                    neighbourCount++;
            }

            if (x < cellsArray.GetLength(0)-1)
            {
                if (cellsArray[x + 1, y].State == (int)States.Live)
                    neighbourCount++;
            }

            if (x > 0 && y > 0)
            {
                if (cellsArray[x - 1, y - 1].State == (int)States.Live)
                    neighbourCount++;
            }

            if (y > 0)
            {
                if (cellsArray[x, y - 1].State == (int)States.Live)
                    neighbourCount++;
            }

            if (x < cellsArray.GetLength(0)-1 && y > 0)
            {
                if (cellsArray[x + 1, y - 1].State == (int)States.Live)
                    neighbourCount++;
            }
            cell.LivingNeighbours = neighbourCount;
        }

        /// <summary>
        /// Updates the board, will create a new board if none is created
        /// </summary>
        public void Iterate()
        {
            if (isInitialized == false)
            {
                SetupStartingBoard();
            }
            else
            {
                for (int x = 0; x < cellsArray.GetLength(0); x++)
                {
                    for (int y = 0; y < cellsArray.GetLength(1); y++)
                    {
                        cellsArray[x, y].State = cellsArray[x, y].NextState;
                    }
                }
                for (int x = 0; x < cellsArray.GetLength(0); x++)
                {
                    for (int y = 0; y < cellsArray.GetLength(1); y++)
                    {
                        SetCellLivingNeighbours(ref cellsArray[x, y]);
                        cellsArray[x, y].SetNextState();

                    }
                }

                if (isTransitionEnabled)
                {
                    for (int x = 0; x < cellsArray.GetLength(0); x++)
                    {
                        for (int y = 0; y < cellsArray.GetLength(1); y++)
                        {
                            if (cellsArray[x, y].State == (int)States.Live && cellsArray[x, y].NextState == (int)States.Dead)
                            {
                                cellsArray[x, y].State = (int)States.Dying;
                            }
                            else if (cellsArray[x, y].State == (int)States.Dead && cellsArray[x, y].NextState == (int)States.Live)
                            {
                                cellsArray[x, y].State = (int)States.Born;
                            }
                        }
                    }
                }

            }
            numIterations++;
        }
    }
}
