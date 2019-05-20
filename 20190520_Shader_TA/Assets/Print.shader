Shader "Custom/Print" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_TestColor("TestColor" , Color) = (1,1,1,1)
		_TestEmission("TestEmission", Float) = 0.5
		_TestR("TestR", Range(0,1)) = 0.5 // R
		_TestG("TestG", Range(0,1)) = 0.5 // G 
		_TestB("TestB", Range(0,1)) = 0.5 // B
		_BrightDark("Brightness $ Darkness", Range(-1,1)) = 0 // 밝고 어둡게 하기
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

		fixed4 _TestColor;
		float _TestEmission;
		fixed _TestR; // 색상 R
		fixed _TestG; // 색상 G
		fixed _TestB; // 색상 B
		fixed _BrightDark; //밝기 조정

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Albedo = fixed3(_TestR, _TestG, _TestB).rgb + _BrightDark; // RGB값 넣기 + 밝기 조정
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
