using ICities;
using TransportLineRenderingFix.Detours;
using TransportLineRenderingFix.Redirection;

namespace TransportLineRenderingFix
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
            {
                return;
            }
            base.OnLevelLoaded(mode);
            Redirector<TransportLineDetour>.Deploy();
            var metro = PrefabCollection<TransportInfo>.FindLoaded("Metro");
            var train = PrefabCollection<TransportInfo>.FindLoaded("Train");
            if (train != null)
            {
                train.m_lineMaterial.shader = metro.m_lineMaterial.shader;
                train.m_lineMaterial2.shader = metro.m_lineMaterial2.shader;
            }
            var bus = PrefabCollection<TransportInfo>.FindLoaded("Bus");
            if (bus != null)
            {
                bus.m_lineMaterial.shader = metro.m_lineMaterial.shader;
                bus.m_lineMaterial2.shader = metro.m_lineMaterial2.shader;
            }
            var tram = PrefabCollection<TransportInfo>.FindLoaded("Tram");
            if (tram != null)
            {
                tram.m_lineMaterial.shader = metro.m_lineMaterial.shader;
                tram.m_lineMaterial2.shader = metro.m_lineMaterial2.shader;
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            Redirector<TransportLineDetour>.Revert();
        }
    }
}