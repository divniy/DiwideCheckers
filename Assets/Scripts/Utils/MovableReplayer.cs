using System;
using System.Collections.Generic;
using UnityEditor;
using Zenject;

namespace Diwide.Checkers.Utils
{
    public class MovableReplayer : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private MovableRegistry _movableRegistry;
        private Queue<IMovable> _pawnMoves;

        public MovableReplayer(SignalBus signalBus, MovableRegistry movableRegistry)
        {
            _signalBus = signalBus;
            _movableRegistry = movableRegistry;
        }

        public void Initialize()
        {
            _pawnMoves = _movableRegistry.GetQueue();
            _signalBus.Subscribe<WaitingForMovementSignal>(OnWaitingForMovement);
            // throw new NotImplementedException();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<WaitingForMovementSignal>(OnWaitingForMovement);
            // throw new NotImplementedException();
        }

        private void OnWaitingForMovement()
        {
            if (_pawnMoves.Count > 0)
            {
                var movable = _pawnMoves.Dequeue();
                _signalBus.Fire(new MovePawnSignal(){ Movable = movable });
            }
            else
            {
                EditorApplication.isPaused = true;
            }
        }
    }
}