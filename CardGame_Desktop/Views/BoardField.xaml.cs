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
    /// Interaction logic for BoardField.xaml
    /// </summary>
    public partial class BoardField : UserControl
    {
        public FieldViewModel Field
        {
            get { return (FieldViewModel)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }

        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register("Field", typeof(FieldViewModel), typeof(BoardField), new PropertyMetadata(null));

        public BoardField()
        {
            InitializeComponent();

            Loaded += BoardField_Loaded;
        }

        private void BoardField_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += BoardField_SizeChanged;
            var relativePoint = fieldButton.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            Field.XCoord = relativePoint.X + fieldButton.ActualWidth / 2;
            Field.YCoord = relativePoint.Y + fieldButton.ActualHeight / 2;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        private void BoardField_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var relativePoint = fieldButton.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            Field.XCoord = relativePoint.X + fieldButton.ActualWidth / 2;
            Field.YCoord = relativePoint.Y + fieldButton.ActualHeight / 2;
        }
    }
}
