#!/bin/bash
set -x
dotnet build && dotnet watch run --no-hot-reload
