using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;

namespace Stop2
{
    class Program
    {
        static void Main(string[] args)
        {
            //量時間

            //讀圖
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //string[] filenamelist = Directory.GetFiles(@".\images\", "22.jpg", SearchOption.AllDirectories);
            //debug
            int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                fileindex++;
                Mat src = Cv2.ImRead(filename, ImreadModes.Grayscale);

                Console.WriteLine(filename);
                Stop2_Detector(src, filename.Substring(9));

            }

            Console.ReadLine();
        }
        static void Stop2_Detector(Mat Src, string filename)
        {
            Int64 OK_NG_Flag = 0;
            Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
            //Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //==========================algorithm===============================

            //mask the inner part noise of src
            int nLabels = 0;//number of labels
            int[,] stats = null;
            Thread t3 = new Thread(delegate ()
            {
                List<OpenCvSharp.Point[]> contours_final = Mask_innercicle2(ref Src);

                //Find outer defect            
                FindContour_and_outer_defect(Src, contours_final, ref nLabels, out stats);
            });
            //MSER  
            //=============difference from stop1
            Cv2.GaussianBlur(Src, Src, new OpenCvSharp.Size(3, 3), 0, 0);
            //================
            List<OpenCvSharp.Point[][]> MSER_Big = null;
            Thread t1 = new Thread(delegate ()
            {
                My_MSER(6, 200, 20000, 0.65, ref Src, ref vis_rgb, 0, out MSER_Big);
            });

            t1.Start();
            t3.Start();
            t1.Join();
            t3.Join();

            //OK or NG
            if (MSER_Big.Count == 0 && nLabels <= 1)//nLabels 1 represent outer defect
            {
                //Console.WriteLine("OK");
                OK_NG_Flag = 0;
            }
            else
            {
                //Console.WriteLine("NG");
                OK_NG_Flag = 1;
                // draw outer defect by stats
                for (int i = 0; i < nLabels; i++)
                {
                    int area = stats[i, 4];
                    if (area < 200000)
                    {
                        vis_rgb.Rectangle(new Rect(stats[i, 0], stats[i, 1], stats[i, 2], stats[i, 3]), Scalar.Green, 3);
                    }
                }
                foreach (OpenCvSharp.Point[][] temp in MSER_Big)
                {
                    Cv2.Polylines(vis_rgb, temp, true, new Scalar(0, 0, 255), 2);
                }

            }
            //==================================================================
            vis_rgb.SaveImage("./result/test" + filename);
            watch.Stop();
            //印出時間
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


        }
        
