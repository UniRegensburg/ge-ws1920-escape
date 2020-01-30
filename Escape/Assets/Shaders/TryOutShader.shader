Shader "Unlit/TryOutShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


			float2 rot(float2 p, float a) {
				float s = sin(a);
				float c = cos(a);

				return mul(float2x2(c, s, -s, c),p);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				float2 p = i.uv;

				static float s = 1000.0;
				static float c = 1000.0;
				static float z = 1000.0;

				for (int i = 0; i < 10; i++) {
					p = abs(p) / dot(p, p) - float2(0.9, 0.7);
					p = rot(p.yx, _Time.y*0.1);
					s = min(s, abs(p.y));
					c = min(c, abs(p.x));
					if (i < 5) z = min(z, length(p));
				}

				float3 col = lerp(float3(0.6, 0.34, 0.13), float3(1.0,1.0,1.0), s);
				col = lerp(col, (2.0), smoothstep(0.3, 1.0, z));

				col = lerp(col, float3(18, 8, 0), smoothstep(0.1, 1.0, c));

				return float4(col, 1.0);
			}

            
           
               
                
            ENDCG
        }
    }
}
