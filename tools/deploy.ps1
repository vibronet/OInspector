param(
    [String]$testSample = "oidc-authorization-code-request",
    [Switch]$live)

# Shutdown Fiddler first to be able to copy the plugin
Stop-Process -Name "Fiddler" -Force -ErrorAction SilentlyContinue

# We need to pause, since inspector's files still might be locked
Start-Sleep -Seconds 1

# At this point we should be okay to replace files at the destination
Copy-Item -Path "$env:REPOROOT\Inspector\bin\Debug\OpenIDConnect.Inspector*" `
          -Destination "${env:ProgramFiles(x86)}\Fiddler2\Inspectors" `
          -Force

Copy-Item -Path "$env:REPOROOT\Inspector\bin\Debug\Newtonsoft.Json.dll" `
          -Destination "${env:ProgramFiles(x86)}\Fiddler2\Inspectors" `
          -Force

Copy-Item -Path "$env:REPOROOT\Inspector\bin\Debug\System.IdentityModel.Tokens.Jwt.dll" `
          -Destination "${env:ProgramFiles(x86)}\Fiddler2\Inspectors" `
          -Force

# Turns out to be name of a test sample in the source tree
if ([IO.File]::Exists($testSample) -eq $false) {
    $testSample = "$env:REPOROOT\Inspector.Tests\testSamples\$testSample.saz"
}

$fiddlerPath = "${env:ProgramFiles(x86)}\Fiddler2\Fiddler.exe"
# See the following link for more command-line arguments: http://fiddler.wikidot.com/commandlineparams
if ($live -eq $false) {
    Start-Process -FilePath $fiddlerPath -ArgumentList @("-viewer", "-noattach", $testSample)
} else {
    Start-Process -FilePath $fiddlerPath
}