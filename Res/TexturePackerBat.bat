@echo off
TexturePacker Res/Atlas/%1 --sheet %1.png --data %1.txt --trim-sprite-names --format unity --no-trim
pause