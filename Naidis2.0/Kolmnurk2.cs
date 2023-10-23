using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis
{
    public partial class Kolmnurk2 : Form
    {
        PictureBox pb;
        Label lbl, lblA, lblB, lblC, lblTriangle;
        TextBox txtA, txtB, txtC;
        Button btn;
        ListView lv;

        public Kolmnurk2()
        {
            this.Width = 800;
            this.Height = 600;
            this.Text = "Kolmnurk Kalkulaator";
            this.BackColor = Color.LightGray;

            lbl = new Label();
            lbl.Text = "Kolmnurk Kalkulaator";
            lbl.Location = new Point(20, 20);
            lbl.Size = new Size(300, 50);
            lbl.Font = new Font("Arial", 24, FontStyle.Bold);
            lbl.ForeColor = Color.DarkBlue;
            this.Controls.Add(lbl);

            lv = new ListView();
            lv.Width = 350;
            lv.Height = 230;
            lv.View = View.Details;
            lv.Columns.Add("Omadus", 150);
            lv.Columns.Add("Väärtus", 150);

            string[] properties = { "Külg A:", "Külg B:", "Külg C:", "Olemas:", "Ümbermõõt:", "Pindala:", "Kõrgus A:", "Kõrgus B:", "Kõrgus C:", "Pindala kõrguse järgi:" };

            foreach (string prop in properties)
            {
                lv.Items.Add(prop);
            }

            lv.Location = new Point(20, lbl.Bottom + 20);
            lv.BackColor = Color.White;
            this.Controls.Add(lv);
            int items = lv.Items.Count;
            for (int i = 0; i < items; i++)
            {
                lv.Items[i].SubItems.Add("");
            }

            lblA = new Label();
            lblB = new Label();
            lblC = new Label();
            lblA.Text = "Külg A:";
            lblA.Location = new Point(20, lv.Bottom + 20);
            lblA.Font = new Font("Arial", 14);
            lblB.Text = "Külg B:";
            lblB.Location = new Point(20, lblA.Bottom + 10);
            lblB.Font = new Font("Arial", 14);
            lblC.Text = "Külg C:";
            lblC.Location = new Point(20, lblB.Bottom + 10);
            lblC.Font = new Font("Arial", 14);
            this.Controls.Add(lblA);
            this.Controls.Add(lblB);
            this.Controls.Add(lblC);

            txtA = new TextBox();
            txtB = new TextBox();
            txtC = new TextBox();
            txtA.Height = 30;
            txtA.Width = 100;
            txtB.Height = 30;
            txtB.Width = 100;
            txtC.Height = 30;
            txtC.Width = 100;
            txtA.Location = new Point(lblA.Right + 10, lblA.Location.Y);
            txtB.Location = new Point(lblB.Right + 10, lblB.Location.Y);
            txtC.Location = new Point(lblC.Right + 10, lblC.Location.Y);
            this.Controls.Add(txtA);
            this.Controls.Add(txtB);
            this.Controls.Add(txtC);

            btn = new Button();
            btn.Width = 150;
            btn.Height = 50;
            btn.Location = new Point(550, lv.Top);
            btn.Text = "Kalkuleeri";
            btn.BackColor = Color.DarkBlue;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Arial", 16, FontStyle.Bold);
            btn.Click += Btn_Click;
            this.Controls.Add(btn);

            pb = new PictureBox();
            pb.Location = new Point(450, btn.Bottom + 20);
            pb.Image = new Bitmap("../../../ravnosotonniyTringle.png");
            pb.Size = new Size(250, 250);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(pb);

            lblTriangle = new Label();
            lblTriangle.Text = "";
            lblTriangle.Location = new Point(pb.Left, pb.Bottom + 5);
            lblTriangle.Size = new Size(200, 25);
            lblTriangle.Font = new Font("Arial", 16);
            this.Controls.Add(lblTriangle);
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            TringleType();
            double a, b, c;
            Tringle tringle;
            try
            {
                if (txtA.Text != "" && txtB.Text != "" && txtC.Text != "")
                {
                    a = Convert.ToDouble(txtA.Text);
                    b = Convert.ToDouble(txtB.Text);
                    c = Convert.ToDouble(txtC.Text);
                    tringle = new Tringle(a, b, c);
                }
                else
                {
                    throw new Exception();
                }

                lv.Items[0].SubItems[1].Text = tringle.OutputA();
                lv.Items[1].SubItems[1].Text = tringle.OutputB();
                lv.Items[2].SubItems[1].Text = tringle.OutputC();
                lv.Items[4].SubItems[1].Text = Convert.ToString(tringle.Perimete());
                lv.Items[5].SubItems[1].Text = Convert.ToString(tringle.Surface());
                lv.Items[6].SubItems[1].Text = Convert.ToString(tringle.Height(a));
                lv.Items[7].SubItems[1].Text = Convert.ToString(tringle.Height(b));
                lv.Items[8].SubItems[1].Text = Convert.ToString(tringle.Height(c));

                double h = tringle.Height(a);

                tringle = new Tringle(a, b, c, h);

                lv.Items[9].SubItems[1].Text = Convert.ToString(tringle.SurfaceH());

                if (tringle.ExistTrinage)
                {
                    lv.Items[3].SubItems[1].Text = "Olemas";
                }
                else
                {
                    lv.Items[3].SubItems[1].Text = "Ei ole olemas";
                }
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("Vigased andmed", "Viga");
            }
        }

        public string TringleType()
        {
            if (txtA.Text == txtB.Text && txtB.Text == txtC.Text)
            {
                lblTriangle.Text = "Võrdkülgne kolmnurk";
                pb.Image = new Bitmap("../../../ravnosotonniyTringle.png");
            }
            else if (txtA.Text == txtB.Text || txtB.Text == txtC.Text || txtA.Text == txtC.Text)
            {
                lblTriangle.Text = "Võrdhaarne kolmnurk";
                pb.Image = new Bitmap("../../../ravnobedrenniyTringle.png");
            }
            else if (txtA.Text != txtB.Text && txtB.Text != txtC.Text)
            {
                lblTriangle.Text = "Skaala kolmnurk";
                pb.Image = new Bitmap("../../../raznostoronniyTringle.png");
            }
            return lblTriangle.Text;
        }
    }
}
