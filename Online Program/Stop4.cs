﻿using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;

namespace Stop4
{
    class Program
    {
        static void Main(string[] args)
        {
            //量時間

            //讀圖
            //string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            string[] filenamelist = Directory.GetFiles(@".\images", "8.jpg", SearchOption.AllDirectories);
            //debug
            int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                fileindex++;
                Mat Src = Cv2.ImRead(filename, ImreadModes.Grayscale);

                Console.WriteLine(filename);

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                Stop4_Detect(Src, fileindex, filename.Substring(9));
                watch.Stop();
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }

            Console.ReadLine();
        }
        static void Stop4_Detect(Mat Src, int fileindex, string filename)
        {
            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);


            //==================================================find real oring===============================================
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Mat thresh1 = Src.Threshold(240, 255, ThresholdTypes.Binary);
            Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // find final circle 
            List<Point[]> contours_final = new List<Point[]>();
            List<Point[]> approx_list = new List<Point[]>();

            foreach (Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) > 300000 && Cv2.ContourArea(contour_now) < 800000)
                {
                    contours_final.Add(contour_now);
                    Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.5, true);
                    approx_list.Add(approx);
                }

            }
            //==================================================outer cirle - inner circle=====================================
            
            // variable
            OpenCvSharp.Point[][] temp = new Point[1][];


            // inner contour
            Mat inner_contour_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Point[] inner_contour = Cv2.ConvexHull(approx_list[1]);
            temp[0] = inner_contour;
            Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);

            // outer contour
            Mat outer_contour_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            /*
            Point[] outer_contour = Cv2.ConvexHull(approx_list[0]);
            temp[0] = outer_contour;*/
            //=======================circle ignore=========================
            double ignore_radius = 10;
            Point2f center;
            float radius;

            Point[] Approx = Cv2.ApproxPolyDP(approx_list[0], 0.5, true);
            temp[0] = Approx;
            Cv2.MinEnclosingCircle(Approx, out center, out radius);
            Cv2.Circle(outer_contour_img, (OpenCvSharp.Point)center, (int)(radius- ignore_radius),255, thickness: -1);
            Cv2.Circle(vis_rgb, (OpenCvSharp.Point)center, (int)(radius - ignore_radius), new Scalar(255, 0, 0), thickness: 1);
            //Cv2.DrawContours(outer_contour_img, temp, -1, 255, -1);
            


            //outer contour2 in order to make mask area = 255
            Mat outer_contour_img2 = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255
            Cv2.Circle(outer_contour_img2, (OpenCvSharp.Point)center, (int)(radius - ignore_radius), 0, thickness: -1);

            //outer - inner
            Mat diff_mask = outer_contour_img - inner_contour_img;
            Mat diff_mask2 = inner_contour_img + outer_contour_img2;

            Mat image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(image, diff_mask);
            //in order to make mask area = 255
            image = image + diff_mask2;

            //image.SaveImage("./mask.jpg");

            //================================use threshold to find defect==========================================
            Point[][] contours2;
            HierarchyIndex[] hierarchly2;
            Mat thresh2 = image.Threshold(85, 255, ThresholdTypes.BinaryInv);

            Mat kernel = Mat.Ones(5, 5, MatType.CV_8UC1);//改變凹角大小
            thresh2 = thresh2.MorphologyEx(MorphTypes.Dilate, kernel);

            Cv2.FindContours(thresh2, out contours2, out hierarchly2, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            

            foreach (Point[] contour_now in contours2)
            {
                /*
                if (Cv2.ContourArea(contour_now) > 500 && 
                    Cv2.ContourArea(contour_now) < 20000 &&
                    (Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)) <10)
                */
                if (Cv2.ContourArea(contour_now) > 250 && Cv2.ContourArea(contour_now) < 20000)
                {
                    
                    Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
                    Console.WriteLine("arc length/area: " + (Cv2.ArcLength(approx, true) / Cv2.ContourArea(approx)) + " Area: " + Cv2.ContourArea(approx));
                    temp[0] = approx;
                    Cv2.Polylines(vis_rgb, temp, true, new Scalar(0, 0, 255), 1);
                }

            }
            //vis_rgb.SaveImage("./result.jpg");
            vis_rgb.SaveImage("./result/test" + filename);
        }
    }
}
