#!/usr/bin/env python3
"""
Q-Verse / USDTgVerse tarzı dosya üst bilgisi — Nyrvexa C# kaynaklarına uygular (idempotent).

Kullanım: depo kökünden  python3 scripts/apply_nyrvexa_file_headers.py
"""
from __future__ import annotations

import os
import re
import sys

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

AUTHOR_ORG = "USDTG GROUP TECHNOLOGY LLC"
DEVELOPER = "Irfan Gedik"

# Tarih: toplu güncelleme veya yeni dosya ekleme
DATE_ISO = "2026-04-26"

# İlk \"## [x.y.z]\" sürümü CHANGELOG.md içinden; yoksa yedek.
LICENSE_BLOCK = (
    " *   Proprietary. All rights reserved. See LICENSE in the repository root."
)

OLD_COPYRIGHT = "Copyright (c) 2026 Irfan Gedik"


def read_version() -> str:
    p = os.path.join(ROOT, "CHANGELOG.md")
    if not os.path.isfile(p):
        return "0.1.6"
    with open(p, encoding="utf-8") as f:
        m = re.search(r"^## \[([0-9.]+)\]", f.read(), re.MULTILINE)
    return m.group(1) if m else "0.1.6"


VERSION = read_version()


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


def strip_existing_header(text: str) -> str:
    s = text
    if s.startswith("\ufeff"):
        s = s[1:]

    # Yeni: /* ===== ... */ blok
    t = s.lstrip()
    if t.startswith("/* ="):
        end = t.find("*/")
        if end != -1:
            s = t[end + 2 :].lstrip("\n")
            t = s.lstrip()

    # Eski: // Copyright ...
    if t.startswith(f"// {OLD_COPYRIGHT}"):
        lines = t.splitlines(keepends=True)
        i = 0
        while i < len(lines) and (lines[i].startswith("//") or lines[i].strip() == ""):
            i += 1
        s = "".join(lines[i:])

    return s


def _clean_doc_comment(s: str) -> str:
    t = re.sub(r"<[^>]+>", " ", s)
    t = re.sub(r"///\s*", " ", t)
    t = re.sub(r"\s+", " ", t).strip()
    t = t.replace("—", "-")
    if len(t) > 220:
        t = t[:217] + "..."
    return t


def extract_description(relpath: str, body: str) -> str:
    # 1) Tip bildiriminin (class/struct/…) hemen üstündeki <summary> — yöntem özetlerini
    #    (public bool vs.) yakala ma.
    m = re.search(
        r"///\s*<summary>\s*([\s\S]+?)\s*</summary>\s*(?:\r?\n\s*)"
        r"(?:\[[\s\S]*?\]\s*(?:\r?\n)*)*"
        r"(?:(?:public|internal|file)\s+)?"
        r"(?:(?:static|sealed|abstract|new|readonly|required|unsafe|partial)\s+)*"
        r"(?:class|struct|interface|enum|record)\s+",
        body,
    )
    if m and m.group(1).strip():
        return _clean_doc_comment(m.group(1))
    base, _ = os.path.splitext(os.path.basename(relpath))
    return f"{base} — Nyrvexa modülü (ayrıntı kaynakta)."


def format_desc_for_block(desc: str) -> str:
    desc = re.sub(r"\s+", " ", desc.replace("\n", " ")).strip()
    if not desc:
        return " *   (Nyrvexa source file.)"
    max_len = 75
    lines: list[str] = []
    while desc:
        if len(desc) <= max_len:
            lines.append(" *   " + desc)
            break
        cut = desc.rfind(" ", 0, max_len + 1)
        if cut < 24:
            cut = max_len
        lines.append(" *   " + desc[:cut].rstrip())
        desc = desc[cut:].lstrip()
    return "\n".join(lines)


def build_block_header(relpath: str, desc: str) -> str:
    fpath = relpath.replace(os.sep, "/")
    desc_fmt = format_desc_for_block(desc)
    return f"""/* =============================================================================
 * File:           {fpath}
 * Author:         {AUTHOR_ORG}
 * Developer:      {DEVELOPER}
 * Created Date:   {DATE_ISO}
 * Last Update:    {DATE_ISO}
 * Version:        {VERSION}
 * 
 * Description:
{desc_fmt}
 * 
 * License:
{LICENSE_BLOCK}
 * ============================================================================= */

"""


def process_file(path: str) -> bool:
    with open(path, encoding="utf-8") as f:
        raw = f.read()

    body = strip_existing_header(raw)
    relpath = os.path.relpath(path, ROOT)
    desc = extract_description(relpath, body)
    header = build_block_header(relpath, desc)
    new_text = header + body

    if new_text == raw:
        return False
    with open(path, "w", encoding="utf-8", newline="\n") as f:
        f.write(new_text)
    return True


def main() -> int:
    bases = [os.path.join(ROOT, "Assets"), os.path.join(ROOT, "tools")]
    n = 0
    for base in bases:
        if not os.path.isdir(base):
            continue
        for path in iter_cs_files(base):
            if process_file(path):
                print(path.replace(ROOT + os.sep, ""))
                n += 1
    print(f"Updated {n} file(s).", file=sys.stderr)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
