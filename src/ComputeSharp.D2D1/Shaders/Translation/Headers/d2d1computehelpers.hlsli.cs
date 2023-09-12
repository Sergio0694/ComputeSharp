using System;

#pragma warning disable IDE0025

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// A helper class with the contents of the custom <c>d2d1computehelpers.hlsli</c> header.
/// </summary>
internal static class D2D1ComputeHelpers
{
    /// <summary>
    /// Gets the contents of the custom<c>d2d1computehelpers.hlsli</c> header, as UTF8 text.
    /// </summary>
    public static ReadOnlySpan<byte> TextUtf8
    {
        get
        {
            return """
                #define __D2D_DEFINE_CS_GLOBALS(inputIndex)                           \
                Texture2D<float4> InputTexture##inputIndex : register(t##inputIndex); \
                SamplerState InputSampler##inputIndex : register(s##inputIndex);      \

                // Define a texture and sampler pair for each D2D effect input (same as with D2D pixel shaders)
                #if (D2D_INPUT_COUNT >= 1) 
                __D2D_DEFINE_CS_GLOBALS(0)
                #endif
                #if (D2D_INPUT_COUNT >= 2)
                __D2D_DEFINE_CS_GLOBALS(1)
                #endif       
                #if (D2D_INPUT_COUNT >= 3)
                __D2D_DEFINE_CS_GLOBALS(2)
                #endif        
                #if (D2D_INPUT_COUNT >= 4)
                __D2D_DEFINE_CS_GLOBALS(3)
                #endif        
                #if (D2D_INPUT_COUNT >= 5)
                __D2D_DEFINE_CS_GLOBALS(4)
                #endif        
                #if (D2D_INPUT_COUNT >= 6)
                __D2D_DEFINE_CS_GLOBALS(5)
                #endif        
                #if (D2D_INPUT_COUNT >= 7)
                __D2D_DEFINE_CS_GLOBALS(6)
                #endif        
                #if (D2D_INPUT_COUNT >= 8)
                __D2D_DEFINE_CS_GLOBALS(7)
                #endif

                #define __D2D_MAXIMUM_INPUT_COUNT 8

                // Validate that the input count is defined
                #ifndef D2D_INPUT_COUNT
                #error D2D_INPUT_COUNT is undefined. 
                #endif 

                // Validate that the input count is within bounds
                #if (D2D_INPUT_COUNT > __D2D_MAXIMUM_INPUT_COUNT)
                #error D2D_INPUT_COUNT exceeds the maximum input count.
                #endif

                // Validate that the shader profile version has been defined
                #ifndef D2D_SHADER_PROFILE_MAJOR_VERSION
                #error D2D_SHADER_PROFILE_MAJOR_VERSION is undefined. 
                #endif

                // Define the output texture
                #if (D2D_SHADER_PROFILE_MAJOR_VERSION >= 5)
                RWTexture2D<float4> OutputTexture : register(u0);
                #else
                RWStructuredBuffer<float4> OutputTexture : register(u0);
                #endif

                // Helper macros to define system constants below
                #define __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(inputIndex, axis) sceneToInput##inputIndex##axis
                #define __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(inputIndex)            \
                    float2 __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(inputIndex, X); \
                    float2 __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(inputIndex, Y); \

                // Define the system provided constant buffer
                cbuffer _ : register(b0)
                {
                    int4 resultRect;
                #if (D2D_SHADER_PROFILE_MAJOR_VERSION >= 5)
                    int2 outputOffset;
                #endif
                #if (D2D_INPUT_COUNT >= 1) 
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(0)
                #endif
                #if (D2D_INPUT_COUNT >= 2)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(1)
                #endif       
                #if (D2D_INPUT_COUNT >= 3)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(2)
                #endif        
                #if (D2D_INPUT_COUNT >= 4)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(3)
                #endif        
                #if (D2D_INPUT_COUNT >= 5)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(4)
                #endif        
                #if (D2D_INPUT_COUNT >= 6)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(5)
                #endif        
                #if (D2D_INPUT_COUNT >= 7)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(6)
                #endif        
                #if (D2D_INPUT_COUNT >= 8)
                __D2D_DEFINE_CS_INPUT_BUFFER_OFFSETS(7)
                #endif
                };

                // Ensure the dispatch thread numbers have been defined
                #ifndef D2D_THREAD_NUM_X
                #error D2D_THREAD_NUM_X is undefined. 
                #endif
                #ifndef D2D_THREAD_NUM_Y
                #error D2D_THREAD_NUM_Y is undefined. 
                #endif
                #ifndef D2D_THREAD_NUM_Z
                #error D2D_THREAD_NUM_Z is undefined. 
                #endif

                // Define the entry point
                #define D2D_CS_ENTRY(name)                                         \
                [numthreads(D2D_THREAD_NUM_X, D2D_THREAD_NUM_Y, D2D_THREAD_NUM_Z)] \
                void name(                                                         \
                    uint3 ThreadIds : SV_DispatchThreadID,                         \
                    uint3 GroupIds : SV_GroupThreadID,                             \
                    uint3 GridIds : SV_GroupID,                                    \
                    uint  __GroupIds__get_Index : SV_GroupIndex)                   \

                // Macros to declare the helper to map input coordinates
                #define __D2D_DEFINE_CONVERT_INPUT_POSITION_NAME(inputIndex) D2DConvertSceneToTexelSpaceForInput##inputIndex
                #define __D2D_DEFINE_CONVERT_INPUT_POSITION(inputIndex)                                       \
                inline float2 __D2D_DEFINE_CONVERT_INPUT_POSITION_NAME(inputIndex)(float2 inputScenePosition) \
                {                                                                                             \
                    float2 ret;                                                                               \
                    ret.x =                                                                                   \
                        (inputScenePosition.x * __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(0, X)[0]) +          \
                        __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(0, X)[1];                                    \
                    ret.y =                                                                                   \
                        (inputScenePosition.y * __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(0, Y)[0]) +          \
                        __D2D_DEFINE_CS_INPUT_BUFFER_OFFSET_NAME(0, Y)[1];                                    \
                                                                                                              \
                    return ret;                                                                               \
                }

                // Define the internal helpers to map input coordinates
                #if (D2D_INPUT_COUNT >= 1) 
                __D2D_DEFINE_CONVERT_INPUT_POSITION(0)
                #endif
                #if (D2D_INPUT_COUNT >= 2)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(1)
                #endif       
                #if (D2D_INPUT_COUNT >= 3)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(2)
                #endif        
                #if (D2D_INPUT_COUNT >= 4)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(3)
                #endif        
                #if (D2D_INPUT_COUNT >= 5)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(4)
                #endif        
                #if (D2D_INPUT_COUNT >= 6)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(5)
                #endif        
                #if (D2D_INPUT_COUNT >= 7)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(6)
                #endif        
                #if (D2D_INPUT_COUNT >= 8)
                __D2D_DEFINE_CONVERT_INPUT_POSITION(7)
                #endif

                #if (D2D_INPUT_COUNT > 0)
                // Define the helper to map input coordinates
                #define D2DConvertInputSceneToTexelSpace(index, inputScenePosition)      \
                __D2D_DEFINE_CONVERT_INPUT_POSITION_NAME(inputIndex)(inputScenePosition) \

                // Helper to read from an input with a normalized coordinate
                #define D2DSampleInput(index, position) InputTexture##index.SampleLevel(0, position, 0)

                // Helper to read from an input texture with absolute offset
                #define D2DSampleInputAtOffset(index, offset)                                \
                InputTexture##index.SampleLevel(                                             \
                    InputSampler##index,                                                     \
                    __D2D_DEFINE_CONVERT_INPUT_POSITION_NAME(index)(offset + resultRect.xy), \
                    0)                                                                       \

                // Need a trailing blank line between a multiline #define and #endif, or parsing will fail
                #endif

                // Helper to set the output value
                #if (D2D_SHADER_PROFILE_MAJOR_VERSION >= 5)
                #define D2DSetOutput(value, offset) OutputTexture[offset + outputOffset.xy] = value
                #else
                #error Compute shaders with a shader profile lower than 5.0 are not supported yet.
                #endif

                // Helper to get the resulting rectangle
                inline float4 D2DGetResultRectangleBounds()
                {
                    return resultRect;
                }

                // Helpers to also directly get the result rectangle bounds
                inline uint2 D2DGetResultRectangleSize()
                {
                    uint x = uint(resultRect.z - resultRect.x);
                    uint y = uint(resultRect.w - resultRect.y);

                    return uint2(x, y);
                }

                """u8;
        }
    }
}