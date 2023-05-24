Shader "Custom/ToonMaterialShader"
{
    Properties {
        _Thickness ("Thickness", Float) = 0.1
        _Color ("Color", Color) = (1,1,1,1)
        _BaseDiff("BaseDiff", Float) = 0.3
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Pass { 
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"     

            struct vertexInput {
                float4 vertex: POSITION;
                float3 normal: NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct vertexOutput {
                float4 pos : SV_POSITION;
                float4 col : TEXCOORD0;
                float3 tex : TEXCOORD1;
            };           

            uniform sampler2D _MainTex;
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float _BaseDiff;
            uniform float _Thickness;

            vertexOutput vert(vertexInput input) {
                vertexOutput output;
                output.pos = UnityObjectToClipPos(input.vertex);

                float4x4 modelMatrixInverse = unity_WorldToObject;

                float3 normalDirection = normalize(
                    mul(float4(input.normal, 0.0), modelMatrixInverse).xyz
                );

                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

                float diffuse = dot(normalDirection, lightDirection);

                if (diffuse > 0.6) {
                    diffuse = _BaseDiff + 0.7;
                } else if (diffuse > -0.2) {
                    diffuse = _BaseDiff + 0.35;
                } else {
                    diffuse = _BaseDiff;
                }

                //float3 diffuseReflection = _LightColor0.rgb * _Color.rgb * max(0.0, diffuse);
                float3 diffuseReflection = _LightColor0.rgb *  max(0.0, diffuse);
                output.col = float4(diffuseReflection, 1.0);
                output.tex = input.texcoord;
                return output;
            }

            float4 frag(vertexOutput input) : COLOR {
                return input.col * tex2D(_MainTex, input.tex.xy);
            }
            ENDCG
        }

        Pass {
            Cull Off
            Stencil {
                Ref 1
                Comp NotEqual
                Pass Keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            uniform float _Thickness;
                
            float4 vert(float4 vertex : POSITION, float3 normal : NORMAL) : SV_POSITION {
                return UnityObjectToClipPos(vertex + normal * _Thickness);
            }

            float4 frag(void) : COLOR {
                return float4(0.0, 0.0, 0.0, 0.0);
            }
            ENDCG
        }
    }
}
