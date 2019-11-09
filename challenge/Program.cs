using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TetrisWrapper;
using TetrisWrapper.Helpers;
using System.Drawing;
using System.Collections.Generic;

namespace TetrisClient
{
    internal class Program
    {
        #region Private Methods


        /// <summary>
        /// -------------------------------------------------------------------------------
        /// ------                                                                   ------
        /// ------   PARTICIPANT #1:
        /// ------   PARTICIPANT #2:
        /// ------   # ÉQUIPE:
        /// ------                                                                   ------
        /// ------------------------------------------------------------------------------- 
        /// </summary>
        [STAThread]
        private static void Main(String[] _args)
        {
            Int32 numberOfBlockPerGame = 500;       // Edit this value to change the number of block per game
            String blockGenerationSeed = "seed999"; // Edit this value to change sequence of block generation

            ITetrisAPI tetris = new TetrisWrapper.TetrisWrapper(TeamId, numberOfBlockPerGame, blockGenerationSeed, _gameSpeed: GameSpeed.Fast);

            tetris.Run(OnTick);
        }

        /// <summary>
        /// Triggered on each game cycle
        /// </summary>
        /// <param name="_state">The current game state.</param>
        /// <param name="_controller">The game controller api.</param>
        private static void OnTick(IState _state, ITetrisControllerAPI _controller)
        {
            // code here...
            TetrominoShape s = _state.CurrentPiece.Shape;

            int x, y;

            if (s == TetrominoShape.O)
            {
                TrouverCases(out x, out y, 2, _state);
                int my = 29;
                foreach (Point p in _state.CurrentPiece.Blocks)
                {
                    if (p.X < my)
                    {
                        my = p.X;
                    }
                }
                if (my < y)
                {
                    _controller.TryMoveRight();
                }
                else if (my > y)
                {
                    _controller.TryMoveLeft();
                }
                else if (my == y)
                {
                    _controller.TryMoveDown();
                }

            }



        }

        #endregion

        #region Constants

        private const String TeamId = "6c6808b9-9fe3-4126-975e-5dae24ea103c";

        #endregion


        #region MyCode

        private static void TrouverCases(out int x, out int y, int nbCase, IState _state)
        {
            int largeur = _state.GridWidth;
            int hauteur = _state.GridHeight;
            for (int i = hauteur - 1; i >= 0; i--)
            {
                for (int j = 0; j < largeur; j++)
                {
                    if (!_state.GetBlock(j, i) && !_state.GetBlock(j, i - 1))
                    {
                        for (nbCase = nbCase - 0; nbCase > 1; nbCase--)
                        {
                            if (_state.GetBlock(j + 1, i) || _state.GetBlock(j + 1, i - 1))
                            {
                                break;
                            }
                        }
                        x = i;
                        y = j;
                        return;
                    }

                }
            }
            x = 0;
            y = 0;
        }


        #endregion
    }

}