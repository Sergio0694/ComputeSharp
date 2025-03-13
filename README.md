![](https://user-images.githubusercontent.com/10199417/108635546-3512ea00-7480-11eb-8172-99bc59f4eb6f.png)
<br/>
[![.NET](https://github.com/Sergio0694/ComputeSharp/workflows/.NET/badge.svg)](https://github.com/Sergio0694/ComputeSharp/actions) [![NuGet](https://img.shields.io/nuget/dt/ComputeSharp.svg)](https://www.nuget.org/stats/packages/ComputeSharp?groupby=Version) [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.svg)](https://www.nuget.org/packages/ComputeSharp/)

# What is it? 🚀

**ComputeSharp** is a .NET library to run C# code in parallel on the GPU through DX12, D2D1, and dynamically generated HLSL compute and pixel shaders. The available APIs let you access GPU devices, allocate GPU buffers and textures, move data between them and the RAM, write compute shaders entirely in C# and have them run on the GPU. The goal of this project is to make GPU computing easy to use for all .NET developers!

# What can it actually do? ✨

Since a video is worth a thousand words, here's a showcase of some pixel shaders (originally from [shadertoy.com](https://www.shadertoy.com/)), ported from GLSL to C# and running with **ComputeSharp** in a WinUI 3 sample app. You can use this library to create all sorts of things from scientific simulations, to animated backgrounds, audio visualizers and more!

https://user-images.githubusercontent.com/10199417/126792509-c11feb11-a609-4fab-86b3-426d43df6d90.mp4

# Where is it being used? ✈️

**ComputeSharp** is production ready, and it's being used by several projects running on millions of devices!

Here's a showcase of some of them:

| Screenshot | Project |
| ------ | ------  |
| ![image](https://user-images.githubusercontent.com/10199417/223806227-3a08e65c-8387-4b44-90f4-5dda46a9a02c.png) | The [**Microsoft Store**](https://apps.microsoft.com/) is a first party application that ships with Windows and allows users to discover, search and download digital content such as apps, games, movies and more. Starting from the January 2023 release, it is using ComputeSharp.D2D1.Uwp to leverage custom effects and pixel shaders to power several graphics elements in the application, such as the new app cards. |
| ![image](https://user-images.githubusercontent.com/10199417/223808546-1f6ecbf1-920d-407a-8385-d894fef0719c.png) | [**Paint.NET**](https://getpaint.net/) is an image and photo editing application for Windows. Starting from its 5.0 release, it is using ComputeSharp.D2D1 as a core component of its architecture (in fact, ComputeSharp.D2D1 was initially developed specifically to support Paint.NET!). The library is used both internally to accelerate dozens of built-in effects using custom D2D1 pixel shaders, as well as for external plugins. |

# Try out the sample app! 💻

The sample app is available in the Microsoft Store, showcasing several pixel shaders all powered by **ComputeSharp**!

<a href="https://apps.microsoft.com/store/detail/9PDC095X3PKV?cid=github&mode=direct"><img src="https://learn.microsoft.com/en-us/windows/apps/images/new-badge-light.png" alt="Download from the Microsoft Store" width='320' /></a>

# Available packages 📦

| Name | Description |
| ------ | ------  |
| **ComputeSharp** | The main library, with compiled shaders support |
| **ComputeSharp.D3D12MemoryAllocator** | An extension library for **ComputeSharp**, adding D3D12MA as the memory allocator for graphics resources. |
| **ComputeSharp.Dxc** | An extension library for **ComputeSharp**, bundling the DXC compiler and enabling shader reflection |
| **ComputeSharp.Pix** | An extension library for **ComputeSharp**, enabling PIX support to produce debugging information |
| **ComputeSharp.Uwp** | A UWP library with controls to render DX12 shaders powered by **ComputeSharp** |
| **ComputeSharp.WinUI** | A WinUI 3 library with controls to render DX12 shaders powered by **ComputeSharp** |
| **ComputeSharp.D2D1** | A library to write D2D1 pixel shaders entirely with C# code, and to easily register and create ID2D1Effect-s from them |
| **ComputeSharp.D2D1.Uwp** | A UWP library with custom effects for Win2D powered by **ComputeSharp.D2D1** |
| **ComputeSharp.D2D1.WinUI** | A WinUI 3 library with custom effects for Win2D powered by **ComputeSharp.D2D1** |

Preview builds are available at https://pkgs.computesharp.dev/index.json (as well as GitHub Packages).

# Documentation 📖

All the documentation for **ComputeSharp** is available in the [wiki pages](https://github.com/Sergio0694/ComputeSharp/wiki). That includes a recap of everything in this readme, as well as info and detailed samples on all features and APIs from the available packages. Make sure to go through it to get familiar with this library!

# Where can I learn more? 📑

I've written about **ComputeSharp** in my master's thesis in Engineering in Computer Engineering, which you can find in this repo! There, I cover everything there is to know about this project: the basics of COM, WinRT, UWP XAML, and Win2D, the underlying infrastructure for ComputeSharp both in terms of DirectX interop and its source generators (including an extensive breakdown of all generated code and what it does), how HLSL intrinsics are transpiled, how I extended Win2D to add support for custom effects, how **ComputeSharp** leverages this in its `PixelShaderEffect<T>` type to seamlessly integrate with Win2D, and more! I also included a detailed case study on how we implemented several pixel shaders in the Microsoft Store, with an overview of their effect graphs, and some code snippets as well.

<a href="/docs/ComputeSharp.pdf"><img src="https://github.com/user-attachments/assets/2354ce4b-29c8-4a4e-8657-f1220f3a9db6" /></a>

If you're curious, you can read the thesis [here](/docs/ComputeSharp.pdf)!

# Sponsors 🎁

Huge thanks to these sponsors for directly supporting my work on **ComputeSharp**, it means a lot! 🙌

<a href="https://github.com/paintdotnet"><img src="https://avatars.githubusercontent.com/u/11067286" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/iamabigartist"><img src="https://avatars.githubusercontent.com/u/53459343" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/dgellow"><img src="https://avatars.githubusercontent.com/u/2451004" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/ptasev"><img src="https://avatars.githubusercontent.com/u/23424044" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/xoofx"><img src="https://avatars.githubusercontent.com/u/715038" height="auto" width="60" style="border-radius:50%"></a>
<a href="https://github.com/hawkerm"><img src="https://avatars.githubusercontent.com/u/8959496" height="auto" width="60" style="border-radius:50%"></a>

# Special thanks

**ComputeSharp** was originally based on some of the code from the [DX12GameEngine](https://github.com/Aminator/DirectX12GameEngine) project by [Amin Delavar](https://github.com/Aminator).

Additionally, **ComputeSharp** uses NuGet packages from the following repositories:

- [.NET Community Toolkit](https://aka.ms/toolkit/dotnet)
- [Windows Community Toolkit](https://aka.ms/toolkit/windows)
- [TerraFX.Interop.Windows](https://github.com/terrafx/terrafx.interop.windows)
- [TerraFX.Interop.D3D12MemoryAllocator](https://github.com/terrafx/terrafx.interop.d3d12memoryallocator)
