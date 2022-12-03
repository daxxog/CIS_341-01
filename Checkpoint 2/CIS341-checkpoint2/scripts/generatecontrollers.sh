#!/bin/bash
set -x

# https://gist.github.com/szydan/b225749445b3602083ed?permalink_comment_id=1941065#gistcomment-1941065
_CONTROLLER_NS=$(cat Views/_ViewImports.cshtml | head -n 1 | sed 's/@using //g' | tr -d '\n' | awk '{ gsub(/\xef\xbb\xbf/,""); print }').Controllers

for controller in $(ls Data/Entities | sed 's/.cs//g'); do
    # clean build the controller, so delete it first
    rm -f Controllers/${controller}Controller.cs

    # use aspnet-codegenerator to build the controller
    dotnet-aspnet-codegenerator \
        -p $(ls | grep .csproj | tr -d '\n') \
        controller \
        -f \
        -name ${controller}Controller \
        -m ${controller} \
        -outDir Controllers \
        -sqlite \
        -async \
        -dc SqliteContext \
        --layout _Layout \
        -namespace ${_CONTROLLER_NS} \
    ;
done

scripts/cleanupcode.sh
