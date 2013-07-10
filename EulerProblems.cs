using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public long Factorial(int num)
        {
            long val = 1;
            for (int i = 2; i <= num; ++i)
                val *= i;
            return val;
        }

        public long MakePandigital(long index, int digits)
        {
            List<int> digit = new List<int>();
            for (int i = 1; i <= digits; i++)
            {
                digit.Add(i);
            }
            long num = 0;
            long m = Factorial(digits);
            int k;
            for (int i = digits; i >= 1; --i)
            {
                m /= i;
                num *= 10;
                k = (int)(index / m);
                num += digit[k];
                digit.RemoveAt(k);
                index %= m;
            }
            return num;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            problem32();
        }
        [Obsolete]
        void problem32()
        {
            List<int> nums = new List<int>();
            for (int i = 6; i <= 9; ++i)
            {
                int max = (int)Factorial(i);
                for (int j = 0; j < max; ++j)
                {
                    long k = MakePandigital(j, i);
                    string x = k.ToString();
                    for (int l = 1; l < x.Length - 1; l++)
                    {
                        int a = int.Parse(x.Substring(0, l));
                        for (int m = l + 1; m < x.Length; m++)
                        {
                            int b = int.Parse(x.Substring(l, m - l));
                            int c = a * b;
                            int d = int.Parse(x.Substring(m));
                            if (c == d && !nums.Contains(d))
                                nums.Add(d);
                        }
                    }
                }
            }
            int total = 0;
            foreach (int num in nums)
                total += num;
            show(total);
        }
        private void problem160()
        {
            long total = 1;
            for (long i = 2; i < 1000000000000; ++i)
            {
                long n = i;
                while (n % 10 == 0) n /= 10;
                total *= n;
                while (total % 10 == 0)
                {
                    total /= 10;
                }
                total %= 100000;
            }
            textBox1.Text = total.ToString();
        }
        private void problem21()
        {
            List<int> found = new List<int>();
            for (int a = 10; a <= 10000; ++a)
            {
                if (!found.Contains(a))
                {
                    int b = d(a);
                    if (d(b) == a && a != b)
                    {
                        found.Add(a);
                        if (!found.Contains(b))
                        {
                            found.Add(b);
                        }
                    }
                }
            }
            int total = 0;
            foreach (int v in found) total += v;
            textBox1.Text = total.ToString();
        }
        private int d(int num)
        {
            int total = 0;
            for (int i = 1; i < num; ++i)
                if (num % i == 0)
                    total += i;
            return total;
        }
        private void problem34()
        {
            int[] facts = new int[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };
            int total = 0;
            for (int i = 10; i < 10000000; ++i)
            {
                string num = i.ToString();
                int val = 0;
                foreach (char c in num)
                {
                    val += facts[c - '0'];
                }
                if (val == i)
                {
                    total += val;
                }
            }
            textBox1.Text = total.ToString();
        }
        private void problem69()
        {
            List<int> primes = new List<int>();
            bool[] isNotPrime = new bool[1000001];
            isNotPrime[0] = true;
            isNotPrime[1] = true;
            for (int i = 4; i < isNotPrime.Length; i += 2)
                isNotPrime[i] = true;
            for (int i = 3; i < isNotPrime.Length; ++i)
            {
                int k = i * 2;
                for (int j = i * 3; j < isNotPrime.Length; j += k)
                {
                    isNotPrime[j] = true;
                }
            }
            for (int i = 0; i < isNotPrime.Length; ++i)
            {
                if (!isNotPrime[i]) primes.Add(i);
            }
            int max = 1;
            for (int i = 0; i < primes.Count; ++i)
            {
                if (max * primes[i] < 1000000)
                {
                    max *= primes[i];
                }
                else
                {
                    break;
                }
            }

            textBox1.Text = max.ToString();
        }
        private int totient(int n, List<int> primes)
        {
            int primeFactors = 0;
            foreach (int prime in primes)
            {
                if (prime >= n)
                    break;
                if (n % prime == 0)
                    primeFactors++;
            }
            return n - primeFactors;
        }
        int start = 37;
        private void problem59()
        {
            int[] msg = new int[] { 79, 59, 12, 2, 79, 35, 8, 28, 20, 2, 3, 68, 8, 9, 68, 45, 0, 12, 9, 67, 68, 4, 7, 5, 23, 27, 1, 21, 79, 85, 78, 79, 85, 71, 38, 10, 71, 27, 12, 2, 79, 6, 2, 8, 13, 9, 1, 13, 9, 8, 68, 19, 7, 1, 71, 56, 11, 21, 11, 68, 6, 3, 22, 2, 14, 0, 30, 79, 1, 31, 6, 23, 19, 10, 0, 73, 79, 44, 2, 79, 19, 6, 28, 68, 16, 6, 16, 15, 79, 35, 8, 11, 72, 71, 14, 10, 3, 79, 12, 2, 79, 19, 6, 28, 68, 32, 0, 0, 73, 79, 86, 71, 39, 1, 71, 24, 5, 20, 79, 13, 9, 79, 16, 15, 10, 68, 5, 10, 3, 14, 1, 10, 14, 1, 3, 71, 24, 13, 19, 7, 68, 32, 0, 0, 73, 79, 87, 71, 39, 1, 71, 12, 22, 2, 14, 16, 2, 11, 68, 2, 25, 1, 21, 22, 16, 15, 6, 10, 0, 79, 16, 15, 10, 22, 2, 79, 13, 20, 65, 68, 41, 0, 16, 15, 6, 10, 0, 79, 1, 31, 6, 23, 19, 28, 68, 19, 7, 5, 19, 79, 12, 2, 79, 0, 14, 11, 10, 64, 27, 68, 10, 14, 15, 2, 65, 68, 83, 79, 40, 14, 9, 1, 71, 6, 16, 20, 10, 8, 1, 79, 19, 6, 28, 68, 14, 1, 68, 15, 6, 9, 75, 79, 5, 9, 11, 68, 19, 7, 13, 20, 79, 8, 14, 9, 1, 71, 8, 13, 17, 10, 23, 71, 3, 13, 0, 7, 16, 71, 27, 11, 71, 10, 18, 2, 29, 29, 8, 1, 1, 73, 79, 81, 71, 59, 12, 2, 79, 8, 14, 8, 12, 19, 79, 23, 15, 6, 10, 2, 28, 68, 19, 7, 22, 8, 26, 3, 15, 79, 16, 15, 10, 68, 3, 14, 22, 12, 1, 1, 20, 28, 72, 71, 14, 10, 3, 79, 16, 15, 10, 68, 3, 14, 22, 12, 1, 1, 20, 28, 68, 4, 14, 10, 71, 1, 1, 17, 10, 22, 71, 10, 28, 19, 6, 10, 0, 26, 13, 20, 7, 68, 14, 27, 74, 71, 89, 68, 32, 0, 0, 71, 28, 1, 9, 27, 68, 45, 0, 12, 9, 79, 16, 15, 10, 68, 37, 14, 20, 19, 6, 23, 19, 79, 83, 71, 27, 11, 71, 27, 1, 11, 3, 68, 2, 25, 1, 21, 22, 11, 9, 10, 68, 6, 13, 11, 18, 27, 68, 19, 7, 1, 71, 3, 13, 0, 7, 16, 71, 28, 11, 71, 27, 12, 6, 27, 68, 2, 25, 1, 21, 22, 11, 9, 10, 68, 10, 6, 3, 15, 27, 68, 5, 10, 8, 14, 10, 18, 2, 79, 6, 2, 12, 5, 18, 28, 1, 71, 0, 2, 71, 7, 13, 20, 79, 16, 2, 28, 16, 14, 2, 11, 9, 22, 74, 71, 87, 68, 45, 0, 12, 9, 79, 12, 14, 2, 23, 2, 3, 2, 71, 24, 5, 20, 79, 10, 8, 27, 68, 19, 7, 1, 71, 3, 13, 0, 7, 16, 92, 79, 12, 2, 79, 19, 6, 28, 68, 8, 1, 8, 30, 79, 5, 71, 24, 13, 19, 1, 1, 20, 28, 68, 19, 0, 68, 19, 7, 1, 71, 3, 13, 0, 7, 16, 73, 79, 93, 71, 59, 12, 2, 79, 11, 9, 10, 68, 16, 7, 11, 71, 6, 23, 71, 27, 12, 2, 79, 16, 21, 26, 1, 71, 3, 13, 0, 7, 16, 75, 79, 19, 15, 0, 68, 0, 6, 18, 2, 28, 68, 11, 6, 3, 15, 27, 68, 19, 0, 68, 2, 25, 1, 21, 22, 11, 9, 10, 72, 71, 24, 5, 20, 79, 3, 8, 6, 10, 0, 79, 16, 8, 79, 7, 8, 2, 1, 71, 6, 10, 19, 0, 68, 19, 7, 1, 71, 24, 11, 21, 3, 0, 73, 79, 85, 87, 79, 38, 18, 27, 68, 6, 3, 16, 15, 0, 17, 0, 7, 68, 19, 7, 1, 71, 24, 11, 21, 3, 0, 71, 24, 5, 20, 79, 9, 6, 11, 1, 71, 27, 12, 21, 0, 17, 0, 7, 68, 15, 6, 9, 75, 79, 16, 15, 10, 68, 16, 0, 22, 11, 11, 68, 3, 6, 0, 9, 72, 16, 71, 29, 1, 4, 0, 3, 9, 6, 30, 2, 79, 12, 14, 2, 68, 16, 7, 1, 9, 79, 12, 2, 79, 7, 6, 2, 1, 73, 79, 85, 86, 79, 33, 17, 10, 10, 71, 6, 10, 71, 7, 13, 20, 79, 11, 16, 1, 68, 11, 14, 10, 3, 79, 5, 9, 11, 68, 6, 2, 11, 9, 8, 68, 15, 6, 23, 71, 0, 19, 9, 79, 20, 2, 0, 20, 11, 10, 72, 71, 7, 1, 71, 24, 5, 20, 79, 10, 8, 27, 68, 6, 12, 7, 2, 31, 16, 2, 11, 74, 71, 94, 86, 71, 45, 17, 19, 79, 16, 8, 79, 5, 11, 3, 68, 16, 7, 11, 71, 13, 1, 11, 6, 1, 17, 10, 0, 71, 7, 13, 10, 79, 5, 9, 11, 68, 6, 12, 7, 2, 31, 16, 2, 11, 68, 15, 6, 9, 75, 79, 12, 2, 79, 3, 6, 25, 1, 71, 27, 12, 2, 79, 22, 14, 8, 12, 19, 79, 16, 8, 79, 6, 2, 12, 11, 10, 10, 68, 4, 7, 13, 11, 11, 22, 2, 1, 68, 8, 9, 68, 32, 0, 0, 73, 79, 85, 84, 79, 48, 15, 10, 29, 71, 14, 22, 2, 79, 22, 2, 13, 11, 21, 1, 69, 71, 59, 12, 14, 28, 68, 14, 28, 68, 9, 0, 16, 71, 14, 68, 23, 7, 29, 20, 6, 7, 6, 3, 68, 5, 6, 22, 19, 7, 68, 21, 10, 23, 18, 3, 16, 14, 1, 3, 71, 9, 22, 8, 2, 68, 15, 26, 9, 6, 1, 68, 23, 14, 23, 20, 6, 11, 9, 79, 11, 21, 79, 20, 11, 14, 10, 75, 79, 16, 15, 6, 23, 71, 29, 1, 5, 6, 22, 19, 7, 68, 4, 0, 9, 2, 28, 68, 1, 29, 11, 10, 79, 35, 8, 11, 74, 86, 91, 68, 52, 0, 68, 19, 7, 1, 71, 56, 11, 21, 11, 68, 5, 10, 7, 6, 2, 1, 71, 7, 17, 10, 14, 10, 71, 14, 10, 3, 79, 8, 14, 25, 1, 3, 79, 12, 2, 29, 1, 71, 0, 10, 71, 10, 5, 21, 27, 12, 71, 14, 9, 8, 1, 3, 71, 26, 23, 73, 79, 44, 2, 79, 19, 6, 28, 68, 1, 26, 8, 11, 79, 11, 1, 79, 17, 9, 9, 5, 14, 3, 13, 9, 8, 68, 11, 0, 18, 2, 79, 5, 9, 11, 68, 1, 14, 13, 19, 7, 2, 18, 3, 10, 2, 28, 23, 73, 79, 37, 9, 11, 68, 16, 10, 68, 15, 14, 18, 2, 79, 23, 2, 10, 10, 71, 7, 13, 20, 79, 3, 11, 0, 22, 30, 67, 68, 19, 7, 1, 71, 8, 8, 8, 29, 29, 71, 0, 2, 71, 27, 12, 2, 79, 11, 9, 3, 29, 71, 60, 11, 9, 79, 11, 1, 79, 16, 15, 10, 68, 33, 14, 16, 15, 10, 22, 73 };
            int[] keys = new int[3];
            keys[start % 3] = msg[start] ^ 't';
            keys[(start + 1) % 3] = msg[start + 1] ^ 'h';
            keys[(start + 2) % 3] = msg[start + 2] ^ 'e';
            int x = 0;
            for (int i = 0; i < msg.Length; ++i)
            {
                x += msg[i] ^ keys[i % 3];
            }
            show(x);
        }
        private void show(object obj)
        {
            this.textBox1.Text = obj.ToString();
        }
        private void problem29()
        {
            List<double> vals = new List<double>();
            for (int a = 2; a <= 100; a++)
            {
                for (int b = 2; b <= 100; ++b)
                {
                    double d = Math.Pow(a, b);
                    if (!vals.Contains(d))
                    {
                        vals.Add(d);
                    }
                }
            }
            textBox1.Text = vals.Count.ToString();
        }

        private void problem31()
        {
            int count = 1;
            for (int p = 0; p <= 200; p += 100)
            {
                int v = p;
                if (v == 200)
                    count++;
                else if (v < 200)
                {
                    for (int f = 0; f <= 200 - v; f += 50)
                    {
                        v += f;
                        if (v == 200)
                        {
                            count++;
                        }
                        else if (v < 200)
                        {
                            for (int tw = 0; tw <= 200 - v; tw += 20)
                            {
                                v += tw;
                                if (v == 200)
                                {
                                    count++;
                                }
                                else if (v < 200)
                                {
                                    for (int tn = 0; tn <= 200 - v; tn += 10)
                                    {
                                        v += tn;
                                        if (v == 200)
                                        {
                                            count++;
                                        }
                                        else if (v < 200)
                                        {
                                            for (int nk = 0; nk <= 200 - v; nk += 5)
                                            {
                                                v += nk;
                                                if (v == 200)
                                                {
                                                    count++;
                                                }
                                                else if (v < 200)
                                                {
                                                    for (int tp = 0; tp <= 200 - v; tp += 2)
                                                    {
                                                        v += tp;
                                                        if (v <= 200)
                                                        {
                                                            count++;
                                                        }
                                                        v -= tp;
                                                    }
                                                }
                                                v -= nk;
                                            }
                                        }
                                        v -= tn;
                                    }
                                }
                                v -= tw;
                            }
                        }
                        v -= f;
                    }
                }
            }
            textBox1.Text = count.ToString();
        }
        private void problem52()
        {
            bool found = false;
            for (int numDigits = 1; numDigits <= 10; ++numDigits)
            {
                int p = (int)Math.Pow(10, numDigits - 1);
                int q = p * 10 / 6;
                for (int number = p; number <= q; ++number)
                {
                    found = digitsMatch(number, number * 2, number * 3, number * 4, number * 5, number * 6);
                    if (found)
                    {
                        textBox1.Text = number.ToString();
                        break;
                    }
                }
                if (found) break;
            }
            if (!found) textBox1.Text = "Not Found";
        }
        public bool digitsMatch(int a, int b, int c, int d, int e, int f)
        {
            string A = a.ToString();
            string B = b.ToString();
            string C = c.ToString();
            string D = d.ToString();
            string E = e.ToString();
            string F = f.ToString();
            if (A.Length == B.Length && B.Length == C.Length && C.Length == D.Length
                && D.Length == E.Length && E.Length == F.Length)
            {
                foreach (char t in A)
                    if (!(B.IndexOf(t) > -1 && C.IndexOf(t) > -1
                        && D.IndexOf(t) > -1 && E.IndexOf(t) > -1 && F.IndexOf(t) > -1))
                        return false;
                return true;
            }
            return false;
        }
        public void problem48()
        {
            long total = 0;
            for (int i = 1; i <= 10000; ++i)
            {
                total = (total + power(i, i)) % 10000000000;
            }
            textBox1.Text = total.ToString();
        }

        private long power(int n, int p)
        {
            long total = 1;
            for (int i = 0; i < p; ++i)
            {
                total = (total * n) % 10000000000;
            }
            return total;
        }
    }
}