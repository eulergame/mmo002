Shader "X/Tangent"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Stroke ("Stroke", float) = 0.1
        _Strength ("Strength", float) = 1
        _Zero ("Zero", float) = 0
        _SegmentX0 ("SegmentX0", Vector) = (0,0,0,0)
        _SegmentX1 ("SegmentX1", Vector) = (1,1,0,0)
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
            float2 _SegmentX0;
            float2 _SegmentX1;
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
            float2 parab(float2 p)
            {
                return float2(abs(p.y - (p.x-_Zero)*(p.x-_Zero)), 2*(p.x-_Zero));
            }
            float2 tangent(float2 p)
            {
                float c = cos(p.x-_Zero);
                return float2(abs(p.y - tan(p.x-_Zero)), 1/(c*c + 0.0001));
            }
            float2 cosine(float2 p)
            {
                float c = -sin(p.x-_Zero);
                return float2(abs(p.y - cos(p.x-_Zero)), c);
            }
            float2 sine(float2 p)
            {
                float c = cos(p.x-_Zero);
                return float2(abs(p.y - sin(p.x-_Zero)), c);
            }
            float2 squareroot(float2 p)
            {
                float c = 1/(2*(sqrt(p.x-_Zero) + 0.0001));
                return float2(abs(p.y - sqrt(p.x-_Zero)), c);
            }
            float2 lines(float2 p)
            {
                //f(x) = kx+b
                float k =1;
                float b = 0.1;
                float c = k;
                return float2(abs(p.y - (k*(p.x-_Zero) + b)), c);
            }
            float2 distancecircle(float2 p, float radius, float2 center)
            {
                return length(p - center) - radius;
            }
            float2 distancesegment(float2 p, float2 a, float2 b)
            {
                if(a.x > b.x)
                {
                    float2 temp = a;
                    a = b;
                    b = temp;
                }
                if(a.y == b.y)
                {
                    if(p.x < a.x){
                        return length(p-a);
                        }
                    else if(p.x > b.x){
                        return length(p-b);
                        }
                    else
                    {
                        return abs(p.y - a.y);
                    }
                }
                if(a.x == b.x)
                {
                    if(a.y > b.y)
                    {
                        float2 temp = a;
                        a = b;
                        b = temp;
                    }
                    if(p.y < a.y){
                        return length(p-a);
                        }
                    else if(p.y > b.y){
                        return length(p-b);
                        }
                    else
                    {
                        return abs(p.x - a.x);
                    }
                }
                float k = -(b.x-a.x)/(b.y-a.y);
                if(k < 0)
                {
                    if(p.y < (k*(p.x-a.x) + a.y)){
                        return length(p-a);
                    }
                    if(p.y > (k*(p.x-b.x) + b.y)){
                        return length(p-b);
                    }
                }
                else if(k>0)
                {
                    if(p.y > (k*(p.x-a.x) + a.y)){
                        return length(p-a);
                    }
                    if(p.y < (k*(p.x-b.x) + b.y)){
                        return length(p-b);
                    }
                }
                
                float2 c = p-a;
                float cd = length(c);
                float d = dot(c, normalize(b-a));
                return sqrt(max(cd*cd - d*d, 0));
            }
            float sdSegment(float2 p, float2 a, float2 b )
            {
                float2 pa = p-a, ba = b-a;
                float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
                return length( pa - ba*h );
            }
            float distance(float2 p)
            {
                float2 v = lines(p);
                return v.x / sqrt(1 + v.y * v.y);
            }
            float distancesquare(float2 p, float2 center, float halfAxis)
            {
                float a = min(
                    sdSegment(p, center + float2(-halfAxis, -halfAxis), center + float2(-halfAxis, halfAxis)),
                    sdSegment(p, center + float2(halfAxis, -halfAxis), center + float2(halfAxis, halfAxis)));
                float b = min(
                    sdSegment(p, center + float2(-halfAxis, halfAxis), center + float2(halfAxis, halfAxis)),
                    sdSegment(p, center + float2(-halfAxis, -halfAxis), center + float2(halfAxis, -halfAxis))); 
                return min(a,b);
            }
            float distancebezier(float2 p, float2 p0, float2 p1, float2 p2)
            {
                if(p.x < p0.x){
                    return length(p-p0);
                }
                    
                if(p.x > p2.x){
                    return length(p-p2);
                }
                float a = p0.x - 2*p1.x + p2.x;
                float b = 2*p1.x - 2*p0.x;
                float c = p0.x - p.x;
                float delta2 = b*b - 4*a*c;
                
                float t = (-b+sqrt(delta2))/(2*a);
                float y = (1-t)*(1-t)*p0.y + 2*t*(1-t)*p1.y + t*t*p2.y;
                float deltaY = abs(p.y - y);
                return deltaY;
                //float derivative = 2*t*(a+c-2*b) + 2*b - 2*a;
                //return deltaY / sqrt(1 + derivative * derivative);
            }
            float dot2( float2 v ) { return dot(v,v); }
            //https://inspirnathan.com/posts/51-shadertoy-tutorial-part-5
            float sdBezier(float2 pos, float2 A, float2 B, float2 C )
            {
                float2 a = B - A;
                float2 b = A - 2.0*B + C;
                float2 c = a * 2.0;
                float2 d = A - pos;
                float kk = 1.0/dot(b,b);
                float kx = kk * dot(a,b);
                float ky = kk * (2.0*dot(a,a)+dot(d,b)) / 3.0;
                float kz = kk * dot(d,a);
                float res = 0.0;
                float p = ky - kx*kx;
                float p3 = p*p*p;
                float q = kx*(2.0*kx*kx-3.0*ky) + kz;
                float h = q*q + 4.0*p3;
                if( h >= 0.0)
                {
                    h = sqrt(h);
                    float2 x = (float2(h,-h)-q)/2.0;
                    float2 uv = sign(x)*pow(abs(x), float2(1.0/3.0,1.0/3.0));
                    float t = clamp( uv.x+uv.y-kx, 0.0, 1.0 );
                    res = dot2(d + (c + b*t)*t);
                }
                else
                {
                    float z = sqrt(-p);
                    float v = acos( q/(p*z*2.0) ) / 3.0;
                    float m = cos(v);
                    float n = sin(v)*1.732050808;
                    float3  t = clamp(float3(m+m,-n-m,n-m)*z-kx,0.0,1.0);
                    res = min( dot2(d+(c+b*t.x)*t.x),
                               dot2(d+(c+b*t.y)*t.y) );
                    // the third root cannot be the closest
                    // res = min(res,dot2(d+(c+b*t.z)*t.z));
                }
                return sqrt( res );
            }

            //https://inspirnathan.com/posts/51-shadertoy-tutorial-part-5
            float sdHeart(float2 uv, float size, float2 offset) {
                  float x = uv.x - offset.x;
                  float y = uv.y - offset.y;
                  float xx = x * x;
                  float yy = y * y;
                  float yyy = yy * y;
                  float group = xx + yy - size;
                  float d = group * group * group - xx * yyy;
                  return d;
                }
            fixed4 frag (v2f i) : SV_Target
            {
                //float d = sdSegment(i.worldPos.xy, _SegmentX0, _SegmentX1);
                //float d = distancesegment(i.worldPos.xy, _SegmentX0, _SegmentX1);
                //float d = distance(i.worldPos.xy);
                //float d = distancesquare(i.worldPos.xy, float2(0,0), 3);
                //float d = distancebezier(i.worldPos.xy, float2(0,0), _SegmentX0, _SegmentX1);
                float d = sdBezier(i.worldPos.xy, float2(0,0), _SegmentX0, _SegmentX1);
                //float d = sdHeart(i.worldPos.xy, _SegmentX0.x, _SegmentX0.y);
                
                float d2 = d;
                float s = _Stroke;// * (fwidth(d2));
                d = smoothstep(-s,s,d)*smoothstep(s,-s,d);
                fixed4 col = _Color * sqrt(d) * _Strength;
                return col;
            }
            ENDCG
        }
    }
}
