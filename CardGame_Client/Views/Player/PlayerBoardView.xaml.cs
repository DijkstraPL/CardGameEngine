using CardGame_Client.ViewModels.Interfaces;
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

namespace CardGame_Client.Views.Player
{
    /// <summary>
    /// Interaction logic for PlayerBoardView.xaml
    /// </summary>
    public partial class PlayerBoardView : UserControl
    {
        private readonly Window _window;

        public PlayerBoardView()
        {
            InitializeComponent();

            _window = Application.Current.MainWindow;
            _window.SizeChanged += Window_SizeChanged;

            Loaded += PlayerBoardView_Loaded;
        }

        private void PlayerBoardView_Loaded(object sender, RoutedEventArgs e)
        {
            SetCoords();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCoords();
        }

        private void SetCoords()
        {
            try
            {
                if (this.DataContext is IPosition position)
                {
                    var point = playerButton.TransformToAncestor(_window).Transform(new Point(0, 0));
                    position.XCoord = point.X + playerButton.ActualWidth / 2;
                    position.YCoord = point.Y + playerButton.ActualHeight / 2;
                }
            }
            catch
            {
            }
        }
    }
}
