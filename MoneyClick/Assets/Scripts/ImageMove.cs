using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMove : MonoBehaviour
{
    public float speed = 200f; 
    public float maxRotationSpeed = 90f; 
    public RectTransform topBoundary; 
    public Canvas canvas; 

    private RectTransform rectTransform;
    private Vector2 moveDirection; 
    private float rotationSpeed;

    private float bottomEdge;
    private float leftEdge;
    private float rightEdge;
    private float halfWidth;
    private ObjectPool objectPool;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        objectPool = FindObjectOfType<ObjectPool>();
    }

    public void Initialize()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        bottomEdge = -canvasRect.rect.height / 2;
        leftEdge = -canvasRect.rect.width / 2;
        rightEdge = canvasRect.rect.width / 2;

 
        halfWidth = rectTransform.rect.width * rectTransform.localScale.x / 2f;


        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);


        float randomAngle = Random.Range(-45f, 45f);
        float angleRad = randomAngle * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Sin(angleRad), Mathf.Cos(angleRad)).normalized;
    }

    void Update()
    {
        float delta = speed * Time.deltaTime;

        rectTransform.localPosition += (Vector3)(moveDirection * delta);

        BounceOffSides();

        if (moveDirection.y > 0 && topBoundary != null && IsOverlapping(rectTransform, topBoundary))
        {
            moveDirection.y = -moveDirection.y;
        }

       
        if (moveDirection.y < 0 && rectTransform.localPosition.y <= bottomEdge - rectTransform.rect.height)
        {
            objectPool.ReturnObject(gameObject);
        }

       
        float rotationDelta = rotationSpeed * Time.deltaTime;
        rectTransform.Rotate(0f, 0f, rotationDelta);
    }

    private void BounceOffSides()
    {
        float posX = rectTransform.localPosition.x;

        if (posX - halfWidth <= leftEdge && moveDirection.x < 0)
        {
           
            moveDirection.x = -moveDirection.x;
        }
        else if (posX + halfWidth >= rightEdge && moveDirection.x > 0)
        {
           
            moveDirection.x = -moveDirection.x;
        }
    }

    private bool IsOverlapping(RectTransform movingRect, RectTransform boundaryRect)
    {
       

        Vector3 movingPos = movingRect.localPosition;
        Vector3 boundaryPos = boundaryRect.localPosition;

        
        Vector2 movingSize = movingRect.rect.size * movingRect.localScale;
        Vector2 boundarySize = boundaryRect.rect.size * boundaryRect.localScale;

        
        Rect movingBounds = new Rect(movingPos.x - movingSize.x / 2, movingPos.y - movingSize.y / 2, movingSize.x, movingSize.y);
        Rect boundaryBounds = new Rect(boundaryPos.x - boundarySize.x / 2, boundaryPos.y - boundarySize.y / 2, boundarySize.x, boundarySize.y);

        
        return movingBounds.Overlaps(boundaryBounds);
    }
}
