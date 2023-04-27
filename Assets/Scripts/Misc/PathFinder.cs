using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PathFinder
    {
        public List<IMovable> ValidMoves = new();

        [Inject] private PawnFacade Pawn;
        [Inject] private MoveValidator _moveValidator;
        [Inject] private PlayerManager _playerManager;
        [Inject] private IMovable.RelativeFactory _moveFactory;

        public void GenerateValidMoves()
        {
            ValidMoves = new();
            var moves = new List<IMovable>();
            int moveRow = _playerManager.CurrentPlayer.PawnsColor == ColorType.Black ? 1 : -1;

            moves.Add(_moveFactory.Create(Pawn, new TileIndex(moveRow, -1)));
            moves.Add(_moveFactory.Create(Pawn, new TileIndex(moveRow, 1)));
            moves.Add(_moveFactory.Create(Pawn, new TileIndex(2, -2)));
            moves.Add(_moveFactory.Create(Pawn, new TileIndex(2, 2)));
            moves.Add(_moveFactory.Create(Pawn, new TileIndex(-2, -2)));
            moves.Add(_moveFactory.Create(Pawn, new TileIndex(-2, 2)));
            
            ValidMoves = moves.FindAll(_ => _moveValidator.IsValid(_));
            
            ValidMoves.ForEach(m => Debug.LogFormat("Valid move: {0}", m));
        }
    }
}