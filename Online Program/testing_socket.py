import os
import sys
import random
import math
import re
import time
import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import skimage
import cv2
import socket
import threading
import io
from PIL import Image
import signal
import sys
#===============detector
from Stop3Detector import Stop3Detector
from Stop4Detector import Stop4Detector


Debug_Flag = 0

Image_Number = 1
WaitLimit = 2.0

#==============================================thread==================================================
exitFlag = 0
ReceiveByte_A_Time = 16384
#image_data =  bytearray()

#=====================Create a TCP/IP socket
sock1 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Bind the socket to the port
server_address1 = ('localhost', 1200)
print('starting up on %s port %s' % server_address1)
sock1.bind(server_address1)
# Listen for incoming connections
sock1.listen(1)

sock2 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Bind the socket to the port
server_address2 = ('localhost', 600)
print('starting up on %s port %s' % server_address2)
sock2.bind(server_address2)
# Listen for incoming connections
sock2.listen(1)

sock3 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Bind the socket to the port
server_address3 = ('localhost', 300)
print('starting up on %s port %s' % server_address3)
sock3.bind(server_address3)
# Listen for incoming connections
sock3.listen(1)

sock4 = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# Bind the socket to the port
server_address4 = ('localhost', 150)
print('starting up on %s port %s' % server_address4)
sock4.bind(server_address4)
# Listen for incoming connections
sock4.listen(1)
#=============================

threads = []
threadLock = threading.Lock()
WaitToDetect_Queue_Lock = threading.Lock()

#================================================================================================

number_of_image = 50000
Now_Image_Count = 0


#Global Variable Pool
Wait_To_Detect_Pool = []
'''
OK_NG_List = []
Image_send_list = []
#Image_send_count = []
'''

Stop_List = []
#Stop_send_List = []

OK_NG_List1 = []
Image_send_list1 = []
Image_send_count1 = []
Stop_Send_List1 = []

OK_NG_List2 = []
Image_send_list2 = []
Image_send_count2 = []
Stop_Send_List2 = []

OK_NG_List3 = []
Image_send_list3 = []
Image_send_count3 = []
Stop_Send_List3 = []

OK_NG_List4 = []
Image_send_list4 = []
Image_send_count4 = []
Stop_Send_List4 = []


PriorityQueue = [-1,-1,-1,-1]#從前到後表示是 priority 高~低的人
Stop_Total_Count = [2**32-1,0,0,0,0] # index 用 1 2 3 4 比較好處理，故index 0 是用不到的位置
CPU_send_schedule = []



