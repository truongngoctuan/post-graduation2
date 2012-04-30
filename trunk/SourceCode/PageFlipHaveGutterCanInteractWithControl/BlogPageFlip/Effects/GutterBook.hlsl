sampler2D input : register(s0);

float fcenter:register(C0);
float dark:register(C1);
float darker:register(C2);
float darkest:register(C3);

float4 DarkColor(float2 uv : TEXCOORD, float center, float fDark) : COLOR
{
	float4 color = tex2D(input, uv);
	float dk = fDark - abs(center - uv.x);
	color[0] = color[0] * (1 - dk);
	color[1] = color[1] * (1 - dk);
	color[2] = color[2] * (1 - dk);
	return color;
}

float4 DarkerColor(float2 uv : TEXCOORD, float center, float fWidth, float fDark, float fDarker) : COLOR
{
	float4 color = tex2D(input, uv);
	float dk = fDark - abs(center - uv.x);
	dk = dk + fDarker * (1 - abs(center - uv.x) / fWidth);
	color[0] = color[0] * (1 - dk);
	color[1] = color[1] * (1 - dk);
	color[2] = color[2] * (1 - dk);
	return color;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
//float center = 0.5;
//
//float dark = 0.2;
//float darker = 0.15;
//float darkest = 0.4;

	if (abs(fcenter - uv.x) < 0.003)
	{
		return DarkColor(uv, fcenter, darkest);
	}

	if (abs(fcenter - uv.x) < 0.1)
	{
		return DarkerColor(uv, fcenter, 0.1, dark, darker);
	}

	return DarkColor(uv, fcenter, dark);
}