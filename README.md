![](https://user-images.githubusercontent.com/10199417/108635546-3512ea00-7480-11eb-8172-99bc59f4eb6f.png)
<br/>
[![.NET](https://github.com/Sergio0694/ComputeSharp/workflows/.NET/badge.svg)](https://github.com/Sergio0694/ComputeSharp/actions) [![NuGet](https://img.shields.io/nuget/dt/ComputeSharp.svg)](https://www.nuget.org/stats/packages/ComputeSharp?groupby=Version)

# What is it? 🚀

**ComputeSharp** is a .NET library to run C# code in parallel on the GPU through DX12 and dynamically generated HLSL compute shaders. The available APIs let you access GPU devices, allocate GPU buffers and textures, move data between them and the RAM, write compute shaders entirely in C# and have them run on the GPU. The goal of this project is to make GPU computing easy to use for all .NET developers!

# What can it actually do? ✨

Since a video is worth a thousand words, here's a showcase of some pixel shaders (originally from [shadertoy.com](https://www.shadertoy.com/)), ported from GLSL to C# and running with **ComputeSharp** in a WinUI 3 sample app. You can use this library to create all sorts of things from scientific simulations, to animated backgrounds, audio visualizers and more!

https://user-images.githubusercontent.com/10199417/126792509-c11feb11-a609-4fab-86b3-426d43df6d90.mp4

# Where is it being used? ✈️

**ComputeSharp** is production ready, and it's being used by several projects running on millions of devices!

Here's a showcase of some of them:

| Project | Screenshot |
| ------ | ------  |
| [**Paint.NET**](https://getpaint.net/) is an image and photo editing application for Windows. Starting from its 5.0 release, it is using ComputeSharp.D2D1 as a core component of its architecture (in fact, ComputeSharp.D2D1 was initially developed specifically to support Paint.NET!). The library is used both internally to accelerate dozens of built-in effects using custom D2D1 pixel shaders, as well as for plugins, allowing developers to implement their own custom effects powered by custom D2D1 pixel shaders, which can then be loaded and used in Paint.NET with ease. | <a href="https://getpaint.net/"><img src="https://user-images.githubusercontent.com/10199417/211784712-158b1045-ded6-4d4c-a43d-91314ee2ec98.png" width="480"></a> |

# Try out the sample app! 💻

The sample app is available in the Microsoft Store, showcasing several pixel shaders all powered by **ComputeSharp**!

<a href="https://www.microsoft.com/store/apps/9PDC095X3PKV"><img src="https://developer.microsoft.com/en-us/store/badges/images/English_get-it-from-MS.png" alt="Get it from Microsoft" width='280' /></a>

# Available packages 📦

| Name | Description | Latest version |
| ------ | ------  | ------ |
| **ComputeSharp** | The main library, with compiled shaders support | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.svg)](https://www.nuget.org/packages/ComputeSharp/) |
| **ComputeSharp.Dynamic** | An extension for **ComputeSharp**, enabling dynamic shader compilation at runtime and shader metaprogramming | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.Dynamic.svg)](https://www.nuget.org/packages/ComputeSharp.Dynamic/) |
| **ComputeSharp.Pix** | An extension library for **ComputeSharp**, enabling PIX support to produce debugging information | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.Pix.svg)](https://www.nuget.org/packages/ComputeSharp.Pix/) |
| **ComputeSharp.Uwp** | A UWP library (targeting .NET Standard 2.0) with controls to render DX12 shaders powered by **ComputeSharp** | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.Uwp.svg)](https://www.nuget.org/packages/ComputeSharp.Uwp/) |
| **ComputeSharp.WinUI** | A WinUI 3 library (targeting .NET 6) with controls to render DX12 shaders powered by **ComputeSharp** | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.WinUI.svg)](https://www.nuget.org/packages/ComputeSharp.WinUI/) |
| **ComputeSharp.D2D1** | A library to write D2D1 pixel shaders entirely with C# code, and to easily register and create ID2D1Effect-s from them | [![NuGet](https://img.shields.io/nuget/vpre/ComputeSharp.D2D1.svg)](https://www.nuget.org/packages/ComputeSharp.D2D1/) |

Preview builds are available at https://pkgs.computesharp.dev/index.json (as well as GitHub Packages).

# Documentation 📖

All the documentation for **ComputeSharp** is available in the [wiki pages](https://github.com/Sergio0694/ComputeSharp/wiki). That includes a recap of everything in this readme, as well as info and detailed samples on all features and APIs from the available packages. Make sure to go through it to get familiar with this library!

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
