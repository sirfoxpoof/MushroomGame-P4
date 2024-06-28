Shader "Unlit/ParticleCloud"
{
    Properties
    {
        _MainTex ("CloudColor", 2D) = "white" {}
        _MaskTex ("CloudMask", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Zwrite off
        
        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members color)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
       

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color; COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color ; COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MaskTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = (v.uv);
                O.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed maskR = Tex2D(_MaskTex.i.uv).r;
                col.a = maskR * i.color.a;
                return col;
            }
            ENDCG
        }
    }
}
