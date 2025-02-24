using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class Helper
    {
        public void HelperNumberRestriction(TextBox phoneNum)
        {
            string currentText = phoneNum.Text;//ginakuha niya ang sulod sa phoneNum na textbox 
            phoneNum.Text = string.Concat(currentText.Where(char.IsDigit));//ang purpose ani kay gina kuha lang nya kay 0-9  ug gina wala ang letra o  special char
                                                                           //human i butang ang value sa textbox

            if (phoneNum.Text.Length > 10)// if ang gi input na nuumber kay subra sa 10 putlon niya ang subra 
            {
                phoneNum.Text = phoneNum.Text.Substring(0, 10); //ang unang 10 digits lang ang kuhaon niya
            }

            phoneNum.SelectionStart = phoneNum.Text.Length;// para ang cursur magpundo sa last
        }
        public void Copies(TextBox copy)
        {
            string currentText = copy.Text;
            copy.Text = string.Concat(currentText.Where(char.IsDigit));
            if (currentText.Length > 2)
            {
                copy.Text = currentText.Substring(0, 2);
            }
            copy.SelectionStart = copy.Text.Length;
        }
        public void PasswordHelper(TextBox passwordtext)
        {
            string currentText = passwordtext.Text;

            if (currentText.Length > 8)
            {
                passwordtext.Text = currentText.Substring(0, 8);
            }
            passwordtext.SelectionStart = passwordtext.Text.Length;
        }

        public bool isValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        public bool isValidAddress(string street)
        {
            return !string.IsNullOrEmpty(street);
        }
        public bool isValidEmail(string email)
        {
            return email.EndsWith("@gmail.com") && email.Contains("@");
        }
      
        public bool isValidPhoneNumber(TextBox countryCode, TextBox phoneNumber)
        {
            string fullNumber = countryCode.Text + phoneNumber.Text;

            if (string.IsNullOrWhiteSpace(countryCode.Text) || string.IsNullOrWhiteSpace(phoneNumber.Text))
            {
                return false;
            }

            return Regex.IsMatch(fullNumber, @"^\+\d{1,3}\d{10}$");
        }
    }
}
