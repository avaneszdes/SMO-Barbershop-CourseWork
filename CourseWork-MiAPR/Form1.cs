using System;
using System.Windows.Forms;

namespace CourseWork_MiAPR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Вероятность отсутствия клиентов
        private double Calculate_P0(double p, double n, double m)
        {
            double result = 0;
            if (p == n)
            {
                for (int i = 0; i <= n; i++)
                {
                    result += (Math.Pow(n, i) / Factorial(i));
                }
                result += Math.Pow(n, n) / Factorial(n) * m;
                return Math.Pow(result, -1);
            }

            for (int i = 0; i <= n; i++)
            {
                result += Math.Pow(p, i) / Factorial(i);
            }

            for (int i = 0; i <= m; i++)
            {
                result += Math.Pow(p, n + i) / Math.Pow(n, i) * Factorial(n);
            }
            return Math.Pow(result, -1);
        }

        ////Вероятность отказа
        private double Calculate_Potk(double p, double m, double n, double p0)
        {
            return Math.Pow(p, n + m) / (Math.Pow(n, m) * Factorial(n)) * p0;
        }

        // относительно пропускная способность
        private double Calculate_Q(double pOtk)
        {
            return 1 - pOtk;
        }

        //Интенсивность обслуживания
        private double Calculate_Lоч(double p, double n, double m, double p0)
        {
            if (n == p)
            {
                return (Math.Pow(n, n) / Factorial(n)) * (m * (m + 1) / 2) * p0;
            }


            return (Math.Pow(p, n + 1) / (n * Factorial(n))) * ((1 - Math.Pow(p / n, m) * (1 + m * (1 - p / n))) / Math.Pow(1 - p / n, 2)) * p0;
        }


        //среднее время прибывания

        private double Calculate_t(double lОч, double l, double q, double M)
        {
            return (lОч / l) + (q / M);
        }



        private double Factorial(double n)
        {
            if (n <= 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double p = double.Parse(textBox1.Text) / double.Parse(textBox3.Text);
            double n = double.Parse(textBox2.Text);
            double m = double.Parse(textBox5.Text);

            label12.Text = Math.Round(Calculate_P0(p, n, m) * 100, 2) + "  %";
            label13.Text = Math.Round(Calculate_Potk(p, m, n, Calculate_P0(p, n, m) * 100), 2) + "  %";
            label14.Text = Math.Round(Calculate_Q(Calculate_Potk(p, m, n, Calculate_P0(p, n, m))) * 100, 2) + "  %";
            label18.Text = Math.Round(Calculate_Lоч(p, n, m, Calculate_P0(p, n, m)), 2).ToString();
            label17.Text = Math.Ceiling(Calculate_t(Calculate_Lоч(p, n, m, Calculate_P0(p, n, m)),
                double.Parse(textBox1.Text), Calculate_Q(Calculate_Potk(p, m, n, Calculate_P0(p, n, m))),
                double.Parse(textBox3.Text)) * 60) + "  минут";
        }
    }
}
