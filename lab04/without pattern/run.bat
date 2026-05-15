@echo off
echo Компиляция...
javac -cp "lib/mysql-connector-j-9.7.0.jar" *.java -d out
if %errorlevel% neq 0 (
    echo Ошибка компиляции!
    pause
    exit /b
)
echo Запуск...
java -cp "out;lib/mysql-connector-j-9.7.0.jar" Main
pause