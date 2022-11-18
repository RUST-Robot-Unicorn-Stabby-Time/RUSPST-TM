Shader "UI_Rainbow Shader"
{
    Properties
    {
        Vector1_d34cbc6ca56c44a682e5039b0a8726a1("ScrollSpeed", Range(0, 10)) = 0
        Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7("Tiling", Vector) = (1, 1, 0, 0)
        Vector1_12099adc799f47a3ab28a9f6b8a8d486("AlphaScroll", Range(0, 10)) = 5
        Vector1_26e6208e64274d6dbf44fe0ba49cd12c("DistortionScale", Range(0, 10)) = 1
        Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45("DistorionSpeed", Range(0, 1)) = 0
        Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd("DistorionIntensity", Range(0, 1)) = 0
        [NoScaleOffset]Texture2D_97e74005f4314558975099848000c8a1("Mask", 2D) = "white" {}
        Vector1_fc971ba86fc9493da309bdda370ac26c("Wander", Float) = 0
        [NonModifiableTextureData][NoScaleOffset]_MainTex("Texture2D", 2D) = "white" {}
        [HideInInspector]_EmissionColor("Color", Color) = (1, 1, 1, 1)
        [HideInInspector]_RenderQueueType("Float", Float) = 6
        [HideInInspector][ToggleUI]_AddPrecomputedVelocity("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DepthOffsetEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentWritingMotionVec("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_AlphaCutoffEnable("Boolean", Float) = 0
        [HideInInspector]_TransparentSortPriority("_TransparentSortPriority", Float) = 0
        [HideInInspector][ToggleUI]_UseShadowThreshold("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DoubleSidedEnable("Boolean", Float) = 1
        [HideInInspector][Enum(Flip, 0, Mirror, 1, None, 2)]_DoubleSidedNormalMode("Float", Float) = 2
        [HideInInspector]_DoubleSidedConstants("Vector4", Vector) = (1, 1, -1, 0)
        [HideInInspector][ToggleUI]_TransparentDepthPrepassEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentDepthPostpassEnable("Boolean", Float) = 0
        [HideInInspector]_SurfaceType("Float", Float) = 1
        [HideInInspector]_BlendMode("Float", Float) = 0
        [HideInInspector]_SrcBlend("Float", Float) = 1
        [HideInInspector]_DstBlend("Float", Float) = 0
        [HideInInspector]_AlphaSrcBlend("Float", Float) = 1
        [HideInInspector]_AlphaDstBlend("Float", Float) = 0
        [HideInInspector][ToggleUI]_AlphaToMask("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_AlphaToMaskInspectorValue("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_ZWrite("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentZWrite("Boolean", Float) = 0
        [HideInInspector]_CullMode("Float", Float) = 2
        [HideInInspector][ToggleUI]_EnableFogOnTransparent("Boolean", Float) = 0
        [HideInInspector]_CullModeForward("Float", Float) = 2
        [HideInInspector][Enum(Front, 1, Back, 2)]_TransparentCullMode("Float", Float) = 2
        [HideInInspector][Enum(UnityEditor.Rendering.HighDefinition.OpaqueCullMode)]_OpaqueCullMode("Float", Float) = 2
        [HideInInspector]_ZTestDepthEqualForOpaque("Float", Int) = 4
        [HideInInspector][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestTransparent("Float", Float) = 8
        [HideInInspector][ToggleUI]_TransparentBackfaceEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_EnableBlendModePreserveSpecularLighting("Boolean", Float) = 0
        [HideInInspector]_StencilRef("Float", Int) = 0
        [HideInInspector]_StencilWriteMask("Float", Int) = 6
        [HideInInspector]_StencilRefDepth("Float", Int) = 0
        [HideInInspector]_StencilWriteMaskDepth("Float", Int) = 8
        [HideInInspector]_StencilRefMV("Float", Int) = 32
        [HideInInspector]_StencilWriteMaskMV("Float", Int) = 40
        [HideInInspector]_StencilRefDistortionVec("Float", Int) = 4
        [HideInInspector]_StencilWriteMaskDistortionVec("Float", Int) = 4
        [HideInInspector]_StencilWriteMaskGBuffer("Float", Int) = 14
        [HideInInspector]_StencilRefGBuffer("Float", Int) = 2
        [HideInInspector]_ZTestGBuffer("Float", Int) = 4
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

         _Color("Tint", Color) = (1.000000,1.000000,1.000000,1.000000)
        _StencilComp("Stencil Comparison", Float) = 8.000000
        _Stencil("Stencil ID", Float) = 0.000000
        _StencilOp("Stencil Operation", Float) = 0.000000
        _StencilWriteMask("Stencil Write Mask", Float) = 255.000000
        _StencilReadMask("Stencil Read Mask", Float) = 255.000000
        _ColorMask("Color Mask", Float) = 15.000000
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="HDRenderPipeline"
            "RenderType"="HDUnlitShader"
            "Queue"="Transparent"
        }
        Pass
        {
            Name "DepthForwardOnly"
            Tags
            {
                "LightMode" = "DepthForwardOnly"
            }

            // Render State
        Cull [_CullMode]
        ZWrite On
        Stencil 
        {
           Ref[_Stencil]
           ReadMask[_StencilReadMask]
           WriteMask[_StencilWriteMask]
           Comp[_StencilComp]
           Pass[_StencilOp]
        }
        Blend One OneMinusSrcAlpha
        ColorMask[_ColorMask]

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer

            // Keywords
            #pragma multi_compile _ WRITE_MSAA_DEPTH
        #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _BLENDMODE_OFF _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local _ _ENABLE_FOG_ON_TRANSPARENT
            // GraphKeywords: <None>

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" 
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl" 

            // --------------------------------------------------
            // Defines

            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR

            #define HAVE_MESH_MODIFICATION


            #define SHADERPASS SHADERPASS_DEPTH_ONLY

            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT 
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif

            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end

            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif

            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT

        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it

            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif

            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif

            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif

            // Dots Instancing
            // DotsInstancingOptions: <None>

            // Various properties

            // HybridV1InjectedBuiltinProperties: <None>

            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
        float2 Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
        float Vector1_12099adc799f47a3ab28a9f6b8a8d486;
        float Vector1_26e6208e64274d6dbf44fe0ba49cd12c;
        float Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
        float Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd;
        float4 Texture2D_97e74005f4314558975099848000c8a1_TexelSize;
        float Vector1_fc971ba86fc9493da309bdda370ac26c;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(Texture2D_97e74005f4314558975099848000c8a1);
        SAMPLER(samplerTexture2D_97e74005f4314558975099848000c8a1);

            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif

            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif

            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct AttributesMesh
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv2 : TEXCOORD2;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            float4 positionCS : SV_POSITION;
            float3 positionRWS;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord1;
            float4 texCoord2;
            float4 texCoord3;
            float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float4 uv0;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryingsMeshToPS
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float3 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            float4 interp4 : TEXCOORD4;
            float4 interp5 : TEXCOORD5;
            float4 interp6 : TEXCOORD6;
            float4 interp7 : TEXCOORD7;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };

            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionRWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord1;
            output.interp5.xyzw =  input.texCoord2;
            output.interp6.xyzw =  input.texCoord3;
            output.interp7.xyzw =  input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.positionRWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord1 = input.interp4.xyzw;
            output.texCoord2 = input.interp5.xyzw;
            output.texCoord3 = input.interp6.xyzw;
            output.color = input.interp7.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph


            // Graph Functions
            
        void Unity_Multiply_float(float A, float B, out float Out)
        {
            Out = A * B;
        }

        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }

        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }


        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
        }

        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }


        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);

            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);

            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;

            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            Out = t;
        }

        void Unity_Lerp_float2(float2 A, float2 B, float2 T, out float2 Out)
        {
            Out = lerp(A, B, T);
        }

            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2;
            Unity_Multiply_float(1, IN.TimeParameters.x, _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2);
            float _Property_b60892c5746043649f99bf2c66cc56c5_Out_0 = Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
            float _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2;
            Unity_Multiply_float(_Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2, _Property_b60892c5746043649f99bf2c66cc56c5_Out_0, _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2);
            float2 _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), (_Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2.xx), _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3);
            float _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0 = Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
            float _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2;
            Unity_Multiply_float(-0.9, _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0, _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2);
            float _Multiply_064d66b2632a41288f85866dba571a49_Out_2;
            Unity_Multiply_float(_Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2, IN.TimeParameters.x, _Multiply_064d66b2632a41288f85866dba571a49_Out_2);
            float2 _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), (_Multiply_064d66b2632a41288f85866dba571a49_Out_2.xx), _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3);
            float _Property_7be3c100a4d5447b8980bcc9de7fe4a1_Out_0 = Vector1_26e6208e64274d6dbf44fe0ba49cd12c;
            float _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3;
            Unity_Remap_float(_Property_7be3c100a4d5447b8980bcc9de7fe4a1_Out_0, float2 (0, 10), float2 (0, 500), _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3);
            float _SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3, _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3, _SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2);
            float _Property_072b6d7671c24baea9016177302bcdee_Out_0 = Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd;
            float2 _Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3;
            Unity_Lerp_float2(_TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3, (_SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2.xx), (_Property_072b6d7671c24baea9016177302bcdee_Out_0.xx), _Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3);
            float2 _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0 = Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
            float2 _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3;
            Unity_TilingAndOffset_float(_Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3, _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0, float2 (1, 1), _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
            float4 _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_MainTex).tex, UnityBuildTexture2DStructNoScale(_MainTex).samplerstate, _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_R_4 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.r;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_G_5 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.g;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_B_6 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.b;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_A_7 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.a;
            UnityTexture2D _Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0 = UnityBuildTexture2DStructNoScale(Texture2D_97e74005f4314558975099848000c8a1);
            float4 _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.tex, _Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.samplerstate, IN.uv0.xy);
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_R_4 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.r;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_G_5 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.g;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_B_6 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.b;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_A_7 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.a;
            surface.BaseColor = (_SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.xyz);
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = (_SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0).x;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }

        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)

            // evaluate vertex graph
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);

            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;

            return input;
        }
            FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);

            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position

            output.positionRWS = input.positionRWS;
            output.tangentToWorld = BuildTangentToWorld(input.tangentWS, input.normalWS);
            output.texCoord0 = input.texCoord0;
            output.texCoord1 = input.texCoord1;
            output.texCoord2 = input.texCoord2;
            output.texCoord3 = input.texCoord3;
            output.color = input.color;

            return output;
        }

        SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
            output.uv0 =                         input.texCoord0;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value

            return output;
        }

        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
            VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }

            // --------------------------------------------------
            // Build Surface Data (Specific Material)

        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);

            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;

            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif

            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif

            #if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
                HDShadowContext shadowContext = InitShadowContext();
                float shadow;
                float3 shadow3;
                // We need to recompute some coordinate not computed by default for shadow matte
                posInput = GetPositionInput(fragInputs.positionSS.xy, _ScreenSize.zw, fragInputs.positionSS.z, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
                float3 upWS = normalize(fragInputs.tangentToWorld[1]);
                uint renderingLayers = GetMeshRenderingLightLayer();
                ShadowLoopMin(shadowContext, posInput, upWS, asuint(_ShadowMatteFilter), renderingLayers, shadow3);
                shadow = dot(shadow3, float3(1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0));

                float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);

                // Keep the nested lerp
                // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                #ifdef _SURFACE_TYPE_TRANSPARENT
                    surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow), surfaceDescription.Alpha);
                #else
                    surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow);
                #endif
                localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;

                surfaceDescription.Alpha = localAlpha;
            #endif
        }

            // --------------------------------------------------
            // Get Surface And BuiltinData

            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif

                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif

                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT

                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);

                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif

                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif

                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif

                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);

                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);

                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);

                ZERO_INITIALIZE(BuiltinData, builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;

                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif

                #endif // SHADER_UNLIT

                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif

                // override sampleBakedGI - not used by Unlit

                builtinData.emissiveColor = surfaceDescription.Emission;

                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.

                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif

                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif

                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif

                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassDepthOnly.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "ForwardOnly"
            Tags
            {
                "LightMode" = "ForwardOnly"
            }

            // Render State
            Cull [_CullModeForward]
        Blend [_SrcBlend] [_DstBlend], [_AlphaSrcBlend] [_AlphaDstBlend]
        ZTest [_ZTestDepthEqualForOpaque]
        ZWrite [_ZWrite]
        ColorMask [_ColorMaskTransparentVel] 1
        Stencil
        {
            WriteMask [_StencilWriteMask]
            Ref [_StencilRef]
            CompFront Always
            PassFront Replace
            CompBack Always
            PassBack Replace
        }

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma vertex Vert
        #pragma fragment Frag
        #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
        #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer

            // Keywords
            #pragma shader_feature _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local _BLENDMODE_OFF _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
        #pragma shader_feature_local _ _ADD_PRECOMPUTED_VELOCITY
        #pragma shader_feature_local _ _TRANSPARENT_WRITES_MOTION_VEC
        #pragma shader_feature_local _ _ENABLE_FOG_ON_TRANSPARENT
        #pragma multi_compile _ DEBUG_DISPLAY
            // GraphKeywords: <None>

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl" // Need to be here for Gradient struct definition

            // --------------------------------------------------
            // Defines

            // Attribute
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0

            #define HAVE_MESH_MODIFICATION


            #define SHADERPASS SHADERPASS_FORWARD_UNLIT
        #define RAYTRACING_SHADER_GRAPH_DEFAULT

            // Following two define are a workaround introduce in 10.1.x for RaytracingQualityNode
            // The ShaderGraph don't support correctly migration of this node as it serialize all the node data
            // in the json file making it impossible to uprgrade. Until we get a fix, we do a workaround here
            // to still allow us to rename the field and keyword of this node without breaking existing code.
            #ifdef RAYTRACING_SHADER_GRAPH_DEFAULT 
            #define RAYTRACING_SHADER_GRAPH_HIGH
            #endif

            #ifdef RAYTRACING_SHADER_GRAPH_RAYTRACED
            #define RAYTRACING_SHADER_GRAPH_LOW
            #endif
            // end

            #ifndef SHADER_UNLIT
            // We need isFrontFace when using double sided - it is not required for unlit as in case of unlit double sided only drive the cullmode
            // VARYINGS_NEED_CULLFACE can be define by VaryingsMeshToPS.FaceSign input if a IsFrontFace Node is included in the shader graph.
            #if defined(_DOUBLESIDED_ON) && !defined(VARYINGS_NEED_CULLFACE)
                #define VARYINGS_NEED_CULLFACE
            #endif
            #endif

            // Specific Material Define
        // Setup a define to say we are an unlit shader
        #define SHADER_UNLIT

        // Following Macro are only used by Unlit material
        #if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
        #define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        #define HAS_LIGHTLOOP
        #endif
            // Caution: we can use the define SHADER_UNLIT onlit after the above Material include as it is the Unlit template who define it

            // To handle SSR on transparent correctly with a possibility to enable/disable it per framesettings
            // we should have a code like this:
            // if !defined(_DISABLE_SSR_TRANSPARENT)
            // pragma multi_compile _ WRITE_NORMAL_BUFFER
            // endif
            // i.e we enable the multicompile only if we can receive SSR or not, and then C# code drive
            // it based on if SSR transparent in frame settings and not (and stripper can strip it).
            // this is currently not possible with our current preprocessor as _DISABLE_SSR_TRANSPARENT is a keyword not a define
            // so instead we used this and chose to pay the extra cost of normal write even if SSR transaprent is disabled.
            // Ideally the shader graph generator should handle it but condition below can't be handle correctly for now.
            #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
            #if !defined(_DISABLE_SSR_TRANSPARENT) && !defined(SHADER_UNLIT)
                #define WRITE_NORMAL_BUFFER
            #endif
            #endif

            #ifndef DEBUG_DISPLAY
                // In case of opaque we don't want to perform the alpha test, it is done in depth prepass and we use depth equal for ztest (setup from UI)
                // Don't do it with debug display mode as it is possible there is no depth prepass in this case
                #if !defined(_SURFACE_TYPE_TRANSPARENT)
                    #if SHADERPASS == SHADERPASS_FORWARD
                    #define SHADERPASS_FORWARD_BYPASS_ALPHA_TEST
                    #elif SHADERPASS == SHADERPASS_GBUFFER
                    #define SHADERPASS_GBUFFER_BYPASS_ALPHA_TEST
                    #endif
                #endif
            #endif

            // Translate transparent motion vector define
            #if defined(_TRANSPARENT_WRITES_MOTION_VEC) && defined(_SURFACE_TYPE_TRANSPARENT)
                #define _WRITE_TRANSPARENT_MOTION_VECTOR
            #endif

            // Dots Instancing
            // DotsInstancingOptions: <None>

            // Various properties

            // HybridV1InjectedBuiltinProperties: <None>

            // -- Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
        float2 Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
        float Vector1_12099adc799f47a3ab28a9f6b8a8d486;
        float Vector1_26e6208e64274d6dbf44fe0ba49cd12c;
        float Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
        float Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd;
        float4 Texture2D_97e74005f4314558975099848000c8a1_TexelSize;
        float Vector1_fc971ba86fc9493da309bdda370ac26c;
        float4 _EmissionColor;
        float _UseShadowThreshold;
        float4 _DoubleSidedConstants;
        float _BlendMode;
        float _EnableBlendModePreserveSpecularLighting;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(Texture2D_97e74005f4314558975099848000c8a1);
        SAMPLER(samplerTexture2D_97e74005f4314558975099848000c8a1);

            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif

            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif

            // Includes
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Debug/DebugDisplay.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct AttributesMesh
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct VaryingsMeshToPS
        {
            float4 positionCS : SV_POSITION;
            float3 positionRWS;
            float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float4 uv0;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryingsMeshToPS
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
        };

            PackedVaryingsMeshToPS PackVaryingsMeshToPS (VaryingsMeshToPS input)
        {
            PackedVaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionRWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }
        VaryingsMeshToPS UnpackVaryingsMeshToPS (PackedVaryingsMeshToPS input)
        {
            VaryingsMeshToPS output;
            output.positionCS = input.positionCS;
            output.positionRWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph


            // Graph Functions
            
        void Unity_Multiply_float(float A, float B, out float Out)
        {
            Out = A * B;
        }

        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }

        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }


        inline float Unity_SimpleNoise_RandomValue_float (float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
        }

        inline float Unity_SimpleNnoise_Interpolate_float (float a, float b, float t)
        {
            return (1.0-t)*a + (t*b);
        }


        inline float Unity_SimpleNoise_ValueNoise_float (float2 uv)
        {
            float2 i = floor(uv);
            float2 f = frac(uv);
            f = f * f * (3.0 - 2.0 * f);

            uv = abs(frac(uv) - 0.5);
            float2 c0 = i + float2(0.0, 0.0);
            float2 c1 = i + float2(1.0, 0.0);
            float2 c2 = i + float2(0.0, 1.0);
            float2 c3 = i + float2(1.0, 1.0);
            float r0 = Unity_SimpleNoise_RandomValue_float(c0);
            float r1 = Unity_SimpleNoise_RandomValue_float(c1);
            float r2 = Unity_SimpleNoise_RandomValue_float(c2);
            float r3 = Unity_SimpleNoise_RandomValue_float(c3);

            float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
            float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
            float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
            return t;
        }
        void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
        {
            float t = 0.0;

            float freq = pow(2.0, float(0));
            float amp = pow(0.5, float(3-0));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            freq = pow(2.0, float(1));
            amp = pow(0.5, float(3-1));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            freq = pow(2.0, float(2));
            amp = pow(0.5, float(3-2));
            t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

            Out = t;
        }

        void Unity_Lerp_float2(float2 A, float2 B, float2 T, out float2 Out)
        {
            Out = lerp(A, B, T);
        }

            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float4 VTPackedFeedback;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2;
            Unity_Multiply_float(1, IN.TimeParameters.x, _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2);
            float _Property_b60892c5746043649f99bf2c66cc56c5_Out_0 = Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
            float _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2;
            Unity_Multiply_float(_Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2, _Property_b60892c5746043649f99bf2c66cc56c5_Out_0, _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2);
            float2 _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), (_Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2.xx), _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3);
            float _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0 = Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
            float _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2;
            Unity_Multiply_float(-0.9, _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0, _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2);
            float _Multiply_064d66b2632a41288f85866dba571a49_Out_2;
            Unity_Multiply_float(_Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2, IN.TimeParameters.x, _Multiply_064d66b2632a41288f85866dba571a49_Out_2);
            float2 _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), (_Multiply_064d66b2632a41288f85866dba571a49_Out_2.xx), _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3);
            float _Property_7be3c100a4d5447b8980bcc9de7fe4a1_Out_0 = Vector1_26e6208e64274d6dbf44fe0ba49cd12c;
            float _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3;
            Unity_Remap_float(_Property_7be3c100a4d5447b8980bcc9de7fe4a1_Out_0, float2 (0, 10), float2 (0, 500), _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3);
            float _SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2;
            Unity_SimpleNoise_float(_TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3, _Remap_cbc0e45b0d374b0bb12b866637dab33f_Out_3, _SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2);
            float _Property_072b6d7671c24baea9016177302bcdee_Out_0 = Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd;
            float2 _Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3;
            Unity_Lerp_float2(_TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3, (_SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2.xx), (_Property_072b6d7671c24baea9016177302bcdee_Out_0.xx), _Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3);
            float2 _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0 = Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
            float2 _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3;
            Unity_TilingAndOffset_float(_Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3, _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0, float2 (1, 1), _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
            float4 _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_MainTex).tex, UnityBuildTexture2DStructNoScale(_MainTex).samplerstate, _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_R_4 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.r;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_G_5 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.g;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_B_6 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.b;
            float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_A_7 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.a;
            UnityTexture2D _Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0 = UnityBuildTexture2DStructNoScale(Texture2D_97e74005f4314558975099848000c8a1);
            float4 _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.tex, _Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.samplerstate, IN.uv0.xy);
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_R_4 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.r;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_G_5 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.g;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_B_6 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.b;
            float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_A_7 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.a;
            surface.BaseColor = (_SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.xyz);
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = (_SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0).x;
            {
                surface.VTPackedFeedback = float4(1.0f,1.0f,1.0f,.0f);
            }
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            
        VertexDescriptionInputs AttributesMeshToVertexDescriptionInputs(AttributesMesh input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }

        AttributesMesh ApplyMeshModification(AttributesMesh input, float3 timeParameters)
        {
            // build graph inputs
            VertexDescriptionInputs vertexDescriptionInputs = AttributesMeshToVertexDescriptionInputs(input);
            // Override time paramters with used one (This is required to correctly handle motion vector for vertex animation based on time)

            // evaluate vertex graph
            VertexDescription vertexDescription = VertexDescriptionFunction(vertexDescriptionInputs);

            // copy graph output to the results
            input.positionOS = vertexDescription.Position;
            input.normalOS = vertexDescription.Normal;
            input.tangentOS.xyz = vertexDescription.Tangent;

            return input;
        }
            FragInputs BuildFragInputs(VaryingsMeshToPS input)
        {
            FragInputs output;
            ZERO_INITIALIZE(FragInputs, output);

            // Init to some default value to make the computer quiet (else it output 'divide by zero' warning even if value is not used).
            // TODO: this is a really poor workaround, but the variable is used in a bunch of places
            // to compute normals which are then passed on elsewhere to compute other values...
            output.tangentToWorld = k_identity3x3;
            output.positionSS = input.positionCS;       // input.positionCS is SV_Position

            output.positionRWS = input.positionRWS;
            output.texCoord0 = input.texCoord0;

            return output;
        }

        SurfaceDescriptionInputs FragInputsToSurfaceDescriptionInputs(FragInputs input, float3 viewWS)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            #if defined(SHADER_STAGE_RAY_TRACING)
            #else
            #endif
            output.uv0 =                         input.texCoord0;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value

            return output;
        }

        // existing HDRP code uses the combined function to go directly from packed to frag inputs
        FragInputs UnpackVaryingsMeshToFragInputs(PackedVaryingsMeshToPS input)
        {
            UNITY_SETUP_INSTANCE_ID(input);
            VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(input);
            return BuildFragInputs(unpacked);
        }

            // --------------------------------------------------
            // Build Surface Data (Specific Material)

        void BuildSurfaceData(FragInputs fragInputs, inout SurfaceDescription surfaceDescription, float3 V, PositionInputs posInput, out SurfaceData surfaceData)
        {
            // setup defaults -- these are used if the graph doesn't output a value
            ZERO_INITIALIZE(SurfaceData, surfaceData);

            // copy across graph values, if defined
            surfaceData.color = surfaceDescription.BaseColor;

            #ifdef WRITE_NORMAL_BUFFER
            // When we need to export the normal (in the depth prepass, we write the geometry one)
            surfaceData.normalWS = fragInputs.tangentToWorld[2];
            #endif

            #if defined(DEBUG_DISPLAY)
            if (_DebugMipMapMode != DEBUGMIPMAPMODE_NONE)
            {
                // TODO
            }
            #endif

            #if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
                HDShadowContext shadowContext = InitShadowContext();
                float shadow;
                float3 shadow3;
                // We need to recompute some coordinate not computed by default for shadow matte
                posInput = GetPositionInput(fragInputs.positionSS.xy, _ScreenSize.zw, fragInputs.positionSS.z, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
                float3 upWS = normalize(fragInputs.tangentToWorld[1]);
                uint renderingLayers = GetMeshRenderingLightLayer();
                ShadowLoopMin(shadowContext, posInput, upWS, asuint(_ShadowMatteFilter), renderingLayers, shadow3);
                shadow = dot(shadow3, float3(1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0));

                float4 shadowColor = (1.0 - shadow) * surfaceDescription.ShadowTint.rgba;
                float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);

                // Keep the nested lerp
                // With no Color (bsdfData.color.rgb, bsdfData.color.a == 0.0f), just use ShadowColor*Color to avoid a ring of "white" around the shadow
                // And mix color to consider the Color & ShadowColor alpha (from texture or/and color picker)
                #ifdef _SURFACE_TYPE_TRANSPARENT
                    surfaceData.color = lerp(shadowColor.rgb * surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow), surfaceDescription.Alpha);
                #else
                    surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1.0 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow);
                #endif
                localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;

                surfaceDescription.Alpha = localAlpha;
            #endif
        }

            // --------------------------------------------------
            // Get Surface And BuiltinData

            void GetSurfaceAndBuiltinData(FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData RAY_TRACING_OPTIONAL_PARAMETERS)
            {
                // Don't dither if displaced tessellation (we're fading out the displacement instead to match the next LOD)
                #if !defined(SHADER_STAGE_RAY_TRACING) && !defined(_TESSELLATION_DISPLACEMENT)
                #ifdef LOD_FADE_CROSSFADE // enable dithering LOD transition if user select CrossFade transition in LOD group
                LODDitheringTransition(ComputeFadeMaskSeed(V, posInput.positionSS), unity_LODFade.x);
                #endif
                #endif

                #ifndef SHADER_UNLIT
                #ifdef _DOUBLESIDED_ON
                    float3 doubleSidedConstants = _DoubleSidedConstants.xyz;
                #else
                    float3 doubleSidedConstants = float3(1.0, 1.0, 1.0);
                #endif

                ApplyDoubleSidedFlipOrMirror(fragInputs, doubleSidedConstants); // Apply double sided flip on the vertex normal
                #endif // SHADER_UNLIT

                SurfaceDescriptionInputs surfaceDescriptionInputs = FragInputsToSurfaceDescriptionInputs(fragInputs, V);
                SurfaceDescription surfaceDescription = SurfaceDescriptionFunction(surfaceDescriptionInputs);

                // Perform alpha test very early to save performance (a killed pixel will not sample textures)
                // TODO: split graph evaluation to grab just alpha dependencies first? tricky..
                #ifdef _ALPHATEST_ON
                    float alphaCutoff = surfaceDescription.AlphaClipThreshold;
                    #if SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_PREPASS
                    // The TransparentDepthPrepass is also used with SSR transparent.
                    // If an artists enable transaprent SSR but not the TransparentDepthPrepass itself, then we use AlphaClipThreshold
                    // otherwise if TransparentDepthPrepass is enabled we use AlphaClipThresholdDepthPrepass
                    #elif SHADERPASS == SHADERPASS_TRANSPARENT_DEPTH_POSTPASS
                    // DepthPostpass always use its own alpha threshold
                    alphaCutoff = surfaceDescription.AlphaClipThresholdDepthPostpass;
                    #elif (SHADERPASS == SHADERPASS_SHADOWS) || (SHADERPASS == SHADERPASS_RAYTRACING_VISIBILITY)
                    // If use shadow threshold isn't enable we don't allow any test
                    #endif

                    GENERIC_ALPHA_TEST(surfaceDescription.Alpha, alphaCutoff);
                #endif

                #if !defined(SHADER_STAGE_RAY_TRACING) && _DEPTHOFFSET_ON
                ApplyDepthOffsetPositionInput(V, surfaceDescription.DepthOffset, GetViewForwardDir(), GetWorldToHClipMatrix(), posInput);
                #endif

                #ifndef SHADER_UNLIT
                float3 bentNormalWS;
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData, bentNormalWS);

                // Builtin Data
                // For back lighting we use the oposite vertex normal
                InitBuiltinData(posInput, surfaceDescription.Alpha, bentNormalWS, -fragInputs.tangentToWorld[2], fragInputs.texCoord1, fragInputs.texCoord2, builtinData);

                #else
                BuildSurfaceData(fragInputs, surfaceDescription, V, posInput, surfaceData);

                ZERO_INITIALIZE(BuiltinData, builtinData); // No call to InitBuiltinData as we don't have any lighting
                builtinData.opacity = surfaceDescription.Alpha;

                #if defined(DEBUG_DISPLAY)
                    // Light Layers are currently not used for the Unlit shader (because it is not lit)
                    // But Unlit objects do cast shadows according to their rendering layer mask, which is what we want to
                    // display in the light layers visualization mode, therefore we need the renderingLayers
                    builtinData.renderingLayers = GetMeshRenderingLightLayer();
                #endif

                #endif // SHADER_UNLIT

                #ifdef _ALPHATEST_ON
                    // Used for sharpening by alpha to mask - Alpha to covertage is only used with depth only and forward pass (no shadow pass, no transparent pass)
                    builtinData.alphaClipTreshold = alphaCutoff;
                #endif

                // override sampleBakedGI - not used by Unlit

                builtinData.emissiveColor = surfaceDescription.Emission;

                // Note this will not fully work on transparent surfaces (can check with _SURFACE_TYPE_TRANSPARENT define)
                // We will always overwrite vt feeback with the nearest. So behind transparent surfaces vt will not be resolved
                // This is a limitation of the current MRT approach.
                builtinData.vtPackedFeedback = surfaceDescription.VTPackedFeedback;

                #if _DEPTHOFFSET_ON
                builtinData.depthOffset = surfaceDescription.DepthOffset;
                #endif

                // TODO: We should generate distortion / distortionBlur for non distortion pass
                #if (SHADERPASS == SHADERPASS_DISTORTION)
                builtinData.distortion = surfaceDescription.Distortion;
                builtinData.distortionBlur = surfaceDescription.DistortionBlur;
                #endif

                #ifndef SHADER_UNLIT
                // PostInitBuiltinData call ApplyDebugToBuiltinData
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
                #else
                ApplyDebugToBuiltinData(builtinData);
                #endif

                RAY_TRACING_OPTIONAL_ALPHA_TEST_PASS
            }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPassForwardUnlit.hlsl"

            ENDHLSL
        }
    }
    CustomEditor "Rendering.HighDefinition.HDUnlitGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}