using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Diwide.Checkers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<TilesRegistry>().AsSingle().NonLazy();
            
            Container.BindFactory<TileIndex, TileFacade, TileFacade.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(64)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_settings.TilePrefab, InstallTile)
                    .UnderTransformGroup("Board"));

            Container.BindInterfacesTo<BoardGenerator>().AsSingle();

            Container.BindFactory<TileFacade, ColorType, PawnFacade, PawnFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabMethod(_settings.PawnPrefab, InstallPawn);

            Container.BindInterfacesTo<PawnsGenerator>().AsSingle();
        }

        private void InstallTile(DiContainer subcontainer)
        {
            subcontainer.Bind<TileFacade>().FromComponentOnRoot().AsSingle().NonLazy();
            subcontainer.Bind<Tile>().AsSingle().NonLazy();
            subcontainer.BindInterfacesAndSelfTo<TilePointerHandler>()
                .FromComponentOnRoot().AsSingle().NonLazy();

            // var tileFacade = subcontainer.Resolve<TileFacade>();
            
            // .FromComponentInNewPrefab(_settings.PawnPrefab)
            // .UnderTransform(tileFacade.transform);
        }

        private void InstallPawn(DiContainer subcontainer, TileFacade tileFacade, ColorType color)
        {
            subcontainer.Bind<TileFacade>().FromInstance(tileFacade);
            subcontainer.Bind<ColorType>().FromInstance(color);
            subcontainer.BindInterfacesAndSelfTo<PawnFacade>()
                .FromComponentOnRoot().AsSingle().NonLazy();
        }

        // Transform GetPawnParent(InjectContext context)
        // {
        //     if (context.ObjectInstance is Component)
        //     {
        //         return ((Component)context.ObjectInstance).transform;
        //     }
        //
        //     return null;
        // }
        
        [Serializable]
        public class Settings
        {
            public int BoardSize = 8;
            public GameObject TilePrefab;
            public GameObject PawnPrefab;
            public Material WhiteTileMaterial;
            public Material BlackTileMaterial;
            [FormerlySerializedAs("TargetTileMaterial")] public Material TargetingTileMaterial;
            public Material WhitePawnMaterial;
            public Material BlackPawnMaterial;
        }
    }
}