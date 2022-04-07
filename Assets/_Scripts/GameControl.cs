using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    //[SerializeField] GameObject guidingLight;
    //private float counter = 0;
    //private float _currentScale;
    //private bool expansion = true;

    //void Start()
    //{
    //    counter = 0;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    counter += Time.deltaTime;
    //    if (counter > 4)
    //    {
    //        expansion = !expansion;
    //        counter = 0;
            
    //    }

    //    Debug.Log(expansion);
    //    Debug.Log(counter);

    //    if (expansion)
    //    {
    //        _currentScale += 0.01f;
    //    }
    //    else if (!expansion)
    //    {
    //        _currentScale -= 0.01f;
    //    }

    //    transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
    //}

    public float speed = .1f;

    private Vector3 start;
    private Vector3 des;
    private float fraction = 0;


    void Start()
    {
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        des = new Vector3(transform.position.x, 10f, transform.position.z);

    }

    void Update()
    {

        if (fraction < 1)
        {
            fraction += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, des, fraction);
        }
    }
}
