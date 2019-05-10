/* 
 *Copyright (C) 2018-2019 Osama Alwash, Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/iwih/IPv4AddressTextBox
 */
using System;
using System.Windows.Forms;

namespace IPv4Address
{
    public partial class IPv4AddressTextBox : UserControl
    {
        public IPv4AddressTextBox()
        {
            InitializeComponent();
        }

        public byte this[int index] 
        {
            get
            {
                if (index >= 0 & index <= 3) return this.GetAddressBytes()[index];
                else throw new IndexOutOfRangeException("Index starts at 0 and must not exceed 3");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        ipDiv0.Text = value.ToString();
                        break;
                    case 1:
                        ipDiv1.Text = value.ToString();
                        break;
                    case 2:
                        ipDiv2.Text = value.ToString();
                        break;
                    case 3:
                        ipDiv3.Text = value.ToString();
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index starts at 0 and must not exceed 3");
                }
            }
        }

        public byte[] GetAddressBytes()
        {
            byte[] textInput = new byte[4];
            textInput[0] = Convert.ToByte(ipDiv0.Text);
            textInput[1] = Convert.ToByte(ipDiv1.Text);
            textInput[2] = Convert.ToByte(ipDiv2.Text);
            textInput[3] = Convert.ToByte(ipDiv3.Text);
            return textInput;
        }

        public System.Net.IPAddress IPAddress
        {
            get => new System.Net.IPAddress(this.GetAddressBytes());
            set
            {
                this[0] = value.GetAddressBytes()[0];
                this[1] = value.GetAddressBytes()[1];
                this[2] = value.GetAddressBytes()[2];
                this[3] = value.GetAddressBytes()[3];
            }
        }

        public override string Text
        {
            get => $"{ipDiv0.Text}.{ipDiv1.Text}.{ipDiv2.Text}.{ipDiv3.Text}";
            set
            {
                ipDiv0.Clear();
                ipDiv1.Clear();
                ipDiv2.Clear();
                ipDiv3.Clear();

                var ipTokens = value.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                var counter = 0;
                while ((counter < 4) && (counter < ipTokens.Length))
                {
                    var tokenParsedToInt = int.TryParse(ipTokens[counter], out var ipDivValue);
                    if (tokenParsedToInt)
                    {
                        switch (counter)
                        {
                            case 0:
                                ipDiv0.Text = ipDivValue.ToString();
                                break;

                            case 1:
                                ipDiv1.Text = ipDivValue.ToString();
                                break;

                            case 2:
                                ipDiv2.Text = ipDivValue.ToString();
                                break;

                            case 3:
                                ipDiv3.Text = ipDivValue.ToString();
                                break;
                        }
                    }

                    counter++;
                }
            }
        }

        private void ipDiv_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = IsNonAllowedKeyDown(e.KeyData);

            if (IsDecimalKeyDown(e.KeyData))
            {
                FocusNextIpDiv((TextBox) sender);
                e.SuppressKeyPress = true;
            }

            if ((e.KeyData == Keys.Left & ((TextBox)sender).SelectionStart == 0) 
                | (e.KeyData == Keys.Back & Convert.ToByte(((TextBox)sender).Text)==0))
            { FocusPreviousIpDiv((TextBox)sender); }
            if (e.KeyData == Keys.Right & ((TextBox)sender).SelectionStart == ((TextBox)sender).Text.Length)
            { FocusNextIpDiv((TextBox)sender); }
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

        private void FocusPreviousIpDiv(TextBox sender)
        {
            var senderName = sender.Name;
            TextBox nextDiv = null;

            if (senderName == ipDiv3.Name)
                nextDiv = ipDiv2;
            else if (senderName == ipDiv2.Name)
                nextDiv = ipDiv1;
            else if (senderName == ipDiv1.Name)
                nextDiv = ipDiv0;

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
            if (lastCharacterPosition >= 3)
                FocusNextIpDiv(senderTextBox);
            else
            {
                senderTextBox.SelectionStart = lastCharacterPosition == 0 ? 1 : lastCharacterPosition;
                senderTextBox.SelectionLength = 0;
            }

            _surpressTextChangedEvent = false;
        }

        private void ipDiv_Enter(object sender, EventArgs e)
        {
            ((TextBox) sender).SelectAll();
        }

        public new void Dispose()
        {
            ipDiv0?.Dispose();
            ipDiv1?.Dispose();
            ipDiv2?.Dispose();
            ipDiv3?.Dispose();

            if (!IsDisposed)
                base.Dispose();
        }

        private void IPv4AddressTextBox_FontChanged(object sender, EventArgs e)//new
        {
            dotSeperator0.Font = new System.Drawing.Font(dotSeperator0.Font.Name, this.Font.Size, dotSeperator0.Font.Style);
            dotSeperator1.Font = new System.Drawing.Font(dotSeperator1.Font.Name, this.Font.Size, dotSeperator1.Font.Style);
            dotSeperator2.Font = new System.Drawing.Font(dotSeperator2.Font.Name, this.Font.Size, dotSeperator2.Font.Style);
        }
    }
}