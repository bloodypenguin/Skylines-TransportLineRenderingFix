using ICities;

namespace TransportLineRenderingFix
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            var metro = PrefabCollection<TransportInfo>.FindLoaded("Metro");
            var train = PrefabCollection<TransportInfo>.FindLoaded("Train");
            if (train == null || metro == null)
            {
                return;
            }
            metro.m_lineMaterial.shader = train.m_lineMaterial.shader;
            metro.m_lineMaterial2.shader = train.m_lineMaterial2.shader;
        }
    }
}