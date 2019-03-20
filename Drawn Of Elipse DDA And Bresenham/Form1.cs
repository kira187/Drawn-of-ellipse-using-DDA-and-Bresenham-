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
            if (flag == 0)
            {
                xc = e.X;
                yc = e.Y;

                Punto_C = new Point((int)xc, (int)yc);
                flag++;
            }
            else
            {
                
                xr = e.X;
                yr = e.Y;

                Punto_R = new Point((int)xr, (int)yr);

                GetRadio(xc,yc,xr,yr);
                DrawnCoordenada();
                //ElipseDDA(Rx, Ry);
                //ElipseBresenham(Rx, Ry);
                midptellipse(Rx, Ry, xc, yc);

                flag = 0;
            }
            
        }

        private void GetRadio(int xc,int yc,int xr,int yr)
        {
            Graphics Gfx_C = Graphics.FromImage(bmp);
            int radio1x, radio1y, radio2_x, radio2_y; 
            radio1x =xc;
            radio1y = yr;
            radio2_x = xr;
            radio2_y = yc;

            Rx = Math.Abs(xr -xc);
            Ry = Math.Abs(yc - yr);

            Gfx_C.FillRectangle(Brushes.Blue, radio1x, radio1y, 2, 2);
            Gfx_C.FillRectangle(Brushes.Black, radio2_x, radio2_y, 2, 2);
        }
        private void DrawnQuadrant(int xc, int yc, int x, int y, Color col)
        {

            if (x + xc > 0 && x + xc < width && y + yc > 0 && y + yc < height)  /// Cuadrante 2
                bmp.SetPixel((int)x + xc, (int)y + yc, col);
            if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)  ///CUADRANTE 6
                bmp.SetPixel((int)-x + xc, (int)-y + yc, col);
            if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)  ///Cuadrante 3
                bmp.SetPixel((int)-x + xc, (int)y + yc, col);
            if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)  ///Cuadrante 7
                bmp.SetPixel((int)x + xc, (int)-y + yc, col);
            /*
            setPixel(xc + x, yc + y);
            setPixel(xc - x, yc + y);
            setPixel(xc + x, yc - y);
            setPixel(xc - x, yc - y);
            */
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, width, height);
            pictureBox.Image = bmp;

            lbl_TimeBresenham.Text = "00 :00";
            lbl_TimeDDA.Text = "00 : 00";
        }
        private void DrawnCoordenada()
        {
            int xc, yc, xr, yr;
            Graphics Gfx = Graphics.FromImage(bmp);

            xr = Punto_R.X;
            yr = Punto_R.Y;

            xc = Punto_C.X;
            yc = Punto_C.Y;

            Gfx.FillRectangle(Brushes.Tomato, xr - 1, yr - 1, 2, 2);
            Gfx.FillRectangle(Brushes.Tomato, xc - 1, yc - 1, 2, 2);
            pictureBox.Image = bmp;

        }
        private void ElipseDDA(int Rx,int Ry)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double x,y;
           
            for  (x = 0; x <= Rx; x++)
            {
                y = Math.Sqrt((((Math.Pow(Rx, 2)) * (Math.Pow(Ry, 2))) - ((Math.Pow(x, 2)) * (Math.Pow(Ry, 2)))) / Math.Pow(Rx, 2));


                if (x + xc > 0 && x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel((int)x + xc, (int)y + yc, Color.Cyan);

                if (-x + xc > 0 && -x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel((int)-x + xc, (int)y + yc, Color.Cyan);

                if (-x + xc > 0 && -x + xc < width && -(int)y + yc > 0 && -(int)y + yc < height)
                    bmp.SetPixel((int)-x + xc, -(int)y + yc, Color.Cyan);

                 if (x + xc > 0 && x + xc < width && -(int)y + yc > 0 && -(int)y + yc < height)
                    bmp.SetPixel((int)x + xc, -(int)y + yc, Color.Cyan);

            }
              for (y = 0; y <= Ry; y++)
              {
                  x = Math.Sqrt((((Math.Pow(Rx, 2)) * (Math.Pow(Ry, 2))) - ((Math.Pow(y, 2)) * (Math.Pow(Rx, 2)))) / Math.Pow(Ry, 2));

                  if ((int)x + xc > 0 && (int)x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                      bmp.SetPixel((int)x  + xc, (int)y + yc, Color.Cyan);

                  if (-(int)x + xc > 0 && -(int)x + xc < width && -y + yc > 0 && -y + yc < height)
                      bmp.SetPixel(-(int)x + xc, -(int)y + yc, Color.Cyan);

                  if (-(int)x + xc > 0 && -(int)x + xc < width && y + yc > 0 && y + yc < height)
                      bmp.SetPixel(-(int)x + xc, (int)y  + yc, Color.Cyan);

                  if ((int)x + xc > 0 && (int)x + xc < width && (int)-y + yc > 0 && -y + yc < height)
                     bmp.SetPixel((int)x + xc, -(int)y + yc, Color.Cyan);
              }

            lbl_TimeDDA.Text = String.Format("{0}", sw.Elapsed.TotalMilliseconds);
        }
        private void ElipseBresenham(int Rx, int Ry)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double p1, p2;

            int x = 0;
            int y = Ry;
            double Rx2 = Math.Pow(Rx, 2);
            double Ry2 = Math.Pow(Ry, 2);

            p1 = Ry2 - (Rx2 * Ry) + (0.25 * Rx2);

            while ((Ry2 * x) < (Rx2 * y))
            {
                if (p1 < 0)
                {
                    x++;
                    p1 = p1 + (2 * Ry2 * x) + Ry2;
                }
                else
                {
                    x++;
                    y--;
                    p1 = p1 + (2 * Ry2 * x) - (2 * Rx2 * y) + Ry2;
                }

                if ((int)x + xc > 0 && (int)x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel((int)x + xc, y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)
                    bmp.SetPixel(-x + xc, y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel(-x + xc, -y + yc, Color.Orange);

                if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel(x + xc, -y + yc, Color.Orange);
            }
            p2 = (Ry2) * Math.Pow((x + 0.5), 2) + (Rx2) * Math.Pow((y - 1), 2) - (Rx2 * Ry2);
            while (y > 0)
            {
                if (p2 > 0)
                {
                    y--;
                    p2 = p2 - (2 * Rx2 * y) + Rx2;
                }
                else
                {
                    x++; y--;
                    p2 = p2 + (2 * Ry2 * x) - (2 * Rx2 * y) + Rx2;
                }

                if (x + xc > 0 && x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel(x + xc, y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)
                    bmp.SetPixel(-x + xc, y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel(-x + xc, -y + yc, Color.Orange);

                if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel(x + xc, -y + yc, Color.Orange);
                
            }
            lbl_TimeBresenham.Text = String.Format("{0}", sw.Elapsed.TotalMilliseconds);
        }

        private void midptellipse(int Rx, int Ry,int xc, int yc)
        {

            double dx, dy, d1, d2, x, y;
            x = 0;
            y = Ry;

            // Initial decision parameter of region 1 
            d1 = (Ry * Ry) - (Rx * Rx * Ry) +
                            (0.25f * Rx * Rx);
            dx = 2 * Ry * Ry * x;
            dy = 2 * Rx * Rx * y;

            // For region 1 
            while (dx < dy)
            {

                if ((int)x + xc > 0 && (int)x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel((int)x + xc, (int)y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)
                    bmp.SetPixel((int)-x + xc, (int)y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel((int)-x + xc, (int)-y + yc, Color.Orange);

                if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel((int)x + xc, (int)-y + yc, Color.Orange);


                // Checking and updating value of 
                // decision parameter based on algorithm 
                if (d1 < 0)
                {
                    x++;
                    dx = dx + (2 * Ry * Ry);
                    d1 = d1 + dx + (Ry * Ry);
                }
                else
                {
                    x++;
                    y--;
                    dx = dx + (2 * Ry * Ry);
                    dy = dy - (2 * Rx * Rx);
                    d1 = d1 + dx - dy + (Ry * Ry);
                }
            }

            // Decision parameter of region 2 
            d2 = ((Ry * Ry) * ((x + 0.5f) * (x + 0.5f)))
                + ((Rx * Rx) * ((y - 1) * (y - 1)))
                - (Rx * Rx * Ry * Ry);

            // Plotting points of region 2 
            while (y >= 0)
            {

                // printing points based on 4-way symmetRy 
              
                if ((int)x + xc > 0 && (int)x + xc < width && (int)y + yc > 0 && (int)y + yc < height)
                    bmp.SetPixel((int)x + xc, (int)y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && y + yc > 0 && y + yc < height)
                    bmp.SetPixel((int)-x + xc, (int)y + yc, Color.Orange);

                if (-x + xc > 0 && -x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel((int)-x + xc, (int)-y + yc, Color.Orange);

                if (x + xc > 0 && x + xc < width && -y + yc > 0 && -y + yc < height)
                    bmp.SetPixel((int)x + xc, (int)-y + yc, Color.Orange);

                // Checking and updating parameter 
                // value based on algorithm 
                if (d2 > 0)
                {
                    y--;
                    dy = dy - (2 * Rx * Rx);
                    d2 = d2 + (Rx * Rx) - dy;
                }
                else
                {
                    y--;
                    x++;
                    dx = dx + (2 * Ry * Ry);
                    dy = dy - (2 * Rx * Rx);
                    d2 = d2 + dx - dy + (Rx * Rx);
                }
            }
        }
    }
}


