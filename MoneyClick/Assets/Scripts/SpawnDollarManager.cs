using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDollarManager : MonoBehaviour
{
    public ObjectPool objectPool; 
    public Canvas canvas; 
    public RectTransform topBoundary;

    public void OnButtonClick()
    {
       
        Vector2 mousePosition = Input.mousePosition;

       
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePosition,
            canvas.worldCamera,
            out localPoint);

        
        GameObject newImage = objectPool.GetObject();
        newImage.transform.SetParent(canvas.transform, false);

        
        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.localPosition = localPoint;

       
        ImageMove imageMover = newImage.GetComponent<ImageMove>();
        if (imageMover != null)
        {
            imageMover.topBoundary = topBoundary;
            imageMover.canvas = canvas;
            imageMover.Initialize(); 
        }
        else
        {
            Debug.LogError("Bag");
        }
    }
}
