using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts
{
    public static class ListUtils
    {
        [NotNull]
        public static T GetRandomItem<T>(this List<T> list)
        {
            int id = Random.Range(0, list.Count);
            return list[id];
        }
    }
}