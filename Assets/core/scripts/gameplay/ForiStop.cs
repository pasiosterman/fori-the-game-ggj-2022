using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForiStop : MonoBehaviour
{
    public ForiLoadTrigger foriLoadTrigger;
    public Fori fori;
    public ForiStop oppositeStop;
    public float timeBetweenBoardings = 0.5f;
    public Vector3 randomPositionOffset = Vector3.zero;

    private List<Mounter> entryQueue = new List<Mounter>();
    private List<Mounter> exitQueue = new List<Mounter>();

    float entryTimer = 0.0f;
    float exitTimer = 0.0f;

    private void Start()
    {

        if (foriLoadTrigger == null)
            Debug.LogError("Missing foriLoadTrigger", foriLoadTrigger);

        if (fori == null)
            Debug.LogError("Missing fori", fori);
    }

    private void Update()
    {
        HandleEntryQueue();
        HandleExitQueue();
    }

    private void HandleEntryQueue()
    {
        if (entryQueue.Count > 0 && PassangersCanBoard)
        {
            entryTimer += Time.deltaTime;
            if (entryTimer > timeBetweenBoardings)
            {
                entryTimer = 0.0f;
                Mountable mountable = fori.moutanbleGroup.GetFreeMountable();
                if (mountable != null)
                {
                    oppositeStop.QueueToExit(entryQueue[0]);
                    mountable.AssignMounter(entryQueue[0]);
                    entryQueue.RemoveAt(0);
                }
            }
        }
    }

    private void HandleExitQueue()
    {
        if (exitQueue.Count > 0 && PassangersCanBoard)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > timeBetweenBoardings)
            {
                exitTimer = 0.0f;
                Vector3 unboardPosition = transform.position + CreateRandomOffset(randomPositionOffset);
                exitQueue[0].Unmount(unboardPosition);
                exitQueue.RemoveAt(0);
            }
        }
    }

    internal void RemoveFromQueues(Mounter mounter)
    {
        if(entryQueue.Contains(mounter))
            entryQueue.Remove(mounter);

        if(exitQueue.Contains(mounter))
            exitQueue.Remove(mounter);
    }

    public void QueueToFori(Mounter passanger)
    {
        if (!entryQueue.Contains(passanger))
        {
            entryQueue.Add(passanger);
        }
    }

    public void QueueToExit(Mounter passanger)
    {
        if (!exitQueue.Contains(passanger))
        {
            exitQueue.Add(passanger);
        }
    }

     private Vector3 CreateRandomOffset(Vector3 maxbounds){

        if(randomPositionOffset.magnitude == 0) return Vector3.zero;

        float offsetX = Random.Range(-maxbounds.x, maxbounds.x);
        float offsetZ = Random.Range(-maxbounds.z, maxbounds.z);
        return new Vector3(offsetX, 0.0f, offsetZ);
    }

    public bool PassangersCanBoard
    {
        get
        {
            if (foriLoadTrigger == null || fori == null)
                return false;

            return foriLoadTrigger.IsForiDocked;
        }
    }
}
