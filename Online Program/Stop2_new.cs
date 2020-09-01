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
        public static int OK_Count = 0;
        public static int NG_Count = 0;
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

            Console.WriteLine(OK_Count);
            Console.WriteLine(NG_Count);

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

            List<OpenCvSharp.Point[]> contours_final = Mask_innercicle2(ref Src);
            //Src.SaveImage("./enhance/" + filename);
            Mat Src_copy = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(Src_copy);

            //================outer defect====================================
            FindContour_and_outer_defect(Src,vis_rgb, contours_final, ref OK_NG_Flag);

            //====================Adaptive threshold inner defect==============================================
            //用adaptive threshold 濾出瑕疵
            Cv2.GaussianBlur(Src_copy, Src_copy, new OpenCvSharp.Size(3, 3), 0, 0);
            Cv2.AdaptiveThreshold(Src_copy, Src_copy, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 45, 105 / 10);

            //讓黑白相反(not opetation)
            Mat Src_255 = new Mat(Src_copy.Size(), MatType.CV_8UC1, new Scalar(255));
            Cv2.Subtract(Src_255, Src_copy, Src_copy);

            //denoise to eliminate noise
            Denoise(ref Src_copy, filename, contours_final);
            //use the ad
            Find_Defect_Contour_and_Extract(Src_copy, vis_rgb, filename, contours_final, ref OK_NG_Flag);
            //====================MSER=========================================================================
            Mat img_MSER = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
            Src.CopyTo(img_MSER);
            OpenCvSharp.Point offset_bounding_rec;

            MSER_Preprocessing(ref img_MSER, out offset_bounding_rec, contours_final);
            img_MSER.SaveImage("./mser_proprecessing/" + filename);
            //6 0.9
            My_MSER(7, stop2_inner_defect_size_min, 20000, 1.2, img_MSER, vis_rgb, 0, contours_final,ref  OK_NG_Flag, offset_bounding_rec);
                
            //Src_copy.SaveImage("./enhance/" + filename);



            if (OK_NG_Flag == 0) {

                Console.WriteLine("OK");
                vis_rgb.SaveImage("./OK/" + filename);
                OK_Count++;
            }
            else {
                Console.WriteLine("NG");
                vis_rgb.SaveImage("./NG/" + filename);
                NG_Count++;
            }
            //==================================================================

            watch.Stop();
            //印出時間
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


        }

        static List<OpenCvSharp.Point[]> Mask_innercicle2(ref Mat img)
        {


            Mat img_copy = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            img.CopyTo(img_copy);
            Cv2.GaussianBlur(img_copy, img_copy, new OpenCvSharp.Size(15, 15), 0, 0);

            Mat thresh1 = img_copy.Threshold(200, 255, ThresholdTypes.Binary);
            //thresh1.SaveImage("threshold.jpg");
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
            
            OpenCvSharp.Point[] contours_approx_innercircle;

            var contour_innercircle = contours_final[1];

            Point2f center;
            float radius;
            contours_approx_innercircle = Cv2.ApproxPolyDP(contour_innercircle, 0.001, true);//speedup
            Cv2.MinEnclosingCircle(contours_approx_innercircle, out center, out radius);

            //==================================================outer contour - inner contour=====================================
            // variable
            OpenCvSharp.Point[][] temp = new Point[1][];

            // inner contour
            temp[0] = contours_final[1];
            Mat inner_contour_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);
            //Cv2.Circle(inner_contour_img, (Point)center, (int)radius + stop2_inner_circle_radius, 255, thickness: -1);

            // outer contour
            Mat outer_contour_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Mat outer_contour_img2 = new Mat(img.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255

            temp[0] = contours_final[0];
            Cv2.DrawContours(outer_contour_img, temp, -1, 255, -1);
            //outer contour2 in order to make mask area = 255
            Cv2.DrawContours(outer_contour_img2, temp, -1, 0, -1);
            //outer - inner
            Mat diff_mask = outer_contour_img - inner_contour_img;
            Mat diff_mask2 = inner_contour_img + outer_contour_img2;

            Mat image = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            img.CopyTo(image, diff_mask);
            //in order to make mask area = 255
            img = image + diff_mask2;
            

            return contours_final;
        }
        static void FitCircle(Mat img, Mat vis_rgb, List<OpenCvSharp.Point[]> contours_final, string filename)
        {
            //https://blog.csdn.net/weixin_41049188/article/details/92422241
            
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
            Cv2.DrawContours(Src, temp, -1, 0, 30);
            temp[0] = contours_final[1];
            Cv2.DrawContours(Src, temp, -1, 0, 30);


            Src.SaveImage("./enhance/" + filename);
        }
        static void Find_Defect_Contour_and_Extract(Mat Src, Mat vis_rgb, string filename, List<OpenCvSharp.Point[]> contours_final, ref Int64 OK_NG_Flag)
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

            if (((y_max < 700 && y_max > 630) && (y_min < 700 && y_min > 630)))
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
        static void FindContour_and_outer_defect(Mat img, Mat vis_rgb,List<Point[]> contours_final,ref Int64 OK_NG_Flag)
        {
            // variable
            OpenCvSharp.Point[][] temp = new Point[1][];
            int nLabels = 0;//number of labels
            int[,] stats = null;

            // Contour
            temp[0] = contours_final[0];
            Mat contour_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(contour_mask_img, temp, -1, 255, -1);

            //contour_mask_img.SaveImage("2_1.jpg");
            //Mat kernel_contour = Mat.Ones(5, 5, MatType.CV_8UC1);
            //contour_mask_img = contour_mask_img.MorphologyEx(MorphTypes.Dilate, kernel_contour);
            //contour_mask_img.SaveImage("2_2.jpg");


            // Convex hull
            Point[] Convex_hull = Cv2.ConvexHull(contours_final[0]);
            temp[0] = Convex_hull;
            Mat convex_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(convex_mask_img, temp, -1, 255, -1);

            //convex_mask_img.SaveImage("1.jpg");

            // Subtraction 
            Mat diff_image = convex_mask_img - contour_mask_img;

            //diff_image.SaveImage("3.jpg");
            //Opening
            Mat kernel = Mat.Ones(2, 2, MatType.CV_8UC1);//改變凹角大小
            diff_image = diff_image.MorphologyEx(MorphTypes.Open, kernel);

            //diff_image.SaveImage("4.jpg");
            //diff_image.SaveImage("./result/test" + fileindex + ".jpg");
            //Connected Component
            var labelMat = new MatOfInt();
            var statsMat = new MatOfInt();// Row: number of labels Column: 5
            var centroidsMat = new MatOfDouble();
            nLabels = Cv2.ConnectedComponentsWithStats(diff_image, labelMat, statsMat, centroidsMat);

            var labels = labelMat.ToRectangularArray();
            stats = statsMat.ToRectangularArray();
            var centroids = centroidsMat.ToRectangularArray();

            //judge OK or NG
            if (nLabels > 1)
            {
                // draw outer defect by stats
                for (int i = 0; i < nLabels; i++)
                {

                    int area = stats[i, 4];
                    //Console.WriteLine(area);
                    if (area < 200000
                        && area < stop2_out_defect_size_max
                        && area > stop2_out_defect_size_min)
                    {
                        OK_NG_Flag = 1;
                        vis_rgb.Rectangle(new Rect(stats[i, 0], stats[i, 1], stats[i, 2], stats[i, 3]), Scalar.Green, 3);
                    }

                    /* small area defect
                    else if (area < stop2_out_defect_size_min)//small area
                    {
                        OK_NG_Flag = 1;
                        vis_rgb.Rectangle(new Rect(stats[i, 0], stats[i, 1], stats[i, 2], stats[i, 3]), Scalar.Red, 3);
                    }*/
                }

            }


        }
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
                /*
                Console.WriteLine(out_image[i].At<float>(0, 0));
                Console.WriteLine(out_image[i].At<float>(0, 1));
                Console.WriteLine(out_image[i].At<float>(0, 2));
                Console.WriteLine(out_image[i].At<float>(1, 0));
                Console.WriteLine(out_image[i].At<float>(1, 1));
                Console.WriteLine(out_image[i].At<float>(1, 2));
                */

            }

            return out_image;

        }
        static void My_MSER(int my_delta, int my_minArea, int my_maxArea, double my_maxVariation, Mat Src, Mat vis_rgb, int big_flag, List<OpenCvSharp.Point[]> contours_final, ref Int64 OK_NG_Flag, OpenCvSharp.Point offset_bounding_rec)
        {
            OpenCvSharp.Point[][] temp = new Point[1][];
            
            List<OpenCvSharp.Point[]> final_area = new List<OpenCvSharp.Point[]>();
            Point[][] contours;
            Rect[] bboxes;
            MSER mser = MSER.Create(delta: my_delta, minArea: my_minArea, maxArea: my_maxArea, maxVariation: my_maxVariation);
            mser.DetectRegions(Src, out contours, out bboxes);

            //====================================Local Majority Vote

            // to speed up, create four shift image first
            var shift_mat = set_shift_image(ref Src);
            Mat[] neighbor_img = new Mat[4];
            for (int i = 0; i < 4; i++)
            {
                neighbor_img[i] = new Mat();
                Cv2.WarpAffine(Src, neighbor_img[i], shift_mat[i], Src.Size());
            }

            //for each contour, apply local majority vote
            foreach (Point[] now_contour in contours)
            {
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
                    Mat mask_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
                    Cv2.DrawContours(mask_img, temp, -1, 255, thickness: -1);//notice the difference between temp = Approx and Convex_hull
                    mean_in_area = Src.Mean(mask_img)[0];
                    Src.MinMaxLoc(out min_in_area, out _, out _, out _, mask_img);
                    
                    //neighbor
                    double[] mean_neighbor = { 255, 255, 255, 255 };
                    double[] min_neighbor = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; i++)
                    {
                        // create final mask
                        Mat mask2 = neighbor_img[i].LessThan(225).ToMat();
                        mask2.ConvertTo(mask2, MatType.CV_8U, 1.0 / 255.0);

                        Mat mask_final = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
                        mask_img.CopyTo(mask_final, mask2);


                        mean_neighbor[i] = neighbor_img[i].Mean(mask_final)[0];
                        //compute min:
                        neighbor_img[i].MinMaxLoc(out min_neighbor[i], out _, out _, out _, mask_img);

                    }
                    int vote = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (mean_in_area > mean_neighbor[i])
                            vote++;
                    }
                    if (vote > 1)
                        continue;
                    else
                        final_area.Add(Approx);

                }
                else
                {
                    final_area.Add(Approx);
                }
            }


            foreach (Point[] now_contour in final_area)
            {
                temp[0] = now_contour;
                //final_area.Add(now_contour);
                OK_NG_Flag = 1;
                Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(255, 0, 0), 3, offset: offset_bounding_rec);// offset: offset_bounding_rec
            }


        }
        static void MSER_Preprocessing(ref Mat img, out OpenCvSharp.Point offset_bounding_rec, List<OpenCvSharp.Point[]> contours_final)
        {
            OpenCvSharp.Point[][] temp = new Point[1][];

            Cv2.GaussianBlur(img, img, new OpenCvSharp.Size(7, 7), 0, 0);

            /*
            //忽略內圈和外圈一些面積
            OpenCvSharp.Point[][] temp = new Point[1][];
            temp[0] = contours_final[0];
            Cv2.DrawContours(img_copy, temp, -1, 255, 40);
            temp[0] = contours_final[1];
            Cv2.DrawContours(img_copy, temp, -1, 255, 40);
            */
            
            //忽略外圈一些面積
            temp[0] = contours_final[1];
            Cv2.DrawContours(img, temp, -1, 255, 100);
            //temp[0] = contours_final[0];
            //Cv2.DrawContours(img, temp, -1, 255, 20);
            
            //200原因:外圈預留空間
            var biggestContourRect = Cv2.BoundingRect(contours_final[0]);
            var expand_rect = new Rect(biggestContourRect.TopLeft.X - 200, biggestContourRect.TopLeft.Y - 200, biggestContourRect.Width + 200, biggestContourRect.Height + 200);
            img = new Mat(img, expand_rect);
            offset_bounding_rec = expand_rect.TopLeft;
            



        }
    }
}