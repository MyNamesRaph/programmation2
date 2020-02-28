using System;
using System.Collections;

namespace GameOfLifeBitsLib
{
    public class GameOfLifeBit
    {
        enum States
        {
            
        }
        private string deadStr = " ";
        private string bornStr = "░";
        private string liveStr = "█";
        private string dyingStr = "▓";
        private bool isInitialized = false;
        private ulong numIterations = 0;


        private Nullable<int> seed;
        private int sizeX;
        private int sizeY;
        private int livingRarity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed">Seed for the random number generator</param>
        /// <param name="sizeX">Width of the board; if a negative number is specified, its absolute value will be used</param>
        /// <param name="sizeY">Height of the board; if a negative number is specified, its absolute value will be used</param>
        /// <param name="livingRarity">Number above 0, 1 in X chance of being alive</param>
        public GameOfLife(Nullable<int> seed, int sizeX, int sizeY, int livingRarity, bool isTransitionEnabled)
        {
            this.seed = seed;
            this.sizeX = Math.Abs(sizeX);
            this.sizeY = Math.Abs(sizeY);
            this.livingRarity = livingRarity;
            this.isTransitionEnabled = isTransitionEnabled;

        }
    }
}
