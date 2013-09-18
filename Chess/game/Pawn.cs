using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ChessTest.game
{
    public class Pawn : Piece
    {
        Boolean hasMove = false;

        public Pawn(Side side)
            : base(side)
        {
            _pieceType = Pieces.pawn;
            base.setImage();
        }
        public override List<Coor> getSafeMoves()
        {
            List<Coor> coors = new List<Coor>();

            Direction stirght = Direction.Middle_Top;
            Direction rightAngle = Direction.Right_Top;
            Direction leftAngle = Direction.Left_Top;
 
            if (base._side == Side.black)
            {
                stirght = Direction.Middle_Bot;
                rightAngle = Direction.Right_Bot;
                leftAngle = Direction.Left_Bot;
            }

            int NumberOfMoves = 1;
            if (!hasMove && !isSpotTaken(stirght, 2))
                NumberOfMoves = 2;

            //stright
            if (!isSpotTaken(stirght, 1))
                checkAngle(ref coors, NumberOfMoves, stirght);

            //rightAngle
            if (isSpotTaken(rightAngle, 1))
                checkAngle(ref coors, 1, rightAngle);

            //leftAngle
            if (isSpotTaken(leftAngle, 1))
                checkAngle(ref coors, 1, leftAngle);

            return coors;
        }
        public override void move(Coor coor)
        {
            BoardGame boardGame = BoardGame.getInstance();

            base.move(coor);
            hasMove = true;

            if (_side == Side.black)
            {
                if (coor.y == BoardGame.BOARDSIZE - 1)
                    boardGame.mainWindow.MainGrid.Children.Add(new PieceSelection(this));
            }
            else
            {
                if (coor.y == 0)
                    boardGame.mainWindow.MainGrid.Children.Add(new PieceSelection(this));
            }
        }
        public void swapPieceTo(Pieces piece)
        {
            BoardGame boardGame = BoardGame.getInstance();

            Coor coor = base.getCurrentCoor();

            boardGame.gameBoard.Children.Remove(this.rect);

            Piece newPiece = null;

            switch (piece)
            {
                case Pieces.bishop:
                    newPiece = new Bishop(this._side);
                    break;
                case Pieces.knight:
                    newPiece = new Knight(this._side);
                    break;
                case Pieces.queen:
                    newPiece = new Queen(this._side);
                    break;
                case Pieces.rook:
                    newPiece = new Rook(this._side);
                    break;
                default:
                    throw new Exception("Pawn can only be a bishop, knight, queen or rook.");
            }

            boardGame.board[coor.y, coor.x] = newPiece;

            boardGame.gameBoard.Children.Add(newPiece.rect);
            newPiece.setCoorOnCanvas(coor);
        }
    }

}
