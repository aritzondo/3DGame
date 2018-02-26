// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/TestShaders/2 - Lambert"{
	Properties{
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
	}
	SubShader{
		Pass{
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			//user defined variables
			uniform float4 _Color;

			//unity defined variables
			uniform float4 _LightColor0;

			struct vertexInput{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};

			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;

				float3 normalDirection = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				float atten = 1.0;
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

				//float3 difusseReflection = atten * _lightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				float3 difusseReflection = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				float3 lightFinal = difusseReflection + UNITY_LIGHTMODEL_AMBIENT.xyz;

				o.col=float4(lightFinal, 1.0);
				o.pos=UnityObjectToClipPos(v.vertex);

				return o;
			}

			//fragmentfuction
			float4 frag(vertexOutput i) : COLOR{
				return i.col * _Color;
			}

			ENDCG
		}
	}
	//Fallback "Difusse";
}

