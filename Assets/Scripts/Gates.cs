using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isLowering = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLowering)
        {
            transform.Translate(0,(float)(Time.deltaTime*-0.3),0);
            if (this.transform.position.y < -10)
            {
                isLowering = false;
                gameObject.SetActive(false);
            }
            
        }
    }

    public void Lower()
    {
        this.isLowering = true;
    }
}
