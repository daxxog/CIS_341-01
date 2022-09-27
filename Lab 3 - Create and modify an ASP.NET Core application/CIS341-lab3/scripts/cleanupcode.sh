#!/bin/bash
set -x
for source_code in $(find . | grep -v './obj' | grep '.cs' | grep -v '.csproj' | grep -v 'wwwroot/lib'); do
    jb cleanupcode $source_code
done
