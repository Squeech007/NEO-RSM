#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)"
cd "$ROOT"

SCRIPT="$ROOT/build/Reedit Ship Management/script.cs"

printf '%s\n' '=== BUILDING RSM ==='
dotnet build

if [ ! -f "$SCRIPT" ]; then
    echo
    echo "ERROR: Generated RSM script was not found:"
    echo "$SCRIPT"
    exit 1
fi

printf '\n%s\n' '=== COPYING RSM SCRIPT ==='

if command -v wl-copy >/dev/null 2>&1; then
    wl-copy < "$SCRIPT"
    CLIP="wl-copy"
elif command -v xclip >/dev/null 2>&1; then
    xclip -selection clipboard < "$SCRIPT"
    CLIP="xclip"
elif command -v xsel >/dev/null 2>&1; then
    xsel --clipboard --input < "$SCRIPT"
    CLIP="xsel"
else
    echo "ERROR: No supported clipboard utility found."
    echo "Looked for: wl-copy, xclip, xsel"
    exit 2
fi

SIZE="$(wc -c < "$SCRIPT")"
CHARS="$(wc -m < "$SCRIPT")"

echo "Copied successfully using: $CLIP"
echo "Script: $SCRIPT"
echo "Bytes : $SIZE"
echo "Chars : $CHARS"
