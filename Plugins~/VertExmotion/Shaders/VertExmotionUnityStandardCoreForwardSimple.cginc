#include "UnityStandardCoreForwardSimple.cginc"
#include "VertExmotion.cginc"

VertexOutputBaseSimple vertForwardBaseSimpleVM(VertexInput v)
{
	v.vertex = VertExmotion(v.vertex, v.color);
	return vertForwardBaseSimple(VertexInput v);
}

VertexOutputForwardAddSimpleVM vertForwardAddSimple(VertexInput v)
{
	v.vertex = VertExmotion(v.vertex, v.color);
	return vertForwardAddSimple(VertexInput v);
}
