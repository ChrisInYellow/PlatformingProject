using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkTest : MonoBehaviour
{
    public GameObject enemy;
    private float _currentScale = InitScale;
    private const float TargetScale = 15.1f;
    private const float InitScale = 0.5f;
    private const int FramesCount = 100;
    private const float AnimationTimeSeconds = 0.1f;
    private float _deltaTime = AnimationTimeSeconds / FramesCount;
    private float _dx = (TargetScale - InitScale) / FramesCount;
    public bool _upScale = false;
    public GameObject enemySprite;
    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject") && Input.GetMouseButton(1))
        {
            _upScale = true;
            enemy = coll.gameObject;
            
        }
    }

    IEnumerator Large()
    {


        while (true)
        {


            while (_upScale)
            {
                _currentScale += _dx;
                if (_currentScale > TargetScale)
                {
                    _upScale = false;
                    _currentScale = TargetScale;
                }
                enemySprite.transform.localScale = Vector3.one * _currentScale;
                yield return new WaitForSeconds(_deltaTime);
            }
            
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        

        if (Input.GetMouseButtonUp(0))
        {
            
            enemySprite.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Instantiate(enemySprite, transform.position, transform.rotation);
            StartCoroutine(Large());
            
            
            //if (_upScale)
            //{
            //    _currentScale += _dx;
            //    if (_currentScale > TargetScale)
            //    {
            //        Debug.Log("Yo");
            //        _upScale = false;
            //        //_currentScale = TargetScale;
            //    }
            //    Debug.Log("Hey");
            //    enemySprite.transform.localScale = Vector3.one * _currentScale;
            //}

        }
    }


}
