using UnityEngine;

/// <summary>
/// Is used for creating patrolling behaviour. NPC with this component will move
/// around the waypoints. Each waypoint is a child <see cref="GameObject"/>, so
/// <see cref="GameObject"/> with this script should not contain other meaningful
/// children. For confuguring path in editor each waypoint will be drawn.
/// </summary>
public class PatrolPath : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField]
    private float gizmoRadius = 0.2f;

    [SerializeField]
    private Color gizmoColor = Color.gray;

    [SerializeField]
    [Tooltip("The time during which the character will stay at the point")]
    private float waitTime = 2f;

    #endregion
    
    #region Private fields

    private int targetWaypointID = 0;
    
    #endregion

    #region Unity callbacks

    private void OnDrawGizmos()
    {
        foreach (Transform waypoint in transform)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(waypoint.position, gizmoRadius);
        }

        // draw lines between waypoints
        // draw lines between 2nd and last waypoint
        for (int i = 1; i < transform.childCount; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i - 1).transform.position);
        }

        // if childCount > 2 need connect firs and last child. Otherwise their already connected
        if (transform.childCount > 2)
        {
            Gizmos.DrawLine(transform.GetChild(0).position,
                            transform.GetChild(transform.childCount - 1).position);
        }
    }

    #endregion
    
    #region Public methods
    
    public Vector3 GetTargetWaypoint()
    {
        return transform.GetChild(targetWaypointID).position;
    }

    public void OnTargetWaypointReached()
    {
        targetWaypointID++;
        if (targetWaypointID >= transform.childCount)
        {
            targetWaypointID = 0;
        }
    }
    
    #endregion
}