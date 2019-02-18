using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binary_Counter
{
	public partial class MainForm : Form
	{

		public Button[] buttons = new Button[8];

		public MainForm()
		{
			InitializeComponent();
			foreach (Button b in buttonLayoutTable.Controls)
			{
				buttons[(int)b.Tag] = b;
				b.Tag = 0;
			}
		}

		public void ButtonPress(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			button.Tag = (int)button.Tag ^ 1;
			button.Text = button.Tag.ToString();
			SetDecimalValue();
		}

		public void SetDecimalValue()
		{
			byte total = 0;
			for (int i = 0; i < buttons.Length; i++)
			{
				total += (byte)((int)buttons[i].Tag * Math.Pow(2, i));
			}
			decimalCounter.Value = total;
		}

		public void SetBinaryValue(byte value)
		{
			byte t = 128;
			for (int i = 7; i >= 0; i--)
			{
				if(value >= t)
				{
					buttons[i].Tag = 1;
					value -= t;
				}
				else
				{
					buttons[i].Tag = 0;
				}
				buttons[i].Text = buttons[i].Tag.ToString();
				t /= 2;
			}
		}

		private void decimalCounter_ValueChanged(object sender, EventArgs e)
		{
			if(decimalCounter.Value == decimalCounter.Maximum)
			{
				decimalCounter.Value = decimalCounter.Minimum + 1;
			}
			if (decimalCounter.Value == decimalCounter.Minimum)
			{
				decimalCounter.Value = decimalCounter.Maximum - 1;
			}
			SetBinaryValue((byte)decimalCounter.Value);
		}

		//resize event from button:
		private void button_Resize(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			float fontSize = NewFontSize(button.CreateGraphics(), button.Size, button.Font, button.Text);
			Font f = new Font(button.Font.Name, fontSize, button.Font.Style);
			button.Font = f;
		}

		// method to calculate the size for the resized font:
		public static float NewFontSize(Graphics graphics, Size size, Font font, string str)
		{
			SizeF stringSize = graphics.MeasureString(str, font);
			float wRatio = size.Width / stringSize.Width;
			float hRatio = size.Height / stringSize.Height;
			float ratio = Math.Min(hRatio, wRatio);
			return font.Size * ratio;
		}
	}
}
