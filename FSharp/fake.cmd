SET TOOL_PATH=.fake

IF NOT EXIST "%TOOL_PATH%\fake.exe" (
  dotnet tool install fake-cli --tool-path ./%TOOL_PATH% --version 5.3.1
)

"%TOOL_PATH%/fake.exe" %*