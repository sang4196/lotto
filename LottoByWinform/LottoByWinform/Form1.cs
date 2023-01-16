using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LottoByWinform
{
    public partial class Form1 : Form
    {
        private Color[] _buttonColor = { Color.White, Color.Green, Color.Red };
        private List<int> _normalNumber;
        private List<int> _needNumber;
        private List<int> _outNumber;
        private Random _rnd = new Random();
        public Form1()
        {
            InitializeComponent();

            InitButton();
            InitEvent();
        }

        private void InitEvent()
        {
            foreach (Control item in tableLayoutPanel1.Controls)
            {
                Button btn = item as Button;
                btn.Click += ButtonNumber_Click;
            }

            btn_init.Click += Btn_init_Click;
            btn_lottery.Click += Btn_lottery_Click;
            btn_initLog.Click += Btn_initLog_Click;
        }

        private void Btn_initLog_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void Btn_lottery_Click(object sender, EventArgs e)
        {
            List<int> lotto = new List<int>();
            List<int> tmp = new List<int>();

            lotto.AddRange(_needNumber);
            tmp.AddRange(_normalNumber);

            if (lotto.Count > 6)
            {
                while (lotto.Count > 6)
                {
                    lotto.RemoveAt(_rnd.Next(0, lotto.Count));
                }
            }
            else
            {
                while (lotto.Count < 6)
                {
                    int idx = _rnd.Next(0, tmp.Count);
                    lotto.Add(tmp[idx]);
                    tmp.RemoveAt(idx);
                }
            }

            lotto.Sort();

            listBox1.Items.Add(string.Join(", ", lotto));
        }

        private void Btn_init_Click(object sender, EventArgs e)
        {
            InitButton();
        }

        private void ButtonNumber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            int colorIdx = btn.BackColor == Color.White ? 0 :
                            btn.BackColor == Color.Green ? 1 : 2;

            UpdateButtonColor(btn, ++colorIdx);
            UpdateNumbers();
        }

        private void UpdateNumbers()
        {
            _normalNumber = new List<int>();
            _needNumber = new List<int>();
            _outNumber = new List<int>();

            foreach (Control item in tableLayoutPanel1.Controls)
            {
                Button btn = item as Button;
                
                if (btn.BackColor == Color.White)
                {
                    _normalNumber.Add((int)btn.Tag);
                }
                else if (btn.BackColor == Color.Green)
                {
                    _needNumber.Add((int)btn.Tag);
                }
                else
                {
                    _outNumber.Add((int)btn.Tag);
                }
            }

            _normalNumber.Sort();
            _needNumber.Sort();
            _outNumber.Sort();

            label_normal.Text = string.Join(", ", _normalNumber);
            label_need.Text = string.Join(", ", _needNumber);
            label_out.Text = string.Join(", ", _outNumber);
        }

        private void UpdateButtonColor(Button btn, int colorIdx)
        {
            btn.BackColor = _buttonColor[colorIdx % _buttonColor.Length];
        }

        private void InitButton()
        {
            foreach (Control item in tableLayoutPanel1.Controls)
            {
                Button btn = item as Button;
                TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetCellPosition(btn);

                btn.Tag = (pos.Column + 1) + (pos.Row * 10);
                btn.Text = btn.Tag.ToString();
                btn.BackColor = Color.White;
            }
            UpdateNumbers();
        }
    }
}
