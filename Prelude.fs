[<AutoOpen>]
module Prelude

// return Some(value) if key is found, None otherwise
let (?) (m:Map<_,_>) k = Map.tryFind k m
