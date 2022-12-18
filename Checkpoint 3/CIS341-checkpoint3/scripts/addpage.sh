#!/bin/bash
if [ "$#" -ne 1 ]; then
    echo "usage: scripts/addpage.sh <page name>"
    exit 1
fi

set -x
dotnet new page \
    --name ${1} \
    --namespace $(cat Pages/Index.cshtml.cs|grep namespace|sed 's/namespace //g;s/;//g') \
    --output Pages \
;
