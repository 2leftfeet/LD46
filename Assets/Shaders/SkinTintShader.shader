Shader "Sprites/Character"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SkinBase ("Skin Base", Color) = (1,1,1,1)
        _SkinChanged ("Skin Changed", Color) = (1,1,1,1)
        _Accent1Base ("Accent 1 Base", Color) = (1,1,1,1)
        _Accent1Changed ("Accent 1 Changed", Color) = (1,1,1,1)
        _Accent2Base ("Accent 2 Base", Color) = (1,1,1,1)
        _Accent2Changed ("Accent 2 Changed", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _SkinBase;
            fixed4 _SkinChanged;

            fixed4 _Accent1Base;
            fixed4 _Accent1Changed;

            fixed4 _Accent2Base;
            fixed4 _Accent2Changed;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                fixed4 skinDelta = c - _SkinBase;
                c = length(skinDelta) >= 0.1 ? c : _SkinChanged;

                fixed4 accent1Delta = c - _Accent1Base;
                c = length(accent1Delta) >= 0.1 ? c : _Accent1Changed;

                fixed4 accent2Delta = c - _Accent2Base;
                c = length(accent2Delta) >= 0.1 ? c : _Accent2Changed;
                
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}