Shader "Unlit/UI_Healthbar"
{
    Properties
    {
        Vector1_d34cbc6ca56c44a682e5039b0a8726a1("ScrollSpeed", Range(0, 10)) = 0
        Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7("Tiling", Vector) = (1, 1, 0, 0)
        Vector1_12099adc799f47a3ab28a9f6b8a8d486("BubbleSpeed", Range(0, 10)) = 5
        Vector1_26e6208e64274d6dbf44fe0ba49cd12c("DistortionScale", Range(0, 10)) = 1
        Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45("DistorionSpeed", Range(0, 1)) = 0
        Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd("DistorionIntensity", Range(0, 1)) = 0
        [NoScaleOffset]Texture2D_c86a61e04766453dba18df3520e6b20c("Bubble", 2D) = "white" {}
        Vector1_8686ad52c4d94255af388d482dd9a407("Scale", Float) = 0
        [NonModifiableTextureData][NoScaleOffset]_MainTex("Texture2D", 2D) = "white" {}
        [HideInInspector]_EmissionColor("Color", Color) = (1, 1, 1, 1)
        [HideInInspector]_RenderQueueType("Float", Float) = 4
        [HideInInspector][ToggleUI]_AddPrecomputedVelocity("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DepthOffsetEnable("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_TransparentWritingMotionVec("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_AlphaCutoffEnable("Boolean", Float) = 0
        [HideInInspector]_TransparentSortPriority("_TransparentSortPriority", Float) = 0
        [HideInInspector][ToggleUI]_UseShadowThreshold("Boolean", Float) = 0
        [HideInInspector][ToggleUI]_DoubleSidedEnable("Boolean", Float) = 0
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
        [HideInInspector][ToggleUI]_EnableFogOnTransparent("Boolean", Float) = 1
        [HideInInspector]_CullModeForward("Float", Float) = 2
        [HideInInspector][Enum(Front, 1, Back, 2)]_TransparentCullMode("Float", Float) = 2
        [HideInInspector][Enum(UnityEditor.Rendering.HighDefinition.OpaqueCullMode)]_OpaqueCullMode("Float", Float) = 2
        [HideInInspector]_ZTestDepthEqualForOpaque("Float", Int) = 4
        [HideInInspector][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestTransparent("Float", Float) = 4
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
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always
        ZWrite Off
        Cull Off
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
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl" // Required to be include before we include properties as it define DECLARE_STACK_CB
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl" // Need to be here for Gradient struct definition

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 col : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 view : TEXCOORD1;
                float4 col : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.view = o.vertex;
                o.uv = v.uv;
                o.col = v.col;
                return o;
            }

            float4 _MainTex_TexelSize;
            float Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
            float2 Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
            float Vector1_12099adc799f47a3ab28a9f6b8a8d486;
            float Vector1_26e6208e64274d6dbf44fe0ba49cd12c;
            float Vector1_1bef6a5e2b5a4523bc1cd5c3bcbb9b45;
            float Vector1_3ba8268f48534aad8a9a3ac5ac98f5fd;
            float4 Texture2D_c86a61e04766453dba18df3520e6b20c_TexelSize;
            float Vector1_8686ad52c4d94255af388d482dd9a407;
            float4 _EmissionColor;
            float _UseShadowThreshold;
            float4 _DoubleSidedConstants;
            float _BlendMode;
            float _EnableBlendModePreserveSpecularLighting;

            SAMPLER(SamplerState_Linear_Repeat);
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(Texture2D_c86a61e04766453dba18df3520e6b20c);
            SAMPLER(samplerTexture2D_c86a61e04766453dba18df3520e6b20c);

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

            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }

            void Unity_Subtract_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A - B;
            }

            void Unity_Sine_float(float In, out float Out)
            {
                Out = sin(In);
            }

            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A + B;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float _Property_b60892c5746043649f99bf2c66cc56c5_Out_0 = Vector1_d34cbc6ca56c44a682e5039b0a8726a1;
                float2 _Movement_6f6cc3acec974f2b92acd25be6db2a38_OutVector2_1 = float2 (0, 0);
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
                Unity_Lerp_float2(_Movement_6f6cc3acec974f2b92acd25be6db2a38_OutVector2_1, (_SimpleNoise_8952d724a8a6496a8a190641002021c6_Out_2.xx), (_Property_072b6d7671c24baea9016177302bcdee_Out_0.xx), _Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3);
                float2 _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0 = Vector2_0c1fbe38b4f4447dbbbf4d419fdc3dc7;
                float2 _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3;
                Unity_TilingAndOffset_float(_Lerp_bea12c988bca44f88ac39d7987fa2d6f_Out_3, _Property_e76565bb43d64b05a66f14c2d7999b95_Out_0, float2 (1, 1), _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
                float4 _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_MainTex).tex, UnityBuildTexture2DStructNoScale(_MainTex).samplerstate, _TilingAndOffset_a0c4231f245f42cf9d3956e548666793_Out_3);
                float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_R_4 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.r;
                float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_G_5 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.g;
                float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_B_6 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.b;
                float _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_A_7 = _SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0.a;
                UnityTexture2D _Property_6818af42d50d459bbc55c0eecd49cdd8_Out_0 = UnityBuildTexture2DStructNoScale(Texture2D_c86a61e04766453dba18df3520e6b20c);
                float _Property_1750f8af1490406d9e7feabdf3d6d050_Out_0 = Vector1_8686ad52c4d94255af388d482dd9a407;
                float _Divide_d133363ab3a54df996463c8263806e57_Out_2;
                Unity_Divide_float(0.8, _Property_1750f8af1490406d9e7feabdf3d6d050_Out_0, _Divide_d133363ab3a54df996463c8263806e57_Out_2);
                float _Property_49ebd83af9c147f8bb8debf47bc833d8_Out_0 = Vector1_12099adc799f47a3ab28a9f6b8a8d486;
                float _Divide_4bcdee4f842c4773abd7b3092a55a59d_Out_2;
                Unity_Divide_float(_Property_49ebd83af9c147f8bb8debf47bc833d8_Out_0, 2, _Divide_4bcdee4f842c4773abd7b3092a55a59d_Out_2);
                float _Multiply_42f17a58753442f5a0c1e04e15aee601_Out_2;
                Unity_Multiply_float(_Time.y, _Divide_4bcdee4f842c4773abd7b3092a55a59d_Out_2, _Multiply_42f17a58753442f5a0c1e04e15aee601_Out_2);
                float2 _Vector2_3d7aa46a1d6b4b048b716e10b1b363a5_Out_0 = float2(_Multiply_42f17a58753442f5a0c1e04e15aee601_Out_2, 0.1);
                float2 _TilingAndOffset_b8607c0b199141d88da66c53ab3b0abb_Out_3;
                Unity_TilingAndOffset_float((i.vertex.xy), (_Divide_d133363ab3a54df996463c8263806e57_Out_2.xx), _Vector2_3d7aa46a1d6b4b048b716e10b1b363a5_Out_0, _TilingAndOffset_b8607c0b199141d88da66c53ab3b0abb_Out_3);
                float4 _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6818af42d50d459bbc55c0eecd49cdd8_Out_0.tex, _Property_6818af42d50d459bbc55c0eecd49cdd8_Out_0.samplerstate, _TilingAndOffset_b8607c0b199141d88da66c53ab3b0abb_Out_3);
                float _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_R_4 = _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0.r;
                float _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_G_5 = _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0.g;
                float _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_B_6 = _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0.b;
                float _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_A_7 = _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0.a;
                float4 _Subtract_492d3510eeb44a14ab0c10e1bb541e26_Out_2;
                Unity_Subtract_float4(_SampleTexture2D_0368aa9c67ff41fdaf824f3082183152_RGBA_0, _SampleTexture2D_2f7e3f312c57494e99c30d68c29e645d_RGBA_0, _Subtract_492d3510eeb44a14ab0c10e1bb541e26_Out_2);
                float _Property_9097d7cb5d964803a6f318ab2f0d9b08_Out_0 = Vector1_8686ad52c4d94255af388d482dd9a407;
                float _Divide_dc651c6cb0ca48eca992d22ca552b93e_Out_2;
                Unity_Divide_float(1, _Property_9097d7cb5d964803a6f318ab2f0d9b08_Out_0, _Divide_dc651c6cb0ca48eca992d22ca552b93e_Out_2);
                float _Property_338f7f889ceb43f7bd56dbce714889d0_Out_0 = Vector1_12099adc799f47a3ab28a9f6b8a8d486;
                float _Multiply_de30601169984cacb8683361376f211f_Out_2;
                Unity_Multiply_float(_Time.y, _Property_338f7f889ceb43f7bd56dbce714889d0_Out_0, _Multiply_de30601169984cacb8683361376f211f_Out_2);
                float _Multiply_b3dcb45f0918454b834b044b798eaa11_Out_2;
                Unity_Multiply_float(_Time.y, 0.7, _Multiply_b3dcb45f0918454b834b044b798eaa11_Out_2);
                float _Sine_4c44cbc467604150ad2cec2eec7ec17d_Out_1;
                Unity_Sine_float(_Multiply_b3dcb45f0918454b834b044b798eaa11_Out_2, _Sine_4c44cbc467604150ad2cec2eec7ec17d_Out_1);
                float _Multiply_b133588e868e49028a60ee38b9e45037_Out_2;
                Unity_Multiply_float(_Sine_4c44cbc467604150ad2cec2eec7ec17d_Out_1, _Property_338f7f889ceb43f7bd56dbce714889d0_Out_0, _Multiply_b133588e868e49028a60ee38b9e45037_Out_2);
                float2 _Vector2_e8234008bd854668a97807cd2d40284a_Out_0 = float2(_Multiply_de30601169984cacb8683361376f211f_Out_2, _Multiply_b133588e868e49028a60ee38b9e45037_Out_2);
                float2 _TilingAndOffset_d820b92beb464e3f97b4542974c7e0fa_Out_3;
                Unity_TilingAndOffset_float((i.vertex.xy), (_Divide_dc651c6cb0ca48eca992d22ca552b93e_Out_2.xx), _Vector2_e8234008bd854668a97807cd2d40284a_Out_0, _TilingAndOffset_d820b92beb464e3f97b4542974c7e0fa_Out_3);
                float4 _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6818af42d50d459bbc55c0eecd49cdd8_Out_0.tex, _Property_6818af42d50d459bbc55c0eecd49cdd8_Out_0.samplerstate, _TilingAndOffset_d820b92beb464e3f97b4542974c7e0fa_Out_3);
                float _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_R_4 = _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0.r;
                float _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_G_5 = _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0.g;
                float _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_B_6 = _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0.b;
                float _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_A_7 = _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0.a;
                float4 _Add_c64dd108930047ddb29273b0a59c1316_Out_2;
                Unity_Add_float4(_Subtract_492d3510eeb44a14ab0c10e1bb541e26_Out_2, _SampleTexture2D_8eb46aac1de442758c1a8081dc468de4_RGBA_0, _Add_c64dd108930047ddb29273b0a59c1316_Out_2);
                
                float4 col;
                col.rgb = (_Add_c64dd108930047ddb29273b0a59c1316_Out_2.xyz) * i.col.rgb;
                col.a = i.col.a;
                return col;
            }
            ENDCG
        }
    }
}
