using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerDropper : MonoBehaviour
{
    const string BOAT_TAG = "Boat";
    public float coolDown = 1.0f;
    public MountableGroup mountableGroup;
    public Transform dropPositionsParent;
    public ForiStop[] foriStops;
    float collisionTimeStamp = 0.0f;

    List<Mounter> droppedPassangers = new List<Mounter>();

    private void Start()
    {
        if (dropPositionsParent == null)
        {
            Debug.LogError("Missing reference to dropPositionsParent", this);
        }
        if (mountableGroup == null)
        {
            Debug.LogError("Missing reference to mountableGroup", this);
        }

        foriStops = FindObjectsOfType<ForiStop>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - collisionTimeStamp > coolDown)
        {
            Debug.Log("Fori collided with: " + other.gameObject.name, other.gameObject);
            Rigidbody colliderRB = other.collider.attachedRigidbody;
            if (colliderRB != null && colliderRB.CompareTag(BOAT_TAG))
            {
                Debug.Log("Fori hit a boat!", colliderRB.gameObject);
                collisionTimeStamp = Time.time;
                DropRandomPassanger();

            }
            else
            {
                Debug.Log("Collider has no rigidbody!", other.gameObject);
            }
        }
    }

    private void DropRandomPassanger()
    {
        Mounter mounter = mountableGroup.GetFirstMounter();
        if (mounter != null)
        {
            Vector3 dropPosition = GetRandomDropPosition();
            DropMounterToPosition(mounter, dropPosition);
            ClearObjectivesFromMounter(mounter);
            if(droppedPassangers.Count > 30){
                DespawnDroppedPassanger();
            }

            for (int i = 0; i < foriStops.Length; i++)
            {
                foriStops[i].RemoveFromQueues(mounter);
            }

            ActorModelChanger actorModelChanger = mounter.GetComponent<ActorModelChanger>();
            if (actorModelChanger != null)
            {
                actorModelChanger.ChangeModel(1);
            }
        }
    }

    private void DropMounterToPosition(Mounter mounter, Vector3 dropPosition)
    {
        GameObject go = new GameObject("DropPoint");
        go.transform.position = dropPosition;
        Mountable dropMountable = go.AddComponent<Mountable>();
        mounter.SwapMountable(dropMountable);
        droppedPassangers.Add(mounter);
    }

    private void ClearObjectivesFromMounter(Mounter mounter)
    {
        AIController controller = mounter.GetComponent<AIController>();
        if (controller != null)
        {
            controller.AssignObjectives(new BaseObjective[0]);
        }
    }

    private void DespawnDroppedPassanger(){

        if(droppedPassangers.Count > 0){

            Mounter mounter = droppedPassangers[0];
            droppedPassangers.RemoveAt(0);
            if(mounter != null){
                Destroy(mounter);
            }
        }
    }

    private Vector3 GetRandomDropPosition()
    {

        if (dropPositionsParent != null)
        {
            return dropPositionsParent.GetChild(UnityEngine.Random.Range(0, dropPositionsParent.childCount)).position;
        }
        else
        {
            return transform.position += new Vector3
            (
                    UnityEngine.Random.Range(-5f, 5f),
                    0,
                    UnityEngine.Random.Range(-5, 5)
            );
        }

    }
}
