using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// This interface has the same functions as Controller in MVC pattern.
/// Children should implement children of this class and
/// define its own interface methods.
/// <see cref="TParams"/> is required, because some <see cref="TSystem"/>
/// will contain models (M in MVC). This is done for game designers' convenience,
/// as the can tweak some model values in Inspector.
/// </summary>
/// <typeparam name="TSystem">System, which this controller will manage</typeparam>
/// <typeparam name="TParams">Additional components, which <see cref="TSystem"/> will provide during initialization</typeparam>
public interface IBaseController<TSystem, TParams> 
    where TSystem : IBaseSystem 
    where TParams : class
{
    void Init([NotNull] TSystem system, [CanBeNull] TParams parameters = null);
}