#receive image thread
class myThread1 (threading.Thread):
    def __init__(self, threadID, name,ConnectionFrom):
        threading.Thread.__init__(self)
        self.threadID = threadID 
        self.name = name
        self.count = 0
        self.AccuByte = 0
        self.ConnectionFrom = ConnectionFrom
        self.image_data = bytearray()

    def run(self):
        
        global Stop_Send_List1
        global Image_send_list1
        global Image_send_count1
        global OK_NG_List1

        global Stop_Send_List2
        global Image_send_list2
        global Image_send_count2
        global OK_NG_List2

        global Stop_Send_List3
        global Image_send_list3
        global Image_send_count3
        global OK_NG_List3

        global Stop_Send_List4
        global Image_send_list4
        global Image_send_count4
        global OK_NG_List4
        
        if(self.threadID == 1):
            Image_send_list = Image_send_list1
            Image_send_count = Image_send_count1
            OK_NG_List = OK_NG_List1
            
        elif(self.threadID == 3):
            Image_send_list = Image_send_list2
            Image_send_count = Image_send_count2
            OK_NG_List = OK_NG_List2
        
        elif(self.threadID == 5):
            Image_send_list = Image_send_list3
            Image_send_count = Image_send_count3
            OK_NG_List = OK_NG_List3
        
        elif(self.threadID == 7):
            Image_send_list = Image_send_list4
            Image_send_count = Image_send_count4
            OK_NG_List = OK_NG_List4
        else:
            print("error: unexpected thread ID")
                

        FinishedFileList = []    
        count = 0
        ImageList = []
        FileNameList = []
        StartWait = 0
        EndWait = 0
        

        
        for i in range(number_of_image):
            if(Debug_Flag):
                print('============================== New Image')
            #print("Now iter: ",i)
            global ReceiveByte_A_Time
            data = self.ConnectionFrom.recv(8)# receive一個數字，共8byte
            
            #print('received "%s"' % (int.from_bytes(data, "little")))

            recvByte = int(int.from_bytes(data, "little"))
            #number_recv = int((int.from_bytes(data, "little"))/16384)+1
            #print('should recv: ',end = ' ')
            #print(number_recv ,end = ',  Recv:')
            if(Debug_Flag):
                print(recvByte)
            #======reeive 從哪一站來的
            data = self.ConnectionFrom.recv(8)# receive一個數字，共8byte
            From_Which_Stop = int(int.from_bytes(data, "little"))
            if(Debug_Flag):
                print('Stop From:',From_Which_Stop)
            #======
            while(True):
                time.sleep(0)
                data = self.ConnectionFrom.recv(ReceiveByte_A_Time)
                
                #print('received "%s"' % data, end = ' ')
                #print("data len:"+str(len(data)),end = ' ')
                #print("data: "+ str(AccuByte), end = ' i = ')
                #print(i)
                
                self.count+=1
                self.image_data.extend(data)
                self.AccuByte += len(data)
                
                #if(i == 0 and len(data)!= 0):
                #    print(len(data))
                
                #image.save(savepath)
                 
                global Now_Image_Count    
                if (recvByte - self.AccuByte) <= ReceiveByte_A_Time :
                    if(Debug_Flag):
                        print("Byte Info:" ,recvByte,self.AccuByte,len(self.image_data))
                    data = self.ConnectionFrom.recv(recvByte - self.AccuByte)
                    self.image_data.extend(data)
                    #print("image data length ",len(image_data))
                    #connection.close()
                    image = np.array(Image.open(io.BytesIO(self.image_data)))
                    
                    #image = cv2.imdecode(image_data, cv2.IMREAD_COLOR)
                    #image = Image.open(io.BytesIO(image_data))

                    #===========================RGB or Gray==============================
                    print("===shape: ", image.shape)
                    image = np.expand_dims(image, axis = -1)
                    '''
                    if len(image.shape) != 3 or image.shape[2] != 3:
                        image = np.stack((image,) * 3, -1)
                    '''
                    #====================================================================                   


                    WaitToDetect_Queue_Lock.acquire()
                    Wait_To_Detect_Pool.append(image)
                    Stop_List.append(From_Which_Stop)
                    #print("StopList" , Stop_List," New:",From_Which_Stop)
                    #print("Ready queue=============",Stop_List)
                    WaitToDetect_Queue_Lock.release()

                    break #break from while loop

            #print(count)
            self.count = 0
            self.AccuByte = 0
            count = 0
            self.image_data =  bytearray()
            #if(i==1):
            #    assert(1==2)
            #data = connection.sendall("Hello")

