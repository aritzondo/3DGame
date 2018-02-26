// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/CellShading"{
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
         		float3 normalDir : TEXTCOORD0;
         		float3 posWorld  : TEXTCOORD1;
			};

			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;

         		o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz);
         		o.pos = UnityObjectToClipPos(v.vertex);

         		o.posWorld = mul(unity_ObjectToWorld, v.vertex);

				/*float3 normalDirection = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				float atten = 1.0;
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

				float lighted = max(0.0, dot(normalDirection, lightDirection));
				if(lighted!=0.0){
					lighted=1.0;
				}
				lighted = atten * _LightColor0.xyz * lighted;

				float3 lightFinal = lighted;

				o.col=float4(lightFinal, 1.0);
				o.pos=UnityObjectToClipPos(v.vertex);*/

				return o;
			}

			//fragmentfuction
			float4 frag(vertexOutput i) : COLOR{
				
				float3 normalDirection = i.normalDir;
				float3 lightDirection;

				if(_WorldSpaceLightPos0.w==0){
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}else{
					float3 fragToLightSource = _WorldSpaceLightPos0 - i.posWorld;
					lightDirection = normalize(fragToLightSource);
				}

				float lighted = max(0.0, dot(normalDirection, lightDirection));
				if(lighted!=0.0){
					lighted=1.0;
				}

				float3 lightFinal = _LightColor0.xyz * lighted + UNITY_LIGHTMODEL_AMBIENT;

				return float4(lightFinal, 1.0) * _Color;
			}

			ENDCG
		}

		Pass{
			Tags{ "LightMode" = "ForwardAdd" }
			Blend One One
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
         		float3 normalDir : TEXTCOORD0;
         		float3 posWorld  : TEXTCOORD1;
			};

			//vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;

         		o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz);
         		o.pos = UnityObjectToClipPos(v.vertex);

         		o.posWorld = mul(unity_ObjectToWorld, v.vertex);

				/*float3 normalDirection = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
				float atten = 1.0;
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

				float lighted = max(0.0, dot(normalDirection, lightDirection));
				if(lighted!=0.0){
					lighted=1.0;
				}
				lighted = atten * _LightColor0.xyz * lighted;

				float3 lightFinal = lighted;

				o.col=float4(lightFinal, 1.0);
				o.pos=UnityObjectToClipPos(v.vertex);*/

				return o;
			}

			//fragmentfuction
			float4 frag(vertexOutput i) : COLOR{
				
				float3 normalDirection = i.normalDir;
				float3 lightDirection;

				if(_WorldSpaceLightPos0.w==0){
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}else{
					float3 fragToLightSource = _WorldSpaceLightPos0 - i.posWorld;
					lightDirection = normalize(fragToLightSource);
				}

				float lighted = max(0.0, dot(normalDirection, lightDirection));
				if(lighted!=0.0){
					lighted=1.0;
				}

				float3 lightFinal = _LightColor0.xyz * lighted;

				return float4(lightFinal, 1.0) * _Color;
			}

			ENDCG
		}
	}
	//Fallback "Difusse";
}

