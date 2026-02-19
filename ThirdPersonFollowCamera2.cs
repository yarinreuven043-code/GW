using UnityEngine;

namespace Camera
{
	public class ThirdPersonFollowCamera : MonoBehaviour
	{
		[Header("Target")]
		public Transform target;

		[Header("Settings")]
		public float yawSpeed = 8f;          // מהירות שינוי Yaw
		public float yawSmoothness = 10f;    // כמה חלק המעבר
		public float height = 2f;            // גובה מעל השחקן
		public float distance = 4f;          // מרחק מהגב

		private float currentYaw;
		private float targetYaw;

		private Vector3 lastTargetPosition;

		void Start()
		{
			if (target == null)
			{
				Debug.LogError("No Target Assigned!");
				enabled = false;
				return;
			}

			currentYaw = target.eulerAngles.y;
			lastTargetPosition = target.position;
		}

		void LateUpdate()
		{
			if (target == null)
				return;

			HandleYaw();
			FollowTarget();
		}

		void HandleYaw()
		{
			Vector3 movement = target.position - lastTargetPosition;

			// אם השחקן זז – סובב Yaw לפי כיוון התנועה
			if (movement.magnitude > 0.001f)
			{
				Vector3 flatDir = new Vector3(movement.x, 0f, movement.z).normalized;

				if (flatDir.sqrMagnitude > 0.001f)
				{
					targetYaw = Quaternion.LookRotation(flatDir).eulerAngles.y;
				}
			}

			// סיבוב חלק
			currentYaw = Mathf.LerpAngle(currentYaw, targetYaw, yawSmoothness * Time.deltaTime);

			lastTargetPosition = target.position;
		}

		void FollowTarget()
		{
			Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);

			Vector3 offset = rotation * new Vector3(0f, 0f, -distance);
			Vector3 desiredPosition = target.position + Vector3.up * height + offset;

			transform.position = desiredPosition;

			// LookAt לגב/מרכז השחקן
			Vector3 lookPoint = target.position + Vector3.up * height * 0.8f;
			transform.LookAt(lookPoint);
		}
	}
}
