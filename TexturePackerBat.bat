@echo off
TexturePacker Res/Atlas/%1 --sheet Assets/Atlas/%1.png --data Assets/Atlas/%1.txt --trim-sprite-names --format unity --no-trim
pause