SET zip=D:\Apps\Core\7-Zip\7z.exe
SET ver=1.0

rd /s /q PpsTitleDisplay\bin
rd /s /q PpsTitleDisplay\obj

rem "C:\Program Files (x86)\Pleasant Solutions\KeePass for Pleasant Password Server\KeePass.exe" --plgx-create PpsTitleDisplay
"c:\Program Files (x86)\KeePass Password Safe 2\KeePass.exe" --plgx-create PpsTitleDisplay
rem del  TitleDisplay.dll
rem rd /s /q %foler%

rem %z% a TitleDisplay-%v%.zip TitleDisplay.plgx
rem %z% a TitleDisplay-%v%.zip License.txt
rem %z% a TitleDisplay-%v%.zip Readme.txt

rem rd /s /q ..\TitleDisplay\obj
rem %z% a TitleDisplay-%v%-Source.zip ..\TitleDisplay
rem %z% d TitleDisplay-%v%-Source.zip .svn -r

