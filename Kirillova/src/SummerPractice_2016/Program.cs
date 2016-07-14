﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpaqueFunctions;
using System.Windows.Forms.DataVisualization.Charting; //консольное приложение не имеет ссылки сюда само по себе. Нужно добавить её через меню "Добавить->Ссылка->Платформа".
//System.Windows.Forms + System.Windows.Forms.DataVisualization + System.Drawing
using Microsoft.VisualBasic.FileIO; //то же самое, нужно для обработки .csv


namespace SummerPractice_2016
{
    class Program
    {
        static void Main(string[] args)
        {

            /// Создание CSV-файлов
            int N = 100;
            double absoluteError; // Абсолютная погрешность
            double relativeError; // Относительная погрешность
            double result;
            double x;
            double t = 1.0 / 100.0;
            string dest_folder = ("..\\..\\..\\..\\csv\\");
            System.IO.StreamWriter file;
            for (int j = 0; j < 3; j++)
            {
                ///1
                file = new System.IO.StreamWriter(dest_folder + "Math_1_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");
                result = 0;
                x = -1;

                for (int i = 0; i < 199; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_1_2_ln.Math_1_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log((1 + x) / (1 - x)));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///2
                file = new System.IO.StreamWriter(dest_folder + "Math_2_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");
                result = 0;
                x = -1;


                for (int i = 0; i < 999; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_2_2_ln.Math_2_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///3
                file = new System.IO.StreamWriter(dest_folder + "Math_3_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = -1;


                for (int i = 0; i < 199; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_3_2_ln.Math_3_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 / Math.Sqrt(1 - x * x)));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///4
                file = new System.IO.StreamWriter(dest_folder + "Math_4_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = -1;


                for (int i = 0; i < 999; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_4_2_ln.Math_4_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///5
                file = new System.IO.StreamWriter(dest_folder + "Math_6_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");
                result = 0;
                x = -1;


                for (int i = 0; i < 999; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_6_2_ln.Math_6_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///6
                file = new System.IO.StreamWriter(dest_folder + "Math_7_3_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                double a = 0.8;
                x = -a;
                for (int i = 0; i < 999; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_7_3_ln.Math_7_3_ln(a, x, N);
                    absoluteError = Math.Abs(result - Math.Log(a + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///7
                file = new System.IO.StreamWriter(dest_folder + "Math_8_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = -1;


                for (int i = 0; i < 199; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_8_2_ln.Math_8_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///8
                file =
                new System.IO.StreamWriter(dest_folder + "Math_9_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = -1;


                for (int i = 0; i < 199; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_9_2_ln.Math_9_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(1 + x));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///9
                file = new System.IO.StreamWriter(dest_folder + "Math_10_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = -1;

                for (int i = 0; i < 199; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_10_2_ln.Math_10_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log((1 + x) / (1 - x)));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                ///10
                file = new System.IO.StreamWriter(dest_folder + "Math_11_2_ln_N" + N.ToString() + ".csv");
                file.WriteLine("x" + ';' + "absoluteError" + ';' + "relativeError" + ';' + "computation time (milliseconds)");

                result = 0;
                x = 1;

                for (int i = 0; i < 999; i++)
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // измеритель времени, для каждой точки
                    swatch.Start();
                    x += t;
                    result = СMath_11_2_ln.Math_11_2_ln(x, N);
                    absoluteError = Math.Abs(result - Math.Log(x / (x - 1)));
                    relativeError = Math.Abs(absoluteError / result);
                    swatch.Stop();
                    long time = swatch.ElapsedMilliseconds;
                    file.WriteLine(x.ToString() + ';' + absoluteError.ToString() + ';' + relativeError.ToString() + ';' + time.ToString());
                }
                file.Close();

                if (j == 0) N = 500;
                else N = 1000;
            }

            string offset = "..\\..\\..\\..\\";  
            string source_csv_folder_name = offset + "csv"; 
            makeErrorPlots(source_csv_folder_name);


            Console.ReadKey();
        }

        ///  Создание графиков

        static void makeErrorPlot(string source_csv_file_name)
        {
            TextFieldParser parser = new TextFieldParser(source_csv_file_name);
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

        static void makeErrorPlots(string source_csv_folder_name)
        {
            foreach (String source_csv_file_name in System.IO.Directory.GetFiles(source_csv_folder_name))
                makeErrorPlot(source_csv_file_name);
        }
    }

}
