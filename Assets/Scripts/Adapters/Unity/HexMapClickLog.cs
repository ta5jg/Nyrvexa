// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Simulation.World;
using UnityEngine;

namespace Nyrvexa.Adapters
{
    /// <summary> Play: sol tık → XZ döşemeye ışın, ızgara sütun/satır ve düz indeks. </summary>
    [DisallowMultipleComponent]
    public sealed class HexMapClickLog : MonoBehaviour
    {
        [Min(0.01f)] [SerializeField] private float _hexRadius = 1f;
        [Min(1)] [SerializeField] private int _mapWidth = WorldMapDefaults.MapWidth;
        [Min(1)] [SerializeField] private int _mapHeight = WorldMapDefaults.MapHeight;
        [Tooltip("Izgara kökü (Genişlik/hex bu objenin world pozu ile)")]
        [SerializeField] private Transform _gridRoot;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _planeY;
        [SerializeField] private bool _onlyInPlay = true;
        [SerializeField] private bool _syncMapFromSessionHost;
        [SerializeField] private GameSessionHost _session;
        [Header("v0.2")]
        [Tooltip("Açık: tık → keşifçi hareketi kuyruğa (sonraki tur uygular). Adım modu + EnsureBootstrapped gerekir.")]
        [SerializeField] private bool _queueV02ScoutMove;
        [Tooltip("Açık: sağ tık → o turdaki kuyruğu boşatır (komut yok, kayıt yok).")]
        [SerializeField] private bool _clearQueueOnRightClick = true;

        private void Update()
        {
            if (_onlyInPlay && !Application.isPlaying) return;
            if (Input.GetMouseButtonDown(1) && _clearQueueOnRightClick)
            {
                var hr = _session != null ? _session : GetComponent<GameSessionHost>();
                if (hr == null) hr = UnityEngine.Object.FindFirstObjectByType<GameSessionHost>();
                hr?.V02_ClearCommandQueue();
                return;
            }
            if (!Input.GetMouseButtonDown(0)) return;
            var cam = _camera != null ? _camera : Camera.main;
            if (cam == null) return;
            var root = _gridRoot != null ? _gridRoot : transform;
            if (_syncMapFromSessionHost)
            {
                var h = _session != null ? _session : GetComponent<GameSessionHost>();
                if (h != null)
                {
                    _mapWidth = h.MapWidth;
                    _mapHeight = h.MapHeight;
                }
                if (TryGetComponent<HexGridGizmo>(out var giz))
                    _hexRadius = giz.HexRadiusWorld;
            }
            _planeY = root.position.y;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, new Vector3(0f, _planeY, 0f));
            if (!plane.Raycast(ray, out var dist)) return;
            var w = ray.GetPoint(dist);
            if (!HexLayoutUnity.TryWorldToColRow(_hexRadius, _mapWidth, _mapHeight, w, root.position, out var c, out var r)) return;
            var i = r * _mapWidth + c;
            Debug.Log($"Nyrvexa: tık col={c} row={r} flat={i}  world=({w.x:F2},{w.z:F2})");
            if (_queueV02ScoutMove)
            {
                var h = _session != null ? _session : GetComponent<GameSessionHost>();
                if (h == null) h = UnityEngine.Object.FindFirstObjectByType<GameSessionHost>();
                if (h != null && h.State != null) h.V02_TryQueueScoutMoveTo(i);
            }
        }
    }
}
