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

        public void PerformMove(IMovable move)
        {
            Debug.LogFormat("Perform {0}", move);
            _move = move;
            _pawn = _registry.GetPawnFacade(move.From);
            var targetTile = _registry.GetTileFacade(move.To);
            IsPawnMoving = true;
            _pawn.AssignWithTile(targetTile, true);
            if (!IsCapturingMove())
            {
                MovePawn(targetTile.transform.position, OnCompleteMove);
            }
            else
            {
                CaptureMovePawn(targetTile.transform.position, OnCompleteMove);
            }
        }

        private void MovePawn(Vector3 target, TweenCallback callback)
        {
            
            _pawn.transform
                .DOMove(target, _settings.HorizontalMoveDuration)
                .OnComplete(callback);
        }

        private void CaptureMovePawn(Vector3 target, TweenCallback callback)
        {
            float height = _settings.CaptureMoveHeight;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_pawn.transform.DOMoveY(height, _settings.VerticalMoveDuration));
            sequence.Append(_pawn.transform.DOMove(target + Vector3.up * height, _settings.HorizontalMoveDuration));
            sequence.Append(_pawn.transform.DOMoveY(target.y, _settings.VerticalMoveDuration));
            sequence.OnComplete(callback);
        }
        
        private void OnCompleteMove()
        {
            if(IsCapturingMove()) CapturePawn();
            IsPawnMoving = false;
            _playerManager.EndPlayerTurn();
        }

        private bool IsCapturingMove()
        {
            return _move.Middle != null;
        }

        private void CapturePawn()
        {
            _registry.GetPawnFacade(_move.Middle).Dispose();
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