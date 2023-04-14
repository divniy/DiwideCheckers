using UnityEngine;
using Zenject;


namespace Diwide.Checkers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public override void InstallBindings()
        {
            Container.BindInstance(GameInstaller);
        }
    }
    
}
