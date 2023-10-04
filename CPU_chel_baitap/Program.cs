using System;
using System.Text;

namespace cpu
{
    class CPU_Chedule
    {
        static void draw(char x , int y )
        {
            for ( int i = 0; i < y; i++ )
            {
                Console.Write(x);
            }
        }

        static int Sum(int[] a , int x)
        {
            if(x == a.Length -1)
            {
                return a[x];
            }
            return a[x] + Sum(a, x + 1);
        }

        static int Min(int[] a)
        {
            int min = int.MaxValue;
            for( int i = 0;i < a.Length -1;i++)
            {
                if (a[i] < min)
                {
                    min = a[i];
                }
            }
            return min;
        }


                                                                //tt    tdv             dut         tgth        tgc
        public static int[] gantt (int n, int x , int y, int[] temp1, int[]temp2, int[] temp3, int[]temp4, int[] temp5)
        {
            
            temp5[0] = 0;
            for(int i = 1; i < n; i++)
            {
                temp5[i] = temp5[i - 1] + temp4[i-1];
            }
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Processor  :");
            Console.SetCursorPosition(x, y+2);
            Console.WriteLine("wait timer :");
            Console.SetCursorPosition(x+12, y+1);
            draw('├', 1);
            int count = 0;
            for (int i = 0;i < n;i++)
            {
                Console.SetCursorPosition(12 + count + i, y);
                Console.WriteLine($"P{temp1[i]}");
                Console.SetCursorPosition(x + 12 + count + i, y + 2);
                Console.WriteLine($"{temp5[i]}"); 
                Console.SetCursorPosition(x + 13 + count + i, y + 1);
                int bar = temp4[i] / Min(temp4);
                if (temp4[i]% Min(temp4) >= Min(temp4)/2 )
                {
                    bar++;
                }
                draw('─', bar * 5);
                if(i != n-1)
                {
                    draw('┼', 1);
                }
                else
                {
                    draw('┤', 1);
                }
                count += bar * 5;
            }
            Console.SetCursorPosition(x + 12 + count + n, y + 2);
            Console.WriteLine(Sum(temp4, 0));
            return temp5;
        }

        static void swap(ref int x, ref int y)
        {
            int tmp = x;
            x = y;
            y = tmp;
        }

        static void sortDescending(int temp, int[] temp1, int[] temp2, int[] temp3, int[] temp4) //Ascending
        {
            for (int i = 0; i < temp -1; i++)
            {
                for(int j = 0; j< temp - 1; j++)
                {
                    if (temp1[j] < temp1[j + 1])
                    {
                        swap(ref temp1[j], ref temp1[j + 1]);
                        swap(ref temp2[j], ref temp2[j + 1]);
                        swap(ref temp3[j], ref temp3[j + 1]);
                        swap(ref temp4[j], ref temp4[j + 1]);
                    }
                }
            }
        }
        static void sortAscending(int temp, int[] temp1, int[] temp2, int[] temp3, int[] temp4)
        {
            for(int i = 0; i < temp -1 ; i++)
            {
                for (int j = 0; j < temp -1; j++)
                {
                    if (temp1[j] > temp1[j + 1])
                    {
                        swap(ref temp1[j], ref temp1[j + 1]);
                        swap(ref temp2[j], ref temp2[j + 1]);
                        swap(ref temp3[j], ref temp3[j + 1]);
                        swap(ref temp4[j], ref temp4[j + 1]);
                    }
                }
            }
        }
        static double Averaging(int[] temp, int n)
        {
            double average = 0;
            for (int i = 0; i< temp.Length; i++)
            {
                average += temp[i];
            } 
            return average/n;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.Write("Số lượng tiến trình: ");
            int n = int.Parse(Console.ReadLine());
            int[] tt = new int[n];
            int[] tdv = new int[n];
            int[] dut = new int[n];
            int[] tgth = new int[n];
            int[] tgc = new int[n];
            int[] wait = new int[n];
            int o = 1;
            for (int i = 0; i<n; i++)
            {
                Console.Write($"nhập tiến trình {o} :");
                tt[i] = int.Parse(Console.ReadLine());
                Console.Write($"nhập thời điểm vào {o} :");
                tdv[i] = int.Parse(Console.ReadLine());
                Console.Write($"nhập độ ưu tiên {o} :");
                dut[i] = int.Parse(Console.ReadLine());
                Console.Write($"nhập thời gian thực hiện{o} :");
                tgth[i] = int.Parse(Console.ReadLine());
                o++;
            }
            Console.Clear();
            int x = 0, y = 3;
            Console.WriteLine("Cách 1: FCFS ");
            wait = gantt(n, x, y, tt, tdv, dut, tgth, tgc);
            Console.WriteLine();

            for (int i = 0; i < n; i++)
            {

                Console.WriteLine($"Thời gian chờ của tiến trình P{tt[i]} là {tgc[i] - i}ms và thời gian lưu lại là {wait[i] - (tdv[i] - tdv[0]) + tgth[i]} ms");
                tgc[i] = tgc[i] - i;
            }
            Console.WriteLine($"Thời gian trung bình {Averaging(tgc,n)} ms");

            Console.WriteLine("Nhấn phím bất kỳ để chuyển sang PROTIORITY...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Cách 2: Priority ");
            sortDescending(n, dut, tt, tdv, tgth);
            wait = gantt(n, x, y, tt, tdv, dut, tgth, tgc);
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Thời gian chờ của tiến trình P{tt[i]} là {tgc[i]}ms và thời gian lưu lại là {wait[i] - (tdv[i] - tdv[0]) + tgth[i]} ms");
            }
            Console.WriteLine($"Thời gian trung bình {Averaging(tgc, n)} ms");

            Console.WriteLine("Nhấn phím bất kỳ để chuyển sang STF ...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Cách 3: STF ");
            sortAscending(n, tgth, tt, tdv, dut);
            wait = gantt(n, x, y, tt, tdv, dut, tgth, tgc);
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Thời gian chờ của tiến trình P{tt[i]} là {tgc[i]}ms và thời gian lưu lại là {wait[i] - (tdv[i] - tdv[0]) + tgth[i]} ms");
            }
            Console.WriteLine($"Thời gian trung bình {Averaging(tgc, n)} ms");
            
        }
    }
}