#!/bin/bash
set -x
dotnet new mvc --name "_temp"
mv _temp/wwwroot/lib CIS341-checkpoint2/wwwroot
rm -rf _temp
