using System;
using UnityEngine;
using Zenject;

namespace Diwide.Checkers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [Inject] private Settings _settings;

        public override void InstallBindings()
        {
            
        }
        
        [Serializable]
        public class Settings
        {
            public GameObject BoardPrefab;
        }
    }
}