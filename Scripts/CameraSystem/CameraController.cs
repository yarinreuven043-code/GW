using UnityEngine;

namespace CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        [Tooltip("Player Transform from Persistent Scene")]
        public Transform target;

        [Header("Follow Settings")]
        public Vector3 offset = new Vector3(0f, 6f, -6f);
        public float followSpeed = 5f;
        public float lookHeightOffset = 1.5f;

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSpeed * Time.deltaTime
            );

            Vector3 lookTarget = target.position + Vector3.up * lookHeightOffset;
            transform.LookAt(lookTarget);
        }
    }
}
