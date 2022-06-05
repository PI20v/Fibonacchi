using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fib
{
    public partial class Fibonachi : Form
    {
        Autorization autorization;
        delegate void Del();
        Del del;
        List<int> FibCounts = new List<int>();
        string User;
        int a;

        public Fibonachi(Autorization autorization, string User)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Fib", "Ряд Фибоначчи");
            this.autorization = autorization;
            this.User = User;

            using (StreamWriter writer = new StreamWriter(User + ".txt", File.Exists(User + ".txt")))
            {
                writer.WriteLine("Пользователь авторизировался - " + User + ", время - " + DateTime.Now.ToString().Split(' ')[0]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = (int)numericUpDown1.Value;

            dataGridView1.Rows.Clear();
            FibCounts.Clear();

            if (a >= 1) FibCounts.Add(0);
            if (a >= 2) FibCounts.Add(1);
            if (a > 3)
            {
                del = Fib;
                BeginInvoke(del);
            }

            if ((FibCounts.Count == 1 || FibCounts.Count == 2) && (a == 0 || a == 1))
            {
                using (StreamWriter writer = new StreamWriter(User + ".txt", File.Exists(User + ".txt")))
                {
                    writer.Write("Пользователь ввел число - " + a + " и получил числа Фибоначчи - ");
                    for (int i = 0; i < FibCounts.Count; i++)
                    {
                        dataGridView1.Rows.Add(FibCounts[i].ToString());
                        writer.Write(FibCounts[i] + "; ");
                    }
                    writer.Write('\n');
                }
            }
        }

        private void Fib()
        {
            int temp = FibCounts[FibCounts.Count - 1] + FibCounts[FibCounts.Count - 2];
            if (FibCounts.Count < a)
            {
                FibCounts.Add(temp);
                Fib();
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(User + ".txt", File.Exists(User + ".txt")))
                {
                    writer.Write("Пользователь ввел число - " + a + " и получил числа Фибоначчи - ");
                    for (int i = 0; i < FibCounts.Count; i++)
                    {
                        dataGridView1.Rows.Add(FibCounts[i].ToString());
                        writer.Write(FibCounts[i] + "; ");
                    }
                    writer.Write('\n');
                }
            }
            return;
        }

        private void Fibonachi_FormClosed(object sender, FormClosedEventArgs e)
        {
            autorization.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (File.Exists("Справка.ref")) Process.Start("Справка.ref");
            else
            {
                MessageBox.Show("Файл со справкой отсутствует", "Ошибка");
                return;
            }
        }
    }
}
