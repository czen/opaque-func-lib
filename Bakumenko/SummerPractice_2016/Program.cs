﻿using System;
using OpaqueFunctions;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting; //консольное приложение не имеет ссылки сюда само по себе. Нужно добавить её через меню "Добавить->Ссылка->Платформа".
//System.Windows.Forms + System.Windows.Forms.DataVisualization + System.Drawing
using Microsoft.VisualBasic.FileIO; //то же самое, нужно для обработки .csv
namespace SummerPractice_2016
{
    class Program
    {
        static void Main(string[] args)
        {
            string offset = "..\\..\\..\\"; // чтобы выходные файлы оказались точно в папке с фамилией  
            string source_csv_folder_name = offset + "csv"; // пусть папка для файлов с отчётами .csv называется так.
            makeErrorPlots(source_csv_folder_name);

            MakeResultsSummaryFile("L00_30_1_sin_arccos", CL00_30_1_sin_arccos.L00_30_1_sin_arccos, MyBenchmark.L00_30_1_sin_arccos_benchmark, -1 , 1);
            MakeResultsSummaryFile("L00_31_1_cos_arcsin", CL00_31_1_cos_arcsin.L00_31_1_cos_arcsin, MyBenchmark.L00_31_1_cos_arcsin_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_32_1_sin_arctg_cos_arcctg", CL00_32_1_sin_arctg_cos_arcctg.L00_32_1_sin_arctg_cos_arcctg, MyBenchmark.L00_32_1_sin_arctg_cos_arcctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_33_1_sin_arctg", CL00_33_1_sin_arctg.L00_33_1_sin_arctg, MyBenchmark.L00_33_1_sin_arctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_34_1_cos_arcctg", CL00_34_1_cos_arcctg.L00_34_1_cos_arcctg, MyBenchmark.L00_34_1_cos_arcctg_benchmark,-1, 1);
            MakeResultsSummaryFile("L00_35_1_cos_arctg", CL00_35_1_cos_arctg.L00_35_1_cos_arctg, MyBenchmark.L00_35_1_cos_arctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_36_1_sin_arcctg", CL00_36_1_sin_arcctg.L00_36_1_sin_arcctg, MyBenchmark.L00_36_1_sin_arcctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_37_1_tg_arcsin", CL00_37_1_tg_arcsin.L00_37_1_tg_arcsin, MyBenchmark.L00_37_1_tg_arcsin_benchmark, -1 , 1);
            MakeResultsSummaryFile("L00_38_1_ctg_arccos", CL00_38_1_ctg_arccos.L00_38_1_ctg_arccos, MyBenchmark.L00_38_1_ctg_arccos_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_39_1_tg_arccos", CL00_39_1_tg_arccos.L00_39_1_tg_arccos, MyBenchmark.L00_39_1_tg_arccos_benchmark, -1, 0);
            MakeResultsSummaryFile("L00_40_1_ctg_arcsin", CL00_40_1_ctg_arcsin.L00_40_1_ctg_arcsin, MyBenchmark.L00_40_1_ctg_arcsin_benchmark, -1, 0);
            MakeResultsSummaryFile("L00_41_1_sin_arcctg_cos_arctg", CL00_41_1_sin_arcctg_cos_arctg.L00_41_1_sin_arcctg_cos_arctg, MyBenchmark.L00_41_1_sin_arcctg_cps_arctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_42_1_tg_arctg", CL00_42_1_tg_arctg.L00_42_1_tg_arctg, MyBenchmark.L00_42_1_tg_arctg_benchmark, -Math.PI / 2, Math.PI / 2);
            MakeResultsSummaryFile("L00_43_1_tg_arcsin_ctg_arccos", CL00_43_1_tg_arcsin_ctg_arccos.L00_43_1_tg_arcsin_ctg_arccos, MyBenchmark.L00_43_1_tg_arcsin_ctg_arccos_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_44_1_tg_arccos_ctg_arcsin", CL00_44_1_tg_arccos_ctg_arcsin.L00_44_1_tg_arccos_ctg_arcsin, MyBenchmark.L00_44_1_tg_arccos_ctg_arcsin_benchmark, -1, 0);
            MakeResultsSummaryFile("L00_45_1_ctg_arcctg", CL00_45_1_ctg_arcctg.L00_45_1_ctg_arcctg, MyBenchmark.L00_45_1_ctg_arcctg_benchmark, -10000, 10000);
            MakeResultsSummaryFile("L00_46_1_arcsin_sin", CL00_46_1_arcsin_sin.L00_46_1_arcsin_sin, MyBenchmark.L00_46_1_arcsin_sin_benchmark, -Math.PI / 2, Math.PI / 2);
            MakeResultsSummaryFile("L00_47_1_arccos_cos", CL00_47_1_arccos_cos.L00_47_1_arccos_cos, MyBenchmark.L00_47_1_arccos_cos_benchmark, 0, Math.PI / 2);
            MakeResultsSummaryFile("L00_48_1_arctg_tg", CL00_48_1_arctg_tg.L00_48_1_arctg_tg, MyBenchmark.L00_48_1_arctg_tg_benchmark, -Math.PI / 2, Math.PI / 2);
            MakeResultsSummaryFile("L00_49_1_arcctg_ctg", CL00_49_1_arcctg_ctg.L00_49_1_arcctg_ctg, MyBenchmark.L00_49_1_arcctg_ctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_50_1_arctg_arcctg", CL00_50_1_arctg_arcctg.L00_50_1_arctg_arcctg, MyBenchmark.L00_50_1_arctg_arcctg_benchmark, -10000, 10000);
            MakeResultsSummaryFile("L00_51_1_arctg_arcsin", CL00_51_1_arctg_arcsin.L00_51_1_arctg_arcsin, MyBenchmark.L00_51_1_arctg_arcsin_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_52_1_arcsin_arccos", CL00_52_1_arcsin_arccos.L00_52_1_arcsin_arccos, MyBenchmark.L00_52_1_arcsin_arccos_benchmark, -1, 0);
            MakeResultsSummaryFile("L00_53_1_arcctg_arcsin", CL00_53_1_arcctg_arcsin.L00_53_1_arcctg_arcsin, MyBenchmark.L00_53_1_arcctg_arcsin_benchmark, 0, 1);
            MakeResultsSummaryFile("L00_54_1_arccos_arcctg", CL00_54_1_arccos_arcctg.L00_54_1_arccos_arcctg, MyBenchmark.L00_54_1_arccos_arcctg_benchmark, 0, 1);
            MakeResultsSummaryFile("L00_55_1_arccos_arcctg", CL00_55_1_arccos_arcctg.L00_55_1_arccos_arcctg, MyBenchmark.L00_55_1_arccos_arcctg_benchmark, -1, 1);
            MakeResultsSummaryFile("L00_56_1_arccos_arctg", CL00_56_1_arccos_arctg.L00_56_1_arccos_arctg, MyBenchmark.L00_56_1_arccos_arctg_benchmark, 0, 1);
            MakeResultsSummaryFile("L00_57_1_arctg_arcsin", CL00_57_1_arctg_arcsin.L00_57_1_arctg_arcsin, MyBenchmark.L00_57_1_arctg_arcsin_benchmark, -10000, 10000);
            Console.WriteLine("CSV-files generated successfully");
        }

        static void MakeResultsSummaryFile(string funcname, Func<double, double> f, Func<double, double> benchmark, double left, double right)
        {
            //генерирует файл .csv нужного формата
            string dest_folder = ("..\\..\\..\\csv\\");
            //здесь добавлено только количество итераций в файл. У вас другие параметры? По аналогии.
            string dest = dest_folder + funcname + ".csv";
            System.IO.StreamWriter dest_file_writer =
               new System.IO.StreamWriter(dest);
            dest_file_writer.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");
            //мы хотим равномерно покрыть область определения (или хорошей сходимости) функции number_of_points точками. Подойдите к выбору области аккуратно.
            double left_border_of_range = left;
            double right_border_of_range = right;
            double range_length = Math.Abs(right_border_of_range - left_border_of_range);
            uint number_of_points = 200;
            double dx = range_length / number_of_points;
            for (int i = 1; i < number_of_points; i++)
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                swatch.Start();
                double x = left_border_of_range + i * dx;
                double F = f(x);
                double absoluteError = Math.Abs((F - benchmark(x)));
                double relativeError = Math.Abs((F - benchmark(x)) / benchmark(x));
                swatch.Stop();
                long t = swatch.ElapsedMilliseconds;
                dest_file_writer.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + t.ToString());
            }
            dest_file_writer.Close();
        }


        static void makeErrorPlot(string source_csv_file_name)
        //немного магии от разработчиков .Net, по которой нет ни одного приличного гайда в сети. Нужно осознать.
        {
            TextFieldParser parser = new TextFieldParser(source_csv_file_name); //обрабатываем .csv файл
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            var errorChart = new Chart();
            var relativeErrorChart = new Chart();

            errorChart.Series.Add("absolute_error");
            relativeErrorChart.Series.Add("relative_error");

            string headers = parser.ReadLine();
            var x_coordinates = new List<double>();
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                errorChart.Series["absolute_error"].Points.AddXY(Convert.ToDouble(fields[0]), Convert.ToDouble(fields[1])); //добавляем точки на график
                relativeErrorChart.Series["relative_error"].Points.AddXY(Convert.ToDouble(fields[0]), Convert.ToDouble(fields[2]));
            }
            errorChart.Series["absolute_error"].ChartType = SeriesChartType.FastLine;
            errorChart.Series["absolute_error"].Color = System.Drawing.Color.Red;
            errorChart.Height = 1000;
            errorChart.Width = 1000;
            errorChart.ChartAreas.Add("NewChartArea");
            errorChart.Series["absolute_error"].ChartArea = "NewChartArea";

            String path = source_csv_file_name.Replace("csv\\", "$").Split('$')[0];
            String absoluteErrorFolder = path + "absolute_error_plots";
            if (!System.IO.Directory.Exists(absoluteErrorFolder))
                System.IO.Directory.CreateDirectory(absoluteErrorFolder);
            errorChart.SaveImage(source_csv_file_name.Replace("csv\\", "absolute_error_plots\\").Replace(".csv", ".png"), ChartImageFormat.Png);

            relativeErrorChart.Series["relative_error"].ChartType = SeriesChartType.FastLine;
            relativeErrorChart.Series["relative_error"].Color = System.Drawing.Color.Green;
            relativeErrorChart.Height = 1000;
            relativeErrorChart.Width = 1000;
            relativeErrorChart.ChartAreas.Add("NewRelativeChartArea");
            relativeErrorChart.Series["relative_error"].ChartArea = "NewRelativeChartArea";

            String relativeErrorFolder = path + "relative_error_plots";
            if (!System.IO.Directory.Exists(relativeErrorFolder))
                System.IO.Directory.CreateDirectory(relativeErrorFolder);
            relativeErrorChart.SaveImage(source_csv_file_name.Replace("csv\\", "relative_error_plots\\").Replace(".csv", ".png"), ChartImageFormat.Png);
        }

        static void makeErrorPlots(string source_csv_folder_name) //в директории не должно быть лишнего, считаем, что там только csv, притом нужного формата
        {
            foreach (String source_csv_file_name in System.IO.Directory.GetFiles(source_csv_folder_name))
                makeErrorPlot(source_csv_file_name);
        }
    }

    public class MyBenchmark
    {

        public static double L00_30_1_sin_arccos_benchmark(double x)
        {
            double result;
            result = Math.Sin(Math.Acos(x)) - Math.Sqrt(1 - x * x);
            return result;
        }
        public static double L00_31_1_cos_arcsin_benchmark(double x)
        {
            double result;
            result = Math.Cos(Math.Asin(x)) - Math.Sqrt(1 - x * x);
            return result;
        }
        public static double L00_32_1_sin_arctg_cos_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Sin(Math.Atan(x)) - Math.Cos(Math.PI / 2 - Math.Tan(x));
            return result;
        }
        public static double L00_33_1_sin_arctg_benchmark(double x)
        {
            double result;
            result = Math.Sin(Math.Atan(x)) - x / Math.Sqrt(1 + x * x);
            return result;
        }
        public static double L00_34_1_cos_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Cos(Math.PI / 2 - Math.Atan(x)) - x / Math.Sqrt(1 + x * x);
            return result;
        }
        public static double L00_35_1_cos_arctg_benchmark(double x)
        {
            double result;
            result = Math.Cos(Math.Atan(x)) - 1 / Math.Sqrt(1 + x * x);
            return result;
        }
        public static double L00_36_1_sin_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Sin(Math.PI / 2 - Math.Atan(x)) - 1 / Math.Sqrt(1 + x * x);
            return result;
        }
        public static double L00_37_1_tg_arcsin_benchmark(double x)
        {
            double result;
            result = Math.Tan(Math.Asin(x)) - x / Math.Sqrt(1 - x * x);
            return result;
        }
        public static double L00_38_1_ctg_arccos_benchmark(double x)
        {
            double result;
            result = Math.PI / 2 - Math.Tan(Math.Acos(x)) - x / Math.Sqrt(1 - x * x);
            return result;
        }
        public static double L00_39_1_tg_arccos_benchmark(double x)
        {
            double result;
            result = Math.Tan(Math.Acos(x)) - Math.Sqrt(1 - x * x) / x;
            return result;
        }
        public static double L00_40_1_ctg_arcsin_benchmark(double x)
        {
            double result;
            result = Math.PI / 2 - Math.Tan(Math.Asin(x)) - Math.Sqrt(1 - x * x) / x; ;
            return result;
        }
        public static double L00_41_1_sin_arcctg_cps_arctg_benchmark(double x)
        {
            double result;
            result = Math.Asin(Math.PI / 2 - Math.Atan(x)) - Math.Acos(Math.Atan(x));
            return result;
        }
        public static double L00_42_1_tg_arctg_benchmark(double x)
        {
            double result;
            result = Math.Tan(Math.Atan(x)) - x;
            return result;
        }
        public static double L00_43_1_tg_arcsin_ctg_arccos_benchmark(double x)
        {
            double result;
            result = Math.Tan(Math.Asin(x)) - (1 / Math.Tan(Math.Acos(x)));
            return result;
        }
        public static double L00_44_1_tg_arccos_ctg_arcsin_benchmark(double x)
        {
            double result;
            result = Math.Tan(Math.Acos(x)) - (1 / Math.Tan(Math.Asin(x)));
            return result;
        }
        public static double L00_45_1_ctg_arcctg_benchmark(double x)
        {
            double result;
            result = 1 / Math.Tan(Math.PI / 2 - Math.Atan(x)) - x;
            return result;
        }
        public static double L00_46_1_arcsin_sin_benchmark(double x)
        {
            double result;
            result = Math.Asin(Math.Sin(x)) - x;
            return result;
        }
        public static double L00_47_1_arccos_cos_benchmark(double x)
        {
            double result;
            result = Math.Acos(Math.Cos(x)) - x;
            return result;
        }
        public static double L00_48_1_arctg_tg_benchmark(double x)
        {
            double result;
            result = Math.Atan(Math.Tan(x)) - x;
            return result;
        }
        public static double L00_49_1_arcctg_ctg_benchmark(double x)
        {
            double result;
            result = Math.PI / 2 - Math.Atan(1 / Math.Tan(x)) - x;
            return result;
        }
        public static double L00_50_1_arctg_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Atan(x) + (Math.PI / 2 - Math.Atan(x)) - Math.PI / 2;
            return result;
        }
        public static double L00_51_1_arctg_arcsin_benchmark(double x)
        {
            double result;
            result = Math.Atan(x / Math.Sqrt(1 - x * x)) - Math.Asin(x);
            return result;
        }
        public static double L00_52_1_arcsin_arccos_benchmark(double x)
        {
            double result;
            result = Math.Asin(x) - Math.Acos(Math.Sqrt(1 - x * x));
            return result;
        }
        public static double L00_53_1_arcctg_arcsin_benchmark(double x)
        {
            double result;
            result = Math. PI / 2 - Math.Atan(Math.Sqrt(1 - x * x) / x) - Math.Asin(x);
            return result;
        }
        
        public static double L00_54_1_arccos_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Acos(Math.Sqrt(1 - x * x)) - (Math.PI / 2 - Math.Atan(Math.Sqrt(1 - x * x) / x));
            return result;
        }
        public static double L00_55_1_arccos_arcctg_benchmark(double x)
        {
            double result;
            result = Math.Acos(x) - (Math.PI / 2 - Math.Atan(x / Math.Sqrt(1 - x * x)));
            return result;
        }
        public static double L00_56_1_arccos_arctg_benchmark(double x)
        {
            double result;
            result = Math.Acos(x) - Math.Atan(Math.Sqrt(1 - x * x) / x);
            return result;
        }
        public static double L00_57_1_arctg_arcsin_benchmark(double x)
        {
            double result;
            result = Math.Atan(x) - Math.Asin(x / Math.Sqrt(1 + x * x));
            return result;
        }
    }
}