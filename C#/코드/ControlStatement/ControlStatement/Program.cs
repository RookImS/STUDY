using System;

namespace ControlStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------
            int value = 6;
            bool result1 = (value % 3 == 0);         // 관계 연산자의 결과는 bool
            bool result2 = (value % 3 > 0);
            Console.WriteLine(result1);
            Console.WriteLine(result2);

            Console.WriteLine();
            // -------------------------------------------------
            value = 9;
            bool or = (value % 3 == 0 || value % 5 == 0);       // 이미 value % 3이 참이고 or연산이므로 단락 계산 일어남
            bool and = (value % 3 == 0 && value % 5 == 0);
            bool not = !(value % 5 == 0);

            Console.WriteLine(value + "는 3의 배수 또는 5의 배수 : " + or);
            Console.WriteLine(value + "는 3의 배수 이면서 5의 배수 : " + and);
            Console.WriteLine(value + "는 5의 배수가 아니다 :  " + not);

            Console.WriteLine();
            // -------------------------------------------------
            value = 9;

            if (value < 3)
                Console.WriteLine(value + "는 3보다 작음");
            else
                Console.WriteLine(value + "는 3보다 작은 범위의 밖에 있음");

            if (value < 12)                                         // if문은 서로 별개의 작동이다.
                Console.WriteLine(value + "는 12보다 작음");
            if (value < 15)
                Console.WriteLine(value + "는 15보다 작음");

            if (value < 12)                                         // if-else문은 서로 연동되어 작동된다.
                Console.WriteLine(value + "는 12보다 작음");
            else if (value < 15)
                Console.WriteLine(value + "는 3보다 작은 범위의 밖에 있고 15보다 작음");
            else
                Console.WriteLine(value + "는 15보다 작은 범위의 밖에 있음");

            string str = (value % 3 == 0 ? "3의 배수" : "3의 배수가 아님");
            Console.WriteLine(str);

            char op = '+';
            int a = 5;
            int b = 3;
            int res = (op == '+' ? a + b : a - b);              // 삼항 연산자 또한 피연산자로 식이 올 수 있다.
            Console.WriteLine(a + " " + op + " " + b + " = " + res);

            Console.WriteLine();
            // -------------------------------------------------
            res = 0;
            a = 4;
            b = 2;
            op = '-';

            if (op == '+')
                res = a + b;
            else if (op == '-')
                res = a - b;
            else if (op == '*')
                res = a * b;
            else if (op == '/')
                res = a / b;
            else
                res = a;

            Console.WriteLine(res);

            // 위의 if-else문과 같음
            switch (op)
            {
                case '+':
                    res = a + b;
                    break;
                case '-':
                    res = a - b;
                    break;
                case '*':
                    res = a * b;
                    break;
                case '/':
                    res = a / b;
                    break;
                default:
                    res = a;
                    break;
            }
            Console.WriteLine(res);

            string lang = "C#";

            switch(lang)
            {
                case "C#":          // 특정 case에 대해서 아무런 작업을 안할 수 있음, 대신 다음 case에 있는 명령을 공유함
                case "VB.NET":
                    Console.WriteLine(".NET 호환 언어");
                    break;
                case "Java":
                    Console.WriteLine("JVM 언어");
                    break;
                default:
                    Console.WriteLine("알 수 없음");
                    break;
            }
            // -------------------------------------------------
        }
    }
}
