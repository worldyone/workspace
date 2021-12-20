#!/bin/bash

. "./atcoder/Scripts/Activate"

relativeFileDirname=$1
contestName=${relativeFileDirname:0:6}
problemName=${relativeFileDirname:6:1}

oj test -e 1e-6 -c "python ${contestName}/${problemName}/main.py" -d ${contestName}/${problemName}/test
