using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class CountryCode
    {
        public string Country { get; set; }
        public string Code { get; set; }
    }
    public class PhoneNumberList
    {
        public static void ListPhneNumber(EventHandler eventHandler, ComboBox comboBox)
        {
            var countryCodes = new List<CountryCode>
        {
            new CountryCode { Country = "PHL", Code = "+63" },
         };

            comboBox.DataSource = null;

            foreach (var countryCode in countryCodes)
            {
                comboBox.Items.Add($"{countryCode.Country} | {countryCode.Code}");
            }
            comboBox.SelectedIndexChanged += eventHandler;
        }
        public static void ComboBox_autoModifier(EventHandler eventHandler, ComboBox comboBox, TextBox textBox)
        {
            if (comboBox.SelectedIndex >= 0)
            {
                string selectedItem = comboBox.SelectedItem.ToString();

                var parts = selectedItem.Split('|');
                if (parts.Length == 2)
                {
                    string countryCode = parts[1].Trim();
                    comboBox.SelectedIndexChanged -= eventHandler;
                    textBox.Text = countryCode;
                    comboBox.SelectedIndexChanged += eventHandler;
                }
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }
    }
}
