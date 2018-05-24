using JetBrains.Annotations;

namespace Game.Characters.Scripts
{
    /// <summary>
    /// Should implement all behaviours, which are intended to be interacted by player
    /// </summary>
    /// <typeparam name="TParam">Parameters, should be passed for interation</typeparam>
    /// <typeparam name="TResult">Interaction result</typeparam>
    public interface IInteractable<in TParam, out TResult> : IBaseInteractable
        where TParam : class
        where TResult : class
    {
        TResult Interact([CanBeNull] TParam param = null);
    }

    public interface IBaseInteractable
    {
    }
}