using Zenject;

namespace Diwide.Checkers
{
    public class TileInstaller : Installer<TileInstaller>
    {
        [Inject] private TileIndex TileIndex;
        public override void InstallBindings()
        {
            Container.BindInstance(TileIndex).NonLazy();
            Container.BindInterfacesAndSelfTo<TileFacade>().FromComponentOnRoot().AsSingle().NonLazy();
            // Container.Bind<Tile>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TilePointerHandler>()
                .FromComponentOnRoot().AsSingle().NonLazy();
            Container.BindSignal<SelectValidMoveSignal>()
                .ToMethod<TilePointerHandler>((x, s) =>
                {
                    if(s.TileIndexes.Contains(x.TileIndex)) x.OnSelectValidDestination(s.IsSelected);
                }).FromResolve();
        }
    }
}