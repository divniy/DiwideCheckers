using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class ValidPathHighlighter
    {
        [Inject] private PlayerManager _playerManager;
        [Inject] private SignalBus _signalBus;
        [Inject] private PawnMover _pawnMover;
        private PawnFacade _selectedPawn;
        private List<TileIndex> _validMovesIndexes;
        public void OnTileSelectedSignal(TileSelectedSignal signal)
        {
            if (signal.Tile.PawnFacade != null)
            {
                SelectPawn(signal.Tile.PawnFacade);
            } else if (signal.Tile.IsValidMove && _selectedPawn != null)
            {
                MoveSelectedPawnTo(signal.Tile);
            }
            else if (_selectedPawn != null)
            {
                UnselectPawn();
            }
        }

        private void MoveSelectedPawnTo(TileFacade destinationTile)
        {
            IMovable move = _selectedPawn.ValidMoves.Find(_ => _.To == destinationTile.TileIndex);
            UnselectPawn();
            _pawnMover.PerformMove(move);
        }

        public void SelectPawn([NotNull] PawnFacade pawn)
        {
            if (
                pawn.Color == _playerManager.CurrentPlayer.PawnsColor &&
                pawn.ValidMoves.Any()
            )
            {
                Debug.LogFormat("Clicked pawn on {0}", pawn.TileIndex);
                if(_selectedPawn != null) UnselectPawn();
                HighlightPawnMoves(pawn);
            }
        }

        private void HighlightPawnMoves(PawnFacade pawn)
        {
            _selectedPawn = pawn;
            _validMovesIndexes = pawn.ValidMoves.Select(_ => _.To).ToList();
            _signalBus.Fire(new SelectValidMoveSignal() { TileIndexes = _validMovesIndexes });
        }

        private void UnselectPawn()
        {
            _signalBus.Fire(new SelectValidMoveSignal() { TileIndexes = _validMovesIndexes, IsSelected = false});
            _validMovesIndexes.Clear();
            _selectedPawn = null;
        }
    }
}