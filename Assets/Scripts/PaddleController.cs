using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float _paddleSpeed = .5f;

    [SerializeField] private bool _paddleOne;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float paddleTranslation = (Input.GetAxis("Vertical") * _paddleSpeed);
        //transform.position += new Vector3(0f, paddleTranslation * Time.deltaTime, 0f);
        if (_paddleOne)
        {
            PaddleOneMoviment();    
        }
        else
        {
            PaddleTwoMoviment();
        }
    }

    void KazuoFuctionMovimentUp(KeyCode keyCodeUp, KeyCode keyCodeDown)
    {
        bool isPressingW = Input.GetKey(keyCodeUp);
        bool isPressingS = Input.GetKey(keyCodeDown);

        if (isPressingW)
        {
            transform.Translate(Vector2.up * Time.deltaTime * _paddleSpeed);
        }
        
        if (isPressingS)
        {
            transform.Translate(Vector2.down * Time.deltaTime * _paddleSpeed);
        }
    }
    
    void PaddleOneMoviment()
    {
        KazuoFuctionMovimentUp(KeyCode.W, KeyCode.S);
    }
    
    void PaddleTwoMoviment()
    {
        KazuoFuctionMovimentUp(KeyCode.UpArrow, KeyCode.DownArrow);
    }
}
