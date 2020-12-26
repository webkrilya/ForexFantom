using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ForexTest
{
    public partial class ForexCap : Form
    {


        bool dropper;
        public ForexCap()
        {
            InitializeComponent();

            


        }



        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(System.Drawing.Point p);


        private void Form1_Click(object sender, EventArgs e)
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public IntPtr dwData; // in C/C++ this is an UINT_PTR, not an UINT
            public int cbData;
            public IntPtr lpData;
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 74)
            {
                COPYDATASTRUCT data = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                byte[] b = new byte[data.cbData-1];
                Marshal.Copy(data.lpData, b, 0, data.cbData-1);
                string msg = Encoding.UTF8.GetString(b);



                Console.WriteLine(msg);
            }
            base.WndProc(ref m);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            dropper = false;

            this.CenterToScreen();
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dg.ColumnHeadersHeight = 24;


            dg.Columns.Add("col1", "Ордер");
            dg.Columns.Add("col2", "Время");
            dg.Columns.Add("col3", "Тип");
            dg.Columns.Add("col4", "Объем");
            dg.Columns.Add("col5", "Символ");
            dg.Columns.Add("col6", "Цена");
            dg.Columns.Add("col7", "S / L");
            dg.Columns.Add("col8", "T / P");
            dg.Columns.Add("col9", "Цена");
            dg.Columns.Add("col10", "Комиссия");
            dg.Columns.Add("col11", "Своп");
            dg.Columns.Add("col12", "Прибыль");

            
        }

        private void dg_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        IntPtr windowHandle = IntPtr.Zero;

        private void dg_MouseUp(object sender, MouseEventArgs e)
        {

            if (dropper) return;

            dropper = true;

            this.Cursor = Cursors.Arrow;

            

            windowHandle = WindowFromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));

            timer1.Enabled = true;

//            Console.WriteLine(needRect.Left.ToString() + "; " + needRect.Top.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rect needRect = new Rect();
            GetWindowRect(windowHandle, ref needRect);

            this.Left = needRect.Left;
            this.Top = needRect.Top;

            this.Width = (needRect.Right - needRect.Left);
            //this.Width = 100;
            this.Height = needRect.Bottom - needRect.Top;


        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
