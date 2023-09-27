#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 
namespace AIO
{
    public static class ClassHelper
    {
        public static string GetState(TChunkState state)
        {
            switch (state)
            {
                case TChunkState.Static:
                    return "static";
                case TChunkState.Virtual:
                    return "virtual";
                case TChunkState.Override:
                    return "override";
                case TChunkState.Abstract:
                    return "abstract";
                case TChunkState.SealedOverride:
                    return "sealed override";
                case TChunkState.New:
                    return "new";
                case TChunkState.NewStatic:
                    return "new static";
                case TChunkState.SealedClass:
                    return "sealed";
                default:
                    return string.Empty;
            }
        }

        public static ClassParam Create()
        {
            return new ClassParam();
        }
    }
}