Shader "X/Tangent"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Stroke ("Stroke", float) = 0.1
        _Strength ("Strength", float) = 1
        _Zero ("Zero", float) = 0
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
            float _Stroke;
            float _Strength;
            float _Zero;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //calculate world position of vertex
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            //A important thing about signed distance functions is that when inside a object we get the negative distance to the surface 
            //(that¡¯s what the ¡°signed¡± in signed distance field stands for).
            float distance(float2 p)
            {
                float deltaY = abs(p.y - (p.x-_Zero)*(p.x-_Zero));
                float derivative = 2*(p.x-_Zero);
                float tangentTheta = derivative;
                float cosTheta = 1.0/sqrt(1+tangentTheta*tangentTheta);
                return deltaY * cosTheta;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float d = distance(i.worldPos.xy);
                float d2 = d;
                float s = _Stroke;// * d;//(ddx(d2)+0.015);
                d = smoothstep(-s,s,d)*smoothstep(s,-s,d);
                fixed4 col = _Color * sqrt(d) * _Strength;
                return col;
            }
            ENDCG
        }
    }
}
