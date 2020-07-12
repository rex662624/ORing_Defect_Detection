﻿using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;
using System.Linq;
using NumSharp;

namespace Stop3
{
    class Program
    {
        static void Main(string[] args)
        {
            //量時間
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //讀圖
            //string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            string[] filenamelist = Directory.GetFiles(@".\images\", "100.jpg", SearchOption.AllDirectories);
            //debug
            int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                fileindex++;
                Mat src = Cv2.ImRead(filename, ImreadModes.Grayscale);

                Console.WriteLine(filename);
                Stop3_Detector(src, filename.Substring(9));
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            }

            Console.ReadLine();
        }

        static void Stop3_Detector(Mat Src, string filename)
        {
            //============================threshold===================
            Int64 OK_NG_Flag = 0;
            //=========================================================
            int threshold_1phase = 100;
            int threshold_2phase_1 = 35;//30
            int threshold_2phase_2 = 10;//20
            int blur_size = 3;
            int neighbor_degree = 5;

            //==========================algorithm====================
            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);

            Mat thresh1 = Src.Threshold(70, 255, ThresholdTypes.Binary);

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(4, 3));
            thresh1 = thresh1.MorphologyEx(MorphTypes.Open, kernel);

            //=========================find contours================
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // find final circle 
            List<Point[]> contours_final = new List<Point[]>();
            List<int> index = new List<int>();
            int count = 0;//replace for loop index
            foreach (Point[] contour_now in contours)
            {
                //if (np.array(contours[i]).shape[0] > 1500 and cv2.contourArea(contours[i]) < 5000000):
                if (Cv2.ContourArea(contour_now) > 300000 && Cv2.ContourArea(contour_now) < 500000)
                {
                    Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.005, true);
                    contours_final.Add(approx);
                    index.Add(count);
                }
                count++;

            }
            //==========================================find outer circle==============================================
            Point2f center;
            float radius;
            Cv2.MinEnclosingCircle(contours_final[0], out center, out radius);

            Mat mask_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Cv2.Circle(mask_img, (OpenCvSharp.Point)center, (int)(radius + 7), 255, thickness: -1);

            //outer contour2 in order to make mask area = 255
            Mat mask_img2 = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255
            Cv2.Circle(mask_img2, (OpenCvSharp.Point)center, (int)(radius + 7), 0, thickness: -1);


            Mat image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(image, mask_img);

            //in order to make mask area = 255
            image = image + mask_img2;
            //image.SaveImage("./mask.jpg");
            //============================now already find the ROI, start algorithm=====================================
            Cv2.Blur(image, image, ksize: new Size(blur_size, blur_size));
            //image.SaveImage("./mask2.jpg");

            //==========================create 0.5 degree a line===================================================
            double factor = 3.141592653589793 / 180;
            List<Point> outer_index = new List<Point>();
            List<Point> inner_index = new List<Point>();
            double degree_delta = 0.5;
            int cx = (int)center.X;
            int cy = (int)center.Y;
            int r = (int)radius;
            //Console.WriteLine(r + " "+ cx + " "+ cy);

            for (int degree = 0; degree < (360 / degree_delta); degree++)
            {
                double degree_real = degree * degree_delta;
                int now_x = (int)((r + 2) * Math.Sin(degree_real * factor)) + cx;
                int now_y = (int)((r + 2) * Math.Cos(degree_real * factor)) + cy;
                int now_inner_x = (int)((r - 30) * Math.Sin(degree_real * factor)) + cx;
                int now_inner_y = (int)((r - 30) * Math.Cos(degree_real * factor)) + cy;

                inner_index.Add(new Point(now_inner_x,now_inner_y));
                outer_index.Add(new Point(now_x, now_y));
            }
            //==========================shot from center=========================================
            for (int degree = 0; degree < (360 / degree_delta); degree++)
            {
                double a = 0;
                //var A = NumSharp.np.linspace(new Point(), 200, ref a, 200, dtype: NumpyDotNet.np.Int64);
                /*var A = NumSharp.np.linspace(new Point(0,0), new Point(200,200), ref a, 200, dtype: NumSharp.np.int64);
                if (degree == 0)
                {
                    var points_on_line_x = ((IEnumerable<double>)Enumerable.Range(inner_x[degree], x[degree]).Select(i => (double)i * 0.01)).ToArray();
                    var points_on_line_y = ((IEnumerable<double>)Enumerable.Range(inner_y[degree], y[degree]).Select(i => (double)i * 0.01)).ToArray();
                    Console.WriteLine(points_on_line_x.Length);
                    Console.WriteLine(points_on_line_y.Length);
                }*/
                
                   using(System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Chernger\\Desktop\\plot_data\\raw_data\\" + degree + ".txt", false)) {  
                foreach (var lip in new LineIterator(image, inner_index[degree], outer_index[degree]))
                {
                        
                        
                            file.WriteLine(lip.GetValue<byte>());
                        
                        //Console.WriteLine(lip.GetValue<byte>());
                }
                    }
                


            }
        }

    }
}
