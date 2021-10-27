using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500f;
    [SerializeField] float _maxDragDistance = 5f;

    Vector2 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = GetComponent<Rigidbody2D>().position;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    void OnMouseUp()
    {
        var currentPosition = GetComponent<Rigidbody2D>().position;
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(direction * _launchForce);

    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance < _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }
        GetComponent<Rigidbody2D>().position = desiredPosition;




        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());

    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        GetComponent<Rigidbody2D>().position = _startPosition;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
