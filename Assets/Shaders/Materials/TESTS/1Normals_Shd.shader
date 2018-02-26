// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/TestShaders/1 - Normals"{

	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

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

				o.col=float4(v.normal, 1.0);
				o.pos=UnityObjectToClipPos(v.vertex);

				return o;
			}

			//fragment fuction
			float4 frag(vertexOutput i) : COLOR{
				return i.col;
			}

			ENDCG
		}
	}
	Fallback "Difusse"
}

