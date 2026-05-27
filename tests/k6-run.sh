#!/usr/bin/bash

echo "Start" && date

type=$1

echo $type

source k6.env

if [ "$1" == "rest" ]; then
    k6 run test-rest.js
else
    k6 run test-grpc.js
fi

echo "end" && date