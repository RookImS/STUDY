using System.Runtime.InteropServices;

namespace RepetitiveStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------
            int a = 1;
            Console.Write(a);
            Console.Write('>');
            Console.Write(++a);     // a를 증가시키고 나서 a 사용
            Console.Write('>');
            Console.Write(a);
            Console.Write(' ');
            Console.Write(a);
            Console.Write('>');
            Console.Write(a++);     // a를 사용하고 나서 a 증가
            Console.Write('>');
            Console.WriteLine(a);

            Console.WriteLine();
            // -------------------------------------------------
            int b = 0;
            b += 5;
            Console.WriteLine(b);
            b = b + 5;
            Console.WriteLine(b);

            Console.WriteLine();
            // -------------------------------------------------
            int n = 5;
            while(n > 0)
            {
                Console.Write(n);
                Console.Write(' ');
                --n;
            }
            Console.WriteLine();

            n = 0;
            int m = 0;
            while(n < 3)
            {
                while (m < 3)
                {
                    Console.Write(n);
                    Console.Write(',');
                    Console.Write(m);
                    Console.Write(' ');
                    ++m;
                }
                Console.WriteLine();
                ++n;
            }
            Console.WriteLine();
            // -------------------------------------------------
            for(int i = 0; i < 3; ++i)                  // 사실상 위의 while문과 같은 의미이다.
            {
                for(int j = 0; j < 3; ++j)
                {
                    Console.Write(i);
                    Console.Write(',');
                    Console.Write(j);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            // -------------------------------------------------
            int[] nums = new int[5]{ 12, 25, 35, 42, 5 };
            for(int i = 0; i < 5; ++i)
            {
                if (i == 3)
                    break;
                Console.Write(nums[i]);
                Console.Write(' ');
            }
            Console.WriteLine();

            for (int i = 0; i < 5; ++i)
            {
                if (i == 3)
                    continue;
                Console.Write(nums[i]);
                Console.Write(' ');
            }
            Console.WriteLine();

            for (int i = 1; i <= 5; ++i)
            {
                for(int j = 1; j <= 5; ++j)
                {
                    if (i * j == 15)
                        goto EXIT;
                    Console.Write(i * j);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        EXIT:
            Console.WriteLine("탈출");
            // -------------------------------------------------
        }
    }
}