# send image thread
class myThread2 (threading.Thread):
    def __init__(self, threadID, name, ConnectionFrom):        
        threading.Thread.__init__(self)
        self.threadID = threadID
        self.name = name
        self.ConnectionFrom = ConnectionFrom
    def run(self):          
        
        global Stop_Send_List1
        global Image_send_list1
        global Image_send_count1
        global OK_NG_List1

        global Stop_Send_List2
        global Image_send_list2
        global Image_send_count2
        global OK_NG_List2

        global Stop_Send_List3
        global Image_send_list3
        global Image_send_count3
        global OK_NG_List3

        global Stop_Send_List4
        global Image_send_list4
        global Image_send_count4
        global OK_NG_List4
        global CPU_send_schedule


        
        
        if(self.threadID == 2):#stop1
            Stop_send_List = Stop_Send_List1
            Image_send_list = Image_send_list1
            Image_send_count = Image_send_count1
            OK_NG_List = OK_NG_List1
            
        elif(self.threadID == 4):
            Stop_send_List = Stop_Send_List2
            Image_send_list = Image_send_list2
            Image_send_count = Image_send_count2
            OK_NG_List = OK_NG_List2
 
        elif(self.threadID == 6):
            Stop_send_List = Stop_Send_List3
            Image_send_list = Image_send_list3
            Image_send_count = Image_send_count3
            OK_NG_List = OK_NG_List3
        
        elif(self.threadID == 8):
            Stop_send_List = Stop_Send_List4
            Image_send_list = Image_send_list4
            Image_send_count = Image_send_count4
            OK_NG_List = OK_NG_List4
        else:
            print("error: unexpected thread ID")
                
        FinishSend = 0
        #for i in range(10):
        
        while((FinishSend < number_of_image)):
            
            
            
            #num_bytes = i

            #print('Image_send_count', Image_send_count)
            
            if(len(CPU_send_schedule) == 0):
                time.sleep(0)
                pass
            
            elif(len(Image_send_count) == 0):
                time.sleep(0)
                #print('0', end = ' ')
                pass
            elif(self.threadID == (CPU_send_schedule[0] << 1)):# if there is image ready to sends
                print("Send:========================",CPU_send_schedule)
                #print("NowIndex:",NowSendIndex)
                #print('send')
                print(len(Image_send_count), " , ", len(Image_send_list))
                threadLock.acquire()
                CPU_send_schedule = CPU_send_schedule[1:] 
                Image = Image_send_list[-1]
                Image_send_list[:] = Image_send_list[:-1]
                num_bytes = 8

                Stop = Stop_send_List[-1]
                Stop_send_List[:] = Stop_send_List[:-1]
                #print("Global , Local: ",Stop_send_List,Stop_Send_List1,Stop_Send_List2)
                #num_bytes = num_bytes.to_bytes(8, byteorder='big')
                #Image = Image[:,:,0]
                #//python send rgb image

                #plt.imsave('{}.jpg'.format(FinishSend),Image)
                if(Debug_Flag):
                    print(Image.shape)

                #num_bytes = Image.tobytes()
                is_success, im_buf_arr = cv2.imencode(".jpg", Image)
                num_bytes = im_buf_arr.tobytes()
                
                totalbyte = int(len(num_bytes))
                if(Debug_Flag):
                    print("total byte :",totalbyte," __from:",Stop)

                #print(Image)

                Accu_Send_Time = 0
                Send_One_Time  = 16384

                Total_Send_Time = math.ceil(totalbyte/Send_One_Time)


                #===================send the information of the image==============================

                byte_Totalbyte = totalbyte.to_bytes(8, byteorder='little')
                #print("byte_Totalbyte: ",byte_Totalbyte," ___ ",type(byte_Totalbyte)," ___ ",len(byte_Totalbyte))
                byte_Stop = Stop.to_bytes(8, byteorder='little')
                #print("byte_Stop: ",byte_Stop," ___ ",type(byte_Stop)," ___ ",len(byte_Stop))
                byte_OK_NG_Flag = int(OK_NG_List[-1]).to_bytes(8, byteorder='little')
                OK_NG_List[:] = OK_NG_List[:-1]
                #print("byte_OK_NG_Flag: ",byte_OK_NG_Flag," ___ ",type(byte_OK_NG_Flag)," ___ ",len(byte_OK_NG_Flag))

                Send_metadata = byte_Totalbyte + byte_Stop + byte_OK_NG_Flag
                #print("Send_metadata: ",Send_metadata," ___ ",type(Send_metadata)," ___ ",len(Send_metadata))
                self.ConnectionFrom.send(Send_metadata)
                
                #=================================================================================
                '''
                self.ConnectionFrom.send(totalbyte.to_bytes(8, byteorder='little'))
                
                # 告訴 C# 是來自哪一站的圖片
                self.ConnectionFrom.send(Stop.to_bytes(8, byteorder='little'))
                
                #============send whether this testing image is OK or NG =========================
                print("OK NG: ",int(OK_NG_List[-1]))
                self.ConnectionFrom.send(int(OK_NG_List[-1]).to_bytes(8, byteorder='little'))
                OK_NG_List[:] = OK_NG_List[:-1]
                '''
                #=================================================================================
                while(Accu_Send_Time <= Total_Send_Time):
                    time.sleep(0)
                    sendmsg = num_bytes[Send_One_Time * Accu_Send_Time : min(Send_One_Time*(Accu_Send_Time+1),len(num_bytes))]
                    #print(sendmsg)
                    #print(len(sendmsg))
                    self.ConnectionFrom.send(sendmsg)
                    Accu_Send_Time+=1



                if(Debug_Flag):
                    print('SendOver, count: ',Total_Send_Time)
                FinishSend += 1
                Image_send_count[:] = Image_send_count[:-1]
                
                #print("Local variable value: ", len(Image_send_count),", ",len(Image_send_list),", ",len(Stop_send_List),", ",len(OK_NG_List))
                #print("Global variable value: ",len(Image_send_count1),", ",len(Image_send_list1),", ",len(Stop_Send_List1),", ",len(OK_NG_List1))

                threadLock.release()    
            

