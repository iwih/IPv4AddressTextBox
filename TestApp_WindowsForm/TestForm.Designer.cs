namespace TestApp_WindowsForm
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.iPv4AddressTextBox1 = new IPv4AddressTextBox.IPv4AddressTextBox();
            this.SuspendLayout();
            // 
            // iPv4AddressTextBox1
            // 
            this.iPv4AddressTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iPv4AddressTextBox1.Location = new System.Drawing.Point(70, 42);
            this.iPv4AddressTextBox1.Name = "iPv4AddressTextBox1";
            this.iPv4AddressTextBox1.Size = new System.Drawing.Size(295, 31);
            this.iPv4AddressTextBox1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 253);
            this.Controls.Add(this.iPv4AddressTextBox1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private IPv4AddressTextBox.IPv4AddressTextBox iPv4AddressTextBox1;
    }
}

