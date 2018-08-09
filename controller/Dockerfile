FROM mohaseeb/raspberrypi3-python-opencv
WORKDIR /app

# copy python project
COPY src/ .

ENTRYPOINT python app.py | cvlc --demux=rawvideo --rawvid-fps=25 --rawvid-width=1280 --rawvid-height=720  --rawvid-chroma=RV24 - --sout "#transcode{vcodec=h264,vb=200,fps=25,width=1280,height=720}:rtp{dst=127.0.0.1,port=8081,sdp=rtsp://127.0.0.1:8081/stream.sdp}"
