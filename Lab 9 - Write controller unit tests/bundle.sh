#!/bin/bash
set -x
git clean -fdx
./rebuildwwwroot.sh
zip -9r Volm.David.CIS_341-01.Lab-09.zip CIS341-lab9/
mv Volm.David.CIS_341-01.Lab-09.zip /tmp
echo rsync -vP daxxog@dxg-linuxbook-btw.local:/tmp/Volm.David.CIS_341-01.Lab-09.zip .
