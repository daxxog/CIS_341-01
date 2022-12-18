#!/bin/bash
set -x
_FPATH="$(ls ${1} | sed 's/^/\&/g; s/&.\///g; s/\&//g')"
podman run \
    --rm \
    -t \
    -v "$(pwd)/${_FPATH}":/mnt/${_FPATH} \
    daxxog/jbcleanupcode cleanupcode \
    /mnt/${_FPATH} \
;
