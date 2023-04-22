using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PathFinder
    {
        public PawnFacade Pawn { get; }
        public List<IMovable> ValidMoves = new();
        
        private TileIndex FromIndex => Pawn.Index;
        [Inject] private TilesRegistry _registry;
        [Inject] private MoveValidator _moveValidator;
        [Inject] private PlayerManager _playerManager;

        [Inject]
        public PathFinder(PawnFacade pawn)
        {
            Pawn = pawn;
        }

        public void GenerateValidMoves()
        {
            ValidMoves = new();
            var moves = new List<IMovable>();
            if (_playerManager.CurrentPlayer.PawnsColor == ColorType.Black)
            {
                moves.Add(new PawnMove(Pawn, new TileIndex(1, -1)));
                moves.Add(new PawnMove(Pawn, new TileIndex(1, 1)));
                // AddMoveIfValid<PawnMove>(new PawnMove(Pawn, new TileIndex(1, -1)));
                // AddMoveIfValid(1, -1, (from, to) => new PawnMove(from, to));
                // AddMoveIfValid(1, 1, (from, to) => new PawnMove(from, to));
            }
            else
            {
                moves.Add(new PawnMove(Pawn, new TileIndex(-1, -1)));
                moves.Add(new PawnMove(Pawn, new TileIndex(-1, 1)));
                // AddMoveIfValid(-1, -1, (from, to) => new PawnMove(from, to));
                // AddMoveIfValid(-1, 1, (from, to) => new PawnMove(from, to));
            }
            moves.Add(new PawnAttack(Pawn, new TileIndex(2, -2)));
            moves.Add(new PawnAttack(Pawn, new TileIndex(2, 2)));
            moves.Add(new PawnAttack(Pawn, new TileIndex(-2, -2)));
            moves.Add(new PawnAttack(Pawn, new TileIndex(-2, 2)));
            ValidMoves = moves.FindAll(_ => _moveValidator.IsValid(_));
            ValidMoves.ForEach(m => Debug.LogFormat("Valid {0}", m));
        }
    }
}