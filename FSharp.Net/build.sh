#!/bin/bash
 # use mono
mono .paket/paket.bootstrapper.exe
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi

mono .paket/paket.exe update
exit_code=$?
if [ $exit_code -ne 0 ]; then
  exit $exit_code
fi
/home/travis/.dotnet/tools/fake run build.fsx