class DetectThread (threading.Thread):
    def __init__(self, threadID, name):
        threading.Thread.__init__(self)
        self.threadID = threadID
        self.name = name
        self.stop3_detector = Stop3Detector()
        self.stop4_detector = Stop4Detector()
        image1 = cv2.imread('stop3.jpg', cv2.IMREAD_GRAYSCALE)
        image2 = cv2.imread('stop4.jpg', cv2.IMREAD_GRAYSCALE)
        
        image1 = image1.reshape(image1.shape[0], image1.shape[1], 1)
        image2 = image2.reshape(image2.shape[0], image2.shape[1], 1)

        for i in range(3):#Modify here
            
            result, final_image = self.stop3_detector.detect(image1)
            result, final_image = self.stop4_detector.detect(image2)
        

        print('======================dummy data detect complete====================================')

    def run(self):
    
        global Wait_To_Detect_Pool
        global Stop_Send_List1
        global Image_send_list1
        global Image_send_count1
        global OK_NG_List1
        global Stop_Send_List2
        global Image_send_list2
        global Image_send_count2
        global OK_NG_List2
        global Stop_Send_List3
        global Image_send_list3
        global Image_send_count3
        global OK_NG_List3
        global Stop_Send_List4
        global Image_send_list4
        global Image_send_count4
        global OK_NG_List4
        global CPU_send_schedule
        #========================detect and append image to sendqueue=======
        #print("Wait_To_Detect_Pool: ",np.array(Wait_To_Detect_Pool).shape)

        while(True):
            #time.sleep(0)

            if(len(Wait_To_Detect_Pool)>0):
                #print("==========Variable List:===============")
                #print("Stop1 variable: ", Stop_Send_List1,", ",Image_send_list1,", ",Image_send_count1,", ",OK_NG_List1)
                #print("Stop2 variable: ", Stop_Send_List2,", ",Image_send_list2,", ",Image_send_count2,", ",OK_NG_List2)
                #print("=======================================")
                #if(Now_Image_Count >= Image_Number):# Ready to detect the defect on the batch                    
                
                # get the image and the stop number from the pool
                WaitToDetect_Queue_Lock.acquire()
                #=================================================算出下一個要detect誰
                priority_index = np.argsort(Stop_Total_Count)
                #print("==============================================",priority_index,"==========================================")

                for i in range(4):
                    nowindex = priority_index[i] #看stop1~stop4
                    try:
                        Incoming_Process_index = Stop_List.index(nowindex)
                    except ValueError:
                        Incoming_Process_index = -1
                    
                    if(Incoming_Process_index != -1):
                        break


                if(Debug_Flag):
                    print("======================Stop_Total_Count ",Stop_Total_Count,"stoplist ",Stop_List," and it is stop",nowindex," priority index:",priority_index,"==============")
                #=================================================
                ImageDetectList = [Wait_To_Detect_Pool[Incoming_Process_index]]#因為只有一個圖片，直接取-1會取出元素，但這個model吃的是list形式
                #print("len of image list:" , len(ImageDetectList))
                Wait_To_Detect_Pool[:] = Wait_To_Detect_Pool[0:Incoming_Process_index]+Wait_To_Detect_Pool[Incoming_Process_index+1:]
                From_Which_Stop = Stop_List[Incoming_Process_index]
                Stop_List[:] = Stop_List[0:Incoming_Process_index]+Stop_List[Incoming_Process_index+1:]
                WaitToDetect_Queue_Lock.release()
                
                start = time.time()

                if(From_Which_Stop == 1):
                    if(Debug_Flag):
                        print("Detect by model1 Now index = " , nowindex)
                    # results, final_image = model1.detect(ImageDetectList, verbose=1)
                elif(From_Which_Stop == 2):
                    if(Debug_Flag):
                        print("Detect by model2 = " , nowindex)
                    # results, final_image = model2.detect(ImageDetectList, verbose=1)
                elif(From_Which_Stop == 3):
                    if(Debug_Flag):
                        print("Detect by model3 = " , nowindex)
                    
                    result, final_image = self.stop3_detector.detect(ImageDetectList[-1])
                    # results, final_image = model3.detect(ImageDetectList, verbose=1)
                elif(From_Which_Stop == 4):
                    if(Debug_Flag):
                        print("Detect by model4 = " , nowindex)

                    result, final_image = self.stop4_detector.detect(ImageDetectList[-1])
                    # results, final_image = model4.detect(ImageDetectList, verbose=1)
                else:
                    if(Debug_Flag):
                        print("Error: From the undefined stop")

                end = time.time()
                if(Debug_Flag):
                    print(end - start)                
                
                for i in range(Image_Number):
                    
                    Image_send = np.array(final_image)


                    OK_NG_flag = -1
                    if(result == 0):
                        OK_NG_flag = 0
                        
                    else:
                        OK_NG_flag = 1
                    
                    ###########
                    #plt.close()
                    ###########
                    if(From_Which_Stop == 1):
                        OK_NG_List = OK_NG_List1
                        Image_send_list = Image_send_list1
                        Stop_send_List = Stop_Send_List1
                        Image_send_count = Image_send_count1
                        Stop_Total_Count[1] += 1
                        if(Debug_Flag):
                            print("Detection 1 over ")

                    elif(From_Which_Stop == 2):
                        OK_NG_List = OK_NG_List2
                        Image_send_list = Image_send_list2
                        Stop_send_List = Stop_Send_List2
                        Image_send_count = Image_send_count2
                        Stop_Total_Count[2] += 1
                        if(Debug_Flag):
                            print("Detection 2 over ")

                    elif(From_Which_Stop == 3):
                        OK_NG_List = OK_NG_List3
                        Image_send_list = Image_send_list3
                        Stop_send_List = Stop_Send_List3
                        Image_send_count = Image_send_count3
                        Stop_Total_Count[3] += 1
                        if(Debug_Flag):
                            print("Detection 3 over ")

                    elif(From_Which_Stop == 4):
                        OK_NG_List = OK_NG_List4
                        Image_send_list = Image_send_list4
                        Stop_send_List = Stop_Send_List4
                        Image_send_count = Image_send_count4
                        Stop_Total_Count[4] += 1
                        if(Debug_Flag):
                            print("Detection 4 over ")

                    else:
                        if(Debug_Flag):
                            print("error: unexpected thread ID")
                    if(Debug_Flag):
                        print("========================================")

                    threadLock.acquire()
                    OK_NG_List.append(OK_NG_flag)
                    Image_send_list.append(Image_send)
                    Stop_send_List.append(From_Which_Stop)# 紀錄每一個image 是來自哪一站
                    CPU_send_schedule.append(From_Which_Stop)
                    if(Debug_Flag):
                        print(CPU_send_schedule)
                    Image_send_count.append(1)#每有一個圖片需要傳送就append 1 到這個list(in order to prevent the global variable and the local variable inconsistency)

                    #print("Local variable value: ", len(Image_send_count),", ",len(Image_send_list),", ",len(Stop_send_List),", ",len(OK_NG_List))
                    #print("Global variable value: ",len(Image_send_count1),", ",len(Image_send_list1),", ",len(Stop_Send_List1),", ",len(OK_NG_List1))
                    threadLock.release()
                    #====================================================================================================================

            
            
        






