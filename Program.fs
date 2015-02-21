open System

[<EntryPoint>]
let main argv = 
  printfn "Arguments passed:"
  let pArgs = argv |> CMDParser.parseArgs
  printfn "general arguments: %A" pArgs?("")
  printfn "cmd1: %A" pArgs?cmd1
  printfn "cmd2: %A" pArgs?cmd2
  0 // return an integer exit code
