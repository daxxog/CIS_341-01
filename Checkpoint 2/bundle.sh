#!/bin/bash
set -x
git clean -fdx
rm ./CIS341-checkpoint2/global.json
./rebuildwwwroot.sh
zip -9r Volm.David.CIS_341-01.Checkpoint-2.zip CIS341-checkpoint2/
git restore ./CIS341-checkpoint2/global.json
mv Volm.David.CIS_341-01.Checkpoint-2.zip /tmp
echo rsync -vP daxxog@dxg-linuxbook-btw.local:/tmp/Volm.David.CIS_341-01.Checkpoint-2.zip .
