#!/bin/bash
set -x
PROJECT_NAME="CIS341-lab7"
dotnet new mvc --name "${PROJECT_NAME}"
cat ./${PROJECT_NAME}/Program.cs | \
    sed 's/app\.UseHttpsRedirection();/\/\/ app\.UseHttpsRedirection(); \/\/ nahh/g' | \
    tee ./${PROJECT_NAME}/_Program.cs \
    && mv ./${PROJECT_NAME}/_Program.cs ./${PROJECT_NAME}/Program.cs \
;
