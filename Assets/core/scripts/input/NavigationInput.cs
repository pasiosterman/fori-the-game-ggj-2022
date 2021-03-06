using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationInput : MonoBehaviour
{
    public NodeGroupBehavior nodeGroupBehavior;
    public Mover mover;

    public Transform overrideTargetPosition;

    public float arriveRadius = 0.1f;
    public bool arrived = false;
    public Vector3 randomPositionOffset = Vector3.zero;
    Vector3 targetPosition;
    Vector3 currentTarget = Vector3.zero;
    Node[] currentPath;
    Vector3 currentOffset = Vector3.zero;
    int currentPathIndex;

    void Start()
    {
        if(targetPosition == Vector3.zero){
            targetPosition = transform.position;
        }
    }

    void Update()
    {
        if(nodeGroupBehavior == null) return;

        if(overrideTargetPosition != null){
            targetPosition = overrideTargetPosition.transform.position;
        }

        if (Vector3.Distance(targetPosition, currentTarget) > 0.1f)
        {
            ChangeNavigationTarget(targetPosition);
        }

        Vector3 positionDelta = (GetCurrentMoveTowardsPosition() - transform.position);
        positionDelta.y = 0.0f;
        float distance = positionDelta.magnitude;

        if (distance < arriveRadius)
        {
            StartMovingTowardsNextPointInPath();
        }

        if (!arrived)
        {
            mover.Move(positionDelta.normalized);
        }
        else
        {
            mover.Move(Vector3.zero);
        }
    }

    public void NavigateTowards(Vector3 position){
        targetPosition = position;
        arrived = false;
    }

    private void StartMovingTowardsNextPointInPath()
    {
        if (currentPathIndex < currentPath.Length - 1)
        {
            currentPathIndex++;
            currentOffset = CreateRandomOffset(randomPositionOffset);
        }
        else
        {
            arrived = true;
        }
    }

    private Vector3 GetCurrentMoveTowardsPosition()
    {

        if (currentPathIndex == currentPath.Length - 1)
        {
            return currentTarget + currentOffset;
        }
        else
        {
            return currentPath[currentPathIndex].position + currentOffset;
        }
    }

    private void ChangeNavigationTarget(Vector3 newTarget)
    {
        currentTarget = newTarget;
        currentPath = nodeGroupBehavior.FindPath(transform.position, currentTarget);
        currentPathIndex = 0;
        currentOffset = CreateRandomOffset(randomPositionOffset);
        arrived = false;
    }

    private Vector3 CreateRandomOffset(Vector3 maxbounds){

        if(randomPositionOffset.magnitude == 0) return Vector3.zero;

        float offsetX = Random.Range(-maxbounds.x, maxbounds.x);
        float offsetZ = Random.Range(-maxbounds.z, maxbounds.z);
        return new Vector3(offsetX, 0.0f, offsetZ);
    }

    void OnDrawGizmosSelected()
    {
        if(currentPath == null)
            return;

        Gizmos.color = Color.green;
        if(!arrived){
            Gizmos.DrawLine(transform.position, GetCurrentMoveTowardsPosition());
        }
        else{
            Gizmos.DrawCube(transform.position + (Vector3.up * 0.5f), Vector3.one * 0.3f);
        }
    }
}
