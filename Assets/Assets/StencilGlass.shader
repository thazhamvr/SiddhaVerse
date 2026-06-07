Shader "Custom/StencilGlass"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            ZWrite Off     
            ColorMask 0    

            Stencil
            {
                Ref 1         
                Comp Always   
                Pass Replace  
            }
        }
    }
}