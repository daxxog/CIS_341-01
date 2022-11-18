#!/bin/bash
set -x

# SOURCE LAB
SOURCE_LABN="lab7"
SOURCE_LAB_ALT="Lab-07"
SOURCE_LAB="Lab 7 - Implement model binding and persistence"

# NEW LAB
DEST_LABN="lab8"
DEST_LAB_ALT="Lab-08"
DEST_LAB="Lab 8 - Add authentication and authorization"


SOURCE_PROJ="CIS341-${SOURCE_LABN}"
DEST_PROJ="CIS341-${DEST_LABN}"


git clean -fdx
cd "${SOURCE_LAB}"
./rebuildwwwroot.sh
cd ${SOURCE_PROJ}
scripts/cleanupcode.sh
cd ../..
git clean -fdx
mv \
    "${SOURCE_LAB}" \
    "${DEST_LAB}" \
;
git restore "${SOURCE_LAB}"
cd "${DEST_LAB}"
mv ${SOURCE_PROJ} ${DEST_PROJ}
cd ${DEST_PROJ}
mv ${SOURCE_PROJ}.csproj ${DEST_PROJ}.csproj
cd ..
for repf in $(grep -r -l ${SOURCE_LABN} .); do
    sed -i "s/${SOURCE_LABN}/${DEST_LABN}/g" $repf
done
for repf in $(grep -r -l ${SOURCE_LAB_ALT} .); do
    sed -i "s/${SOURCE_LAB_ALT}/${DEST_LAB_ALT}/g" $repf
done
./rebuildwwwroot.sh
cd ${DEST_PROJ} && dotnet build && scripts/1cleanupcode.sh Program.cs
