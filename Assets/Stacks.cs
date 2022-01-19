using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacks : MonoBehaviour
{
    public const float omega = 150f;
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
            
            item.transform.rotation = Quaternion.Euler(item.transform.rotation.x, item.transform.position.y * omega / Ball.speed, item.transform.rotation.z);
        }
    }

    public int GetListCount()
    {
        return stackList.Count;
    }
}
