# Axon Histories

[Documentation](https://skyfoundry.com/doc/lib-his/doc)


Required tags:

1. `his` marker tag
2. `tz`: string matching timezone name from the Olson timezone database
3. `kind`: string enumeration: "Number", "Bool", or "Str"
4. `unit`: string unit enumeration.


```axon
// hisRead(hisRefs, dates: ObjRange, opts: {})

// Dates
2021 // Integer gives whole year
null  // oldest to newest
function // things like `today`.
```
