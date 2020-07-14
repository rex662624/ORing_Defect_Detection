using System;
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
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //string[] filenamelist = Directory.GetFiles(@".\images\", "33.jpg", SearchOption.AllDirectories);
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
            int threshold_1phase = 120;
            int threshold_2phase_1 = 45;//30
            int threshold_2phase_2 = 30;//20
            int blur_size = 3;
            int neighbor_degree = 15;

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

                inner_index.Add(new Point(now_inner_x, now_inner_y));
                outer_index.Add(new Point(now_x, now_y));
            }
            //==========================shot from center=========================================
            List<int> all_valley_list = new List<int>();
            List<int> all_peak_list = new List<int>();
            List<int> all_diff_list = new List<int>();
            List<int> Candidate_1_phase_index = new List<int>();
            List<byte> value = new List<byte>();
            for (int degree = 0; degree < (360 / degree_delta); degree++)
            {
                double a = 0;

                LineIterator Line = new LineIterator(image, inner_index[degree], outer_index[degree]);
                foreach (var lip in Line)
                {
                    value.Add(lip.GetValue<byte>());
                }

                int peak = 255;
                int valley = 0;
                int peak_index = 0;
                int valley_index = 0;
                int valley_flag = 0;
                int peak_flag = 0;

                int temp_peak = 255;
                int temp_valley = 0;
                int temp_peak_index = 0;
                int temp_valley_index = 0;


                for (int pts_index = 1; pts_index < value.Count - 1; pts_index++)//peak of valley will not at 0 and last element.
                {
                    if (valley_flag == 1 && peak_flag == 1 && Math.Abs(peak_index - valley_index) < 3 && Math.Abs(peak - valley) < )
                    {
                        valley_flag = 0;
                        peak_flag = 0;
                        peak = 255;
                        valley = 0;
                        valley_index = 0;
                        peak_index = 0;
                    }
                    if (value[pts_index] > 250)
                        continue;
                    if (value[pts_index] >= value[pts_index - 1] && value[pts_index] >= value[pts_index + 1] && peak_flag == 0)
                    {
                        peak = value[pts_index];
                        peak_index = pts_index;
                        peak_flag = 1;
                    }
                    else if (value[pts_index] <= value[pts_index - 1] && value[pts_index] <= value[pts_index + 1] && valley_flag == 0)
                    {
                        valley = value[pts_index];
                        valley_index = pts_index;
                        valley_flag = 1;
                    }

                }

                all_peak_list.Add(peak);
                all_valley_list.Add(valley);
                all_diff_list.Add(peak - valley);
                //phase1
                if (valley > 120 || peak - valley < threshold_1phase)
                {
                    Candidate_1_phase_index.Add(degree);
                }

                //Console.WriteLine("Count:"+Candidate_1_phase_index.Count);
                value.Clear();
            }
            //phase 2
            foreach (var candidate_degree in Candidate_1_phase_index)
            {
                //Console.WriteLine(candidate_degree);
                int now_valley_value = all_valley_list[candidate_degree];
                int prev_valley_value = all_valley_list[(candidate_degree + neighbor_degree + 720) % 720];
                int next_valley_value = all_valley_list[(candidate_degree - neighbor_degree + 720) % 720];

                int now_peak_value = all_peak_list[candidate_degree];
                int prev_peak_value = all_peak_list[(candidate_degree + neighbor_degree + 720) % 720];
                int next_peak_value = all_peak_list[(candidate_degree - neighbor_degree + 720) % 720];

                int now_peak_valley_difference = all_diff_list[candidate_degree];
                int prev_peak_valley_difference = all_diff_list[(candidate_degree + neighbor_degree + 720) % 720];
                int next_peak_valley_difference = all_diff_list[(candidate_degree - neighbor_degree + 720) % 720];

                if ((((float)now_valley_value - (float)prev_valley_value) + ((float)now_valley_value - (float)next_valley_value) > threshold_2phase_1)
                    && (((float)prev_peak_valley_difference - (float)now_peak_valley_difference) + ((float)next_peak_valley_difference - (float)now_peak_valley_difference)) > threshold_2phase_2)
                {
                    Console.WriteLine(candidate_degree + " " + now_peak_value + " " + now_valley_value + " " + (now_peak_value - now_valley_value) + " " + (((float)now_valley_value - (float)prev_valley_value) + ((float)now_valley_value - (float)next_valley_value) + " " + (((float)prev_peak_valley_difference - (float)now_peak_valley_difference) + ((float)next_peak_valley_difference - (float)now_peak_valley_difference))));

                    Cv2.Circle(vis_rgb, outer_index[candidate_degree], 30, new Scalar(0, 255, 255), thickness: 5);
                    OK_NG_Flag = 1;
                }

            }

            vis_rgb.SaveImage("./result/test" + filename);
        }

    }
}