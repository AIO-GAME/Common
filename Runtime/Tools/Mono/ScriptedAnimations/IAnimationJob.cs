namespace AIO
{
    public interface IAnimationJob
    {
        bool Execute(float              deltaTime);      // Should return true while the animation is running, false when the animation is finished
        bool CheckAnimatedObject(object animatedObject); // Should return true if the animation is animating the "animatedObject"
        bool IsValid();                                  // Should return true if the animated object is still alive
        void Clear();                                    // Should clear any object references here
    }

    public interface IAnimationSystem
    {
        void Execute(float deltaTime);
        void Clear(bool    invalidAnimationsOnly);
    }
}