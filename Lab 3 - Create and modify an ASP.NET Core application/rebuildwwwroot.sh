#!/bin/bash
set -x
dotnet new razor --name "_temp"
mv _temp/wwwroot/lib CIS341-lab3/wwwroot
rm -rf _temp
