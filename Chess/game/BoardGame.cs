using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ChessTest.game
{
    public class BoardGame
    {
        public const int INCREMENT = 50;
        public const int BOARDSIZE = 8;

        private Piece[,] _boardStart = new Piece[BOARDSIZE, BOARDSIZE]
        {
            { new Rook(Side.black), new Knight(Side.black), new Bishop(Side.black), new King(Side.black), new Queen(Side.black), new Bishop(Side.black), new Knight(Side.black), new Rook(Side.black) },
            { new Pawn(Side.black), new Pawn(Side.black),   new Pawn(Side.black),   new Pawn(Side.black), new Pawn(Side.black),  new Pawn(Side.black),   new Pawn(Side.black),   new Pawn(Side.black) },
            { null,                 null,                   null,                   null,                 null,                  null,                   null,                   null                 },
            { null,                 null,                   null,                   null,                 null,                  null,                   null,                   null                 },
            { null,                 null,                   null,                   null,                 null,                  null,                   null,                   null                 },
            { null,                 null,                   null,                   null,                 null,                  null,                   null,                   null                 },
            { new Pawn(Side.white), new Pawn(Side.white),   new Pawn(Side.white),   new Pawn(Side.white), new Pawn(Side.white),  new Pawn(Side.white),   new Pawn(Side.white),   new Pawn(Side.white) },
            { new Rook(Side.white), new Knight(Side.white), new Bishop(Side.white), new King(Side.white), new Queen(Side.white), new Bishop(Side.white), new Knight(Side.white), new Rook(Side.white) }
        };

        private Piece[,] _board = new Piece[BOARDSIZE, BOARDSIZE];

        public Piece[,] board{
            get{
                return _board;
            }
        }
        public Canvas gameBoard
        {
            get
            {
                return _mainWindow.ChessBoard;
            }
        }
        public Canvas moves
        {
            get
            {
                return _mainWindow.Moves;
            }
        }

        private MainWindow _mainWindow = null;
        public MainWindow mainWindow
        {
            get
            {
                return _mainWindow;
            }
        }


        private static BoardGame instance = null;
        public static BoardGame getInstance()
        {
            if(instance == null)
                instance = new BoardGame();

            return instance;
        }
        public void init(MainWindow mainWindow)
        {
            Array.Copy(_boardStart, _board, _boardStart.Length);

            _mainWindow = mainWindow;

            setupPieces();
            setUpBoard();
        }
        private void setupPieces()
        {
            for (int y = 0; y < _board.GetLength(0); y++)
            {
                for (int x = 0; x < _board.GetLength(1); x++)
                {
                    Piece piece = _board[y,x];

                    if (piece != null)
                    {
                        Rectangle chessPiece = piece.rect;

                        gameBoard.Children.Add(chessPiece);

                        Canvas.SetLeft(chessPiece, x * INCREMENT);
                        Canvas.SetTop(chessPiece, y * INCREMENT);
                    }
                }
            }
        }

        /// <summary>Draws all the squres on the board</summary>
        private void setUpBoard()
        {
            Boolean plotBlock = true;

            for (int y = 0; y < _board.GetLength(0); y++)
            {
                for (int x = 0; x < _board.GetLength(1); x++)
                {
                    if (plotBlock)
                    {
                        Rectangle colorBlock = new Rectangle();
                        colorBlock.Width = BoardGame.INCREMENT;
                        colorBlock.Height = BoardGame.INCREMENT;
                        colorBlock.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#565656"));

                        _mainWindow.grid.Children.Add(colorBlock);

                        Canvas.SetLeft(colorBlock, x * INCREMENT);
                        Canvas.SetTop(colorBlock, y * INCREMENT);
                    }
                    plotBlock = !plotBlock;
                }
                plotBlock = !plotBlock;
            }
        }

        internal void reset()
        {
            Array.Copy(_boardStart, _board, _boardStart.Length);

            gameBoard.Children.Clear();
            setupPieces();
        }
    }
}
