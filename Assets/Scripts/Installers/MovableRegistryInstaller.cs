using Diwide.Checkers;
using Diwide.Checkers.Utils;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MovableRegistryInstaller", menuName = "Installers/MovableRegistryInstaller")]
public class MovableRegistryInstaller : ScriptableObjectInstaller<MovableRegistryInstaller>
{
    public MovableRegistry MovableRegistry;
    // public TileIndex TestTileIndex;
    // public PawnMove TestPawnMove;
    public override void InstallBindings()
    {
        Container.BindInstance(MovableRegistry);
        // Container.BindInstance(TestTileIndex);
        // Container.BindInstance(TestPawnMove);
    }
}