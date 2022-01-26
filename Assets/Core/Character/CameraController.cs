using Pixelplacement;
using UnityEngine;

namespace Core.Character
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        public Transform cameraTarget;
        public float scrollSpeed = 8.0f;
        public float verticalOffset = 3.0f;
        
        private Transform borderContainerTRBL;
        private Vector2 borderMin;
        private Vector3 borderMax;

        private Transform currentBackgroundsObject;
        private Transform currentBackgroundsParent;

        private float scrollLeftPos = -0.1f;
        private float scrollRightPos = 0.1f;
        private float scrollUpPos = 0.0f;
        private float scrollDownPos = -4.0f;

        public bool IsFollowingPlayer { get; set; }

        private PlayerController player;
        private Vector2 targetPosition;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            player = PlayerController.Instance;
            IsFollowingPlayer = true;
        }

        void Update()
        {
            if (!IsFollowingPlayer) return;

            Vector2 playerScreenPos = player.transform.position - cameraTarget.position;

            // Scrolling in X
            if (playerScreenPos.x > scrollRightPos)
                targetPosition.x = Mathf.Min(player.transform.position.x, borderMax.x - 9.0f);
            else if (playerScreenPos.x < scrollLeftPos)
                targetPosition.x = Mathf.Max(player.transform.position.x, borderMin.x + 9.0f);

            cameraTarget.position = new Vector3(
                Mathf.SmoothStep(cameraTarget.position.x, targetPosition.x, Time.deltaTime * scrollSpeed),
                Mathf.SmoothStep(cameraTarget.position.y, targetPosition.y, Time.deltaTime * scrollSpeed),
                cameraTarget.position.z);
        }

        public void MoveToTarget()
        {
            targetPosition.x =
                Mathf.Max(Mathf.Min(player.transform.position.x, borderMax.x - 9.0f), borderMin.x + 9.0f);
            targetPosition.y = Mathf.Min(Mathf.Max(player.transform.position.y + verticalOffset, borderMin.y + 10.0f),
                borderMax.y - 5.0f);

            cameraTarget.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }

        public void SetBorders(Transform borderContainerTRBL)
        {
            this.borderContainerTRBL = borderContainerTRBL;
            UpdateBorders();
        }

        public void UpdateBorders()
        {
            borderMax = new Vector2(borderContainerTRBL.GetChild(1).position.x,
                borderContainerTRBL.GetChild(0).position.y);
            borderMin = new Vector2(borderContainerTRBL.GetChild(3).position.x,
                borderContainerTRBL.GetChild(2).position.y);
            MoveToTarget();
        }

        public void UpdateVertically()
        {
            // Update vertical target position (is controlled from the outside)
            if (IsFollowingPlayer)
            {
                targetPosition.y = Mathf.Min(Mathf.Max(player.transform.position.y + verticalOffset, borderMin.y + 10.0f), borderMax.y - 5.0f);
            }
        }

        public void ShakeCamera(float strength, float duration = 1.0f)
        {
            Tween.Shake(transform, transform.localPosition, new Vector3(strength, strength, 0), duration, 0);
        }
    }
}