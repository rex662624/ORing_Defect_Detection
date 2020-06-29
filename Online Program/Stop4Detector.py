import matplotlib.pyplot as plt
import numpy as np
import cv2
import sklearn
from scipy import ndimage, misc
import math
import cv2
import time
import glob

class Stop4Detector():
    def __init__(self):
        pass
    def detect(self,image):
            
        
        start = time.time()
        OK_NG_flag = 0
        image = image[:,:,0]
        backtorgb = cv2.cvtColor(image,cv2.COLOR_GRAY2RGB)


        #=======================以下找出真正的O-ring=============================
        ret, thresh1 = cv2.threshold(image, 240, 255, cv2.THRESH_BINARY)
        #find contours1
        _ , contours, hierarchy = cv2.findContours(thresh1,cv2.RETR_TREE,cv2.CHAIN_APPROX_SIMPLE)
        contours_final = []
        approx_list = []
        for i in range(np.array(contours).shape[0]):
                if(cv2.contourArea(contours[i])>300000 and cv2.contourArea(contours[i])<800000):
                    contours_final.append(contours[i])
                    #cv2.drawContours(backtorgb,contours[i],-1,(0,0,255),3)

                    #print("Area: ",cv2.contourArea(contours[i]))
                    epsilon = 0.5# * cv2.arcLength(contours[i],True)
                    approx = cv2.approxPolyDP(contours[i], epsilon, True)
                    approx_list.append(approx)
                    #cv2.polylines(backtorgb, [approx], True, (255, 0, 0), 2)
        #====================外圈減內圈得到真的O-ring================================
        inner_contour_img = np.zeros_like(image)
        cv2.drawContours(inner_contour_img, [approx_list[1]],-1,(255,255,255),-1)
        inner_contour_img = 1 - inner_contour_img

        outer_contour_img = np.zeros_like(image)
        cv2.drawContours(outer_contour_img, [approx_list[0]],-1,(255,255,255),-1)
        outer_contour_img = 1 - outer_contour_img

        # for below delete contour after algorithm
        delete_contour_img = np.zeros_like(image)
        cv2.drawContours(delete_contour_img, [approx_list[0]],-1,(255,255,255),3)

        #outer - inner
        oringmask = outer_contour_img- inner_contour_img

        mask_image = oringmask*image
        #mask_image = 1 - mask_image
        image[oringmask != 1] =255
        #================================法1 直接用threshold==============
        
        ret, thresh1 = cv2.threshold(image, 85, 255, cv2.THRESH_BINARY_INV)
        #dilate消除黑洞
        kernel = np.ones((5,5),np.uint8)  
        thresh1 = cv2.dilate(thresh1,kernel,iterations = 1)

        _ , contours, hierarchy = cv2.findContours(thresh1,cv2.RETR_TREE,cv2.CHAIN_APPROX_NONE)
        inner_contours_final = []
        approx_list = []
        index = []
        for i in range(np.array(contours).shape[0]):
            #if (hierarchy[0][i][3] >= 0):
                if(cv2.contourArea(contours[i])<20000 and cv2.contourArea(contours[i])>= 0):
                    #cv2.drawContours(backtorgb, contours, i, (0, 255, 0), 1, 8);
                    #inner_contours_final.append(contours[i])
                    epsilon = 0.0000# * cv2.arcLength(contours[i],True)
                    index.append(i)
                    approx = cv2.approxPolyDP(contours[i], epsilon, False)
                    #print(cv2.contourArea(approx))
                    approx_list.append(approx)
                    #print(np.array(approx_list).shape)
                    cv2.polylines(backtorgb, [approx], True, (255,0, 0), 3)
                    OK_NG_flag = 1

        end = time.time()
        print(end - start)

        return OK_NG_flag, cv2.cvtColor(backtorgb,cv2.COLOR_RGB2GRAY)                    

