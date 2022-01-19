using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    public const float omega = 100f;
    [SerializeField] List<GameObject> stackList;
    // Start is called before the first frame update
    void Start()
    {
        InEditmode();
    }

    // Update is called once per frame
   // [System.Obsolete]
    void Update()
    {
        transform.Rotate(0,omega*Time.deltaTime,0);
    }

    //[System.Obsolete]
    void InEditmode()
    {
        
        
        foreach (var item in stackList)
        {
            item.transform.localEulerAngles = 
                new Vector3(item.transform.localEulerAngles.x,
                    item.transform.localEulerAngles.y + item.transform.position.y * omega / Ball.speed
                    ,item.transform.localEulerAngles.z);
            // item.transform.localRotation = Quaternion.Euler(item.transform.rotation.x, item.transform.position.y * omega / Ball.speed + transform.rotation.y , item.transform.rotation.z);
        }
    }

    public int GetListCount()
    {
        return stackList.Count;
    }
}
