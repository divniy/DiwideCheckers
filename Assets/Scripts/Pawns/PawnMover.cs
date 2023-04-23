using System.Collections;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class PawnMover : MonoBehaviour
    {
        [Inject] private TilesRegistry _registry;
        [Inject] private PlayerManager _playerManager;
        [SerializeField] private float MovementSmoothing = 12;
        public bool IsPawnMoving = false;
        private PawnFacade _pawn;
        // private TileFacade _targetTile;

        public void PerformMove(IMovable move)
        {
            Debug.LogFormat("Perform {0}", move);
            _pawn = _registry.GetPawnFacade(move.From);
            var targetTile = _registry.GetTileFacade(move.To);
            CapturePawn(move);
            MovePawn(targetTile);
        }

        private void MovePawn(TileFacade targetTile)
        {
            _pawn.AssignWithTile(targetTile, true);
            StartCoroutine(AnimatedPawnMove(targetTile.transform.position));
        }

        private IEnumerator AnimatedPawnMove(Vector3 target)
        {
            IsPawnMoving = true;
            yield return HorizontalPawnMove(target);
            IsPawnMoving = false;
            _playerManager.EndPlayerTurn();
        }

        private IEnumerator HorizontalPawnMove(Vector3 target)
        {
            var pawnTransform = _pawn.transform;
            Debug.LogFormat("Pawn pos: {0} target pos: {1}", pawnTransform.position, target);
            while (Vector3.Distance(pawnTransform.position, target) > .01f)
            {
                pawnTransform.position = Vector3.Lerp(pawnTransform.position, target, MovementSmoothing * Time.deltaTime);
                yield return null;
            }
        }

        private void CapturePawn(IMovable move)
        {
            if (move.Middle != null)
            {
                var enemy = _registry.GetPawnFacade(move.Middle);
                enemy.Dispose();
            }
        }
    }
}