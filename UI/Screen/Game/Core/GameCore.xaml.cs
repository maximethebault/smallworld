﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model.Game;

namespace UI.Screen.Game.Core
{
    /// <summary>
    /// Logique d'interaction pour GameCore.xaml
    /// </summary>
    public partial class GameCore : UserControl
    {
        public BitmapImage[] Tiles { get; set; }

        public IGame Game { get; set; }

        public GameCore(IGame game, BitmapImage[] tiles)
        {
            Game = game;
            Tiles = tiles;
            InitializeComponent();
        }
    }
}