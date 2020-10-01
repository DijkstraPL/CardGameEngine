using CardGame_Desktop.ViewModels;
using CardGame_Game.BoardTable;
using System;
using System.Collections.Generic;
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

namespace CardGame_Desktop.Views
{
    /// <summary>
    /// Interaction logic for PlayerBoard.xaml
    /// </summary>
    public partial class PlayerBoard : UserControl
    {
        public BoardSideViewModel BoardSide
        {
            get { return (BoardSideViewModel)GetValue(BoardSideProperty); }
            set { SetValue(BoardSideProperty, value); }
        }

        public static readonly DependencyProperty BoardSideProperty =
            DependencyProperty.Register("BoardSide", typeof(BoardSideViewModel), typeof(PlayerBoard), new PropertyMetadata(null));


        public PlayerBoard()
        {
            InitializeComponent();
        }
    }
}
