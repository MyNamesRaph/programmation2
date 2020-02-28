using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CegepVicto.TechInfo.H2020.P2.DA1933711.JeuxVie;
using System.Threading;

namespace GameOfLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            GameOfLife game = GameOfLife.GetInstance(null,47, 105, 2, false);


            while (true)
            {
                game.Iterate();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game);
                Thread.Sleep(50);
            }
        }
    }
}
