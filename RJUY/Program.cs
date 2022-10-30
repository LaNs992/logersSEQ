using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace RJUY
{
    internal class Program
    {
        static void Main(string[] args)
        {

            double a = 0;
            var prov = false;
            var curs = 100;
            var rub1 = 0d;
            double proc = 0.37, rubproc = 0;
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .Enrich.WithProperty("Курс доллара:", curs)
                        .WriteTo.Seq("http://localhost:5341/", apiKey: "pKs1KLYQ3fl7FdfSss50")
                        .CreateLogger();
            while (!prov || a < 1)
            {
                Console.Write("Напишите сумму перевода из долларов в рубли :");
                var dol = Console.ReadLine();
                prov = double.TryParse(dol, out a);
                if (!prov || a < 1)
                {
                    Log.Error("Введено некорректное значение");
                }
            }
            Log.Information($"Введено верное значение: {a}");
            rub1 = a * curs;
            if (a <= 500)
            {
                Console.WriteLine("Сумма перевода меньше 500$ ");
                Console.WriteLine("Комиссия 8 р.");
                Console.WriteLine("Итог перевода:" + (rub1 - 8));
            }
            else
            {
                rubproc = rub1 * (proc / 100);
                Console.WriteLine("Сумма перевода больше 500$ ");
                Console.WriteLine($"Процент комиссии : {proc} % =  {rubproc}");
                Console.Write("Итог при переводе в рубялх :");
                rub1 = rub1 - rubproc;
                Console.WriteLine(rub1);
            }
            Log.Information($"Перевод в $ прошел успешно, выдано:{rub1}");
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}