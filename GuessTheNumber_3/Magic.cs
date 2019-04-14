﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GuessTheNumber_3
{
    abstract public class Magic
    {
        public MagicNumber magic = new MagicNumber();
        public List<Button> ListBut = new List<Button>();
        public List<TextBox> ListBox = new List<TextBox>();
        public List<Label> ListLabel = new List<Label>();
   
        public Panel FillPanels(Panel p, params string[] Text)
        {
            Button button;
            TextBox Tbox;
            Label lab;
            foreach (string t in Text)
            {
                button = new Button();
                button.Text = t;
                button.Size = new Size(120, 45);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = Color.RoyalBlue;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
                button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
                button.Font = new Font("Mistral", 16F);
                p.Controls.Add(button);
                ListBut.Add(button);
            }
            for (int i = 0; i < 3; i++)
            {
                Tbox = new TextBox();
                Tbox.BackColor = Color.DarkOrange;
                Tbox.BorderStyle = BorderStyle.FixedSingle;
                Tbox.Font = new Font("Microsoft Sans Serif", 10F);
                Tbox.MaxLength = 9;
                Tbox.Size = new Size(100, 25);
                ListBox.Add(Tbox);
                p.Controls.Add(Tbox);
            }

            for (int i = 0; i < 6; i++)
            {
                lab = new Label();
                lab.AutoSize = true;
                lab.Font = new Font("Mistral", 16F);
                lab.Size = new Size(511, 29);
                p.Controls.Add(lab);
                ListLabel.Add(lab);
            }
            return p;
        }

        public Panel Initialization(Panel p)
        {
            //Panel
            p.Size = new Size(700, 600);

            ////поле для вводу від
            ListBox[0].Location = new Point(50, 50);
            ListBox[0].KeyPress += textBoxFrom_KeyPress;


            //Поле для воду до
            ListBox[1].Location = new Point(210, 50);
            ListBox[1].KeyPress += textBoxTo_KeyPress;

            ////Поле для вводу числа користувачем
            ListBox[2].Location = new Point(40, 240);
            ListBox[2].KeyPress += tBoxMyNumber_KeyPress;

            //Кнопка спроби 
            ListBut[3].Location = new Point(150, 230);

            //Кнопка для початку та рестарту
            ListBut[1].Location = new Point(50, 110);
            ListBut[1].Enabled = false;

            //Кнопка виходу
            ListBut[2].Location = new Point(268, 110);


            //Кнопка яка встановлює проміжок
            ListBut[0].Location = new Point(300, 40);
            ListBut[0].Enabled = false;

            //Events buttons
            ListBut[0].Click += butEnter_Click;
            ListBut[1].Click += butPlayRestart_Click;
            //ListBut[2].Click += butExit_Click;
           // ListBut[3].Click += butTry_Click;

            //Заголок лейбл
            ListLabel[0].Location = new Point(10, 10);
            ListLabel[0].Text = "ЗАДАЙТИ ПРОМІЖОК В ЯКОМУ ХОЧЕТЕ ВГАДУВАТИ ЧИСЛО";

            //Лейбл від
            ListLabel[1].Location = new Point(10, 50);
            ListLabel[1].Text = "ВІД:";

            // labelTo
            ListLabel[2].Location = new Point(170, 50);
            ListLabel[2].Text = "ДО:";

            //лейбл інфо
            ListLabel[3].Location = new Point(10, 180);
            ListLabel[3].Text = "";

            //лейбл кількість спроб
            ListLabel[5].Location = new Point(20, 300);
            ListLabel[5].Text = "";

            //лейбл інфо про число
            ListLabel[4].Location = new Point(280, 240);
            ListLabel[4].Text = "";

            return p;
        }

        private void mPanel_Paint(object sender, EventArgs e)
        {
           
        }

        private void textBoxFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void textBoxTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
            else
            {
                ListBut[0].Enabled = true;
            }
        }

        private void tBoxMyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
            else
            {
                ListBut[3].Enabled = true;
            }
        }

        //public abstract void butTry_Click(object sender, EventArgs e);

        private void butPlayRestart_Click(object sender, EventArgs e)
        {
            if (ListBut[1].Text == "ПОЧАТОК")
            {
           
                #region//Деякі дії над компонентами після натиску кнопки Початок
                    ListBut[1].Text = "РЕСТАРТ";
                    ListLabel[3].Visible = true;
                    ListLabel[3].Text = "ЧИСЛО В МЕЖАХ ВІД: " + magic.From.ToString() + " ДО " + magic.To.ToString() + "! ";
                    ListBox[2].Visible = true;
                    ListBox[2].Enabled = true;
                    ListBut[3].Visible = true;
                    ListLabel[4].Visible = true;
                    ListLabel[5].Visible = true;
                    #endregion
           

            }
            else
            {
               
                    magic.CountTry = 0;
                    #region //Деякі дії над компонентами після натиску кнопки рестарт
                    ListBox[1].Enabled = true;
                    ListBox[0].Enabled = true;
                    ListBut[1].Text = "ПОЧАТОК";
                    ListBut[1].Enabled = false;
                    ListBox[2].Visible = false;
                    ListBut[3].Visible = false;
                    ListBox[1].Text = "";
                    ListBox[0].Text = "";
                    ListBox[2].Text = "";
                    ListLabel[3].Text = "";
                    ListLabel[4].Text = "";
                    ListLabel[5].Text = "";
                    ListLabel[5].Enabled = true;
                    ListBox[2].Enabled = false;
                    #endregion
            }
        }

        private void butEnter_Click(object sender, EventArgs e)
        {
            try
            {
                magic.From = Convert.ToInt32(ListBox[0].Text);
                magic.To = Convert.ToInt32(ListBox[1].Text);
                magic.Guess = magic.SetGuess;

                ListBut[1].Enabled = true;
                ListBut[0].Enabled = false;
                ListBox[1].Enabled = false;
                ListBox[0].Enabled = false;
            }
            catch
            {
                MessageBox.Show("ПОМИЛКА");
            }
        }

        private void SingleForm_Load(object sender, EventArgs e)
        {
            ListBut[0].Enabled = false;
            ListBut[1].Enabled = false;
        }

        public abstract void ChekInputNumber();

        public abstract void Clear();
    }
}
