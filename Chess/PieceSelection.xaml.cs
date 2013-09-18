using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessTest.game;

namespace ChessTest
{
    /// <summary>
    /// Interaction logic for PieceSelection.xaml
    /// </summary>
    public partial class PieceSelection : UserControl
    {
        public PieceSelection(Piece piece)
        {
            InitializeComponent();

            Rook_Rec.Fill = piece.getImageBrush(Pieces.rook);
            Knight_Rec.Fill = piece.getImageBrush(Pieces.knight);
            Bishop_Rec.Fill = piece.getImageBrush(Pieces.bishop);
            Queen_Rec.Fill = piece.getImageBrush(Pieces.queen);

            Rook_Btn.Tag = new PieceBtnProperties(piece, Pieces.rook);
            Knight_Btn.Tag = new PieceBtnProperties(piece, Pieces.knight);
            Bishop_Btn.Tag = new PieceBtnProperties(piece, Pieces.bishop);
            Queen_Btn.Tag = new PieceBtnProperties(piece, Pieces.queen);
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            PieceBtnProperties properties = (PieceBtnProperties)btn.Tag;

            Pawn pawn = (Pawn)properties.piece;

            pawn.swapPieceTo(properties.pieceType);

            ((Grid)this.Parent).Children.Remove(this);
        }
    }
    public class PieceBtnProperties
    {
        public Piece piece { get; set; }
        public Pieces pieceType { get; set; }

        public PieceBtnProperties(Piece piece, Pieces pieceType)
        {
            this.piece = piece;
            this.pieceType = pieceType;
        }
    }
}
