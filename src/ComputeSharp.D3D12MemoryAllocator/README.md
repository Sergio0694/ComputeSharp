![ComputeSharp cover image](https://user-images.githubusercontent.com/10199417/108635546-3512ea00-7480-11eb-8172-99bc59f4eb6f.png)

# Overview 📖

**ComputeSharp.D3D12MemoryAllocator** is an extension library for **ComputeSharp** to enable using [D3D12MA](https://gpuopen.com/d3d12-memory-allocator/) as the memory allocator for graphics resources.

# Quick start 🚀

You can configure the D3D12MA allocator by using the `AllocationServices` type at startup:

```csharp
AllocationServices.ConfigureAllocatorFactory(new D3D12MemoryAllocatorFactory());
```

This will make **ComputeSharp** leverage D3D12MA for all subsequent resource allocations.

# There's more!

For a complete list of all features available in **ComputeSharp**, check the documentation in the [GitHub repo](https://github.com/Sergio0694/ComputeSharp).