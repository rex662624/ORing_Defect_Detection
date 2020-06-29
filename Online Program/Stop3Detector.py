import matplotlib.pyplot as plt
import numpy as np
import cv2
import sklearn
from scipy import ndimage, misc
import math
import cv2
import time
import glob
from scipy.ndimage import gaussian_filter1d
from scipy import signal

class Stop3Detector():
    def __init__(self):
        pass
    def detect(self,image):
        OK_NG_flag = 0
        image = image[:,:,0]
        print(image.shape)
        
        #==============read in file==============================
        backtorgb = cv2.cvtColor(image,cv2.COLOR_GRAY2RGB)
        #==============use canny to find the contour
        ret, thresh1 = cv2.threshold(image, 70, 255, cv2.THRESH_BINARY_INV)
        open_kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE,(4,3))
        thresh1 = cv2.morphologyEx(thresh1, cv2.MORPH_OPEN, open_kernel)
        # ===========cut the ROI by the canny edge image================
        _ , contours, hierarchy = cv2.findContours(thresh1,cv2.RETR_TREE,cv2.CHAIN_APPROX_SIMPLE)
        approx_list = []
        index = []
        for i in range(np.array(contours).shape[0]):
            if (hierarchy[0][i][3] >= 0):
                if(cv2.contourArea(contours[i])>300000 and cv2.contourArea(contours[i])<500000):
                    epsilon = 0.005# * cv2.arcLength(contours[i],True)
                    index.append(i)
                    approx = cv2.approxPolyDP(contours[i], epsilon, True)
                    approx_list.append(approx)
        #==========================================find outer circle==============================================
        ret, thresh1 = cv2.threshold(image, 110, 255, cv2.THRESH_BINARY_INV)

        c,r = cv2.minEnclosingCircle(approx_list[0])
        r = int(r)
        cx = int(c[0])
        cy = int(c[1])
        # ===========mask the image to extract the inner circle================
        inner_contour_img = np.zeros_like(image)
        cv2.circle(inner_contour_img,(cx,cy),(r+7), (255,255,255),-1)
        oringmask = inner_contour_img
        image[oringmask == 0] = 255
        #=======optinal blur
        image = cv2.blur(image,(3,3))
        #============create 0.5 degree a line

        const = np.pi/180
        x = []
        y = []
        inner_x = []
        inner_y = []

        degree_delta = 0.5
        for degree in range(int(360//degree_delta)):

            degree_real = degree_delta*degree
            now_x = int((r+2)*np.sin(degree_real*const))+cx
            now_y = int((r+2)*np.cos(degree_real*const))+cy
            now_inner_x = int((r-30)*np.sin(degree_real*const))+cx
            now_inner_y = int((r-30)*np.cos(degree_real*const))+cy

            x.append(now_x)
            y.append(now_y)
            inner_x.append(now_inner_x)
            inner_y.append(now_inner_y)
            
            #============shot from center
            
        threshold_1phase = 30
        threshold_2phase = 10
        all_diff_list = []
        all_min_list = []
        degree_1phase_list = []
        for degree in range(int(360//degree_delta)):
            points_on_line = np.linspace((inner_x[degree], inner_y[degree]),(x[degree],y[degree]) ,200,dtype = int)
            pass_list = image[points_on_line[:,1],points_on_line[:,0]]
            
            #First use gaussian filter to make the curve smooth,and then find peaks and valley.
            pass_list = gaussian_filter1d(pass_list,4)
            peaks, _ = signal.find_peaks(pass_list, height=0, prominence = 10)
            valley, _ = signal.find_peaks(-pass_list, height=0, prominence = 10)
            peak_and_valley_list = np.concatenate((pass_list[peaks], pass_list[valley]), axis=0)
            
            #cacluate the difference of every peak-valley
            shift_peak_and_valley_list = np.concatenate((np.array([0]), peak_and_valley_list))
            peak_and_valley_list = np.concatenate((peak_and_valley_list,np.array([0])))
            diff_list = abs(peak_and_valley_list - shift_peak_and_valley_list)    
            diff_list = diff_list[1::2]
            
            if(len(peaks)==0 or len(valley)==0 ):#如果沒找到local min 和max
                cv2.line(backtorgb,(inner_x[degree],inner_y[degree]),(x[degree],y[degree]),255,2)
                cv2.circle(backtorgb,(x[degree],y[degree]),30, (0,255 ,255), 5)
                continue
                
            now_diff = max(diff_list)
            all_diff_list.append(now_diff)
            all_min_list.append(min(pass_list[valley]))
            
            if((min(pass_list[valley])>120 or now_diff<threshold_1phase)): #如果最小的值>120
                degree_1phase_list.append(degree)
            
        for i in range(len(degree_1phase_list)):#第一階段有問題的degree，第二階段看看是不是真的有問題
            degree = degree_1phase_list[i]
            now_value = all_min_list[degree]
            prev_value = all_min_list[degree-1]
            next_value = all_min_list[degree+1]
            
            if(abs(float(prev_value)-float(now_value))>threshold_2phase and abs(float(next_value) - float(now_value))>threshold_2phase):
                cv2.line(backtorgb,(inner_x[degree],inner_y[degree]),(x[degree],y[degree]),255,2)
                cv2.circle(backtorgb,(x[degree],y[degree]),30, (0,255 ,255), 5)
                OK_NG_flag = 1
        
        # cv2.imwrite(".\\Detect\\Stop3\\" +filenamelist[iter_file][18:-4]+'_Detect.jpg',backtorgb)
        return OK_NG_flag, cv2.cvtColor(backtorgb,cv2.COLOR_RGB2GRAY)

                

