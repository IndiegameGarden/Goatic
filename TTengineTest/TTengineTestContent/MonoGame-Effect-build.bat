@echo off
rem
rem Build file to build Effect (.fx) using MonoGame content builder tool
rem
rem cd "c:\dev\TT\TTengine-5\TTengineTest\TTengineTestContent\"
"C:\Program Files (x86)\MSBuild\MonoGame\v3.0\Tools\MGCB.exe"^
 /outputDir:..\TTengineTest\%1Content^
 /intermediateDir:obj^
 /importer:EffectImporter^
 /processor:EffectProcessor^
 /build:Grayscale.fx^
 /build:FixedColor.fx^
 /build:RandomColor.fx
