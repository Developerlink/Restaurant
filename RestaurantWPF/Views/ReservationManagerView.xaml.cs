﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace RestaurantWPF.Views
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : Window
    {
        public List<TestButton> buttons = new List<TestButton>();

        public TableView()
        {
            InitializeComponent();

            buttons.Add(new TestButton("Awaiting"));
            buttons.Add(new TestButton("Arrived"));
            buttons.Add(new TestButton("Seated"));
            buttons.Add(new TestButton("Ordered"));
            buttons.Add(new TestButton("Finished"));
            buttons.Add(new TestButton("Cancelled"));
            buttons.Add(new TestButton("No show"));

            StatusesControl.ItemsSource = buttons;
        }



    }

    public class TestButton
    {
        public string Name { get; set; }

        public TestButton(string name)
        {
            Name = name;
        }
    }

}
