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
        public static int stop2_inner_defect_size_min = 100;
        public static int stop2_arclength_area_ratio = 100;

        static void Main(string[] args)
        {

            //讀圖
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //string[] filenamelist = Directory.GetFiles(@".\images\", "199.jpg", SearchOption.AllDirectories);
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
            //=======================================================================================

            List<OpenCvSharp.Point[]> contours_final = contour_inner_outer(Src);
            Mat Src_copy = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(Src_copy);

            //用adaptive threshold 濾出瑕疵
            Cv2.GaussianBlur(Src_copy, Src_copy, new OpenCvSharp.Size(3, 3), 0, 0);
            Cv2.AdaptiveThreshold(Src_copy, Src_copy, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 45, 105 / 10);

            //讓黑白相反(not opetation)
            Mat Src_255 = new Mat(Src_copy.Size(), MatType.CV_8UC1, new Scalar(255));
            Cv2.Subtract(Src_255, Src_copy, Src_copy);


            //denoise to eliminate noise
            Denoise(ref Src_copy, filename, contours_final);
            
            Find_Contour_and_Extract_Defect(Src_copy, vis_rgb, filename, contours_final, ref OK_NG_Flag);

            Src_copy.SaveImage("./enhance/" + filename);

            //================circle============================================
            Mat Fit_Circle_img = Mat.Zeros(vis_rgb.Size(), MatType.CV_8UC3);
            vis_rgb.CopyTo(Fit_Circle_img);
            FitCircle(Src, Fit_Circle_img, contours_final, filename);

            //==================MSER============================================
            /*
            Mat img_MSER = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(img_MSER);
            OpenCvSharp.Point offset_bounding_rec;
            Cv2.GaussianBlur(img_MSER, img_MSER, new OpenCvSharp.Size(11, 11), 0, 0);
            MSER_Proprecessing(ref img_MSER, out offset_bounding_rec, contours_final);
            img_MSER.SaveImage("./mser_proprecessing/" + filename);

            List<OpenCvSharp.Point[]> MSER_Big = null;
            
            My_MSER(6, stop2_inner_defect_size_min, 20000, 1.6, ref img_MSER, ref vis_rgb, 0, out MSER_Big);
            

            foreach (Point[] contour_now in MSER_Big)
            {
                OK_NG_Flag = 1;
                OpenCvSharp.Point[][] temp = new Point[1][];
                temp[0] = contour_now;
                Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(0, 0, 255), 3, offset: offset_bounding_rec);
                
            }
            */
            //========================================================================

            if (OK_NG_Flag == 0) {

                Console.WriteLine("OK");
                vis_rgb.SaveImage("./OK/" + filename);
            }
            else {
                Console.WriteLine("NG");
                vis_rgb.SaveImage("./NG/" + filename);
            }
            //==================================================================

            watch.Stop();
            //印出時間
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


        }

        static void FitCircle(Mat img, Mat vis_rgb, List<OpenCvSharp.Point[]> contours_final, string filename)
        {
            //https://blog.csdn.net/weixin_41049188/article/details/92422241
            /*
            Mat new_image = new Mat();
            Cv2.MedianBlur(img, new_image, 3);
            Mat thresh1 = new_image.Threshold(250, 255, ThresholdTypes.Binary);
            */
            OpenCvSharp.Point[][] temp = new Point[1][];
            temp[0] = contours_final[0];

            Mat contour_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(contour_mask_img, temp, -1, 255, -1);

            CircleSegment[] cs = Cv2.HoughCircles(contour_mask_img, HoughMethods.Gradient, 2.5, 500);
            Console.WriteLine(cs.Count());
            for (int i = 0; i < cs.Count(); i++)
            {
                Cv2.Circle(vis_rgb, (int)cs[i].Center.X, (int)cs[i].Center.Y, (int)cs[i].Radius, new Scalar(0, 0, 255), thickness: 2);
            }

            vis_rgb.SaveImage("./contour/" + filename);

            //Connected Component
            var labelMat = new MatOfInt();
            var statsMat = new MatOfInt();// Row: number of labels Column: 5
            var centroidsMat = new MatOfDouble();
            int nLabels = Cv2.ConnectedComponentsWithStats(diff_image, labelMat, statsMat, centroidsMat);

            var labels = labelMat.ToRectangularArray();
            int[,] stats = statsMat.ToRectangularArray();
            var centroids = centroidsMat.ToRectangularArray();

            //Point2f center;
            //float radius;
            //Cv2.MinEnclosingCircle(contours_final[0], out center, out radius);


        }

        static List<OpenCvSharp.Point[]> contour_inner_outer(Mat Src)
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

        static void Find_Contour_and_Extract_Defect(Mat Src, Mat vis_rgb, string filename, List<OpenCvSharp.Point[]> contours_final, ref Int64 OK_NG_Flag)
        {
            //==============================找到圓心=======================
            Point2f center;
            float radius;
            Cv2.MinEnclosingCircle(contours_final[0], out center, out radius);
            
            //=============================================================

            Mat defect_image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);

            Point[][] contours;
            HierarchyIndex[] hierarchly;
            Cv2.FindContours(Src, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            OpenCvSharp.Point[][] temp = new Point[1][];

            // Extract defect candidate 利用和圓心的距離還有面積
            foreach (OpenCvSharp.Point[] contour_now in contours)
            {
                if (Cv2.ContourArea(contour_now) > 150)
                {
                    //Console.WriteLine("Arc Length: " + (Cv2.ArcLength(contour_now, true) + " Area: " + Cv2.ContourArea(contour_now))+" Length/Area:" +(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)));
                    OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
                    temp[0] = approx;
                    Cv2.DrawContours(defect_image, temp, -1, 255, -1);
                    defect_image.SaveImage("./contour/" + filename);


                    // find the distance between contour and center
                    if (Distance_between_contour_and_center(center, approx))
                    {

                        Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(255, 0, 0), 3);
                        OK_NG_Flag = 1;
                    }
                }

            }
            /*
            if (OK_NG_Flag == 0)
                vis_rgb.SaveImage("./OK/" + filename);
            else
                vis_rgb.SaveImage("./NG/" + filename);
            */




        }

        static bool Distance_between_contour_and_center(OpenCvSharp.Point2f center, OpenCvSharp.Point[] contour)
        {
            double diff = 0;
            bool glass_flag = false;
            List<double> diff_list = new List<double>();
            List<int> x_list = new List<int>();
            List<int> y_list = new List<int>();
            int x1 = (int)center.X;
            int y1 = (int)center.Y;
            foreach (OpenCvSharp.Point contour_point in contour)
            {
                int x2 = contour_point.X;
                int y2 = contour_point.Y;
                x_list.Add(x2);
                y_list.Add(y2);
                diff_list.Add(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
                //Console.WriteLine(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
            }

            int x_max = x_list.Max();
            int x_min = x_list.Min();
            int y_max = y_list.Max();
            int y_min = y_list.Min();
            //Console.WriteLine("x_max " + x_max + " x_min " + x_min + " y_max " + y_max + " y_min " + y_min);
            // 玻璃上的裂縫

            if (((y_max < 700 && y_max > 630) || (y_min < 700 && y_min > 630)))
            {
                glass_flag = true;
            }
            //Console.WriteLine("\nMax: " + diff_list.Max());
            //Console.WriteLine("Min: " + diff_list.Min());
            diff = diff_list.Max() - diff_list.Min();
            //Console.WriteLine(diff);
            //Console.WriteLine(diff_list.Max());

            //RotatedRect rotateRect = Cv2.MinAreaRect(contour);
            //Console.WriteLine(rotateRect.Size.Width + " "+ rotateRect.Size.Height);
            return !(glass_flag) && (diff > 10 || diff_list.Max() > 700);


        }

        static void MSER_Proprecessing(ref Mat img, out OpenCvSharp.Point offset_bounding_rec, List<OpenCvSharp.Point[]> contours_final)
        {

            OpenCvSharp.Point[][] temp = new Point[1][];
            temp[0] = contours_final[1];
            Cv2.DrawContours(img, temp, -1, 255, 100);
            //temp[0] = contours_final[0];
            //Cv2.DrawContours(img, temp, -1, 255, 20);

            var biggestContourRect = Cv2.BoundingRect(contours_final[0]);
            var expand_rect = new Rect(biggestContourRect.TopLeft.X-200, biggestContourRect.TopLeft.Y - 200, biggestContourRect.Width + 200, biggestContourRect.Height + 200);
            img = new Mat(img, expand_rect);
            offset_bounding_rec = expand_rect.TopLeft;

        }
        // MSER set shift image
        static Mat[] set_shift_image(ref Mat img)
        {
            float[,,] data = new float[4, 2, 3] {   { { 1,0,15},    { 0,1,-15}  },
                                                {   { 1,0,15},    { 0,1,15}   },
                                                {   { 1,0,-15},   { 0,1,-15}  },
                                                {   { 1,0,-15},   { 0,1,15}   }
                                            };

            Mat[] out_image = new Mat[4];
            for (int i = 0; i < 4; i++)
            {
                out_image[i] = new Mat(2, 3, MatType.CV_32F);
                out_image[i].Set(0, 0, data[i, 0, 0]);
                out_image[i].Set(0, 1, data[i, 0, 1]);
                out_image[i].Set(0, 2, data[i, 0, 2]);
                out_image[i].Set(1, 0, data[i, 1, 0]);
                out_image[i].Set(1, 1, data[i, 1, 1]);
                out_image[i].Set(1, 2, data[i, 1, 2]);
            }

            return out_image;

        }

        //MyMSER
        static void My_MSER(int my_delta, int my_minArea, int my_maxArea, double my_maxVariation, ref Mat img, ref Mat img_rgb, int big_flag, out List<Point[]> final_area)
        {
            final_area = new List<Point[]>();
            Point[][] contours;
            Rect[] bboxes;
            MSER mser = MSER.Create(delta: my_delta, minArea: my_minArea, maxArea: my_maxArea, maxVariation: my_maxVariation);
            mser.DetectRegions(img, out contours, out bboxes);

            //====================================Local Majority Vote

            // to speed up, create four shift image first
            var shift_mat = set_shift_image(ref img);
            Mat[] neighbor_img = new Mat[4];
            for (int i = 0; i < 4; i++)
            {
                neighbor_img[i] = new Mat();
                Cv2.WarpAffine(img, neighbor_img[i], shift_mat[i], img.Size());
            }

            //for each contour, apply local majority vote
            foreach (Point[] now_contour in contours)
            {
                OpenCvSharp.Point[][] temp = new Point[1][];
                OpenCvSharp.Point[][] add_convex_hull = new Point[1][];//We need to draw the bounding box of the defect, so using convex hull is needed.

                Point[] Convex_hull = Cv2.ConvexHull(now_contour);
                Point[] Approx = Cv2.ApproxPolyDP(now_contour, 0.5, true);

                //===============================threshold for arc length and area===============================
                // if the arc length / area too large, that means the shape is thin. (maybe can ad width and height to make them more ensure)
                RotatedRect rotateRect = Cv2.MinAreaRect(Approx);
                if (Cv2.ContourArea(Approx) > 10000 || Cv2.ContourArea(Approx) < stop2_inner_defect_size_min || (rotateRect.Size.Width / rotateRect.Size.Height) > stop2_arclength_area_ratio || (rotateRect.Size.Height / rotateRect.Size.Width) > stop2_arclength_area_ratio)
                    continue;
                //===============================local majority vote===============================
                // Convex hull
                add_convex_hull[0] = Convex_hull;
                temp[0] = Approx;
                if (big_flag == 0)//small area: local majority vote
                {
                    //inside the area
                    double mean_in_area = 0, min_in_area = 0;
                    Mat mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
                    Cv2.DrawContours(mask_img, temp, -1, 255, thickness: -1);//notice the difference between temp = Approx and Convex_hull
                    mean_in_area = img.Mean(mask_img)[0];
                    img.MinMaxLoc(out min_in_area, out _, out _, out _, mask_img);

                    //select by mean
                    //Console.WriteLine(mean_in_area);

                    //neighbor
                    double[] mean_neighbor = { 255, 255, 255, 255 };
                    double[] min_neighbor = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; i++)
                    {
                        // create final mask
                        Mat mask2 = neighbor_img[i].LessThan(225).ToMat();
                        mask2.ConvertTo(mask2, MatType.CV_8U, 1.0 / 255.0);

                        Mat mask_final = Mat.Zeros(img.Size(), MatType.CV_8UC1);
                        mask_img.CopyTo(mask_final, mask2);

                        //mask_final.SaveImage("./mask" + i + ".jpg");

                        mean_neighbor[i] = neighbor_img[i].Mean(mask_final)[0];
                        //compute min:
                        neighbor_img[i].MinMaxLoc(out min_neighbor[i], out _, out _, out _, mask_img);

                    }
                    double total = 0;
                    int count = 0;
                    //Console.Write(mean_in_area + " ");
                    int vote = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (mean_neighbor[i] < 200) { 
                            total += mean_neighbor[i];
                            count++;
                        }
                        //Console.Write(mean_neighbor[i] + " ");
                        if (mean_in_area > mean_neighbor[i])
                            vote++;
                    }
                    //Console.Write("\n");
                    total = total / count;
                    double diff = total - mean_in_area;
                    Console.WriteLine(diff+" "+ mean_in_area);
                    if (vote > 1)
                        continue;
                    else
                        final_area.Add(now_contour);

                }
                else
                {
                    final_area.Add(now_contour);
                }
            }


        }

    }
}