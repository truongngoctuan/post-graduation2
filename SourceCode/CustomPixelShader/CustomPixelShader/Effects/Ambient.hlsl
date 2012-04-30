sampler2D input : register(S0);
float2 center:register(C0);
float amplitude:register(C1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	if(pow((uv.x-center.x),2)+pow((uv.y-center.y),2)<0.15)
	{
		uv.y = uv.y + (sin(uv.y*100)*0.1*amplitude);
	}
	return tex2D( input , uv.xy);
}
