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
        }
    }
}
