using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessTest.game
{
    public class MoveBlockOpption : ICommand
    {
        private Rectangle _rect = new Rectangle();

        public Rectangle block
        {
            get { return _rect; }
        }

        private Coor _coor;
        private Piece _piece;

        public MoveBlockOpption(Piece piece, Coor coor, Canvas canvas)
        {
            this._coor = coor;
            this._piece = piece;

            _rect.Width = BoardGame.INCREMENT;
            _rect.Height = BoardGame.INCREMENT;
            _rect.Opacity = .5;
            _rect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#A82129"));

            canvas.Children.Add(_rect);

            Canvas.SetLeft(_rect, coor.x * BoardGame.INCREMENT);
            Canvas.SetTop(_rect, coor.y * BoardGame.INCREMENT);
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _piece.move(_coor);
        }
    }
}
