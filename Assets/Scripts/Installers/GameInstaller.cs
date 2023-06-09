using System;
using System.Collections.Generic;
using System.Linq;
using Diwide.Checkers.Utils;
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
            Container.DeclareSignal<MovePawnSignal>();
            Container.DeclareSignal<WaitingForMovementSignal>().OptionalSubscriber();

            // Container.BindFactory<int, int, TileIndex, TileIndex.Factory>().AsSingle().NonLazy();
            // Container.BindFactory<PawnFacade, TileIndex, PawnMove, PawnMove.Factory>().AsSingle().NonLazy();
            Container.BindFactory<PawnFacade, TileIndex, IMovable, IMovable.RelativeFactory>()
                .FromFactory<RelativePawnMoveFactory>();
            Container.BindInterfacesAndSelfTo<TilesRegistry>().AsSingle().NonLazy();
            Container.Bind<MoveValidator>().AsSingle().NonLazy();
            Container.Bind<PawnMover>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindSignal<MovePawnSignal>()
                .ToMethod<PawnMover>((_, s) => _.PerformMove(s.Movable)).FromResolve();
            
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

            if (_settings.MovableMode != MovableMode.Playing)
            {
                Container.Bind<ValidPathHighlighter>().AsSingle().NonLazy();
                Container.BindSignal<TileSelectedSignal>()
                    .ToMethod<ValidPathHighlighter>(x => x.OnTileSelectedSignal)
                    .FromResolve();
            }
            
            if (_settings.MovableMode == MovableMode.Recording)
            {
                Container.BindInterfacesTo<MovableObserver>().AsSingle();
            }
            else
            {
                Container.BindInterfacesTo<MovableReplayer>().AsSingle();
            }

            Container.BindInterfacesTo<GameInitializer>().AsSingle();

            Container.BindExecutionOrder<TilesRegistry>(-40);
            Container.BindExecutionOrder<BoardGenerator>(-30);
            Container.BindExecutionOrder<PlayerManager>(-20);
            Container.BindExecutionOrder<PawnsGenerator>(-10);
        }

        

        private void InstallPawn(DiContainer subcontainer, ColorType color)
        {
            // subcontainer.Bind<TileFacade>().FromInstance(tileFacade);
            subcontainer.Bind<ColorType>().FromInstance(color).AsSingle().NonLazy();
            subcontainer.BindInterfacesAndSelfTo<PawnFacade>()
                .FromComponentOnRoot().AsSingle().NonLazy();
            subcontainer.BindInterfacesAndSelfTo<PathFinder>().AsSingle().NonLazy();
        }

        public class GameInitializer : IInitializable
        {
            [Inject] private PlayerManager _playerManager;
            public void Initialize()
            {
                _playerManager.StartPlayerTurn();
            }
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
            public MovableMode MovableMode = MovableMode.Recording;
        }
    }

    public class SelectValidMoveSignal
    {
        public List<TileIndex> TileIndexes;
        public bool IsSelected = true;
    }

    public class MovePawnSignal
    {
        public IMovable Movable;
    }
    
    public class WaitingForMovementSignal{}

    public enum MovableMode
    {
        Recording,
        Playing
    }
}