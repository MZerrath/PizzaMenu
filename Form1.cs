using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace PizzaMenu
{
    public partial class Form1 : Form
    {
        float total = 0f;
        string selectedPizza = "";
        string pizzaSize = "";
        string pizzaTopping = " None";
        string selectedBeverage = "";
        string selectedDessert = "";
        ArrayList typeList = new ArrayList { "Cheese", "Neapolitan", "Margherita",
                                              "Calzone", "Stromboli", "Deep Dish",
                                              "Hawaiian", "Lahma Bi Ajeen", "M&L Special"};
        ArrayList pizzaType = new ArrayList();

        public Form1()
        {
            InitializeComponent();
            total = 0f;
            ResetForm();
        }

        private void CalculatePizzaCost(object sender, EventArgs e)
        {
            if (selectedPizza != "")
            {
                if (rdoSmallSize.Checked || rdoMediumSize.Checked || rdoLargeSize.Checked)
                {
                    // Prices for Pizza Types
                    if (selectedPizza == "Cheese" || selectedPizza == "Neapolitan"
                        || selectedPizza == "Margherita")
                    {
                        total = 10;
                    }
                    else if (selectedPizza == "Calzone" || selectedPizza == "Stromboli"
                        || selectedPizza == "Deep Dish" || selectedPizza == "Hawaiian")
                    {
                        total = 12.5f;
                    }
                    else if (selectedPizza == "Lahma Bi Ajeen")
                    {
                        total = 13;
                    }
                    else if (selectedPizza == "M&L Special")
                    {
                        total = 14;
                    }

                    // Prices for Pizza Sizes
                    if (rdoSmallSize.Checked)
                    {
                        total -= 2;
                        pizzaSize = " Small";
                    }
                    else if (rdoMediumSize.Checked)
                    {
                        total += 0;
                        pizzaSize = " Medium";
                    }
                    else if (rdoLargeSize.Checked)
                    {
                        total += 5;
                        pizzaSize = " Large";
                    }

                    // Prices for Pizza Toppings
                    pizzaTopping = "";
                    if (chkOlives.Checked)
                    {
                        total += 2;
                        pizzaTopping += " Olives";
                    }
                    if (chkMushrooms.Checked)
                    {
                        total += 2;
                        pizzaTopping += " Mushrooms";
                    }
                    if (chkPepperoni.Checked)
                    {
                        total += 2;
                        pizzaTopping += " Pepperoni";
                    }
                    if (chkPineapple.Checked)
                    {
                        total += 100;
                        pizzaTopping += " Pineapple";
                    }
                    if (pizzaTopping == "")
                    {
                        pizzaTopping = " None";
                    }

                    // Prices for beverages
                    selectedBeverage = "";
                    if (chkCola.Checked)
                    {
                        total += 2.5f;
                        selectedBeverage += " Cola";
                    }
                    if (chkJuice.Checked)
                    {
                        total += 3;
                        selectedBeverage += " Juice";
                    }
                    if (selectedBeverage == "")
                    {
                        selectedBeverage = " None";
                    }

                    // Prices for desserts
                    selectedDessert = "";
                    if (chkApplePie.Checked)
                    {
                        total += 3;
                        selectedDessert += " Apple Pie";
                    }
                    if (chkChocCake.Checked)
                    {
                        total += 4;
                        selectedDessert += " Chocolate Cake";
                    }
                    if (selectedDessert == "")
                    {
                        selectedDessert = " None";
                    }

                    // Displays order into txtOrder
                    txtOrder.Text = selectedPizza + Environment.NewLine
                        + "Size:" + pizzaSize + Environment.NewLine
                        + "Toppings:" + pizzaTopping + Environment.NewLine
                        + "Beverage:" + selectedBeverage + Environment.NewLine
                        + "Dessert:" + selectedDessert; ;

                    txtTotal.Text = String.Format("{0:C}", total);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPizza = listBox1.SelectedItem.ToString();
            CalculatePizzaCost(sender, e);
        }

        // Excludes pizzas from the menu depending on dietary restriction
        private void cboDietary_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diet = cboDietary.SelectedItem.ToString();

            // Clears the pizzaType array list then refills the list
            // using values stored in typeList
            pizzaType.Clear();
            foreach (string type in typeList)
            {
                pizzaType.Add(type);
            }

            // Excludes certain pizza types based on dietary restriction
            switch (diet)
            {
                case "Vegan":
                case "Vegetarian":
                    pizzaType.Remove("Hawaiian");
                    pizzaType.Remove("Calzone");
                    pizzaType.Remove("Stromboli");
                    pizzaType.Remove("Lahma Bi Ajeen");
                    pizzaType.Remove("M&L Special");
                    break;
                case "Non-GMO":
                    pizzaType.Remove("Stromboli");
                    break;
                case "Gluten free":
                case "Lactose":
                case "Paleo":
                    pizzaType.Remove("Cheese");
                    pizzaType.Remove("Neapolitan");
                    pizzaType.Remove("Margherita");
                    pizzaType.Remove("Deep Dish");
                    break;
                case "100 Mile":
                case "Kosher":
                    pizzaType.Remove("Hawaiian");
                    pizzaType.Remove("Stromboli");
                    break;
                case "None":
                    break;
            }
            pizzaType.Sort();
            listBox1.Items.Clear(); // Delete all the items already in the ListBox.
            listBox1.Items.AddRange(pizzaType.Cast<object>().ToArray()); // Convert the ArrayList to an array.
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            // Reset checkboxes
            chkOlives.Checked = false;
            chkMushrooms.Checked = false;
            chkPepperoni.Checked = false;
            chkPineapple.Checked = false;
            chkCola.Checked = false;
            chkJuice.Checked = false;
            chkApplePie.Checked = false;
            chkChocCake.Checked = false;

            // Reset radio buttons
            rdoSmallSize.Checked = false;
            rdoMediumSize.Checked = false;
            rdoLargeSize.Checked = false;

            // Reset textboxes
            txtTotal.Text = "";
            txtOrder.Text = "";

            // Reset combobox
            cboDietary.SelectedIndex = 0;

            // Reset listbox
            listBox1.SelectedIndex = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Brought to you by the sisters Maria and Luisa!");
        }
    }
}
