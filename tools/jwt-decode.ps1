# README
# The script takes jwt string as an input parameter and also can read text from the clipboard if no input is provided.
# (very common use case when you copy/paste jwt strings from Fiddler).
# Example output:
#
# header ~> {
#     "typ": "JWT",
#     ...
# }
#
# payload ~> {
#     "iss": "https://xxx.vvv.com/issuer",
#     ...
# }

# Script also is very flexible and makes use of artefacts, such as 'access_token=xyxyxyx' or 'id_token=zczczczczc' and 
# modifies the output accordingly to maximize the readability.
# Example output:
#
# (access_token) header ~> {
#     "typ": "JWT",
#     ...
# }
#
# (access_token) payload ~> {
#     "iss": "https://xxx.vvv.com/issuer",
#     ...
# }

# USAGE: 
# You can call the script directly from PowerShell or you can leverage envy's 'jwtdecode' alias which will save you
# dozens of keystrokes and it's available anywhere in the source tree.
# Example (call script & provide input): .\jwt-decode.ps1 -inputString "xxxxxx"
# Example (call call & use clipboard): .\jwt-decode.ps1
#
# Example (using alias & provide input): jwtdecode "xcxcxcxcxcx"
# Example (using alias & use clipboard): jwtdecode

param([String]$inputString = $null)

function Get-ClipboardText()
{
    $command =
    {
        add-type -an system.windows.forms
        [System.Windows.Forms.Clipboard]::GetText()
    }
    return (powershell -sta -noprofile -command $command)
}


function Format-JwtEncodedString([char[]]$jwtEncodedString, [string]$typeString) {
    $section = 0
    $formatString = ""

    foreach ($char in $jwtEncodedString) {
        if ($char -eq '{') {
            $char += [Environment]::NewLine

            # embed section label
            if ($section -eq 0) {
               $char = "header ~> $char" 
            } elseif ($section -eq 1) {
               $char = "payload ~> $char" 
            }

            # use type string only if it has been detected
            if ([String]::IsNullOrWhiteSpace($typeString) -eq $false) {
                $char = "($typeString) $char"
            }

            $char = [Environment]::NewLine + $char
        } elseif ($char -eq ',') {
            $char +=  [Environment]::NewLine
        } elseif ($char -eq '}') {
            $section++
            $char = [Environment]::NewLine + $char
        } elseif ($char -eq ':') {
            $char += " "
        } elseif (($char -eq '.') -and ($formatString.EndsWith('}'))) {
            # replace sections delimiter with a new line
            $char = [Environment]::NewLine
        } elseif ($formatString.EndsWith([Environment]::NewLine)) {
            $char = "  " + $char
        }

        $formatString += $char
    }

    return $formatString
}

# ensure we have required dependencies before running the tool
&"${env:REPOROOT}\.envy\restore.cmd"

# try to get the input from clipboard to save keystrokes
if ([String]::IsNullOrWhiteSpace($inputString)) {
    $inputString = Get-ClipboardText
}

# cleanup fiddler artefacts from the input string, again to save keystrokes
# and remember the token type to enhance readability of the output
$indexOf = $inputString.IndexOf("=")
$typeString = ""
if ($indexOf -gt -1) {
    $typeString = $inputString.Substring(0, $indexOf)
    $inputString = $inputString.Substring($indexOf + 1)
}

# suppress messages from loading an assembly
$assembly = [Reflection.Assembly]::LoadFile("${env:NUGETROOT}\System.IdentityModel.Tokens.Jwt.4.0.2.202250711\lib\net45\System.IdentityModel.Tokens.Jwt.dll")

# decode jwt string using the library
$jwt = [System.IdentityModel.Tokens.JwtSecurityToken] $inputString

# render jwt
Format-JwtEncodedString -jwtEncodedString $jwt.ToString() -typeString $typeString