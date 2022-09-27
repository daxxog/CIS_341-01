#!/bin/bash
_ALL=""
set -x
rm cleanupcode.Makefile
for source_code in $(find . | grep -v './obj' | grep '.cs' | grep -v '.csproj' | grep -v 'wwwroot/lib'); do
    _ID=$(uuidgen)
    _ALL="${_ID} ${_ALL}"
    printf ".PHONY: ${_ID}\n" | tee -a cleanupcode.Makefile
    printf "${_ID}:\n" | tee -a cleanupcode.Makefile
    printf "\tjb cleanupcode $source_code\n" | tee -a cleanupcode.Makefile
done

printf ".PHONY: cleanupcode\n" | tee -a cleanupcode.Makefile
printf "cleanupcode: ${_ALL}\n" | tee -a cleanupcode.Makefile
cat cleanupcode.Makefile
make -f cleanupcode.Makefile cleanupcode -j$(nproc)
