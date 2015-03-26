# Shutdown Fiddler first to be able to copy the plugin
Stop-Process -Name "Fiddler" -Force -ErrorAction SilentlyContinue

# We need to pause, since inspector's files still might be locked
Start-Sleep -Seconds 1

# At this point we should be okay to replace files at the destination
Copy-Item -Path "$env:REPOROOT\Inspector\bin\Debug\OpenIDConnect.Inspector*" `
          -Destination "${env:ProgramFiles(x86)}\Fiddler2\Inspectors" `
          -Force

# See the following link for more command-line arguments: http://fiddler.wikidot.com/commandlineparams
Start-Process -FilePath "${env:ProgramFiles(x86)}\Fiddler2\Fiddler.exe" `
              -ArgumentList @("-viewer", "-noattach", "$env:REPOROOT\Inspector.Tests\testSamples\loginRequest.saz")