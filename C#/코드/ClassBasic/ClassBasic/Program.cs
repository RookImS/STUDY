using System;
using System.Numerics;

namespace ClassBasic
{
    class CellPhone
    {
        // 속성 정의 = 필드
        public string name;
        public string maker;
        public int storage;
        public int numOfCamera;
        public bool canUseSD;

        // 행위 정의 = 메서드
        public void TurnOn()
        {
            Console.WriteLine(name + "이 켜졌습니다.");
        }

        public void TurnOff()
        {
            Console.WriteLine(name + "이 꺼졌습니다.");
        }

        public int AddStorage(int addSize)          // 추가 받을 크기를 받고, 보너스를 더해서 저장용량을 확정
        {
            storage += addSize;

            int bonus = 20;
            storage += bonus;

            return storage;
        }
    }

    class Mathematics
    {
        // 정적 멤버 변수
        public static int numUsageOfClass;
        private static int numCalcInClass;
            
        // 인스턴스 멤버 변수
        public int a;
        public int b;
        private int sum;
        private double pi;
        private double radius;

        // 프로퍼티
        public double Pi
        {
            get { return pi; }
        }

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public static int NumCalcInClass
        {
            get { return numCalcInClass; }
        }

        // 여러 종류의 생성자
        public Mathematics()                // 기본 생성자와 닮은 생성자
        {
            a = 0;
            b = 0;
            sum = 0;
            pi = 3.14;
        }

        public Mathematics(int na, int nb)  // 2개의 매개변수를 받는 생성자
        {
            a = na;
            b = nb;
            sum = 0;
            pi = 3.14;
        }

        static Mathematics()                // 정적 생성자
        {
            numUsageOfClass = 0;
            numCalcInClass = 0;
        }

        public static int GetCalcNum()             // 현재까지의 계산 수를 얻기 위해 사용하는 public 정적 메서드
        {
            numUsageOfClass++;
            return numCalcInClass;
        }

        public int GetSum(char op)                 // sum의 결과를 얻기 위해 사용하는 public 인스턴스 메서드
        {
            numUsageOfClass++;
            switch (op)
            {
                case '+':
                    plus();
                    break;
                case '-':
                    minus();
                    break;
                case '*':
                    multiply();
                    break;
                case '/':
                    divide();
                    break;
                default:
                    sum = 0;
                    break;
            }

            return sum;
        }

        public int GetStrangeSum()          // 정적 멤버와 인스턴스 멤버를 모두 사용하는 인스턴스 메서드
        {
            numUsageOfClass++;
            numCalcInClass++;
            return sum + numCalcInClass + numUsageOfClass;
        }

        private void plus()                 // 내부적으로 사칙연산을 해서 sum을 구할 때 사용되는 인스턴스 private메서드들
        {
            numCalcInClass++;
            sum = a + b;
        }

        private void minus()
        {
            numCalcInClass++;
            sum = a - b;
        }

        private void multiply()
        {
            numCalcInClass++;
            sum = a * b;
        }

        private void divide()
        {
            if (b != 0)
            {
                sum = a / b;
                numCalcInClass++;
            }
            else
                sum = 0;
        }
    }

    class CtorTest1
    {
    }
    class CtorTest2
    {
        public CtorTest2(int a)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------
            CellPhone galaxy = new CellPhone();         // 객체는 '클래스를 구현'한 것이고, 이는 new 연산자를 통해 만들어낸다.
            CellPhone iphone = new CellPhone();

            galaxy.name = "Galaxy";                     // 클래스의 필드를 부여받은 객체 내부의 실제 데이터에 .연산자를 통해 접근 가능
            Console.WriteLine(galaxy.name);
            iphone.name = "iPhone";
            Console.WriteLine(iphone.name);

            galaxy.TurnOn();
            iphone.TurnOn();

            int add = 10;
            int res = galaxy.AddStorage(add);
            Console.WriteLine(res);

            Console.WriteLine();
            // -------------------------------------------------
            CtorTest1 ctor1 = new CtorTest1();              // 선언한 생성자가 없지만 기본 생성자를 통해 만듦
            //CtorTest2 ctor2 = new CtorTest2();            // 선언한 생성자가 있기 때문에 기본 생성자 사용 불가
            CtorTest2 ctor2 = new CtorTest2(1);             // 선언한 생성자 사용
            // -------------------------------------------------
            Mathematics math1 = new Mathematics(1, 2);      // 매개변수 존재하는 인스턴스 생성자로 실행, Mathematics 클래스가 처음 사용됐으므로 정적 생성자 자동으로 사용
            Mathematics math2 = new Mathematics();          // 매개변수 존재하지 않는 인스턴스 생성자로 실행

            math2.a = 5;                // 인스턴스 필드는 인스턴스를 통해 접근 가능
            math2.b = 0;
            //math.sum = 4;             // 인스턴스 필드이지만 private이므로 접근 불가능

            //int sum = math1.plus();                               // 인스턴스 메서드를 사용했지만 private이라 접근 불가능
            int sum = math1.GetSum('+');                            // 인스턴스 메서드를 사용, 정적 필드 numUsageOfClass, numCalcInClass 1씩 증가
            Console.WriteLine("계산 결과 : " + sum);

            sum = math2.GetSum('/');                                // 인스턴스 메서드를 사용, 정적 필드 numUsageOfClass 1 증가, b가 0일 때는 나누기가 안되므로 numCalcInClass는 증가 안함
            
            //Console.WriteLine("클래스 사용된 횟수 : " + math1.numUsageOfClass);           // 정적 필드는 인스턴스를 통해 접근 불가
            Console.WriteLine("클래스 사용된 횟수 : " + Mathematics.numUsageOfClass);       // 정적 필드는 클래스를 통해 접근
            //Console.WriteLine("클래스 내 계산의 횟수 : " + Mathematics.numCalcInClass);   // 정적 필드를 정상적으로 접근했지만 private이라 접근 불가능 
            Console.WriteLine("클래스 내 계산의 횟수 : " + Mathematics.GetCalcNum());       // 정적 메서드를 통해서 간접적으로 private값에 접근
            Console.WriteLine("이상한 계산 결과 : " + math1.GetStrangeSum());               // 내부에서 정적 필드를 사용한 인스턴스 메서드

            Console.WriteLine();
            // -------------------------------------------------
            //math1.radius = 10;    // private 필드는 사용 불가능
            math1.Radius = 10;      // private 필드에 대한 프로퍼티 내에 set이 있으므로 사용 가능
            //math1.Pi = 5;         // 프로퍼티 내에 get밖에 없으므로 사용 불가능
            Console.WriteLine("프로퍼티의 사용 : " + math1.Pi + ' ' + math1.Radius);
            Console.WriteLine("클래스 내 계산의 횟수 : " + Mathematics.NumCalcInClass);      // 정적 필드 또한 프로퍼티 사용 가능
            // -------------------------------------------------
        }
    }
}