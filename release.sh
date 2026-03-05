#!/bin/bash
# release.sh

VERSION=$1  # e.g. ./release.sh 1.2.0

gh release create "v$VERSION" --title "v$VERSION" --generate-notes
