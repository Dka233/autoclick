using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace AutoClick
{
    class Program
    {

        static void Main(string[] args)
        {
            // Create a new Form
            Form form1 = new Form();

            // Create imput for interval
            NumericUpDown textNum = new NumericUpDown();
            textNum.Location = new Point(40, 10);
            textNum.Size = new Size(50, 10);

            // Start Button
            Button button1 = new Button();
            button1.Text = "Start";
            button1.Location = new Point(25, 35);
            button1.Click += new EventHandler(button1_Click);

            // Stop Button
            Button button2 = new Button();
            button2.Text = "Stop";
            button2.Location
               = new Point(button1.Left, button1.Height + button1.Top + 10);

            // Form Model
            form1.Text = "AutoClick";
            form1.HelpButton = false;
            form1.Size = new Size(150, 150);
            form1.FormBorderStyle = FormBorderStyle.FixedDialog;
            form1.MaximizeBox = false;
            form1.MinimizeBox = false;
            form1.AcceptButton = button1;
            form1.CancelButton = button2;
            form1.StartPosition = FormStartPosition.CenterScreen;
            form1.Controls.Add(button1);
            form1.Controls.Add(button2);
            form1.Controls.Add(textNum);
            form1.ShowDialog();

            // Method OnClick button Start
            void button1_Click(object sender, EventArgs e)
            {
                decimal timeUser = textNum.Value;
                double time = decimal.ToDouble(timeUser);

                // Validatium minimum interval
                if ((time > 1))
                {
                    // Method to do the autoclick
                    void startAutoClick()
                    {
                        var timer = new System.Timers.Timer();
                        timer.Elapsed += new ElapsedEventHandler(ClickFunction);
                        timer.Interval = time * 1000;
                        timer.Enabled = true;
                        void ClickFunction(object sender, ElapsedEventArgs e)
                        {
                            Cursor cur = new Cursor(Cursor.Current.Handle);
                            Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                            // Actual class to do the click
                            Click c = new Click();
                            c.leftClick(p);
                        }
                    }
                    startAutoClick();
                    button1.BackColor = Color.LightGreen;
                }
                else
                {
                    MessageBox.Show("At least 1 second");
                }
            }
        }
    }

    public class Click
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x0000000002,
            LEFTUP = 0x0000000004
        }

        // Method to do the left click
        public void leftClick(Point p)
        {
            Cursor.Position = p;
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }
    }

}
