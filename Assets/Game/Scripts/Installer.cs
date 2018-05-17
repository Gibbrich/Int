using UnityEngine;
using Zenject;

public class Installer : MonoInstaller<Installer>
{
    public override void InstallBindings()
    {
        Container.Bind<GameController>().AsSingle();
        Container.Bind<UIController>().AsSingle();
    }
}