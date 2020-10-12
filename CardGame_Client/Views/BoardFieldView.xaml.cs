using CardGame_Client.ViewModels;
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

namespace CardGame_Client.Views
{
    /// <summary>
    /// Interaction logic for BoardFieldView.xaml
    /// </summary>
    public partial class BoardFieldView : UserControl
    {
        private readonly Window _window;

        public BoardFieldView()
        {
            InitializeComponent();

            _window = Application.Current.MainWindow;
            _window.SizeChanged += Window_SizeChanged;

            Loaded += BoardFieldView_Loaded;
        }

        private void BoardFieldView_Loaded(object sender, RoutedEventArgs e)
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
                    var point = fieldButton.TransformToAncestor(_window).Transform(new Point(0, 0));
                    position.XCoord = point.X + fieldButton.ActualWidth / 2;
                    position.YCoord = point.Y + fieldButton.ActualHeight / 2;
                }
            }
            catch
            {
            }
        }
    }
}
