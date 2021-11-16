// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3dcommon.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop.DirectX
{
    public enum D3D_SHADER_VARIABLE_CLASS
    {
        D3D_SVC_SCALAR = 0,
        D3D_SVC_VECTOR = (D3D_SVC_SCALAR + 1),
        D3D_SVC_MATRIX_ROWS = (D3D_SVC_VECTOR + 1),
        D3D_SVC_MATRIX_COLUMNS = (D3D_SVC_MATRIX_ROWS + 1),
        D3D_SVC_OBJECT = (D3D_SVC_MATRIX_COLUMNS + 1),
        D3D_SVC_STRUCT = (D3D_SVC_OBJECT + 1),
        D3D_SVC_INTERFACE_CLASS = (D3D_SVC_STRUCT + 1),
        D3D_SVC_INTERFACE_POINTER = (D3D_SVC_INTERFACE_CLASS + 1),
        D3D10_SVC_SCALAR = D3D_SVC_SCALAR,
        D3D10_SVC_VECTOR = D3D_SVC_VECTOR,
        D3D10_SVC_MATRIX_ROWS = D3D_SVC_MATRIX_ROWS,
        D3D10_SVC_MATRIX_COLUMNS = D3D_SVC_MATRIX_COLUMNS,
        D3D10_SVC_OBJECT = D3D_SVC_OBJECT,
        D3D10_SVC_STRUCT = D3D_SVC_STRUCT,
        D3D11_SVC_INTERFACE_CLASS = D3D_SVC_INTERFACE_CLASS,
        D3D11_SVC_INTERFACE_POINTER = D3D_SVC_INTERFACE_POINTER,
        D3D_SVC_FORCE_DWORD = 0x7fffffff,
    }
}
