using ColossalFramework;
using TransportLineRenderingFix.Redirection;
using UnityEngine;

namespace TransportLineRenderingFix.Detours
{
    [TargetType(typeof(TransportLine))]
    public class TransportLineDetour
    {
        [RedirectMethod]
        public static void RenderLine(ref TransportLine line, RenderManager.CameraInfo cameraInfo, int layerMask, int typeMask, ushort lineID)
        {
            if (line.m_flags == TransportLine.Flags.None)
                return;
            TransportInfo info =line.Info;
            //begin mod
            if ((1 << (int)(info.m_transportType & (TransportInfo.TransportType)31) & typeMask) == 0)
                return;
            //end mod
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