Shader "Custom/Value" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;


		void surf (Input IN, inout SurfaceOutputStandard o) {

			float4 test = float4(1, 0, 0, 1);
			
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = test.rgb; // rbg만 사용하겠다는 뜻임. float4에서 float3으로 변환 됨. (.rgb)

			// RGB 순서 바꾸기 가능
			o.Albedo = test.brg; // 순서 변경 가능함.
			o.Albedo = test.gbr;
			o.Albedo = test.rrg;
			o.Albedo = test.r; // 1자리 숫자는 문제없이 받아들임.    (1, 1, 1)
			o.Albedo = 0.5; // (0.5, 0.5, 0.5)
			// Metallic and smoothness come from slider variables

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
