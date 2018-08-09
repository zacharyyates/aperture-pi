import cv2
import sys

print("Starting stream...")
# capture the default webcam
stream = cv2.VideoCapture(0)

while(stream.isOpened()):
    running, frame = stream.read()
    if running==True:        
        sys.stdout.write(frame.tostring())
    else:
        break

stream.release()
print("Stream closed")