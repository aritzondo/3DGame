﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShaders/4 - Rim Lighting" {
	Properties{
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess ("Shininess", Float) = 10
		_RimColor ("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower ("Rim Power", Range(0.1, 10.0)) = 3.0
	}
	SubShader{
		Pass{
			Tags{"LightMode"="ForwardBase"}
			CGPROGRAM
			#pragma vertex   vert
			#pragma fragment frag

			//User defined variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
         	uniform float  _Shininess;
         	uniform float4 _RimColor;
         	uniform float  _RimPower;

         	//Unity defined variables
         	uniform float4 _LightColor0;

         	//Base input structs
         	struct vertexInput{
         		float4 vertex : POSITION;
         		float3 normal : NORMAL;
         	};

         	struct vertexOutput{
         		float4 pos       : POSITION;
         		float4 posWorld  : TEXTCOORD0;
         		float3 normalDir : TEXTCOORD1;
         	};

         	//Vertex function
         	vertexOutput vert(vertexInput v){
         		vertexOutput o;

         		o.posWorld = mul(unity_ObjectToWorld, v.vertex);
         		o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz);
         		o.pos = UnityObjectToClipPos(v.vertex);

         		return o;
         	}

         	//Fragment function
         	float4 frag(vertexOutput i) : COLOR{

         		float3 normalDirection = i.normalDir;
         		float3 viewDirection   = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
         		float3 lightDirection  = normalize(_WorldSpaceLightPos0.xyz);
         		float atten = 1.0;

         		//Lighting
         		float3 diffuseReflection  = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
         		float3 specularReflection = diffuseReflection * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);

         		float rim = 1 - saturate(dot(normalize(viewDirection), normalDirection));
         		float3 rimLighting = atten * _LightColor0.xyz * _RimColor * saturate(dot(normalDirection, lightDirection)) * pow(rim, _RimPower);

         		float3 lightFinal = rimLighting + diffuseReflection + specularReflection + UNITY_LIGHTMODEL_AMBIENT;

         		return float4(lightFinal * _Color.xyz, 1.0);
         	}


			ENDCG
		}
	}
	//Fallback "Difusse";
}
