/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using UnityEditor;
using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 摄像机跟随脚本
    /// </summary>
    [HelpURL("https://gist.github.com/yasirkula/542edfadc9506a0e90d16bef8dbc6b05")]
    [ExecuteInEditMode]
    public class CameraFollow : MonoBehaviour
    {
        [Tooltip("Object to follow")]
        public Transform Target;

        [Tooltip("Target distance to the followed object")]
        public Vector3 DistanceToTarget = new Vector3(0f, 3f, -5f);

        [Tooltip("Adds an offset to the camera's focus point")]
        public Vector3 LookAtOffset = new Vector3(0f, 0f, 0f);

        [Range(0f, 1f)]
        [Tooltip("0.0: Camera will look in the forward direction of the Target\n" +
                 "1.0: Camera will look at the Target (With LookAtOffset applied)")]
        public float LookAtTargetModifier = 1f;

        [Tooltip("Lerp modifier for camera's position")]
        public float MovementSpeed = 10f;

        [Tooltip("Lerp modifier for camera's rotation")]
        public float RotationSpeed = 10f;

#if UNITY_EDITOR
        [Tooltip("Camera follows the target while not in Play mode, too")]
        public bool ExecuteInEditMode = false;
#endif

        // If the character is moving with physics inside FixedUpdate, then this function should also be changed to FixedUpdate
        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying && !ExecuteInEditMode)
                return;
#endif

            if (!Target)
                return;

            // Calculate camera's new position
            Vector3 targetPosition   = Target.position;
            Vector3 cameraPosition   = targetPosition + new Vector3(0f, DistanceToTarget.y, 0f);
            Vector3 focusPointOffset = new Vector3(0f, LookAtOffset.y, 0f);

            Vector3 targetForward = Target.forward;
            targetForward.y = 0f;
            targetForward.Normalize();

            cameraPosition   += targetForward * DistanceToTarget.z;
            focusPointOffset += targetForward * LookAtOffset.z;

            if (DistanceToTarget.x != 0f || LookAtOffset.x != 0f)
            {
                Vector3 targetRight = Target.right;
                targetRight.y = 0f;
                targetRight.Normalize();

                cameraPosition   += targetRight * DistanceToTarget.x;
                focusPointOffset += targetRight * LookAtOffset.x;
            }

            // Lerp camera's position to the target position
            Vector3 newPosition = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * MovementSpeed);
            transform.position = newPosition;

            // Lerp camera's rotation to the target rotation
            Quaternion cameraRotation;
            if (LookAtTargetModifier <= 0f)
                cameraRotation = Quaternion.LookRotation(targetForward);
            else
            {
                Vector3 lookAtDirection = targetPosition + focusPointOffset - newPosition;

                if (LookAtTargetModifier >= 1f)
                    cameraRotation = Quaternion.LookRotation(lookAtDirection);
                else
                    cameraRotation = Quaternion.LerpUnclamped(Quaternion.LookRotation(targetForward), Quaternion.LookRotation(lookAtDirection), LookAtTargetModifier);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, Time.deltaTime * RotationSpeed);

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying && ExecuteInEditMode)
            {
                transform.position = cameraPosition;
                transform.rotation = cameraRotation;
            }
#endif
        }
    }
}