#!/bin/bash
set -x
git clean -fdx
./rebuildwwwroot.sh
zip -9r Volm.David.CIS_341-01.Lab-03.zip CIS341-lab3/
