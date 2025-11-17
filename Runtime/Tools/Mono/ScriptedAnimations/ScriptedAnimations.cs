/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIO
{
    /// <summary>
    /// GC-free animation system for simple animations like scaling/moving objects or fading a UI element in Unity
    /// </summary>
    [AddComponentMenu("")] // Hide from Add Component menu
    [HelpURL("https://gist.github.com/yasirkula/86cf0b8cce094fbb93e97913eeda225b")]
    public class ScriptedAnimations : MonoBehaviour
    {
        private const bool POOL_INVALID_ANIMATIONS_ON_SCENE_CHANGE = true;

        private static   ScriptedAnimations     instance;
        private readonly List<IAnimationSystem> animationSystems = new List<IAnimationSystem>(8);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            instance = new GameObject("ScriptedAnimations").AddComponent<ScriptedAnimations>();
            DontDestroyOnLoad(instance.gameObject);
        }

        private void OnEnable()
        {
            if (POOL_INVALID_ANIMATIONS_ON_SCENE_CHANGE)
                SceneManager.activeSceneChanged += Clear;
        }

        private void OnDisable()
        {
            if (POOL_INVALID_ANIMATIONS_ON_SCENE_CHANGE)
                SceneManager.activeSceneChanged -= Clear;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            if (deltaTime <= 0f)
                return;

            for (int i = animationSystems.Count - 1; i >= 0; i--)
                animationSystems[i].Execute(deltaTime);
        }

        // Stop all running animations
        public static void Clear(bool invalidAnimationsOnly = false)
        {
            for (int i = instance.animationSystems.Count - 1; i >= 0; i--)
                instance.animationSystems[i].Clear(invalidAnimationsOnly);
        }

        // Stop all invalid animations when active Scene changes
        private void Clear(Scene s1, Scene s2) { Clear(true); }

        internal static void RegisterAnimationSystem(IAnimationSystem animationSystem)
        {
            if (animationSystem != null)
                instance.animationSystems.Add(animationSystem);
        }

        internal static void UnregisterAnimationSystem(IAnimationSystem animationSystem)
        {
            if (animationSystem != null)
                instance.animationSystems.Remove(animationSystem);
        }
    }

}