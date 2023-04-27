using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers.Utils
{
    public class MovableObserver : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private MovableRegistry _movableRegistry;

        public MovableObserver(SignalBus signalBus, MovableRegistry movableRegistry)
        {
            _signalBus = signalBus;
            _movableRegistry = movableRegistry;
        }

        public void Initialize()
        {
            _movableRegistry.Clear();
            _signalBus.Subscribe<MovePawnSignal>(OnMovePawn);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MovePawnSignal>(OnMovePawn);
        }

        private void OnMovePawn(MovePawnSignal signal)
        {
            Debug.LogFormat("Handle move {0}", signal.Movable);
            _movableRegistry.Add(signal.Movable);
        }
    }
}