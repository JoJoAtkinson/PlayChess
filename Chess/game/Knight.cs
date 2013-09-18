using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTest.game
{
    public class Knight : Piece
    {
        public Knight(Side side)
            : base(side)
        {
            _pieceType = Pieces.knight;
            base.setImage();
        }
        public override List<Coor> getSafeMoves()
        {
            List<Coor> coors = new List<Coor>();
            Coor currentCoor = getCurrentCoor();

            checkMove(ref coors, new Coor(currentCoor.x + 1, currentCoor.y + 2));
            checkMove(ref coors, new Coor(currentCoor.x + 2, currentCoor.y + 1));
            checkMove(ref coors, new Coor(currentCoor.x + 2, currentCoor.y - 1));
            checkMove(ref coors, new Coor(currentCoor.x + 1, currentCoor.y - 2));
            checkMove(ref coors, new Coor(currentCoor.x - 1, currentCoor.y - 2));
            checkMove(ref coors, new Coor(currentCoor.x - 2, currentCoor.y - 1));
            checkMove(ref coors, new Coor(currentCoor.x - 2, currentCoor.y + 1));
            checkMove(ref coors, new Coor(currentCoor.x - 1, currentCoor.y + 2));

            return coors;
        }
        private void checkMove(ref List<Coor> coors, Coor coor)
        {
            if (hasPiece(coor))
            {
                if (!isFriendly(coor))
                {
                    coors.Add(coor);
                }
            }
            else
            {
                coors.Add(coor);
            }
        }
    }
}
