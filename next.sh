#!/bin/bash
set -x
SOURCE_LAB="Lab 4 - Work with routing and error handling"
SOURCE_PROJ="CIS341-lab4"
DEST_LAB="Lab 5 - Implement Controller structure"
DEST_PROJ="CIS341-lab5"

git clean -fdx
mv \
    "${SOURCE_LAB}" \
    "${DEST_LAB}" \
;
git restore "${SOURCE_LAB}"
cd "${DEST_LAB}"
cat init.sh | \
    sed "s/${SOURCE_PROJ}/${DEST_PROJ}/g" | \
    tee _init.sh \
    && mv _init.sh init.sh \
    && chmod +x init.sh \
;
./init.sh
mv ${SOURCE_PROJ}/scripts ${DEST_PROJ}/scripts
rm -rf ${SOURCE_PROJ}
cd ${DEST_PROJ} && dotnet build && scripts/cleanupcode.sh
