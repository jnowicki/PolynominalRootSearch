using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Polowienie_Sieczne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void polowienia()
        {
            //p1 i p2 to przedziały ( początkowy i końcowy )
            Number p1 = new Number(1.0, 2.0);
            Number p2 = new Number(3.0, 2.0);
            //
            Random rnd = new Random();
            Number epsilon = new Number(rnd.Next(2, 7), 100.0);
            polowienieBox.AppendText("epsilon wynosi " + epsilon.value + "\nkolejne podziały: \n");

            Function f_p1 = new Function(p1.value);
            Function f_p2 = new Function(p2.value);

            if (f_p1.val * f_p2.val < 0.0)
            {
                PointPairList lista = new PointPairList();
                Random random = new Random();
                for (int i = 0; i < 15; i++)
                {
                    // dodawanie przedzialow na wykresie
                    lista.Add(f_p1.arg, 0); lista.Add(f_p1.arg, -20 + i * 2.5); lista.Add(f_p2.arg, -20 + i * 2.5); lista.Add(f_p2.arg, 0);
                    System.Threading.Thread.Sleep(25);
                    LineItem myCurve = zedGraphControl1.GraphPane.AddCurve("",
                    lista, Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)), SymbolType.None);
                    myCurve.Line.Width = 1.0f;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                    zedGraphControl1.Refresh();
                    //
                    
                    polowienieBox.AppendText(i+1 + ". " + f_p1.arg + " - " + f_p2.arg + "\n");
                    Function srodek = new Function((f_p1.arg + f_p2.arg) / 2.0);
                    if (srodek.modulus() < epsilon.value)
                    {
                        polowienieBox.AppendText("\n\nMiejsce zerowe znalezione!\nPrzybliżone miejsce zerowe (" + srodek.val.ToString("0.###") + " < " + epsilon.value + ") znaleziono w x = " + srodek.arg.ToString("#.###"));
                        //dodawanie strzalki na wykresie
                        ArrowObj strzalka = new ArrowObj(Color.Black, 10.0f, srodek.arg, srodek.val + 45, srodek.arg, srodek.val + 5);
                        zedGraphControl1.GraphPane.GraphObjList.Add(strzalka);
                        zedGraphControl1.Refresh();
                        break;
                    }
                    else
                    {
                        if (f_p1.val * srodek.val < 0) 
                            f_p2 = new Function(srodek.arg);
                        else 
                            f_p1 = new Function(srodek.arg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Kryczyny błąd", 
                    "wartosci funkcji dla tego przedziału są tego samego znaku - algorytm nie może kontynuować", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void siecznych()
        {
            Number p1 = new Number(1.0, 2.0);
            Number p2 = new Number(3.0, 2.0);

            Random rnd = new Random();
            Number epsilon = new Number(rnd.Next(2, 7), 100.0);
            sieczneBox.AppendText("epsilon wynosi " + epsilon.value + "\nkolejne podziały: \n");

            Function f_p1 = new Function(p1.value);
            Function f_p2 = new Function(p2.value);
            Function temp = new Function();

            if (f_p1.val * f_p2.val < 0.0)
            {
                for (int i = 0; i < 15; i++)
                {
                   sieczneBox.AppendText(i + 1 + ". f(" + f_p1.arg.ToString("#.###") + ") = " + f_p1.val.ToString("#.###") + ";    f(" + f_p2.arg.ToString("#.###") + ") = " + f_p2.val.ToString("#.###") + "\n");
                   temp = f_p2;
                   f_p2 = new Function(f_p2.arg - ((f_p2.val * (f_p2.arg - f_p1.arg)) / (f_p2.val - f_p1.val)));
                   f_p1 = temp;
                   Function srodek = new Function(f_p1.arg);
                   if (srodek.modulus() < epsilon.value)
                   {
                       sieczneBox.AppendText(i + 2 + ". f(" + f_p1.arg.ToString("#.###") + ") = " + f_p1.val.ToString("#.#####") + ";    f(" + f_p2.arg.ToString("#.###") + ") = " + f_p2.val.ToString("#.######") + "\n");
                       sieczneBox.AppendText("\n\nMiejsce zerowe znalezione!\nPrzybliżone miejsce zerowe (" + srodek.val.ToString("#.#####") + " < " + epsilon.value + ") znaleziono w x = " + srodek.arg.ToString("#.###"));
                       break;
                   }
                }
                //jesli tu doszedl program to wynik moze byc bledny
            }
            else
            {
                MessageBox.Show("Kryczyny błąd",
                    "wartosci funkcji dla tego przedziału są tego samego znaku - algorytm nie może kontynuować",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            polowienieBox.Clear();
            polowienia();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        private void CreateGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "Metody Obliczeniowe";
            myPane.XAxis.Title.Text = "X";
            myPane.YAxis.Title.Text = "Y";

            double x, y;
            PointPairList list = new PointPairList();
            for (int i = 0; i < 1000; i++)
            {
                x = (double)i * 0.002 + 0.50;
                y = Math.Exp(x) - Math.Tan(x);
                list.Add(x, y);
            }

            LineItem myCurve = new LineItem("e^x - tan(x)",
                  list, Color.Blue, SymbolType.None,2.0f);
            myPane.CurveList.Add(myCurve);
            zgc.IsEnableWheelZoom = false;
            zgc.ZoomButtons = MouseButtons.None;
            zgc.ZoomButtons2 = MouseButtons.None;
            zgc.GraphPane.YAxis.Cross = 0.0;

            zgc.GraphPane.XAxis.Scale.Max = 2.5;
            zgc.GraphPane.XAxis.Scale.MaxAuto = false;
            zgc.GraphPane.XAxis.Scale.Min = 0.5;
            zgc.GraphPane.XAxis.Scale.MinAuto = false;
            zgc.GraphPane.YAxis.Scale.Max = 150;
            zgc.GraphPane.YAxis.Scale.MaxAuto = false;
            zgc.GraphPane.YAxis.Scale.Min = -150;
            zgc.GraphPane.YAxis.Scale.MinAuto = false;

            zgc.AxisChange();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sieczneBox.Clear();
            siecznych();
        }
    }
}