        //mask the inner part of circle 
        static List<OpenCvSharp.Point[]> Mask_innercicle2(ref Mat img)
        {

            Mat thresh1 = img.Threshold(230, 255, ThresholdTypes.Binary);
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

            ///OpenCvSharp.Point[][] temp = new Point[1][];//for draw on image

            OpenCvSharp.Point[] contours_approx_innercircle;

            var contour_innercircle = contours_final[1];

            //temp[0] = contour_now;

            Point2f center;
            float radius;
            //Cv2.DrawContours(vis_rgb, temp, -1, Scalar.Green, thickness: -1);
            contours_approx_innercircle = Cv2.ApproxPolyDP(contour_innercircle, 0.001, true);//speedup
            Cv2.MinEnclosingCircle(contours_approx_innercircle, out center, out radius);
            Cv2.Circle(img, (OpenCvSharp.Point)center, (int)radius, 255, thickness: -1);
            //Cv2.Circle(vis_rgb, (Point)center, (int)radius, Scalar.White, thickness: -1);

            return contours_final;
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

        //MyMSER
        static void My_MSER(int my_delta, int my_minArea, int my_maxArea, double my_maxVariation, ref Mat img, ref Mat img_rgb, int big_flag, out List<Point[][]> final_area)
        {
            final_area = new List<Point[][]>();
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
                var imageCenter = new Point2f(img.Cols / 2f, img.Rows / 2f);
                var rotationMat = Cv2.GetRotationMatrix2D(imageCenter, 100, 1.3);
                Cv2.WarpAffine(img, neighbor_img[i], shift_mat[i], img.Size());
                //neighbor_img[i].SaveImage("./shift_image" + i + ".jpg");
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
                //Console.WriteLine(rotateRect.Size.Height/ rotateRect.Size.Width);
                if (Cv2.ContourArea(Approx) < 500 || (rotateRect.Size.Height / rotateRect.Size.Width )<0.1|| (rotateRect.Size.Height / rotateRect.Size.Width) > 10)
                    continue;
                //Console.WriteLine("Width/Height Ratio: "+ rotateRect.Size.Width / rotateRect.Size.Height + " Len/area Ratio: " + (Cv2.ArcLength(Approx, true) / Cv2.ContourArea(Approx)) + " Area: " + Cv2.ContourArea(Approx));

                //===============================local majority vote===============================
                // Convex hull
                add_convex_hull[0] = Approx;
                temp[0] = Approx;
                if (big_flag == 0)//small area: local majority vote
                {
                    //Cv2.Polylines(img_rgb, temp, true, new Scalar(0, 0, 255), 1);
                    //inside the area
                    double mean_in_area = 0, min_in_area = 0;
                    Mat mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
                    Cv2.DrawContours(mask_img, temp, -1, 255, thickness: -1);//notice the difference between temp = Approx and Convex_hull
                    mean_in_area = img.Mean(mask_img)[0];
                    img.MinMaxLoc(out min_in_area, out _, out _, out _, mask_img);
                    //Console.WriteLine(min_in_area + " " + mean_in_area);

                    //test 
                    /*
                    Mat mask2 = img.LessThan(230);
                    for (int i = 0; i < img.Cols; i++) {
                        for (int j = 0; j < img.Rows; j++) 
                            if(mask2.At<bool>(i, j)==false)
                                Console.Write(mask2.At<bool>(i,j)+ " ");

                        Console.Write("\n");

                    }
                    */
                    //neighbor
                    double[] mean_neighbor = { 255, 255, 255, 255 };
                    double[] min_neighbor = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; i++)
                    {
                        //先把 img > 230 的變成 0，再餵進 shift 裡面
                        //先把 mask 乘上另一個mask(>230的mask)
                        //Mat mask_neighbor_img = neighbor_img[i].GreaterThan(0);
                        //Console.WriteLine(mask_neighbor_img.At<int>(0,1));
                        // create final mask
                        Mat mask2 = neighbor_img[i].LessThan(225).ToMat();
                        mask2.ConvertTo(mask2, MatType.CV_8U, 1.0 / 255.0);

                        Mat mask_final = Mat.Zeros(img.Size(), MatType.CV_8UC1);
                        mask_img.CopyTo(mask_final, mask2);

                        //mask_final.SaveImage("./mask" + i + ".jpg");

                        mean_neighbor[i] = neighbor_img[i].Mean(mask_final)[0];
                        //compute min:
                        //neighbor_img[i].MinMaxLoc(out min_neighbor[i], out _, out _, out _, mask_img);
                        //Console.WriteLine(min_neighbor[i] + " " + mean_neighbor[i]);

                    }
                    int vote = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (mean_in_area > mean_neighbor[i])
                            vote++;
                    }
                    if (vote > 1 || min_in_area > 110 || mean_in_area > 130)
                        continue;
                    else
                        //Cv2.Polylines(img_rgb, temp, true, new Scalar(0, 0, 255), 1);
                        final_area.Add(add_convex_hull);

                }
                else
                {
                    //Cv2.Polylines(img_rgb, temp, true, new Scalar(0, 0, 255), 1);
                    final_area.Add(add_convex_hull);
                }
            }


        }
        static void FindContour_and_outer_defect(Mat img, List<Point[]> contours_final, ref int nLabels, out int[,] stats)
        {
            // variable
            OpenCvSharp.Point[][] temp = new Point[1][];


            // Convex hull
            Point[] Convex_hull = Cv2.ConvexHull(contours_final[0]);
            temp[0] = Convex_hull;
            Mat convex_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(convex_mask_img, temp, -1, 255, -1);


            // Contour
            temp[0] = contours_final[0];
            Mat contour_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            Cv2.DrawContours(contour_mask_img, temp, -1, 255, -1);

            // Subtraction 
            Mat diff_image = convex_mask_img - contour_mask_img;


            //Opening
            Mat kernel = Mat.Ones(2, 2, MatType.CV_8UC1);//改變凹角大小
            diff_image = diff_image.MorphologyEx(MorphTypes.Open, kernel);

            //diff_image.SaveImage("./result/test" + fileindex + ".jpg");
            //Connected Component
            var labelMat = new MatOfInt();
            var statsMat = new MatOfInt();// Row: number of labels Column: 5
            var centroidsMat = new MatOfDouble();
            nLabels = Cv2.ConnectedComponentsWithStats(diff_image, labelMat, statsMat, centroidsMat);

            var labels = labelMat.ToRectangularArray();
            stats = statsMat.ToRectangularArray();
            var centroids = centroidsMat.ToRectangularArray();



        }

    }
}
