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

            Loaded += BoardField_Loaded;
            Application.Current.MainWindow.SizeChanged += BoardField_SizeChanged;
        }

        private void BoardField_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += BoardField_SizeChanged;
            var relativePoint = playerButton.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            BoardSide.XCoord = relativePoint.X + playerButton.ActualWidth / 2;
            BoardSide.YCoord = relativePoint.Y + playerButton.ActualHeight / 2;
        }

        private void BoardField_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var relativePoint = playerButton.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            BoardSide.XCoord = relativePoint.X + playerButton.ActualWidth / 2;
            BoardSide.YCoord = relativePoint.Y + playerButton.ActualHeight / 2;
        }
    }
}
