Shader "Custom/StencilWriter"
{
    Properties
    {
        _Color ("Lens Tint (Optional)", Color) = (1,1,1,0.05)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Geometry-1" }
        LOD 100

        // Write a 1 into the stencil buffer for every pixel this lens covers
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }

        // Do not block objects behind it from rendering physically
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
            };

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color; // Gives a very subtle glass tint
            }
            ENDCG
        }
    }
}