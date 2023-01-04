namespace HandleValue
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------
            byte b = 250;   // 1바이트
            short s = b;    // 1바이트 --> 2바이트의 암시적 변환
            Console.Write(b);
            Console.Write(' ');
            Console.WriteLine(s);

            ushort u = 65;  // 2바이트
            char c = 'a';   // 2바이트, UTF16의 97번 문자
            //char c = u;     // 2바이트 --> 2바이트이지만 char는 문자만 저장해야하므로 암시적 변환 실패
            c = (char)u;    // 명시적 변환으로 강제로 변환은 가능
            Console.Write(u);
            Console.Write(' ');
            Console.WriteLine(c);
            u = (ushort)c;

            int n = 40000;
            s = (short)n;
            Console.Write(n);
            Console.Write(' ');
            Console.WriteLine(s);
            // -------------------------------------------------
            const bool result = false;
            //result = true;        // 상수는 변경할 수 없음
            int maxN = Math.Max(0, 5);          // 결과가 5임을 확실히 알 수 있고 이는 변수에 대입 가능
            //const int constMaxN = Math.Max(0, 5);    // 결과가 5임이 당연하지만 컴파일러 입장에서는 판단이 불가능하므로 불가능
            const int max = 100 + 100;              // 수식으로 돼있는 경우에는 컴파일러가 바로 계산 가능하므로 상수 가능
            // -------------------------------------------------
        }
    }
}