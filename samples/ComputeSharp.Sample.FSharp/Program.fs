// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Globalization
open System.Linq
open ComputeSharp
open ComputeSharp.Sample.FSharp.Shaders

/// <summary>Prints a matrix in a properly formatted way.</summary>
/// <param name="array">The input <see langword="float"/> array representing the matrix to print.</param>
/// <param name="width">The width of the array to print.</param>
/// <param name="height">The height of the array to print.</param>
/// <param name="name">The name of the matrix to print.</param>
let printMatrix (array : float32[]) width height (name : string) =
    let pad = 48 - name.Length
    let title = $"%s{new string('=', pad / 2)} %s{name} %s{new string('=', (pad + 1) / 2)}"
    let numberWidth = Math.Max(array.Max().ToString(CultureInfo.InvariantCulture).Length, 4)

    printfn "%s" title

    for i in 0 .. height - 1 do
        let row = array.[i * width .. (i + 1) * width - 1]
        let text = String.Join(",", [for x in row -> x.ToString(CultureInfo.InvariantCulture).PadLeft(numberWidth)])

        printfn "%s" text

    printfn "%s" (new string('=', 50))

[<EntryPoint>]
let main _ =

    let array = [| for i in 1 .. 100 -> (float32)i |]

    // Create the graphics buffer
    use buffer = Gpu.Default.AllocateReadWriteBuffer(array)

    let shader = new MultiplyByTwo(buffer)

    // Run the shader (passed by reference)
    Gpu.Default.For(100, &shader)

    // Print the initial matrix
    printMatrix array 10 10 "BEFORE"

    // Get the data back
    buffer.CopyTo array

    // Print the updated matrix
    printMatrix array 10 10 "AFTER"

    0
