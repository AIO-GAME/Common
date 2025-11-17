using System.Collections.Generic;

namespace AIO
{
    public class AnimationSystem<T> : IAnimationSystem
    where T : class, IAnimationJob, new()
    {
        private const int DEFAULT_INITIAL_CAPACITY = 4;

        private static AnimationSystem<T> instance;

        private readonly List<T> animations;
        private readonly List<T> animationsPool;
        private          float   animationSpeed;

        private AnimationSystem(int initialCapacity, float animationSpeed = 1f)
        {
            if (initialCapacity < 0)
                initialCapacity = 0;

            animations          = new List<T>(initialCapacity);
            animationsPool      = new List<T>(initialCapacity);
            this.animationSpeed = animationSpeed;

            PopulatePool(initialCapacity);
            ScriptedAnimations.RegisterAnimationSystem(this);
        }

        // Optional: Set the initial pool size and 'deltaTime' multiplier of the AnimationSystem
        public static void Initialize(int initialCapacity, float animationSpeed = 1f)
        {
            if (initialCapacity < 0)
                initialCapacity = 0;

            if (instance == null)
                instance = new AnimationSystem<T>(initialCapacity, animationSpeed);
            else
            {
                instance.PopulatePool(initialCapacity);
                instance.animationSpeed = animationSpeed;
            }
        }

        // Create a new animation and return it
        public static T NewAnimation()
        {
            if (instance == null)
                instance = new AnimationSystem<T>(DEFAULT_INITIAL_CAPACITY);

            return instance.NewAnimationInternal();
        }

        // Stop any animations that are animating "animatedObject"
        public static void StopAnimation(object animatedObject)
        {
            if (instance != null)
                instance.StopAnimationInternal(animatedObject);
        }

        // Stop all running animations
        public static void StopAllAnimations()
        {
            if (instance != null)
                instance.Clear(false);
        }

        // Stop processing this animation system and release all of its resources
        public static void Kill()
        {
            ScriptedAnimations.UnregisterAnimationSystem(instance);
            instance = null;
        }

        public void Execute(float deltaTime)
        {
            deltaTime *= animationSpeed;

            for (int i = animations.Count - 1; i >= 0; i--)
            {
                if (!animations[i].Execute(deltaTime))
                    RemoveAnimationAt(i);
            }
        }

        private T NewAnimationInternal()
        {
            T animation;
            if (animationsPool.Count > 0)
            {
                int lastPooledIndex = animationsPool.Count - 1;
                animation = animationsPool[lastPooledIndex];
                animationsPool.RemoveAt(lastPooledIndex);
            }
            else
                animation = new T();

            animations.Add(animation);
            return animation;
        }

        private void StopAnimationInternal(object animatedObject)
        {
            for (int i = animations.Count - 1; i >= 0; i--)
            {
                if (animations[i].CheckAnimatedObject(animatedObject))
                    RemoveAnimationAt(i);
            }
        }

        private void RemoveAnimationAt(int index)
        {
            animations[index].Clear();
            animationsPool.Add(animations[index]);

            // Replace the finished animation with the last animation
            int lastAnimationIndex = animations.Count - 1;
            animations[index] = animations[lastAnimationIndex];
            animations.RemoveAt(lastAnimationIndex);
        }

        private void PopulatePool(int capacity)
        {
            for (int i = capacity - animationsPool.Count; i > 0; i--)
                animationsPool.Add(new T());
        }

        // Pool all running animations
        public void Clear(bool invalidAnimationsOnly)
        {
            if (invalidAnimationsOnly)
            {
                for (int i = animations.Count - 1; i >= 0; i--)
                {
                    if (!animations[i].IsValid())
                        RemoveAnimationAt(i);
                }
            }
            else
            {
                for (int i = animations.Count - 1; i >= 0; i--)
                {
                    animations[i].Clear();
                    animationsPool.Add(animations[i]);
                }

                animations.Clear();
            }
        }
    }
}