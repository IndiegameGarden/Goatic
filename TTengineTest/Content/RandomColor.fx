
#include "Macros.fxh"

sampler TextureSampler : register(s0);



//DECLARE_TEXTURE(Texture, 0);


BEGIN_CONSTANTS

float4 DiffuseColor             _vs(c0)  _ps(c1)  _cb(c0);
float3 EmissiveColor            _vs(c1)  _ps(c2)  _cb(c1);
float3 SpecularColor            _vs(c2)  _ps(c3)  _cb(c2);
float  SpecularPower            _vs(c3)  _ps(c4)  _cb(c2.w);

float3 DirLight0Direction       _vs(c4)  _ps(c5)  _cb(c3);
float3 DirLight0DiffuseColor    _vs(c5)  _ps(c6)  _cb(c4);
float3 DirLight0SpecularColor   _vs(c6)  _ps(c7)  _cb(c5);

float3 DirLight1Direction       _vs(c7)  _ps(c8)  _cb(c6);
float3 DirLight1DiffuseColor    _vs(c8)  _ps(c9)  _cb(c7);
float3 DirLight1SpecularColor   _vs(c9)  _ps(c10) _cb(c8);

float3 DirLight2Direction       _vs(c10) _ps(c11) _cb(c9);
float3 DirLight2DiffuseColor    _vs(c11) _ps(c12) _cb(c10);
float3 DirLight2SpecularColor   _vs(c12) _ps(c13) _cb(c11);

float3 EyePosition              _vs(c13) _ps(c14) _cb(c12);

float3 FogColor                          _ps(c0)  _cb(c13);
float4 FogVector                _vs(c14)          _cb(c14);

float4x4 World                  _vs(c19)          _cb(c15);
float3x3 WorldInverseTranspose  _vs(c23)          _cb(c19);

MATRIX_CONSTANTS

float4x4 WorldViewProj          _vs(c15)          _cb(c0);

END_CONSTANTS

#include "Common.fxh"

struct VSInput
{
	float4 Position : POSITION;
};

struct VSOutput
{
	float4 PositionPS : SV_Position;
	float4 Diffuse    : COLOR0;
	float4 Specular   : COLOR1;
};


// Input: It uses texture coords as the random number seed.
// Output: Random number: [0,1), that is between 0.0 and 0.999999... inclusive.
// Author: Michael Pohoreski
// Copyright: Copyleft 2012 :-)
float random( float2 p )
{
  // We need irrationals for pseudo randomness.
  // Most (all?) known transcendental numbers will (generally) work.
  const float2 r = float2(
    23.1406926327792690,  // e^pi (Gelfond's constant)
     2.6651441426902251); // 2^sqrt(2) (Gelfond–Schneider constant)
  return frac( cos( 123456789. % 1e-7 + 256. * dot(p,r) ) );  
}

// Vertex shader: basic.
VSOutput VSBasic(VSInput vin)
{
	VSOutput vout;

	//CommonVSOutput cout = ComputeCommonVSOutput(vin.Position);
	vout.PositionPS = mul(vin.Position, WorldViewProj);
	//vout.PositionPS = cout.Pos_ps;

	return vout;
}

float4 PixelShaderFunction(float4 position : SV_Position, float4 color : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 tex = tex2D(TextureSampler, coords);
    tex.rgb = tex.rgb * float3(random(coords), random(coords/2) , random(coords/3) );
    return tex;
}

void VertexShaderFunction(inout float4 color    : COLOR0,
	inout float2 texCoord : TEXCOORD0,
	inout float4 position : POSITION0)
{
}

technique Technique1  
{  
    pass Pass1  
    {  
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
		//VertexShader = compile vs_4_0_level_9_1 VSBasic();
    }  
}
