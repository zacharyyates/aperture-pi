FROM microsoft/dotnet:2.1-sdk as build

# TODO -- this should be it's own phase based on something else...
ENV DEBIAN_FRONTEND noninteractive
# install build tools
RUN apt-get update && \
    apt-get -y install devscripts build-essential debhelper git cmake bison flex libssl-dev libpcap-dev

WORKDIR /src
# clone and build opencv dependency
RUN git clone https://github.com/opencv/opencv.git
WORKDIR /src/opencv
RUN cmake .

# clone and build opencvsharp dependency
RUN git clone https://github.com/shimat/opencvsharp.git
WORKDIR /src/opencvsharp
RUN git submodule update --init --recursive
RUN cmake src

WORKDIR /app
# copy sln and csproj to restore as a distinct layer
COPY src/*.sln .
COPY src/AperturePi.Controller/*.csproj ./AperturePi.Controller/
RUN dotnet restore

# copy everything else and build
COPY src/. .
WORKDIR /app/AperturePi.Controller
RUN dotnet build

FROM build as publish
WORKDIR /app/AperturePi.Controller
RUN dotnet publish -c Release -o out

#FROM mohaseeb/raspberrypi3-python-opencv as opencv

FROM microsoft/dotnet:2.1-runtime as runtime
WORKDIR /app
COPY --from=publish /app/AperturePi.Controller/out ./
# RUN copy dll/x64/.* .
# # copy opencv compiled binaries to this image
# COPY --from=opencv /usr/local/include/opencv /usr/local/include/opencv/
# COPY --from=opencv /usr/local/include/opencv2 /usr/local/include/opencv2/

#ENTRYPOINT ["dotnet", "AperturePi.Controller.dll"]
ENTRYPOINT tail -f /dev/null
