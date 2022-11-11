#!/bin/bash
set -x
dotnet new mvc --name "_temp"
mv _temp/wwwroot/lib CIS341-lab7/wwwroot
rm -rf _temp
