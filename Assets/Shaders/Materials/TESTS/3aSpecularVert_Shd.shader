// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShaders/3a - Specular Lighting" {
	Properties{
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess ("Shininess", Float) = 10
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

         	//Unity defined variables
         	uniform float4 _LightColor0;

         	//Base input structs
         	struct vertexInput{
         		float4 vertex : POSITION;
         		float3 normal : NORMAL;
         	};

         	struct vertexOutput{
         		float4 pos : POSITION;
         		float4 col : COLOR;
         	};

         	//Vertex function
         	vertexOutput vert(vertexInput v){
         		vertexOutput o;

         		float3 normalDirection = normalize(mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz);
         		float3 viewDirection   = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
         		float3 lightDirection  = normalize(_WorldSpaceLightPos0.xyz);
         		float atten = 1.0;

         		//Lighting
         		float3 diffuseReflection  = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
         		float3 specularReflection = _SpecColor * diffuseReflection * pow(saturate(dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);

         		float3 lightFinal = diffuseReflection + specularReflection + UNITY_LIGHTMODEL_AMBIENT;

         		o.pos = UnityObjectToClipPos(v.vertex);
         		o.col = float4(lightFinal, 1.0);

         		return o;
         	}

         	//Fragment function
         	float4 frag(vertexOutput i) : COLOR{

         		return i.col * _Color;
         	}


			ENDCG
		}
	}
	//Fallback "Difusse";
}
