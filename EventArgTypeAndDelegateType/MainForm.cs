using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventArgTypeAndDelegateType
{
    public partial class MainForm : Form
    {
        // The total pixels the special control has moved left
        private int _moveLeftAmount;

        public MainForm()
        {
            InitializeComponent();
        }

        private void specialControl1_MoveLeft(object sender, MovedLeftEventArgs e)
        {
            // Add amount moved to total before displaying
            _moveLeftAmount += e.MoveAmount;
            this.Text = _moveLeftAmount.ToString();
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            // Move special control left by 3 pixels
            specialControl1.Location = new Point(specialControl1.Location.X - 3, specialControl1.Location.Y);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            // Move special control right by 3 pixels
            specialControl1.Location = new Point(specialControl1.Location.X + 3, specialControl1.Location.Y);
        }
    }
}
