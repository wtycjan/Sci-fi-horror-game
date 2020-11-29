using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private OpenDoorButton elevatorDoors;
    public OpenDoorButton upperDoor;
    private Sounds sound;
    private void Start()
    {
        elevatorDoors = GetComponentInChildren<OpenDoorButton>();
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }
    public void ResetElevator()
    {
        StartCoroutine(ResetElevator2());
    }

    public IEnumerator ResetElevator2()
    {
        elevatorDoors.LockDoor();
        upperDoor.LockDoor();
        yield return new WaitForSeconds(1);
        sound.Sound5();
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveOverSpeed(gameObject, new Vector3(0.03f, -3.19f, 0.47f), .35f));
        yield return new WaitForSeconds(12);
        elevatorDoors.UnlockDoor();
        GameData.level1 = true;
    }

        public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.localPosition != end)
        {
            objectToMove.transform.localPosition = Vector3.MoveTowards(objectToMove.transform.localPosition, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
}
