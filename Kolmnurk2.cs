using Naidis;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace Naidis
{
    public partial class Kolmnurk2 : Form
    {
        Label lbl, lbl1, lbl2;
        PictureBox pb;
        Button btn;
        ListBox lb, lb1;
        TextBox tb1, tb2;
        int j = 0;

        public Kolmnurk2()
        {
            this.Height = 400;
            this.Width = 600;
            this.Text = "Kolmnurk";

            lbl = new Label();
            lbl.Text = "Kolmnurk";
            lbl.Location = new Point(0, 0);
            lbl.Size = new Size(this.Width, 50);
            lbl.BackColor = Color.LightGray;
            lbl.BorderStyle = BorderStyle.Fixed3D;
            lbl.Font = new Font("Tahoma", 24);
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            pb = new PictureBox();
            pb.Location = new Point(this.Width - 250, this.Height - 250);
            pb.Image = new Bitmap("../../../ravnosotonniyTringle.png");
            pb.Size = new Size(200, 200);
            pb.SizeMode = PictureBoxSizeMode.Zoom;

            lb = new ListBox();
            lb.Height = 60;
            lb.Width = 200;
            lb.Location = new Point(50, lbl.Bottom + 50);
            lb.Items.Add("Külg: ");
            lb.Items.Add("Kõrgus: ");
            lb.Items.Add("Pindala: ");

            lb1 = new ListBox();
            if (j == 0)
            {
                foreach (var item in lb.Items)
                {
                    lb1.Items.Add(item);
                }
                j = 1;
            }

            lbl1 = new Label();
            lbl1.Text = "Külg:";
            lbl1.Location = new Point(lb.Right + 20, lb.Top);
            lbl2 = new Label();
            lbl2.Text = "Height:";
            lbl2.Location = new Point(lb.Right + 20, lb.Top + 25);

            tb1 = new TextBox();
            tb2 = new TextBox();
            tb1.Height = 50;
            tb1.Width = 100;
            tb2.Height = 50;
            tb2.Width = 100;
            tb1.Location = new Point(lbl1.Left + 50, lbl1.Location.Y);
            tb2.Location = new Point(lbl2.Left + 50, lbl2.Location.Y);

            btn = new Button();
            btn.Height = 50;
            btn.Width = 150;
            btn.Text = "Käivitada";
            btn.Font = lbl.Font = new Font("Tahoma", 20);
            btn.Location = new Point(lb.Left, lb.Bottom);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Black;
            btn.Click += Btn_Click;

            ControlsAdd(new Control[] { lbl, pb, btn, lb, tb1, tb2, lbl1, lbl2 });
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            tb1.BackColor = Color.White;
            tb2.BackColor = Color.White;
            int o = 0;
            foreach (var item in lb1.Items)
            {
                lb.Items[o] = item;
                o++;
            }
            Tringle triangle;
            bool aB, hB;
            aB = hB = false;
            try
            {
                double a, h;
                a = h = 0;
                try { a = Convert.ToDouble(tb1.Text); aB = true; } catch (Exception) { a = 0; aB = false; }
                try { h = Convert.ToDouble(tb2.Text); hB = true; } catch (Exception) { h = 0; hB = false; }
                if (tb1.Text != "" && tb2.Text != "" && a != 0 && h != 0)
                {
                    triangle = new Tringle(a, h);
                }
                else
                {
                    throw new Exception();
                }
                string option = "{0,-20}\t{1,-20}";
                lb.Items[0] = string.Format(option, lb.Items[0], triangle.a);
                lb.Items[1] = string.Format(option, lb.Items[1], triangle.h);
                lb.Items[2] = string.Format(option, lb.Items[2], triangle.SurfaceWithOutOn());
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("Sinu kolmnurgaga on midagi valesti", "Viga", MessageBoxButtons.OK);
                if (!aB)
                {
                    tb1.BackColor = Color.Red;
                }
                if (!hB)
                {
                    tb2.BackColor = Color.Red;
                }
                if (aB && hB)
                {
                    tb1.BackColor = Color.Red;
                    tb2.BackColor = Color.Red;
                }
            }
        }

        private void ControlsAdd([Optional] Control[] arrayVisibleTrue, [Optional] Control[] arrayVisibleFalse)
        {
            if (arrayVisibleTrue != null)
            {
                foreach (Control item in arrayVisibleTrue)
                {
                    this.Controls.Add(item);
                }
            }
            if (arrayVisibleFalse != null)
            {
                foreach (Control item in arrayVisibleFalse)
                {
                    this.Controls.Add(item);
                    item.Visible = false;
                }
            }
        }
    }
}