#include "UnitySprites.cginc"
#include "VertExmotion.cginc"

v2f VertExmotionSpriteVert(appdata_t IN)
{
	IN.vertex = VertExmotion(IN.vertex, float4(1, 1, 1, 1));
	return SpriteVert(IN);
}