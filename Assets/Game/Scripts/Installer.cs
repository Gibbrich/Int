using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using UnityEngine;
using Zenject;

/// <summary>
/// Defines game rules for current scene. Provides required dependencies for other classes.
/// </summary>
public class Installer : MonoInstaller<Installer>
{
    #region Editor tweakable fields

    [SerializeField] private QuestTitleController questTitleControllerPrefab;
    
    #endregion
    
    public override void InstallBindings()
    {
        Container.Bind<GameController>().AsSingle();
        Container.Bind<UIController>().AsSingle();
        Container.Bind<QuestSystem>().AsSingle();
        
        Container.Bind<IWeaponSystemController>().To<WeaponSystemController>().AsTransient();
        Container.Bind<IHealthSystemController>().To<HealthSystemController>().AsTransient();

        Container
            .BindFactory<QuestTitleController, QuestTitleController.Factory>()
            .FromComponentInNewPrefab(questTitleControllerPrefab);
    }
}