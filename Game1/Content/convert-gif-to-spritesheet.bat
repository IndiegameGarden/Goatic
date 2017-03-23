@echo off
mkdir temp-frames
del /Q temp-frames\*.*
c:\Apps\Im\convert -coalesce %1.gif temp-frames/%%02d.png
c:\Apps\Im\montage -background transparent -tile 4x temp-frames/*.png %1.png
del /Q temp-frames\*.*
rmdir temp-frames