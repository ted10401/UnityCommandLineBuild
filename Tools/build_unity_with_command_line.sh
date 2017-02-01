#!/bin/bash

UNITY_PATH=/Applications/Unity/Unity.app/Contents/MacOS/Unity
PROJECT_PATH=/Users/ted/SideProjects/UnityCommandLineBuild
BUILD_LOG_PATH=${PROJECT_PATH}/build.log
DESTINATION_PATH=/Users/ted/Desktop/

$UNITY_PATH -quit -batchmode -projectPath ${PROJECT_PATH} -executeMethod BuildTool.Build -logFile ${BUILD_LOG_PATH} -destinationPath ${DESTINATION_PATH}