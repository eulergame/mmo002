Shader "X/Dot"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OutlineColor ("OutlineColor", Color) = (1,1,1,1)
        _Radius ("Radius", float) = 1
        _Stroke ("Stroke", float) = 0.1
        _Strength ("Strength", float) = 1
        _Softness ("Softness", float) = 1
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
            float4 _OutlineColor;
            float _Radius;
            float2 _Center;
            float _Stroke;
            float _Strength;
            float _Softness;
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
            //(that��s what the ��signed�� in signed distance field stands for).
            float distance(float2 p, float radius, float2 center)
            {
                return circle(p, center) - radius;
            }
            fixed4 GetColor(half d, fixed4 faceColor, fixed4 outlineColor, half outline, half softness)
            {
	            half faceAlpha = 1-saturate((d - outline * 0.5 + softness * 0.5) / (1.0 + softness));
	            half outlineAlpha = saturate((d + outline * 0.5)) * sqrt(min(1.0, outline));

	            faceColor.rgb *= faceColor.a;
	            outlineColor.rgb *= outlineColor.a;

	            faceColor = lerp(faceColor, outlineColor, outlineAlpha);

	            faceColor *= faceAlpha;

	            return faceColor;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float d = distance(i.worldPos.xy, _Radius, _Center);
                d = smoothstep(-_Stroke,_Stroke,d)*smoothstep(_Stroke,-_Stroke,d);
                fixed4 col = _Color * sqrt(d) * _Strength;
                //col = GetColor(d, _Color, _OutlineColor, _Stroke, _Softness);
                return col;
            }
            ENDCG
        }
    }
}
