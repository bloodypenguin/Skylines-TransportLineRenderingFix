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
            TransportInfo info = line.Info;
            //begin mod
            if ((1 << (int)(info.m_transportType & (TransportInfo.TransportType)31) & typeMask) == 0)
                return;
            //end mod
            TransportManager instance1 = Singleton<TransportManager>.instance;
            TerrainManager instance2 = Singleton<TerrainManager>.instance;
            Mesh[] lineMesh = instance1.m_lineMeshes[(int)lineID];
            if (lineMesh == null)
                return;
            int length = lineMesh.Length;
            for (int index = 0; index < length; ++index)
            {
                Mesh mesh = lineMesh[index];
                if ((UnityEngine.Object)mesh != (UnityEngine.Object)null && cameraInfo.Intersect(mesh.bounds))
                {
                    Material lineMaterial2 = info.m_lineMaterial2;
                    lineMaterial2.color = line.GetColor();
                    lineMaterial2.SetFloat(instance1.ID_StartOffset, -1000f);
                    if (info.m_requireSurfaceLine)
                        instance2.SetWaterMaterialProperties(mesh.bounds.center, lineMaterial2);
                    if (lineMaterial2.SetPass(0))
                    {
                        ++instance1.m_drawCallData.m_overlayCalls;
                        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
                    }
                }
            }
        }
    }
}