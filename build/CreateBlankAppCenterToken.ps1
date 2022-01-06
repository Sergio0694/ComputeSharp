# Create the tokens folder
$TOKENS_FOLDER_RELATIVE_PATH = Join-Path -Path $PSScriptRoot -ChildPath "..\samples\ComputeSharp.SwapChain.Uwp\Assets\ServiceTokens"
if(!(Test-Path -Path $TOKENS_FOLDER_RELATIVE_PATH)) {
  New-Item -ItemType directory -Path $TOKENS_FOLDER_RELATIVE_PATH
}
$TOKENS_FOLDER_PATH = Resolve-Path -Path $TOKENS_FOLDER_RELATIVE_PATH

# AppCenter tokens
$APPCENTER_TOKENS_NAME = "AppCenter.txt"
$APPCENTER_TOKENS_PATH = Join-Path -Path $TOKENS_FOLDER_PATH -ChildPath $APPCENTER_TOKENS_NAME
if (!(Test-Path -Path $APPCENTER_TOKENS_PATH))
{
  echo "<APP_CENTER_SECRET>" > $APPCENTER_TOKENS_PATH
}