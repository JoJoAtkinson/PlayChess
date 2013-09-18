using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTest.game
{
    public class King : Piece
    {
        public King(Side side)
            : base(side)
        {
            _pieceType = Pieces.king;
            base.setImage();
        }
        public override List<Coor> getSafeMoves()
        {
            List<Coor> coors = new List<Coor>();

            checkAngle(ref coors, 1, Direction.Middle_Top);
            checkAngle(ref coors, 1, Direction.Right_Top);
            checkAngle(ref coors, 1, Direction.Right_Mid);
            checkAngle(ref coors, 1, Direction.Right_Bot);
            checkAngle(ref coors, 1, Direction.Middle_Bot);
            checkAngle(ref coors, 1, Direction.Left_Bot);
            checkAngle(ref coors, 1, Direction.Left_Mid);
            checkAngle(ref coors, 1, Direction.Left_Top);

            return coors;
        }
    }
}
