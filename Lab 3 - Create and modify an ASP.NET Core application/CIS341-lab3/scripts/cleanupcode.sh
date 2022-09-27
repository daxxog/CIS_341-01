#!/bin/bash
set -x
for source_code in $(find . | grep '.cs' | grep -v '.csproj'); do
    jb cleanupcode $source_code
done
