using ICities;

namespace TransportLineRenderingFix
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            var metro = PrefabCollection<TransportInfo>.FindLoaded("Metro");
            if (metro == null)
            {
                return;
            }
            metro.m_secondaryLayer = 9;
            metro.m_secondaryLineMaterial = metro.m_lineMaterial;
            metro.m_secondaryLineMaterial2 = metro.m_lineMaterial2;
        }
    }
}