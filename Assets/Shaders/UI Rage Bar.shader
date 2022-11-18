Shader "Unlit/UI Rage Bar"
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
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100
        Cull Off
        ZTest Always
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Stencil
        {
           Ref[_Stencil]
           ReadMask[_StencilReadMask]
           WriteMask[_StencilWriteMask]
           Comp[_StencilComp]
           Pass[_StencilOp]
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" 
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl" 

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

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

            SAMPLER(SamplerState_Linear_Repeat);
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(Texture2D_97e74005f4314558975099848000c8a1);
            SAMPLER(samplerTexture2D_97e74005f4314558975099848000c8a1);

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


            inline float Unity_SimpleNoise_RandomValue_float(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            inline float Unity_SimpleNnoise_Interpolate_float(float a, float b, float t)
            {
                return (1.0 - t) * a + (t * b);
            }


            inline float Unity_SimpleNoise_ValueNoise_float(float2 uv)
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
                float amp = pow(0.5, float(3 - 0));
                t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                freq = pow(2.0, float(1));
                amp = pow(0.5, float(3 - 1));
                t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                freq = pow(2.0, float(2));
                amp = pow(0.5, float(3 - 2));
                t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

                Out = t;
            }

            void Unity_Lerp_float2(float2 A, float2 B, float2 T, out float2 Out)
            {
                Out = lerp(A, B, T);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2;
                Unity_Multiply_float(1, _Time.y, _Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2);
                float _Property_b60892c5746043649f99bf2c66cc56c5_Out_0 = Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
                float _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2;
                Unity_Multiply_float(_Multiply_9e7c869d284d4dbb8e66cce4110587bb_Out_2, _Property_b60892c5746043649f99bf2c66cc56c5_Out_0, _Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2);
                float2 _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3;
                Unity_TilingAndOffset_float(i.uv.xy, float2 (1, 1), (_Multiply_542dc7668b7d4e9da07e0f0c6bb6261a_Out_2.xx), _TilingAndOffset_c6c66c49a9de43ec806517a0f1008506_Out_3);
                float _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0 = Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
                float _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2;
                Unity_Multiply_float(-0.9, _Property_b681cf9b99de4d8c96ba0fa4cb2b4132_Out_0, _Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2);
                float _Multiply_064d66b2632a41288f85866dba571a49_Out_2;
                Unity_Multiply_float(_Multiply_dd3f7fa742e946bcaa7b6c3d06e9e1fd_Out_2, _Time.y, _Multiply_064d66b2632a41288f85866dba571a49_Out_2);
                float2 _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3;
                Unity_TilingAndOffset_float(i.uv.xy, float2 (1, 1), (_Multiply_064d66b2632a41288f85866dba571a49_Out_2.xx), _TilingAndOffset_2fb9eb48758842249a75bd01cd1d1068_Out_3);
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
                float4 _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.tex, _Property_3637b8ab4a6f43cf8b39f8b030fdedf3_Out_0.samplerstate, i.uv.xy);
                float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_R_4 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.r;
                float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_G_5 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.g;
                float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_B_6 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.b;
                float _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_A_7 = _SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0.a;
                
                float4 col;
                col.rgb = (_SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.xyz);
                col.a = (_SampleTexture2D_de2cfd80c9e24716b24cc28a16ca8bde_RGBA_0).x;

                return col;
            }
            ENDCG
        }
    }
}
