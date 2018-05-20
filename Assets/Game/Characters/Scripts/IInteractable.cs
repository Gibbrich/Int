/// <summary>
/// Should implement all behaviours, which are intended to be interacted by player
/// </summary>
/// <typeparam name="TParam">Parameters, should be passed for interation</typeparam>
/// <typeparam name="TResult">Interaction result</typeparam>
public interface IInteractable<in TParam, out TResult>
{
    TResult Interact(TParam param);
}