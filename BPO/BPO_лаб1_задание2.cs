using System;

// интерфейс для сравнения дробей
interface IFractionOperations
{
    bool IsGreater(FractionBase other);
    bool IsEqual(FractionBase other);
}

// абстрактный класс
abstract class FractionBase
{
    protected long integerPart;          // целая часть (со знаком)
    protected ushort fractionalPart;     // дробная часть (без знака)

    // конструктор
    public FractionBase(long integerPart, ushort fractionalPart)
    {
        this.integerPart = integerPart;
        this.fractionalPart = fractionalPart;
    }

    // абстрактные методы
    public abstract FractionBase Add(FractionBase other);
    public abstract FractionBase Subtract(FractionBase other);
    public abstract FractionBase Multiply(FractionBase other);
    public abstract void Show();
}

// конкретный класс дроби
class Fraction : FractionBase, IFractionOperations
{
    // конструктор
    public Fraction(long integerPart, ushort fractionalPart) : base(integerPart, fractionalPart) {}

    // деструктор
    ~Fraction()
    {
        Console.WriteLine("Объект Fraction удален");
    }

    // Перевод объекта в число double
    private double ToDouble()
    {
        // Если число отрицательное
        if (integerPart < 0)
        {
            return integerPart - fractionalPart / 100.0;
        }

        return integerPart + fractionalPart / 100.0;
    }

    // Создание объекта Fraction из double
    private static Fraction FromDouble(double value)
    {
        // Получаем целую часть
        long integer = (long)value;

        // Получаем дробную часть
        double frac = Math.Abs(value - integer);

        // Переводим в ushort
        ushort fraction = (ushort)Math.Round(frac * 100);

        // Нормализация
        if (fraction >= 100)
        {
            integer++;

            fraction = 0;
        }

        return new Fraction(integer, fraction);
    }

    // Сложение
    public override FractionBase Add(FractionBase other)
    {
        Fraction second = (Fraction)other;      // приведение other базового класса к классу Fraction
        double result = this.ToDouble() + second.ToDouble();
        return FromDouble(result);
    }

    // Вычитание
    public override FractionBase Subtract(FractionBase other)
    {
        Fraction second = (Fraction)other;
        double result = this.ToDouble() - second.ToDouble();
        return FromDouble(result);
    }

    // Умножение
    public override FractionBase Multiply(FractionBase other)
    {
        Fraction second = (Fraction)other;
        double result = this.ToDouble() * second.ToDouble();
        return FromDouble(result);
    }

    // Сравнение >
    public bool IsGreater(FractionBase other)
    {
        Fraction second = (Fraction)other;

        return this.ToDouble() > second.ToDouble();
    }

    // Сравнение ==
    public bool IsEqual(FractionBase other)
    {
        Fraction second = (Fraction)other;

        return this.ToDouble() == second.ToDouble();
    }

    // вывод дроби
    public override void Show()
    {
        Console.WriteLine($"{integerPart},{fractionalPart:D2}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите первую дробь");
            Console.Write("Целая часть: ");
            long int1 = Convert.ToInt64(Console.ReadLine());
            Console.Write("Дробная часть: ");
            ushort frac1 = Convert.ToUInt16(Console.ReadLine());
            Fraction f1 = new Fraction(int1, frac1);
            Console.WriteLine();

            Console.WriteLine("Введите вторую дробь");
            Console.Write("Целая часть: ");
            long int2 = Convert.ToInt64(Console.ReadLine());
            Console.Write("Дробная часть: ");
            ushort frac2 = Convert.ToUInt16(Console.ReadLine());
            Fraction f2 = new Fraction(int2, frac2);

            Console.WriteLine("\nПервая дробь:");
            f1.Show();
            Console.WriteLine("Вторая дробь:");
            f2.Show();

            // сложение
            FractionBase sum = f1.Add(f2);
            Console.WriteLine("\nСумма:");
            sum.Show();
            // вычитание
            FractionBase difference = f1.Subtract(f2);
            Console.WriteLine("Разность:");
            difference.Show();
            // умножение
            FractionBase multiplication = f1.Multiply(f2);
            Console.WriteLine("Произведение:");
            multiplication.Show();
            // сравнение
            if (f1.IsGreater(f2))
                Console.WriteLine("Первая дробь больше второй");
            else if (f1.IsEqual(f2))
                Console.WriteLine("Дроби равны");
            else
                Console.WriteLine("Вторая дробь больше первой");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: неправильный формат ввода");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: число слишком большое");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        Console.ReadKey();
    }
}
