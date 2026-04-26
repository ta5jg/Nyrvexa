// Copyright (c) 2026 Irfan Gedik. All rights reserved.
// This file is part of Nyrvexa. Licensing: see LICENSE in the repository root (TBD).
//


using Nyrvexa.Adapters;
using UnityEditor;
using UnityEngine;

namespace Nyrvexa.Editor
{
    public static class AddGameSessionHostMenu
    {
        [MenuItem("GameObject/Nyrvexa/GameSessionHost ekle (Play için Run On Start)", false, 10)]
        public static void AddHostWithRunOnStart()
        {
            var go = new GameObject("Nyrvexa_Session");
            Undo.RegisterCreatedObjectUndo(go, "Nyrvexa session");
            var host = Undo.AddComponent<GameSessionHost>(go);
            Undo.RecordObject(host, "Nyrvexa run on start");
            var so = new SerializedObject(host);
            so.FindProperty("_runOnStart").boolValue = true;
            so.ApplyModifiedProperties();
            Selection.activeGameObject = go;
        }

        [MenuItem("GameObject/Nyrvexa/Heks ızgara gizmosu (10×6, vurgu: düz 23 = scout)", false, 11)]
        public static void AddHexGridGizmo()
        {
            var go = new GameObject("Nyrvexa_HexGizmo");
            Undo.RegisterCreatedObjectUndo(go, "Nyrvexa hex gizmo");
            Undo.AddComponent<HexGridGizmo>(go);
            Selection.activeGameObject = go;
        }

        [MenuItem("GameObject/Nyrvexa/Prototip: Session + Heks gizmo (tek tık)", false, 9)]
        public static void AddSessionAndGizmo()
        {
            var go = new GameObject("Nyrvexa_Prototype");
            Undo.RegisterCreatedObjectUndo(go, "Nyrvexa");
            var host = Undo.AddComponent<GameSessionHost>(go);
            var giz = Undo.AddComponent<HexGridGizmo>(go);
            var rig = Undo.AddComponent<HexBoardCameraRig>(go);
            var clk = Undo.AddComponent<HexMapClickLog>(go);
            var board = Undo.AddComponent<NyrvexaV02HexBoardRenderer>(go);
            var hud = Undo.AddComponent<NyrvexaV02HudController>(go);
            var mapChrome = Undo.AddComponent<NyrvexaV02MapChrome>(go);
            var gso = new SerializedObject(giz);
            gso.FindProperty("_syncMapFromSessionHost").boolValue = true;
            gso.ApplyModifiedProperties();
            var rso = new SerializedObject(rig);
            rso.FindProperty("_alignOnPlay").boolValue = true;
            rso.ApplyModifiedProperties();
            var cso = new SerializedObject(clk);
            cso.FindProperty("_syncMapFromSessionHost").boolValue = true;
            cso.FindProperty("_queueV02ScoutMove").boolValue = true;
            cso.FindProperty("_gridRoot").objectReferenceValue = go.transform;
            cso.ApplyModifiedProperties();
            var bso = new SerializedObject(board);
            bso.FindProperty("_session").objectReferenceValue = host;
            bso.ApplyModifiedProperties();
            var hudSo = new SerializedObject(hud);
            hudSo.FindProperty("_session").objectReferenceValue = host;
            hudSo.ApplyModifiedProperties();
            var mapSo = new SerializedObject(mapChrome);
            mapSo.FindProperty("_session").objectReferenceValue = host;
            mapSo.FindProperty("_cameraRig").objectReferenceValue = rig;
            mapSo.ApplyModifiedProperties();
            Undo.RecordObject(rig, "Nyrvexa kamera alanı");
            rig.PullMapFromSiblings();
            Undo.RecordObject(host, "Nyrvexa v0.2 adım modu");
            var hso = new SerializedObject(host);
            hso.FindProperty("_runOnStart").boolValue = false;
            hso.FindProperty("_v02StepByStepMode").boolValue = true;
            hso.ApplyModifiedProperties();
            Selection.activeGameObject = go;
        }
    }
}
