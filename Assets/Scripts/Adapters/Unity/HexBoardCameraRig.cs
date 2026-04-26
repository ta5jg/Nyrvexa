/* =============================================================================
 * File:           Assets/Scripts/Adapters/Unity/HexBoardCameraRig.cs
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-04-26
 * Last Update:    2026-04-26
 * Version:        0.1.9
 * 
 * Description:
 *   Basit üstten (XZ) odak: ortografik, harita merkezine ve boyuta otur.
 * 
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

using Nyrvexa.Simulation.World;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nyrvexa.Adapters
{
    /// <summary> Basit üstten (XZ) odak: ortografik, harita merkezine ve boyuta otur. </summary>
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public sealed class HexBoardCameraRig : MonoBehaviour
    {
        [Tooltip("Boş: Camera.main, yoksa aynı objedeki kamera")]
        [SerializeField] private Camera _camera;
        [Min(0.01f)] [SerializeField] private float _hexRadius = 1f;
        [Min(1)] [SerializeField] private int _mapWidth = WorldMapDefaults.MapWidth;
        [Min(1)] [SerializeField] private int _mapHeight = WorldMapDefaults.MapHeight;
        [SerializeField] private float _heightY = 24f;
        [SerializeField] private float _orthoPad = 1.15f;
        [SerializeField] private bool _alignOnPlay = true;
        [Header("Play — üstten görünüm ok tuşu / WASD / teker")]
        [SerializeField] private bool _playPanZoom = true;
        [SerializeField] private float _panSpeed = 16f;
        [SerializeField] private float _zoomPerWheel = 3f;
        [SerializeField] [Min(0.5f)] private float _minOrtho = 2f;
        [SerializeField] [Min(0.5f)] private float _maxOrtho = 80f;
        [Tooltip("Açık: imleç UI üstündeyken tekerle zoom yapmaz (panel/hud).")]
        [SerializeField] private bool _skipZoomOnUi = true;

        private Vector3? _viewBookmarkPosition;
        private float? _viewBookmarkOrtho;
        public bool HasViewBookmark => _viewBookmarkPosition.HasValue;

        private void Start()
        {
            if (Application.isPlaying && _alignOnPlay)
                Align();
        }

        private void LateUpdate()
        {
            if (!Application.isPlaying || !_playPanZoom) return;
            var cam = ResolveCamera();
            if (cam == null) return;
            if (!cam.orthographic) return;
            var skipUi = _skipZoomOnUi && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            if (!skipUi)
            {
                var w = Input.GetAxis("Mouse ScrollWheel");
                if (Mathf.Abs(w) > 0.0001f)
                    cam.orthographicSize = Mathf.Clamp(
                        cam.orthographicSize - w * _zoomPerWheel, _minOrtho, _maxOrtho);
            }
            var dx = Input.GetAxis("Horizontal");
            var dz = Input.GetAxis("Vertical");
            if (Mathf.Abs(dx) < 0.0001f && Mathf.Abs(dz) < 0.0001f) return;
            var p = cam.transform.position;
            p.x += dx * _panSpeed * Time.deltaTime;
            p.z += dz * _panSpeed * Time.deltaTime;
            cam.transform.position = p;
        }

        /// <summary> Başlangıç harita kadrajı (Align ile aynı). </summary>
        public void ResetViewToMap() => Align();

        public void SaveViewBookmark()
        {
            var cam = ResolveCamera();
            if (cam == null) return;
            _viewBookmarkPosition = cam.transform.position;
            _viewBookmarkOrtho = cam.orthographicSize;
        }

        public void RestoreViewBookmark()
        {
            if (!HasViewBookmark) return;
            var cam = ResolveCamera();
            if (cam == null) return;
            cam.transform.position = _viewBookmarkPosition.Value;
            cam.orthographicSize = _viewBookmarkOrtho ?? cam.orthographicSize;
        }

        public void ClearViewBookmark()
        {
            _viewBookmarkPosition = null;
            _viewBookmarkOrtho = null;
        }

        [ContextMenu("Nyrvexa/Üstten haritaya hizala")]
        public void Align()
        {
            var cam = ResolveCamera();
            if (cam == null)
            {
                Debug.LogWarning("Nyrvexa: HexBoardCameraRig — kamera yok (Camera + rig veya atama ekle).");
                return;
            }
            var o = transform.position;
            HexLayoutUnity.GetMapAxisAlignedBounds(_hexRadius, _mapWidth, _mapHeight, o, 0f, out var center, out var hx, out var hz);
            var half = Mathf.Max(hx, hz) * _orthoPad;
            cam.orthographic = true;
            cam.orthographicSize = half;
            cam.nearClipPlane = 0.1f;
            cam.farClipPlane = _heightY + 500f;
            var pos = new Vector3(center.x, o.y + _heightY, center.z);
            cam.transform.position = pos;
            cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        /// <summary> Aynı objede GameSessionHost + HexGridGizmo varsa genişlik / yükseklik + hex yarıçapını oradan doldur. </summary>
        [ContextMenu("Nyrvexa/Genişliği yükseklik+hex: Host veya gizmo'dan al")]
        public void PullMapFromSiblings()
        {
            if (TryGetComponent<GameSessionHost>(out var h))
            {
                _mapWidth = h.MapWidth;
                _mapHeight = h.MapHeight;
            }
            if (TryGetComponent<HexGridGizmo>(out var g)) _hexRadius = g.HexRadiusWorld;
        }

        public Camera ResolveCamera()
        {
            if (_camera != null) return _camera;
            if (TryGetComponent<Camera>(out var c)) return c;
            return Camera.main;
        }
    }
}
