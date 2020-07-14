using System;
using System.Collections.Generic;
using System.Windows;

namespace StoreFrontCodeProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Item> Items = new List<Item>
                                    {
                                        new Item {Name = "Aluminum Shackles", ShelfLife = 10, Worth = 20},
                                        new Item {Name = "Gold", ShelfLife = 2, Worth = 50},
                                        new Item {Name = "Plutonium Pinball Parts", ShelfLife = 5, Worth = 7},
                                        new Item {Name = "Cadmium", ShelfLife = 0, Worth = 80},
                                        new Item {Name = "Helium", ShelfLife = 15, Worth = 38},
                                        new Item {Name = "Alchemy Iron", ShelfLife = 3, Worth = 75}
                                    };

        public MainWindow()
        {
            InitializeComponent();
            ItemsDataGrid.ItemsSource = Items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateWorth();
        }

        private void UpdateWorth()
        {
            foreach (Item item in Items)
            {
                Boolean doNotChange = false;
                Boolean increaseWorth = false;
                Boolean shelfLifeHasPassed = false;
                Int32 maxWorth = 50;

                if (item.Name == "Cadmium" ||
                    item.Worth == 0)
                {
                    doNotChange = true;
                }
                else if (item.Name == "Gold" ||
                         item.Name == "Helium")
                {
                    increaseWorth = true;
                }
                else if (item.Name == "Alchemy Iron")
                {
                    maxWorth = 100;
                }

                if (!doNotChange)
                {
                    if (item.ShelfLife <= 0)
                    {
                        shelfLifeHasPassed = true;
                    }

                    if (increaseWorth)
                    {
                        if (item.Name == "Helium")
                        {
                            if (shelfLifeHasPassed &&
                                item.Worth != 0)
                            {
                                item.Worth = 0;
                            }
                            else if (item.Worth < 50)
                            {
                                item.Worth += 1;

                                if (item.ShelfLife <= 10 &&
                                    item.Worth < 50)
                                {
                                    item.Worth += 1;
                                }
                                
                                if (item.ShelfLife <= 5 &&
                                    item.Worth < 50)
                                {
                                    item.Worth += 1;
                                }
                            }
                        }
                        else
                        {
                            if (item.Worth < 50)
                            {
                                item.Worth += 1;
                            }
                        }
                    }
                    else
                    {
                        if (item.Worth > 0)
                        {
                            item.Worth -= 1;
                        }

                        if (item.Name == "Alchemy Iron" &&
                            item.Worth > 0)
                        {
                            item.Worth -= 1;
                        }

                        if (shelfLifeHasPassed &&
                            item.Worth > 0)
                        {
                            item.Worth -= 1;

                            if (item.Name == "Alchemy Iron" &&
                                item.Worth > 0)
                            {
                                item.Worth -= 1;
                            }
                        }
                    }

                    if (item.ShelfLife > 0)
                    {
                        item.ShelfLife -= 1;
                    }
                    
                    if (item.Worth > maxWorth)
                    {
                        item.Worth = maxWorth;
                    }
                }
            }

            ItemsDataGrid.ItemsSource = null;
            ItemsDataGrid.ItemsSource = Items;
            ItemsDataGrid.Items.Refresh();
        }
    }
}
