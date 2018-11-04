Shader "Unlit/shd_animated_texture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScrollSpeeds ("Scroll Speeds", vector) = (0, 0, 0, 0)
		_EmissionColor ("Color", Color) = (0.000000,0.000000,0.000000,1.000000)
 		_EmissionMap ("Emission", 2D) = "white" { }
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			uniform sampler2D _EmissionMap;
			uniform Vector _EmissionColor;
			float4 _MainTex_ST;
			float4 _ScrollSpeeds;


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv += _ScrollSpeeds * _Time.x;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 albedo = tex2D(_MainTex, i.uv);
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				//half4 output = half4(albedo.rgb * lighting.rgb, albedo.a);
				fixed4 emission = tex2D(_EmissionMap, i.uv) * _EmissionColor;
				//output.rgb += emission.rgb;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				col.rgb += emission.rgb;
				return col;
			}
			ENDCG
		}
	}
}
