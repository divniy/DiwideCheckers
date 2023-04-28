using System;
using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Diwide.Checkers
{
    public class PawnMover : MonoBehaviour
    {
        [Inject] private TilesRegistry _registry;
        [Inject] private PlayerManager _playerManager;
        [Inject] private Settings _settings;
        public bool IsPawnMoving = false;
        private IMovable _move;
        private PawnFacade _pawn;
        private PawnFacade _enemy;

        public void PerformMove(IMovable move)
        {
            Debug.LogFormat("Perform {0}", move);
            _move = move;
            _pawn = _registry[move.From].PawnFacade;
            var targetTile = _registry[move.To];
            IsPawnMoving = true;
            _pawn.AssignWithTile(targetTile, true);
            if (!IsCapturingMove())
            {
                MovePawn(targetTile.transform.position);
            }
            else
            {
                CaptureMovePawn(targetTile.transform.position);
            }
        }

        private void MovePawn(Vector3 target)
        {
            
            _pawn.transform
                .DOMove(target, _settings.HorizontalMoveDuration)
                .OnComplete(() => OnCompleteMove());
        }

        private void CaptureMovePawn(Vector3 target)
        {
            float height = _settings.CaptureMoveHeight;
            // CapturePawn();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_pawn.transform.DOMoveY(height, _settings.VerticalMoveDuration));
            sequence.Append(_pawn.transform.DOMove(target + Vector3.up * height, _settings.HorizontalMoveDuration)
                .OnComplete(CapturePawn));
            sequence.Append(_pawn.transform.DOMoveY(target.y, _settings.VerticalMoveDuration));
            sequence.OnComplete(() => OnCompleteMove());
        }
        
        private void OnCompleteMove()
        {
            // Debug.Log("OnCompleteMove start");
            // if(IsCapturingMove()) CapturePawn();
            IsPawnMoving = false;
            _playerManager.EndPlayerTurn();
        }

        private bool IsCapturingMove()
        {
            if (_move.Middle == null) return false;
            _enemy = _registry[_move.Middle].PawnFacade;
            return _enemy != null;
        }

        private void CapturePawn()
        {
            // _registry.GetPawnFacade(_move.Middle).Dispose();
            // _registry[_move.Middle].PawnFacade.Dispose();
            _enemy.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public float HorizontalMoveDuration;
            public float VerticalMoveDuration;
            public float CaptureMoveHeight;
        }
    }
}