using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;
using System.Linq;

namespace Stop2_New
{
    class Program
    {
        public static int stop2_inner_circle_radius = 0;
        public static int stop2_out_defect_size_min = 200;
        public static int stop2_out_defect_size_max = 20000;
        public static int stop2_inner_defect_size_min = 20;
        public static int stop2_arclength_area_ratio = 100;

        static void Main(string[] args)
        {

            //讀圖
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //string[] filenamelist = Directory.GetFiles(@".\images\", "1.jpg", SearchOption.AllDirectories);
            //debug
            int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                fileindex++;
                Mat src = Cv2.ImRead(filename, ImreadModes.Grayscale);

                Console.WriteLine(filename);

                Stop2_Detector(src, filename.Substring(9));

                //================================
            }

            Console.ReadLine();
        }
        static void Stop2_Detector(Mat Src, string filename)
        {

            //========================================================================================

            Int64 OK_NG_Flag = 0;
            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
            //Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //==========================algorithm===============================

            //========================================================================================
            List<OpenCvSharp.Point[]> contours_final = contour_inner_outer(Src);
            Mat Src_saveImage = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(Src_saveImage);
            //Mat _enhanced_image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            //Cv2.GaussianBlur(Src, image, new OpenCvSharp.Size(0, 0), 10);
            //Cv2.AddWeighted(Src, 2, image, -1, 0, image);
            Cv2.GaussianBlur(Src, Src, new OpenCvSharp.Size(3, 3), 0, 0);
            Cv2.AdaptiveThreshold(Src, Src, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 45, 105 / 10);

            Mat Src_255 = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));
            Cv2.Subtract(Src_255, Src, Src);


            //denoise to eliminate noise
            Denoise(ref Src, filename, contours_final);
            Find_Contour_and_Extract_Defect(Src, Src, filename);

            Src.SaveImage("./enhance/" + filename);


            //==================================================================

            watch.Stop();
            //印出時間
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


        }

        static List<OpenCvSharp.Point[]> contour_inner_outer (Mat Src)
        {
            Mat img_copy = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(img_copy);
            Cv2.GaussianBlur(img_copy, img_copy, new OpenCvSharp.Size(15, 15), 0, 0);

            //img = img.Threshold(250, 255, ThresholdTypes.Binary);
            Mat thresh1 = img_copy.Threshold(250, 255, ThresholdTypes.Binary);
            thresh1.SaveImage("./thresold.jpg");
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // find final circle 
            List<OpenCvSharp.Point[]> contours_final = new List<OpenCvSharp.Point[]>();

            foreach (OpenCvSharp.Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) > 1000000 && Cv2.ContourArea(contour_now) < 2500000)
                {
                    contours_final.Add(contour_now);
                }

            }
            return contours_final;
        }

        static void Denoise(ref Mat Src, string filename, List<OpenCvSharp.Point[]> contours_final)
        {


            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(Src, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            OpenCvSharp.Point[][] temp = new Point[1][];

            foreach (OpenCvSharp.Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) < 100)
                {
                    //Console.WriteLine("Arc Length: " + (Cv2.ArcLength(contour_now, true) + " Area: " + Cv2.ContourArea(contour_now))+" Length/Area:" +(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)));
                    OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
                    temp[0] = approx;
                    Cv2.DrawContours(Src, temp, -1, 0, -1);

                }

            }

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(13, 7));
            Src = Src.MorphologyEx(MorphTypes.Close, kernel);

            //=========================draw outer and inner contour
            
            temp[0] = contours_final[0];
            Cv2.DrawContours(Src, temp, -1, 0, 40);
            temp[0] = contours_final[1];
            Cv2.DrawContours(Src, temp, -1, 0, 40);

            
            Src.SaveImage("./enhance/" + filename);
        }

        static void Find_Contour_and_Extract_Defect(Mat Src, Mat Dst, string filename)
        {

            Mat defect_image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);

            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(Src, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            OpenCvSharp.Point[][] temp = new Point[1][];

            // Extract defect candidate
            foreach (OpenCvSharp.Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) > 150)
                {
                    //Console.WriteLine("Arc Length: " + (Cv2.ArcLength(contour_now, true) + " Area: " + Cv2.ContourArea(contour_now))+" Length/Area:" +(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)));
                    OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
                    temp[0] = approx;
                    //Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(255, 0, 0), 3);
                    Cv2.DrawContours(defect_image, temp, -1, 255, -1);
                }

            }
            /*
            foreach (OpenCvSharp.Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) > 150)
                {
                    //Console.WriteLine("Arc Length: " + (Cv2.ArcLength(contour_now, true) + " Area: " + Cv2.ContourArea(contour_now))+" Length/Area:" +(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)));
                    OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
                    temp[0] = approx;
                    //Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(255, 0, 0), 3);
                    Cv2.DrawContours(defect_image, temp, -1, 255, -1);
                }

            }*/
            defect_image.SaveImage("./contour/" + filename);

            



        }
        
        static void Distance_between_contour_and_center(Mat Src, Mat Dst, string filename)
        {

            

        }


    }
}
