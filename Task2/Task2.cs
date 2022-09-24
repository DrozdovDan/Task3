using System.Text;
using OneVariableFunction = System.Func<double, double>;
using FunctionName = System.String;

namespace Task2
{
    public class Task2
    {

/*
 * В этом задании необходимо написать программу, способную табулировать сразу несколько
 * функций одной вещественной переменной на одном заданном отрезке.
 */


// Сформируйте набор как минимум из десяти вещественных функций одной переменной
internal static Dictionary<FunctionName, OneVariableFunction> AvailableFunctions =
            new Dictionary<FunctionName, OneVariableFunction>
            {
                { "square", x => x * x },
                { "sin", Math.Sin },
                { "cos", Math.Cos },
                { "tan", Math.Tan },
                { "square root", Math.Sqrt},
                { "log2", Math.Log2},
                { "log10", Math.Log10},
                { "cube", x => x * x * x},
                { "reciprocal", x => 1 / x},
                { "opposite", x => -x}
            };

// Тип данных для представления входных данных
internal record InputData(double FromX, double ToX, int NumberOfPoints, List<string> FunctionNames);

// Чтение входных данных из параметров командной строки
        private static InputData PrepareData(string[] args)
        {
            double fromX = Double.Parse(args[0]);
            double toX = Double.Parse(args[1]);
            int numberOfPoints = int.Parse(args[2]);
            List<String> functionNames = new List<String>();
            for (int i = 3; i < args.Length; i++)
            {
                functionNames.Add(args[i]);
            }

            return new InputData(fromX, toX, numberOfPoints, functionNames);
        }

// Тип данных для представления таблицы значений функций
// с заголовками столбцов и строками (первый столбец --- значение x,
// остальные столбцы --- значения функций). Одно из полей --- количество знаков
// после десятичной точки.
internal record FunctionTable
{
    public List<List<double>> Rows { get; init; } = new List<List<double>>();

            // Код, возвращающий строковое представление таблицы (с использованием StringBuilder)
            // Столбец x выравнивается по левому краю, все остальные столбцы по правому.
            // Для форматирования можно использовать функцию String.Format.
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int j = 0; j < Rows.Count; j++)
                {
                    for (int i = 0; i < Rows[j].Count; i++)
                    {
                        if (i == 0)
                        {
                            stringBuilder.Append($"{Rows[j][i],-8:0.000} ");
                        }
                        else
                        {
                            stringBuilder.Append($"{Rows[j][i],8:0.000} ");
                        }
                    }

                    if(j != Rows.Count - 1) stringBuilder.Append(Environment.NewLine);
                }

                return stringBuilder.ToString();
            } 
}

/*
 * Возвращает таблицу значений заданных функций на заданном отрезке [fromX, toX]
 * с заданным количеством точек.
 */
        internal static FunctionTable Tabulate(InputData input)
        {
            FunctionTable functionTable = new FunctionTable();
            double x = input.FromX;
            double step = (input.ToX - input.FromX) / input.NumberOfPoints;
            for (int i = 0; i <= input.NumberOfPoints; i++)
            {
                List<double> row = new List<double> { x };
                foreach (var functionName in input.FunctionNames)
                {
                    row.Add(AvailableFunctions[functionName](x));
                }
                functionTable.Rows.Add(row);
                x += step;
            }

            return functionTable;
        }
        
        
        public static void Main(string[] args)
        {
            // Входные данные принимаются в аргументах командной строки
            // fromX fromY numberOfPoints function1 function2 function3 ...

   //         var input = PrepareData(args);
   var funNames = new List<string> { "square", "sin" };
   var nOfPoints = 10;
   var res = Tabulate(new InputData(0.0, 10.0, nOfPoints, funNames));
   Console.WriteLine(res.ToString());
            // Собственно табулирование и печать результата (что надо поменять в этой строке?):
     //       Console.WriteLine(Tabulate(input).ToString());
        }

    }
}