using UnityEngine;

namespace BoardDefenceGame.Core
{
    public class CameraController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float bottomPadding = 1f;
        [SerializeField] private float topPadding = 1f;
        private float boardWidth = 4f;
        private float boardHeight = 8f;
        private Camera mainCamera;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            mainCamera = Camera.main;
        }
        #endregion

        #region Public Methods
        public void ArrangeCamera(LevelDataScriptableObject currentLevelData)
        {
            boardWidth = currentLevelData.BoardDimensions.x;
            boardHeight = currentLevelData.BoardDimensions.y;
            SetCameraOrtographicSize();
            SetCameraPosition();
        }
        #endregion

        #region Private Methods
        private void SetCameraOrtographicSize()
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = boardWidth / (boardHeight + bottomPadding + topPadding);
            if (screenRatio >= targetRatio)
            {
                mainCamera.orthographicSize = (boardHeight + bottomPadding + topPadding) / 2;
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                mainCamera.orthographicSize = (boardHeight + bottomPadding + topPadding) / 2 * differenceInSize;
            }
        }

        private void SetCameraPosition()
        {
            float cameraOffsetY = (bottomPadding - topPadding) / 2;
            mainCamera.transform.position = new Vector3(boardWidth / 2 - .5f, (boardHeight / 2 - .5f) - cameraOffsetY, -10);
        }
        #endregion

    }

}
