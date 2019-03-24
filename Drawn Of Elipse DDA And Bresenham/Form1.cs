//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Company: CUCEI - UDG
// Developer:  Misael A.
// Project Name: Drawn ellipse using: DDA and Bresenham
// Date compilation: 2019/03/23
// Additional comments: 
// - The two algorithms run at the same time and compared their execution time.
// - The quadrants are called from an external function to the dda and bresenham algorithm
// - The first point is the center of the ellipse and the second point is the distance between the radius in x and y
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Drawn_Of_Elipse_DDA_And_Bresenham
{
    public partial class Form1 : Form
    {
        const int width = 470;
        const int height = 345;
        Bitmap bmp = new Bitmap(width, height);


        Point Punto_C;
        Point Punto_R;
        
        int xc, yc = 0;
        int xr, yr = 0;
        int Rx, Ry;
        int flag = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox.Image = bmp;

            if (flag == 0)
            {
                flag++;
                xc = e.X;
                yc = e.Y;

                Punto_C = new Point((int)xc, (int)yc);
                
            }
            else
            {
                flag = 0;
                xr = e.X;
                yr = e.Y;

                Punto_R = new Point((int)xr, (int)yr);

                Rx = Math.Abs(xr - xc);
                Ry = Math.Abs(yc - yr);

                ElipseDDA(Rx, Ry);
                ElipseMidPoint(Rx, Ry, xc, yc);
            }
            
        }
        private void ElipseDDA(int Rx, int Ry)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double x, y;

            for (x = 0; x <= Rx; x++)
            {
                y = Math.Sqrt((((Math.Pow(Rx, 2)) * (Math.Pow(Ry, 2))) - ((Math.Pow(x, 2)) * (Math.Pow(Ry, 2))))/ Math.Pow(Rx, 2));

                DrawnCuadrants(xc, yc, (int)x, (int)y, Color.Cyan);

            }
            for (y = 0; y <= Ry; y++)
            {
                x = Math.Sqrt((((Math.Pow(Rx, 2)) * (Math.Pow(Ry, 2))) - ((Math.Pow(y, 2)) * (Math.Pow(Rx, 2)))) / Math.Pow(Ry, 2));

                DrawnCuadrants(xc, yc, (int)x, (int)y, Color.Cyan);
            }

            lbl_TimeDDA.Text = String.Format("{0}", sw.Elapsed.TotalMilliseconds);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, width, height);
            pictureBox.Image = bmp;

            lbl_TimeBresenham.Text = "00 :00";
            lbl_TimeDDA.Text = "00 : 00";
        }
        private void ElipseMidPoint(int Rx, int Ry,int xc, int yc)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            double dx, dy, p1, p2, x, y;
            x = 0;
            y = Ry;

            p1 = Math.Pow(Ry, 2) - (Math.Pow(Rx, 2)*Ry)+(0.25 * Math.Pow(Rx, 2));
            dx = (2 * Math.Pow(Ry, 2)) * x;
            dy = (2 * Math.Pow(Rx, 2)) * y;
            while (dx < dy)
            {

                DrawnCuadrants(xc, yc, (int)x, (int)y, Color.Orange);

                if (p1 < 0)
                {
                    x++;
                    dx = dx + (2 * Math.Pow(Ry, 2));
                    p1 = p1 + dx + (Math.Pow(Ry, 2));
                  
                }
                else
                {
                    x++;
                    y--;
                    dx = dx + (2 * Math.Pow(Ry, 2));
                    dy = dy - (2 * Math.Pow(Rx, 2));
                    p1 = p1 + dx - dy + (Math.Pow(Ry, 2));
                  
                }
            }

            p2 = ((Math.Pow(Ry,2)) * ((x + 0.5) * (x + 0.5)))
                + (Math.Pow(Rx, 2) * ((y - 1) * (y - 1)))
                - (Math.Pow(Rx, 2) * Math.Pow(Ry, 2));

            while (y >= 0)
            {

                DrawnCuadrants(xc,yc,(int)x,(int)y,Color.Orange);

                if (p2 > 0)
                {
                    y--;
                    dy = dy - (2 * Math.Pow(Rx, 2));
                    p2 = p2 + (Math.Pow(Rx, 2)) - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * Math.Pow(Ry, 2));
                    dy = dy - (2 * Math.Pow(Rx, 2));
                    p2 = p2 + dx - dy + (Math.Pow(Rx, 2));
                }
            }
            lbl_TimeBresenham.Text = String.Format("{0}", sw.Elapsed.TotalMilliseconds);
        }
        private void DrawnCuadrants(int xc, int yc, int x, int y, Color color)
        {

            if (x + xc > 0 && x + xc < width && y + yc > 0 && y + yc < height)  /// Cuadrante 2
                bmp.SetPixel(x + xc, y + yc, color);
            if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)  ///Cuadrante 6
                bmp.SetPixel(-x + xc, -y + yc, color);
            if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)  ///Cuadrante 3
                bmp.SetPixel(-x + xc, y + yc, color);
            if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)  ///Cuadrante 7
                bmp.SetPixel(x + xc, -y + yc, color);

        }


    }
}


