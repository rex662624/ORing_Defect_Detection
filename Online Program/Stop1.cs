﻿using System;
using System.IO;
using System.Collections.Generic;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Threading;


namespace Stop1_multi_thread
{
    class Program
    {
        static int stop1_inner_circle_radius = 0;
        static int stop1_out_defect_size_min = 300;
        static int stop1_out_defect_size_max = 20000;
        static int stop1_inner_defect_size_min = 500;
        static int stop1_arclength_area_ratio = 20;
        static void Main(string[] args)
        {
            //量時間

            //讀圖
            string[] filenamelist = Directory.GetFiles(@".\images\", "*.jpg", SearchOption.AllDirectories);
            //string[] filenamelist = Directory.GetFiles(@".\images\", "10.jpg", SearchOption.AllDirectories);
            //debug
            //int fileindex = 0;

            foreach (string filename in filenamelist)
            {
                //fileindex++;
                Mat src = Cv2.ImRead(filename, ImreadModes.Grayscale);

                Console.WriteLine(filename);
                Stop1_Detect(src, filename.Substring(9));//fileindex);

            }
            
            Console.ReadLine();
        }
        static void Stop1_Detect(Mat src, string fileindex)
        {
            int OK_NG_flag = 0;
            
            Mat vis_rgb = src.CvtColor(ColorConversionCodes.GRAY2RGB);
            //Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //==========================algorithm===============================

            //mask the inner part noise of src
            int nLabels = 0;//number of labels
            int[,] stats = null ;

            List<Point[]> contours_final = Mask_innercicle(ref src);
            //Find outer defect            
            FindContour_and_outer_defect(src, contours_final, ref nLabels, out stats);

            //=================image_crop
            var biggestContourRect = Cv2.BoundingRect(contours_final[0]);
            src = new Mat(src, biggestContourRect);
            OpenCvSharp.Point offset_bounding_rec = biggestContourRect.TopLeft;
            //output_mat.SaveImage("rec.jpg");
            //=============================

            //MSER  
            //Cv2.GaussianBlur(src, src, new OpenCvSharp.Size(3, 3), 0, 0);

            List<Point[][]> MSER_Big = null;
            List<Point[][]> MSER_Small = null;
            My_MSER(6, 800, 20000, 0.7, ref src, ref vis_rgb, 1, out MSER_Big);
            My_MSER(6, 120, 800, 1.5, ref src, ref vis_rgb, 0, out MSER_Small);
    
            //OK or NG            
            // draw outer defect by stats
            for (int i = 0; i < nLabels; i++)
            {
                int area = stats[i, 4];
                if (area < 200000 && area < stop1_out_defect_size_max && area > stop1_out_defect_size_min)
                {
                    vis_rgb.Rectangle(new Rect(stats[i, 0], stats[i, 1], stats[i, 2], stats[i, 3]), Scalar.Green, 3);
                    OK_NG_flag = 1;
                }
            }
            foreach(Point[][] temp in MSER_Big)
            {
                Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(0, 0, 255), 3,offset: offset_bounding_rec);
                OK_NG_flag = 1;
            }
            foreach (Point[][] temp in MSER_Small)
            {
                Cv2.DrawContours(vis_rgb, temp, -1, new Scalar(0, 0, 255), 3, offset: offset_bounding_rec);
                OK_NG_flag = 1;
            }

            Console.WriteLine(OK_NG_flag == 1 ? "NG" : "OK");

            //src.SaveImage("./result/test" + fileindex + ".jpg");
            if(OK_NG_flag == 1)
                vis_rgb.SaveImage("./result/NG/test" + fileindex);
            else
                vis_rgb.SaveImage("./result/OK/test" + fileindex);

            //==================================================================
            watch.Stop();

            //印出時間
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

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
        static void My_MSER(int my_delta, int my_minArea, int my_maxArea, double my_maxVariation, ref Mat img, ref Mat img_rgb, int big_flag,out List<Point[][]> final_area )
        {
            //img.SaveImage("img_detected.jpg");

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

                Point[] Convex_hull = Cv2.ConvexHull(now_contour);
                Point[] Approx = Cv2.ApproxPolyDP(now_contour, 0.5, true);

                RotatedRect rotateRect = Cv2.MinAreaRect(Approx);
                //Debug
                //Console.WriteLine(Cv2.ContourArea(Approx)+" "+ rotateRect.Size.Height / rotateRect.Size.Width+ " "+rotateRect.Size.Width / rotateRect.Size.Height);

                if (Cv2.ContourArea(Approx) > 10000 || (Cv2.ContourArea(Approx) < stop1_inner_defect_size_min || ((rotateRect.Size.Height / rotateRect.Size.Width)) > stop1_arclength_area_ratio || ((rotateRect.Size.Width / rotateRect.Size.Height)) > stop1_arclength_area_ratio))
                    continue;

                //======================intensity in the area
                temp[0] = Approx;
                double mean_in_area_temp = 0, min_in_area_temp = 0;
                Mat mask_img_temp = Mat.Zeros(img.Size(), MatType.CV_8UC1);
                Cv2.DrawContours(mask_img_temp, temp, -1, 255, thickness: -1);//notice the difference between temp = Approx and Convex_hull
                mean_in_area_temp = img.Mean(mask_img_temp)[0];
                img.MinMaxLoc(out min_in_area_temp, out _, out _, out _, mask_img_temp);
                //Console.WriteLine(min_in_area_temp + " " + mean_in_area_temp);
                if (min_in_area_temp > 100 || mean_in_area_temp > 130)
                    continue;

                // Convex hull
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
                    if (vote > 2 || min_in_area > 100 || mean_in_area > 130) { 
                        //Debug
                        //Console.WriteLine(vote + " " + min_in_area + " ", min_in_area);
                        continue;
                    }
                    else
                        //Cv2.Polylines(img_rgb, temp, true, new Scalar(0, 0, 255), 1);
                        final_area.Add(temp);

                }
                else
                {

                    //Cv2.Polylines(img_rgb, temp, true, new Scalar(0, 0, 255), 1);
                    final_area.Add(temp);
                }
            }
            

        }


        //mask the inner part of circle 
        static List<Point[]> Mask_innercicle(ref Mat img)
        {
            Mat img_copy = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            img.CopyTo(img_copy);
            Cv2.GaussianBlur(img_copy, img_copy, new OpenCvSharp.Size(15, 15), 0, 0);

            Mat thresh1 = img_copy.Threshold(200, 255, ThresholdTypes.Binary);
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
            //Cv2.Circle(img, (Point)center, (int)radius+ stop1_inner_circle_radius, 255, thickness: -1);
            //Cv2.Circle(vis_rgb, (Point)center, (int)radius, Scalar.White, thickness: -1);

            //==================================================outer contour - inner contour=====================================
            // variable
            OpenCvSharp.Point[][] temp = new Point[1][];


            // inner contour
            Mat inner_contour_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
            //Point[] inner_contour = Cv2.ConvexHull(contours_final[1]);
            //temp[0] = inner_contour;
            //Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);
            Cv2.Circle(inner_contour_img, (Point)center, (int)radius + stop1_inner_circle_radius, 255, thickness: -1);

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

        //Find outer defect
        static void FindContour_and_outer_defect(Mat img, List<Point[]> contours_final, ref int nLabels, out int [,] stats)
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

            //diff_image.SaveImage("./mask.jpg");
            
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
