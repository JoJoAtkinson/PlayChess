using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessTest.game
{
    public class Bishop : Piece
    {
        public Bishop(Side side)
            : base(side)
        {
            _pieceType = Pieces.bishop;
            base.setImage();
        }
        public override List<Coor> getSafeMoves()
        {
            List<Coor> coors = new List<Coor>();

            checkAngle(ref coors, 8, Direction.Right_Top);
            checkAngle(ref coors, 8, Direction.Right_Bot);
            checkAngle(ref coors, 8, Direction.Left_Bot);
            checkAngle(ref coors, 8, Direction.Left_Top);

            return coors;
        }
    }
}
