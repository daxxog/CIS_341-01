#!/bin/bash
set -x
git clean -fdx
rm ./CIS341-checkpoint3/global.json
./rebuildwwwroot.sh
zip -9r Volm.David.CIS_341-01.Checkpoint-3.zip CIS341-checkpoint3/
git restore ./CIS341-checkpoint3/global.json
mv Volm.David.CIS_341-01.Checkpoint-3.zip /tmp
echo rsync -vP daxxog@dxg-linuxbook-btw.local:/tmp/Volm.David.CIS_341-01.Checkpoint-3.zip .
