import cv2
import glob

img_list = []

for img in glob.glob("CCD-1/*.jpg"):
    img_list.append(cv2.imread(img,0))


import matplotlib.pyplot as plt
import numpy as np


gray = img_list[0]

ret, thresh1 = cv2.threshold(gray, 200, 255, cv2.THRESH_BINARY)
print(gray)

#print(thresh1)


contours, hierarchy = cv2.findContours(thresh1,cv2.RETR_TREE,cv2.CHAIN_APPROX_SIMPLE)

backtorgb = cv2.cvtColor(gray,cv2.COLOR_GRAY2RGB)

for i in range(np.array(contours).shape[0]):
    if(np.array(contours[i]).shape[0]>1000):
        cv2.drawContours(backtorgb,contours[i],-1,(0,0,255),3)
        print(i)

plt.figure(figsize=(20,20))
plt.imshow(backtorgb)
#cv2.imshow("img", gray)
#cv2.waitKey(0)


for i in range(np.array(contours).shape[0]):
        if(np.array(contours[i]).shape[0]>1000):
            print(np.array(contours[i]).shape)


import circle_fit as cf



cor = []

#for i in range(20):
#    cor.append(contours[35][i])
    
cor = np.array(contours[1])
cor = cor.reshape(cor.shape[0],cor.shape[2])
print(cor.shape)


xc,yc,r,_ = cf.least_squares_circle(cor)
print(xc,yc,r)