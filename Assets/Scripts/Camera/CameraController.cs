using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Fields

        private Transform _playerTransform;
        private UnityEngine.Camera _camera;
        private Terrain _terrain;

        [SerializeField] private Transform player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Terrain terrain;
        [SerializeField] private float smoothDuration = 0.2f;
        
        private Vector2 _minBounds;
        private Vector2 _maxBounds;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
            CalculateBounds();
        }

        private void LateUpdate()
        {
            FollowPlayerWithDoTween();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (player == null)
            {
                Debug.LogError("Player transform is not assigned in CameraController.");
                return;
            }
        
            if (terrain == null)
            {
                Debug.LogError("Terrain is not assigned in CameraController.");
                return;
            }

            _playerTransform = player;
            _camera = UnityEngine.Camera.main;
            _terrain = terrain;
        }
        
        private void CalculateBounds()
        {
            var terrainPos = _terrain.transform.position;
            var terrainSize = _terrain.terrainData.size;

            _minBounds = new Vector2(terrainPos.x, terrainPos.z);
            _maxBounds = new Vector2(terrainPos.x + terrainSize.x, terrainPos.z + terrainSize.z);
        }

        private void FollowPlayerWithDoTween()
        {
            var desiredPosition = _playerTransform.position + offset;
            var clampedPosition = new Vector3(
                Mathf.Clamp(desiredPosition.x, _minBounds.x, _maxBounds.x),
                desiredPosition.y,
                Mathf.Clamp(desiredPosition.z, _minBounds.y, _maxBounds.y)
            );
        
            _camera.transform.DOMove(clampedPosition, smoothDuration).SetEase(Ease.InOutSine);
        }

        #endregion
    }
}
