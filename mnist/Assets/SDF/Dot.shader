Shader "X/Dot"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Radius", float) = 1
        _Stroke ("Stroke", float) = 0.1
        _Strength ("Strength", float) = 1
        _Center ("Center", vector) = (0,0,0)
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
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD0;
            };

            float4 _Color;
            float _Radius;
            float2 _Center;
            float _Stroke;
            float _Strength;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //calculate world position of vertex
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            float circle(float2 p, float2 center)
            {
                return length(p - center);
            }
            //A important thing about signed distance functions is that when inside a object we get the negative distance to the surface 
            //(that¡¯s what the ¡°signed¡± in signed distance field stands for).
            float distance(float2 p, float radius, float2 center)
            {
                return circle(p, center) - radius;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                float d = distance(i.worldPos.xy, _Radius, _Center);
                d = smoothstep(-_Stroke,_Stroke,d)*smoothstep(_Stroke,-_Stroke,d);
                fixed4 col = _Color * sqrt(d) * _Strength;
                return col;
            }
            ENDCG
        }
    }
}
