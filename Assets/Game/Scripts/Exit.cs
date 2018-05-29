using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class Exit : MonoBehaviour
{
    #region Editor tweakable fields

    [NotNull]
    [SerializeField]
    [Tooltip("Level name to go to")]
    private string levelName;

    #endregion

    #region Unity callbacks

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActor>() && levelName.Length != 0)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    #endregion
}