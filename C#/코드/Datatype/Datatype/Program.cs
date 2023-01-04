namespace MyDatatype
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------
            int n1 = 50;        // 선언과 동시에 초기화
            int n2;
            n2 = 100;           // 선언 후 초기화

            int sum = n1 + n2;
            Console.WriteLine(sum);

            Console.WriteLine();
            // -------------------------------------------------
            float f = 5.2f;                     // float, decimal 기본 타입은 사용시 실수 뒤에 f, m을 각각 붙여야함
            double d = 10.5;
            decimal money = 200.099m;

            Console.WriteLine();
            // -------------------------------------------------
            char ch1 = 'A';
            char ch2 = 'B';
            // char charSum1 = ch1 + ch2;        // ushort와 같은 범위의 수를 나타낼 수 있는 char이지만 사칙연산 후 다시 char에 넣는 것은 불가
            
            Console.WriteLine(ch1 + ch2);
            Console.WriteLine(ch1 + " " + ch2);

            Console.WriteLine();
            // -------------------------------------------------
            char es1 = '\n';                    // escape sequence를 활용해서 키보드로 입력이 불가능한 문자를 표현할 수 있음
            char es2 = '\t';
            char es3 = '\'';
            char es4 = '\\';                    // 막상 출력이 불가능한 \를 출력할때 사용
            char es5 = '\u2023';                // Unicode를 출력할때 사용 es4는 '▶'와 대응
            Console.Write(ch1);
            Console.Write(es1);
            Console.Write(ch2);
            Console.Write(es2);
            Console.Write(ch2);
            Console.Write(es3);
            Console.Write(es4);
            Console.WriteLine(es5);             // cmd에는 폰트가 존재하지 않아 ?로 출력

            Console.WriteLine();
            // -------------------------------------------------
            string text1 = "Hello, World";
            Console.WriteLine(text1);
            text1 = "\"Hello, World\"\n";		// 문자열에 대한 escape sequence 활용
            Console.Write(text1);
            text1 = @"\tHello, World\n";		// @을 이용해 escape sequence 무시 가능
            Console.WriteLine(text1);

            text1 = "Concatenate ";
            string text2 = "String";
            string text3 = text1 + text2;		// 문자열은 +연산을 활용해서 붙이기 가능
            Console.WriteLine(text3);

            Console.WriteLine();
            // -------------------------------------------------
            Console.WriteLine(typeof(int));             // int와 같은 기본 타입은 닷넷 형식에 대한 C#의 예약어
            Console.WriteLine(typeof(System.Int32));

            Console.WriteLine();
            // -------------------------------------------------
        }
    }
}