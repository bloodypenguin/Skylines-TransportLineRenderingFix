using ColossalFramework;
using TransportLineRenderingFix.Redirection;
using UnityEngine;

namespace TransportLineRenderingFix.Detours
{
    [TargetType(typeof(TransportLine))]
    public class TransportLineDetour
    {
        [RedirectMethod]
        public static void RenderLine(ref TransportLine line, RenderManager.CameraInfo cameraInfo, int layerMask, ushort lineID)
        {
            if (line.m_flags == TransportLine.Flags.None)
                return;
            TransportInfo info =line.Info;
//            if ((1 << info.m_prefabDataLayer & layerMask) == 0)
//                return;
            TransportManager instance = Singleton<TransportManager>.instance;
            Mesh mesh = instance.m_lineMeshes[(int) lineID];
            if (!((UnityEngine.Object) mesh != (UnityEngine.Object) null))
                return;
            Material material = info.m_lineMaterial2;
            material.color = line.GetColor();
            if (!material.SetPass(0))
                return;
            ++instance.m_drawCallData.m_overlayCalls;
            Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
        }
    }
}