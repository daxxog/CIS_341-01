#!/bin/bash
_ALL=""
set -x
rm -f cleanupcode.Makefile
for source_code in $(find . | grep -v './obj' | grep -v './bin' | grep '.cs' | grep -v '.csproj' | grep -v 'wwwroot/lib'); do
    _ID=$(uuidgen)
    _ALL="${_ID} ${_ALL}"
    printf ".PHONY: ${_ID}\n" | tee -a cleanupcode.Makefile
    printf "${_ID}:\n" | tee -a cleanupcode.Makefile
    printf "\tscripts/1cleanupcode.sh $source_code\n" | tee -a cleanupcode.Makefile
done

printf ".PHONY: cleanupcode\n" | tee -a cleanupcode.Makefile
printf "cleanupcode: ${_ALL}\n" | tee -a cleanupcode.Makefile
cat cleanupcode.Makefile
make -f cleanupcode.Makefile cleanupcode -j$(nproc)

# cleanupcode breaks Index views
for view in Views/*/Index.cshtml; do git restore $view; done
