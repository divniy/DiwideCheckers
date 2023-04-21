using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PathFinder : IInitializable
    {
        public PawnFacade Pawn { get; }
        public List<PawnMove> ValidMoves = new();
        
        private TileIndex FromIndex => Pawn.Index;
        [Inject] private TilesRegistry _registry;
        [Inject] private MoveValidator _moveValidator;

        [Inject]
        public PathFinder(PawnFacade pawn)
        {
            Pawn = pawn;
        }

        public void Initialize()
        {
            Debug.Log(GetRelativeTile(1, -1));
            AddMoveIfValid(1, 1);
        }

        public void AddMoveIfValid(int deltaRow, int deltaCol)
        {
            var ToIndex = FromIndex + new TileIndex(deltaRow, deltaCol);
            var move = new PawnMove(FromIndex, ToIndex);
            if (_moveValidator.IsValid(move))
            {
                ValidMoves.Add(move);
                Debug.LogFormat("New valid {0}", move);
            }
        }

        public TileFacade GetRelativeTile(int row, int col)
        {
            return GetRelativeTile(new TileIndex(row, col));
        }

        public TileFacade GetRelativeTile(TileIndex deltaIndex)
        {
            var destIndex = new TileIndex(FromIndex.Row + deltaIndex.Row, FromIndex.Col + deltaIndex.Col);
            return _registry.GetTileFacade(destIndex);
        }
        
        
    }
}