try:
    while True:
        try:
            # Wait for a connection
            print('waiting for a connection1')
            connection1, client_address1 = sock1.accept()
            print('connection1 from', client_address1)
            
            print('waiting for a connection2')
            connection2, client_address2 = sock2.accept()
            print('connection2 from', client_address2)

            print('waiting for a connection3')
            connection3, client_address3 = sock3.accept()
            print('connection3 from', client_address3)

            print('waiting for a connection4')
            connection4, client_address4 = sock4.accept()
            print('connection4 from', client_address4)

            # Create new threads
            thread1 = myThread1(1, "Thread-1", connection1)
            thread2 = myThread2(2, "Thread-2", connection1)
            
            thread3 = myThread1(3, "Thread-3", connection2)
            thread4 = myThread2(4, "Thread-4", connection2)

            thread5 = myThread1(5, "Thread-5", connection3)
            thread6 = myThread2(6, "Thread-6", connection3)

            thread7 = myThread1(7, "Thread-7", connection4)
            thread8 = myThread2(8, "Thread-8", connection4)

            thread_detection = DetectThread(9,"DetectionThread")

            # Start new Threads
            thread1.start()
            thread2.start()
            thread3.start()
            thread4.start()
            thread5.start()
            thread6.start()
            thread7.start()
            thread8.start()
            thread_detection.start()

            threads.append(thread1)
            threads.append(thread2)
            threads.append(thread3)
            threads.append(thread4)
            threads.append(thread5)
            threads.append(thread6)
            threads.append(thread7)
            threads.append(thread8)
            threads.append(thread_detection)

            # Wait for all threads to complete
            for t in threads:
                t.join()
                        
        except socket.timeout:
            print('pass')
            pass
        finally:
            # Clean up the connection
            print("finally")
            connection.close()
            
#In windows environment command prompt, use ctrl+Fn+pause(break) to raise a keyboard interrupt exception
except KeyboardInterrupt:
    print("close")
    connection.close()

    
    #assert(1==0)


print ("Exiting Main Thread")
