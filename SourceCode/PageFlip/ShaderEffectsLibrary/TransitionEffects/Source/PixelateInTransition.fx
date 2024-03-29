float progress : register(C0);
sampler2D implicitInput : register(s0);
sampler2D oldInput : register(s1);



float4 PixelateIn(float2 uv)
{
	float pixels = 10 + 1000 * progress * progress;
	float2 newUV = round(uv * pixels) / pixels;
    float4 c2 = tex2D(implicitInput, newUV);

	return c2;
}

float4 main(float2 uv : TEXCOORD0) : COLOR0
{
	return PixelateIn(uv);
}