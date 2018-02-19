using System;
using System.Windows.Forms;

namespace IPv4AddressTextBox
{
    public partial class IPv4AddressTextBox : UserControl
    {
        public IPv4AddressTextBox()
        {
            InitializeComponent();
        }

        private void ipDiv_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = IsNonAllowedKeyDown(e.KeyData);

            if (IsDecimalKeyDown(e.KeyData))
            {
                FocusNextIpDiv((TextBox) sender);
                e.SuppressKeyPress = true;
            }
        }

        private void FocusNextIpDiv(TextBox sender)
        {
            var senderName = sender.Name;
            TextBox nextDiv = null;

            if (senderName == ipDiv0.Name)
                nextDiv = ipDiv1;
            else if (senderName == ipDiv1.Name)
                nextDiv = ipDiv2;
            else if (senderName == ipDiv2.Name)
                nextDiv = ipDiv3;

            nextDiv?.Focus();
        }

        private bool IsNonAllowedKeyDown(Keys key)
        {
            var isKeyAllowed = false;

            foreach (var allowedKey in _allowedKeys)
            {
                if (key == allowedKey)
                {
                    isKeyAllowed = true;
                    break;
                }
            }

            return !isKeyAllowed;
        }

        private bool IsDecimalKeyDown(Keys key)
        {
            return key == Decimal;
        }

        private static readonly Keys Decimal = Keys.Decimal;

        private readonly Keys[] _allowedKeys =
        {
            Keys.D0,
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.D5,
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9,
            Keys.NumPad0,
            Keys.NumPad1,
            Keys.NumPad2,
            Keys.NumPad3,
            Keys.NumPad4,
            Keys.NumPad5,
            Keys.NumPad6,
            Keys.NumPad7,
            Keys.NumPad8,
            Keys.NumPad9,
            Decimal,
            Keys.Back,
            Keys.Delete,
            Keys.Up,
            Keys.Down,
            Keys.Right,
            Keys.Left
        };

        private bool _surpressTextChangedEvent;

        private void IpDiv_TextChanged(object sender, EventArgs e)
        {
            if (_surpressTextChangedEvent) return;
            _surpressTextChangedEvent = true;

            var senderTextBox = (TextBox) sender;
            var textDivision = senderTextBox.Text;

            var isValueParsed = int.TryParse(textDivision, out var valueDivision);
            if (isValueParsed)
            {
                //in case the division value is greater than largest IPv4 value (255)
                senderTextBox.Text = valueDivision > 255 ? @"255" : valueDivision.ToString();
            }
            else
            {
                //just to be sure there is no mistaken input
                senderTextBox.Text = '0'.ToString();
            }

            //set cursor location to end of the division
            var lastCharacterPosition = textDivision.Length;
            if(lastCharacterPosition >= 3)
                FocusNextIpDiv(senderTextBox);
            else
            {
                senderTextBox.SelectionStart = lastCharacterPosition == 0 ? 1 : lastCharacterPosition;
                senderTextBox.SelectionLength = 0;
            }

            _surpressTextChangedEvent = false;
        }

        private void ipDiv0_Enter(object sender, EventArgs e)
        {
            ((TextBox) sender).SelectAll();
        }
    }
}