#!/usr/bin/env python3
"""Prepend Nyrvexa copyright header to C# sources (idempotent)."""
from __future__ import annotations

import os
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

MARKER = "Copyright (c) 2026 Irfan Gedik"

HEADER = """// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


"""


def should_skip_dir(name: str) -> bool:
    return name in ("obj", "bin", "Library", "Temp", "Build", "Builds", ".git")


def iter_cs_files(base: str) -> list[str]:
    out: list[str] = []
    for dirpath, dirnames, filenames in os.walk(base):
        dirnames[:] = [d for d in dirnames if not should_skip_dir(d)]
        for fn in filenames:
            if fn.endswith(".cs"):
                out.append(os.path.join(dirpath, fn))
    return sorted(out)


def main() -> int:
    bases = [os.path.join(ROOT, "Assets"), os.path.join(ROOT, "tools")]
    n = 0
    for base in bases:
        if not os.path.isdir(base):
            continue
        for path in iter_cs_files(base):
            with open(path, encoding="utf-8") as f:
                text = f.read()
            if MARKER in text:
                continue
            if text.startswith("\ufeff"):
                text = text[1:]
            with open(path, "w", encoding="utf-8", newline="\n") as f:
                f.write(HEADER + text)
            n += 1
            print(path.replace(ROOT + os.sep, ""))
    print(f"Updated {n} file(s).", file=sys.stderr)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
