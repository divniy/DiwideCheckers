using System;
using System.Collections.Generic;
using System.Linq;
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
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<TileSelectedSignal>();
            Container.DeclareSignal<SelectValidMoveSignal>();
            
            Container.Bind<TilesRegistry>().AsSingle().NonLazy();
            Container.Bind<MoveValidator>().AsSingle().NonLazy();
            Container.Bind<PawnMover>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            Container.BindFactory<TileIndex, TileFacade, TileFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<TileInstaller>(_settings.TilePrefab)
                .UnderTransformGroup("Board");

            Container.BindInterfacesTo<BoardGenerator>().AsSingle().NonLazy();

            Container.BindFactory<ColorType, PawnFacade, PawnFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabMethod(_settings.PawnPrefab, InstallPawn);

            Container.BindInterfacesTo<PawnsGenerator>().AsSingle().NonLazy();

            Container.BindFactory<ColorType, Player, Player.Factory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerManager>().AsSingle().NonLazy();

            Container.Bind<ValidPathHighlighter>().AsSingle().NonLazy();
            Container.BindSignal<TileSelectedSignal>()
                .ToMethod<ValidPathHighlighter>(x => x.OnTileSelectedSignal)
                .FromResolve();
            
            Container.BindExecutionOrder<BoardGenerator>(-30);
            Container.BindExecutionOrder<PawnsGenerator>(-20);
            Container.BindExecutionOrder<PlayerManager>(-10);
        }

        

        private void InstallPawn(DiContainer subcontainer, ColorType color)
        {
            // subcontainer.Bind<TileFacade>().FromInstance(tileFacade);
            subcontainer.Bind<ColorType>().FromInstance(color).AsSingle().NonLazy();
            subcontainer.BindInterfacesAndSelfTo<PawnFacade>()
                .FromComponentOnRoot().AsSingle().NonLazy();
            subcontainer.BindInterfacesAndSelfTo<PathFinder>().AsSingle().NonLazy();
        }

        [Serializable]
        public class Settings
        {
            public int BoardSize = 8;
            public GameObject TilePrefab;
            public GameObject PawnPrefab;
            public Material WhiteTileMaterial;
            public Material BlackTileMaterial;
            [FormerlySerializedAs("TargetTileMaterial")] public Material TargetingTileMaterial;
            public Material ValidMoveMaterial;
            public Material WhitePawnMaterial;
            public Material BlackPawnMaterial;
        }
    }

    public class SelectValidMoveSignal
    {
        public List<TileIndex> TileIndexes;
        public bool IsSelected = true;
    }
}