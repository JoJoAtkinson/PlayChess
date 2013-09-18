using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;

namespace ChessTest.game
{

    public enum Side
    {
        white,
        black
    }

    public enum Pieces
    {
        bishop,
        king,
        knight,
        pawn,
        queen,
        rook
    }

    public abstract class Piece : ICommand
    {
        protected Side _side { get; set; }
        protected Rectangle _rect = new Rectangle();

        protected Pieces _pieceType { get; set; }
        public Pieces pieceType { get { return _pieceType; } }
        public Rectangle rect { get{ return _rect; } }

        public Piece(Side side)
        {
            _side = side;

            _rect.Width = BoardGame.INCREMENT;
            _rect.Height = BoardGame.INCREMENT;
            _rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
             
            _rect.InputBindings.Add(new MouseBinding(
                    this, new MouseGesture(MouseAction.LeftClick)
                ));
        }

        #region ICommand Functions
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            List<Coor> coors = getSafeMoves();

            BoardGame boardGame = BoardGame.getInstance();
            boardGame.moves.Children.Clear();

            drawMoveOptions(coors);
        }
        #endregion

        #region Graphics Setup
        protected void setImage()
        {
            _rect.Fill = getImageBrush(pieceType);
        }
        public ImageBrush getImageBrush(Pieces pieceType)
        {
            /*images were imbeded, this gets em*/
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream stream = myAssembly.GetManifestResourceStream(getImageResorcePath(pieceType));

            BitmapFrame bmp = BitmapFrame.Create(stream);

            Image image = new Image();
            image.Source = bmp;

            return new ImageBrush(image.Source);
        }
        public string getImageResorcePath(Pieces pieceType)
        {
            string ResorcePath = "ChessTest.Resources.Images.";

            if (_side == Side.white)
                ResorcePath += "white_" + pieceType.ToString() + ".png";
            else
                ResorcePath += "black_" + pieceType.ToString() + ".png";

            return ResorcePath;
        }
        #endregion

        #region Movement Controls
        public virtual void move(Coor coor)
        {
            Coor currentLocation = getCurrentCoor();

            BoardGame boardGame = BoardGame.getInstance();

            Piece PieceInSpot = boardGame.board[coor.y, coor.x];

            if (PieceInSpot != null)
            {
                boardGame.gameBoard.Children.Remove(PieceInSpot.rect);
            }

            setCoorOnCanvas(coor);

            boardGame.board[currentLocation.y, currentLocation.x] = null;
            boardGame.board[coor.y, coor.x] = this;

            boardGame.moves.Children.Clear();
        }
        public abstract List<Coor> getSafeMoves();

        /// <summary>Postions Piece on its current canvas</summary>
        public void setCoorOnCanvas(Coor coor)
        {
            Canvas.SetLeft(rect, coor.x * BoardGame.INCREMENT);
            Canvas.SetTop(rect, coor.y * BoardGame.INCREMENT);
        }

        /// <summary>Draws all the red blocks and attaches them to click events to allow chess moves</summary>
        protected void drawMoveOptions(List<Coor> coors)
        {
            BoardGame boardGame = BoardGame.getInstance();

            foreach (Coor coor in coors)
            {
                if (coor.isValid) /*Double checks*/
                {
                    MoveBlockOpption moveBlock = new MoveBlockOpption(this, coor, boardGame.moves);

                    moveBlock.block.InputBindings.Add(new MouseBinding(
                        moveBlock, new MouseGesture(MouseAction.LeftClick)
                    ));
                }
            }
        }

        protected Coor getCurrentCoor()
        {
            return new Coor(
                 Convert.ToInt32(Canvas.GetLeft(rect)) / BoardGame.INCREMENT,
                 Convert.ToInt32(Canvas.GetTop(rect)) / BoardGame.INCREMENT);
        }

        public enum Direction
        {
            Middle_Top,
            Right_Top,
            Right_Mid,
            Right_Bot,
            Middle_Bot,
            Left_Bot,
            Left_Mid,
            Left_Top
        }
        private int[,] angleMods = new int[8, 2] { { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 } };
        
        /// <summary>Checks a single angle starting from Piece location</summary>
        /// <param name="coors">Populates reference list with all found spots</param>
        /// <param name="NumberOfSpaces">How far out it checks</param>
        protected void checkAngle(ref List<Coor> coors, int NumberOfSpaces, Direction direction)
        {
            int xMod = angleMods[(int)direction, 0];
            int yMod = angleMods[(int)direction, 1];

            Coor currentCoor = getCurrentCoor();

            for (int i = 0; i < NumberOfSpaces; i++)
            {
                Coor coor = new Coor(currentCoor.x + (i + 1) * xMod, currentCoor.y + (i + 1) * yMod); /*+1 because zero is the piece itself*/

                if (!coor.isValid) /*So i dont add any unnessary movements*/
                    break;

                if (hasPiece(coor))
                {
                    if (!isFriendly(coor))
                        coors.Add(coor);

                    break;
                }
                coors.Add(coor);
            }
        }

        /// <summary> Using the current Piece location as origin, checks if a single spot has a piece and is unfriendly</summary>
        protected Boolean isSpotTaken(Direction direction, int NumberOfSpacesOut)
        {
            int xMod = angleMods[(int)direction, 0];
            int yMod = angleMods[(int)direction, 1];

            Coor currentCoor = getCurrentCoor();

            Coor coor = new Coor(currentCoor.x + NumberOfSpacesOut * xMod, currentCoor.y + NumberOfSpacesOut * yMod);

            if (!coor.isValid)
                return false;

            if (hasPiece(coor) && !isFriendly(coor))
                return true;
            else
                return false;
        }

        protected Boolean hasPiece(Coor coor)
        {
            if (coor.isValid)
            {
                BoardGame boardGame = BoardGame.getInstance();

                Piece target = boardGame.board[coor.y, coor.x];

                if (target == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        protected Boolean isFriendly(Coor coor)
        {
            if (coor.isValid)
            {
                BoardGame boardGame = BoardGame.getInstance();

                Piece target = boardGame.board[coor.y, coor.x];

                if (target == null)
                    return true;

                if (this._side == target._side)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
    public class Coor
    {
        public Boolean isValid { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Coor(int x, int y)
        {
            if (x >= BoardGame.BOARDSIZE || y >= BoardGame.BOARDSIZE || x < 0 || y < 0)
                isValid = false;
            else
                isValid = true;

            this.x = x;
            this.y = y;
        }
    }
}
