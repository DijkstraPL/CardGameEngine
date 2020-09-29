using CardGame_Desktop.ViewModels;
using CardGame_Game.Players.Interfaces;
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
    /// Interaction logic for PlayerData.xaml
    /// </summary>
    public partial class PlayerData : UserControl
    {
        public PlayerViewModel Player
        {
            get { return (PlayerViewModel)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static readonly DependencyProperty PlayerProperty =
            DependencyProperty.Register("Player", typeof(PlayerViewModel), typeof(PlayerData));
        public PlayerData()
        {
            InitializeComponent();
        }
    }
}
