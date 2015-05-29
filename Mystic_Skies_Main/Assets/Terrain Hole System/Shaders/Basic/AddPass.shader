Shader "Hidden/TerrainEngine/Splatmap/Lightmap-AddPass"
{
	Properties
	{
		_Control ("Control (RGBA)", 2D) = "black" {}
		_Splat3 ("Layer 3 (A)", 2D) = "white" {}
		_Splat2 ("Layer 2 (B)", 2D) = "white" {}
		_Splat1 ("Layer 1 (G)", 2D) = "white" {}
		_Splat0 ("Layer 0 (R)", 2D) = "white" {}
	}

	// without no-shadows-mode
	SubShader
	{
		LOD 1001
		Tags
		{
			"SplatCount" = "4"
			"Queue" = "Geometry+1"
			"IgnoreProjector"="True"
			"RenderType" = "Transparent"
		}
		Cull Off // comment out this line to only show the front face of terrain surfaces
		
		CGPROGRAM
		#pragma surface surf Lambert decal:add exclude_path:prepass
		struct Input
		{
			float2 uv_Control : TEXCOORD0;
			float2 uv_Splat0 : TEXCOORD1;
			float2 uv_Splat1 : TEXCOORD2;
			float2 uv_Splat2 : TEXCOORD3;
			float2 uv_Splat3 : TEXCOORD4;
		};
		
		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;

		float _CutoutModeHideAlpha;
		sampler2D _TransparencyMap;
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 splat_control = tex2D(_Control, IN.uv_Control);
			fixed3 col;
			col  = splat_control.r * tex2D(_Splat0, IN.uv_Splat0).rgb;
			col += splat_control.g * tex2D(_Splat1, IN.uv_Splat1).rgb;
			col += splat_control.b * tex2D(_Splat2, IN.uv_Splat2).rgb;
			col += splat_control.a * tex2D(_Splat3, IN.uv_Splat3).rgb;
			o.Albedo = col;
			o.Alpha = 0.0;
			
			fixed transApplication = tex2D(_TransparencyMap, IN.uv_Control).a;
			fixed totalOpacity = 1 - transApplication;
			
			if (_CutoutModeHideAlpha != -1) // cutout mode enabled
			{
				if (totalOpacity > _CutoutModeHideAlpha)
					o.Albedo = col / totalOpacity;
				else
					o.Albedo = float3(0, 0, 0);
			}
		}
		ENDCG
	}
	
	// with no-shadows-mode
	SubShader
	{
		LOD 1000
		Tags
		{
			"SplatCount" = "4"
			"Queue" = "AlphaTest+101"
			"IgnoreProjector"="True"
			"RenderType" = "Transparent"
		}
		Cull Off // comment out this line to only show the front face of terrain surfaces
		
		CGPROGRAM
		#pragma surface surf Lambert decal:add exclude_path:prepass
		struct Input
		{
			float2 uv_Control : TEXCOORD0;
			float2 uv_Splat0 : TEXCOORD1;
			float2 uv_Splat1 : TEXCOORD2;
			float2 uv_Splat2 : TEXCOORD3;
			float2 uv_Splat3 : TEXCOORD4;
		};
		
		sampler2D _Control;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;

		float _CutoutModeHideAlpha;
		sampler2D _TransparencyMap;
		
		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 splat_control = tex2D (_Control, IN.uv_Control);
			fixed3 col;
			col  = splat_control.r * tex2D (_Splat0, IN.uv_Splat0).rgb;
			col += splat_control.g * tex2D (_Splat1, IN.uv_Splat1).rgb;
			col += splat_control.b * tex2D (_Splat2, IN.uv_Splat2).rgb;
			col += splat_control.a * tex2D (_Splat3, IN.uv_Splat3).rgb;
			o.Albedo = col;
			o.Alpha = 0.0;
			
			fixed transApplication = tex2D(_TransparencyMap, IN.uv_Control).a;
			fixed totalOpacity = 1 - transApplication;
			
			if (_CutoutModeHideAlpha != -1) // cutout mode enabled
			{
				if (totalOpacity > _CutoutModeHideAlpha)
					o.Albedo = col / totalOpacity;
				else
					o.Albedo = float3(0, 0, 0);
			}
		}
		ENDCG
	}
	
	Fallback Off
}