FROM resin/rpi-python

RUN apt-get update && \
    apt-get upgrade -y

# build dependencies
# https://raspberrypi.stackexchange.com/questions/69169/how-to-install-opencv-on-raspberry-pi-3-in-raspbian-jessie
RUN apt-get install -y --no-install-recommends \
    build-essential \
    git \
    cmake \
    pkg-config \
    wget

# opencv dependencies
RUN apt-get install -y --no-install-recommends \
    libjpeg-dev \
    libtiff5-dev \
    libjasper-dev \
    libpng12-dev \
    libavcodec-dev \
    libavformat-dev \
    libswscale-dev \
    libv4l-dev \
    libxvidcore-dev \
    libx264-dev

# opencv optimizations
# https://www.pyimagesearch.com/2016/04/18/install-guide-raspberry-pi-3-raspbian-jessie-opencv-3/
RUN apt-get install -y --no-install-recommends \
    libatlas-base-dev \
    gfortran

# python3 and packages
# todo: install python 3.7
# https://www.scivision.co/compile-install-python-beta-raspberry-pi/
RUN apt-get install -y --no-install-recommends \
    python3-dev
RUN wget https://bootstrap.pypa.io/get-pip.py
RUN python3 get-pip.py
RUN pip3 install numpy

# clone opencv
ARG OPENCV_VERSION=3.4

WORKDIR /usr/src
RUN git clone https://github.com/opencv/opencv.git
WORKDIR opencv
RUN git checkout ${OPENCV_VERSION}

WORKDIR /usr/src
RUN git clone https://github.com/opencv/opencv_contrib.git
WORKDIR opencv_contrib
RUN git checkout ${OPENCV_VERSION}

# build opencv
# TODO: use a few different flags
WORKDIR /usr/src/opencv/build
RUN cmake \
    -D CMAKE_BUILD_TYPE=RELEASE \
    -D CMAKE_INSTALL_PREFIX=/usr/local \
    -D INSTALL_C_EXAMPLES=OFF \
    -D INSTALL_PYTHON_EXAMPLES=OFF \
    -D OPENCV_EXTRA_MODULES_PATH=/usr/src/opencv_contrib/modules \
    -D BUILD_EXAMPLES=OFF ..
RUN make -j -l 4
RUN make install
RUN ldconfig

ENTRYPOINT ["python3", "-V"]