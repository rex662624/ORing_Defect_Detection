using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace TestStop1
{
    class Program
    {
        static void Main(string[] args)
        {
            //量時間
            
            //讀圖
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //debug
            int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                fileindex++;
                Mat src = Cv2.ImRead(filename, ImreadModes.Grayscale);
                Mat vis_rgb = src.CvtColor(ColorConversionCodes.GRAY2RGB);
                //Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                //==========================algorithm===============================

                //mask the iner part noise of src
                List<Point[]> contours_final = Mask_innercicle(ref src);

                src.SaveImage("./result/test" + fileindex + ".jpg");
                //vis_rgb.SaveImage("./result/test"+ fileindex + ".jpg");
                //==================================================================
                watch.Stop();

                //印出時間
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }
        
            Console.ReadLine();
        }

        //mask the inner part of circle 
        static List<Point[]> Mask_innercicle(ref Mat img)
        {

            Mat thresh1 = img.Threshold(200, 255, ThresholdTypes.Binary);
            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // find final circle 
            List<Point[]> contours_final = new List<Point[]>();

            foreach (Point[] contour_now in contours)
            {
                //if (np.array(contours[i]).shape[0] > 1500 and cv2.contourArea(contours[i]) < 5000000):
                if (contour_now.Length > 1500 && Cv2.ContourArea(contour_now) < 5000000)
                {
                    contours_final.Add(contour_now);
                }

            }

            ///OpenCvSharp.Point[][] temp = new Point[1][];//for draw on image

            Point[] contours_approx_innercircle;

            var contour_innercircle = contours_final[1];
            
            //temp[0] = contour_now;

            Point2f center;
            float radius;
            //Cv2.DrawContours(vis_rgb, temp, -1, Scalar.Green, thickness: -1);
            contours_approx_innercircle = Cv2.ApproxPolyDP(contour_innercircle, 0.001, true);//speedup
            Cv2.MinEnclosingCircle(contours_approx_innercircle, out center, out radius);
            Cv2.Circle(img, (Point)center, (int)radius,255, thickness: -1);
            //Cv2.Circle(vis_rgb, (Point)center, (int)radius, Scalar.White, thickness: -1);

            return contours_final;
        }
        /*
        static void FindContour_and_outer_defect(Mat img, List<Point[]> contours_final)
        {
            Point[] Convex_hull = Cv2.ConvexHull(contours_final[0]);

            OpenCvSharp.Point[][] temp = new Point[1][];
            temp[0] = contour_now;
            Mat convex_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(convex_mask_img, Convex_hull, -1, 0, -1)

        }
        */
    }
}
