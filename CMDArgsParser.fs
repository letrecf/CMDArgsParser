module CMDParser

open  System.Text.RegularExpressions

// parse command using regex
// if matched, return (command_name, command_value)
let private (|Command|_|) (s:string) =
  let r = new Regex(@"^(?:-{1,2}|\/)(?<command>\w+)[=:]*(?<value>.*)$",RegexOptions.IgnoreCase)
  let m = r.Match(s)
  if m.Success
  then let g = m.Groups in Some(g.["command"].Value.ToLower(), g.["value"].Value)
  else None

let private buildList (d:(string*string) seq) = 
  d |> Seq.map (fun (_,v) -> v) |> Seq.filter (fun i -> i.Length>0) |> List.ofSeq // ignore empty strings in data

let private groupData (d:(string*string) seq) =
  d |> Seq.groupBy (fun (n,_) -> n) |> Seq.map (fun (n,s) -> (n, s |> buildList))


// take a sequence of argument values
// map them into a (name,value) tuple
// scan the tuple sequence and put command name into all subsequent tuples without name
// discard the initial ("","") tuple
// group tuples by name 
// convert the tuple sequence into a map of (name,value list)
let parseArgs (args:string array) =
  args 
  |> Seq.map (fun i -> match i with | Command (n,v) -> (n,v) | _ -> ("",i) )
  |> Seq.scan (fun (sn,_) (n,v) -> if n.Length>0 then (n,v) else (sn,v)) ("","")
  |> Seq.skip 1
  |> groupData
  |> Map.ofSeq
