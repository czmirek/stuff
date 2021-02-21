<#
  Following powershell function recursively sorts a hashtable by keys.
  This is usefull for example for consistent JSON generation
  
  Usage:
    
    $sorted = SortHashtableByKey $myHashtable
    $sorted | ConvertTo-Json -Depth 10 | Out-File "my-consistently-generated-data.json"
#>
function SortHashtableByKey {
    param($arg)
    if($arg -is [Hashtable] -and $arg.count -gt 1) {
        $sorted = [ordered]@{}
        $arg.GetEnumerator() | Sort-Object Name | ForEach-Object {
            $newval = SortHashtableByKey $_.Value
            $sorted[$_.Key] = $newval
        }
        return $sorted
    } else {
        return $arg
    }
}
