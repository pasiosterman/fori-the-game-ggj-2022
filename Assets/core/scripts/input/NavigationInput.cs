using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationInput : MonoBehaviour
{
    public NodeGroupBehavior nodeGroupBehavior;
    public Vector3 targetPosition = Vector3.zero;
    public Mover mover;


    public Transform overrideTargetPosition;

    public float arriveRadius = 0.1f;
    public bool arrived = false;
    public Vector3 randomPositionOffset = Vector3.zero;

    private Vector3 currentTarget = Vector3.zero;
    private Node[] currentPath;
    private Vector3 currentOffset = Vector3.zero;

    private int currentPathIndex;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if(overrideTargetPosition != null){
            targetPosition = overrideTargetPosition.transform.position;
        }

        if (Vector3.Distance(targetPosition, currentTarget) > 0.1f)
        {
            ChangeTarget(targetPosition);
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
            return targetPosition + randomPositionOffset;
        }
        else
        {
            return currentPath[currentPathIndex].position + randomPositionOffset;
        }
    }

    private void ChangeTarget(Vector3 newTarget)
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
        float offsetY = Random.Range(-maxbounds.y, maxbounds.y);
        return new Vector3(offsetX, 0.0f, offsetY);
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
