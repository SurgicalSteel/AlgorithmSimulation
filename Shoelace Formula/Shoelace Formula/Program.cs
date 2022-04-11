using System;
using System.Collections.Generic;

namespace Shoelace_Formula
{
    class Program
    {
        struct Point
        {
            public double x { get; set; }
            public double y { get; set; }
            public Point(double px, double py) 
            {
                this.x = px;
                this.y = py;
            }
            public Point(int px, int py) 
            {
                this.x = (double) px;
                this.y = (double) py;
            }
        }
        static double CalculateAreaWithShoelaceFormula(List<Point> points) 
        {
            double area, sideA = 0, sideB = 0;
            List<double> lx = new List<double>();
            List<double> ly = new List<double>();

            for(int i = 0; i < points.Count; i++)
            {
                lx.Add(points[i].x);
                ly.Add(points[i].y);
            }
            
            lx.Add(lx[0]);
            ly.Add(ly[0]);

            for(int i=0;i< points.Count; i++)
            {
                sideA += (lx[i] * ly[i + 1]);
                sideB += (ly[i] * lx[i + 1]);
            }

            area = Math.Abs(0.5 * (sideA - sideB));

            return area;
        }

        static void Main(string[] args)
        {
            List<Point> pointSetA = new List<Point>() { new Point(20, 40), new Point(100, 40), new Point(100, 0), new Point(20, 0) };
            double areaA = CalculateAreaWithShoelaceFormula(pointSetA);
            Console.WriteLine("{0:F6}", areaA);

            List<Point> pointSetB = new List<Point>() { new Point(-12, -10), new Point(8, -5), new Point(0, 14), new Point(12, 10) };
            double areaB = CalculateAreaWithShoelaceFormula(pointSetB);
            Console.WriteLine("{0:F6}", areaB);
        }
    }
}
