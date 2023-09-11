using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 limitPosition; 
    [SerializeField] private int centerPosition;
    [SerializeField] private float sensitivity;

    private Vector3 cameraPosition;
    private Vector2 centerPos;
    private Vector2 beginPos;
    private Vector2 dragPos; 
    private float radius;
    
    private GridField gridField;
    
    private float hight;
    private Touch touchA;
    private Touch touchB;
    private Vector2 touchADirection;
    private Vector2 touchBDirection; 
    private float dstBtwTouchesPosition;
    private float dstBtwTouchesDirections;
    private float zoom;
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        gridField = FindObjectOfType<GridField>();

        Invoke("GetFloats",0.05f);
        Invoke("GetCentrPosition",0.05f);
    }

    private void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            touchA = Input.GetTouch(0);
            
            transform.Translate((-touchA.deltaPosition.x / 4) * Time.deltaTime,(-touchA.deltaPosition.y / 4) * Time.deltaTime,0);
            cameraPosition = transform.position;
        
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, -1, limitPosition.x);
            cameraPosition.z = Mathf.Clamp(cameraPosition.z, -1, limitPosition.z);

            transform.position = cameraPosition;
        }
        else if (Input.touchCount == 2)
        {
            touchA = Input.GetTouch(0);
            touchB = Input.GetTouch(1);
            
            touchADirection = touchA.position - touchA.deltaPosition;
            touchBDirection = touchB.position - touchB.deltaPosition;

            dstBtwTouchesPosition = Vector2.Distance(touchA.position, touchB.position);
            dstBtwTouchesDirections = Vector2.Distance(touchADirection, touchBDirection);
            
            zoom = dstBtwTouchesPosition - dstBtwTouchesDirections;
            var currentZoom = transform.position.y - zoom * sensitivity;
            
            hight = Mathf.Clamp(currentZoom, 8, limitPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, hight, transform.position.z), 0.3f);
            
            if (touchBDirection != touchB.position)  
            {  
                var angle = Vector3.SignedAngle(  
                    touchB.position - touchA.position,  
                    touchBDirection - touchADirection,  
                    -transform.forward);  
                transform.RotateAround(transform.position, -transform.forward, angle);  
            }
        }
    }
    
    private void GetFloats()
    {
        limitPosition.y = gridField.SetHighLimited((int)limitPosition.y);

        limitPosition.x = gridField.sizeGrid.x;
        limitPosition.z = gridField.sizeGrid.y;
        
        centerPosition = gridField.cells.Count / 2;
    }
    
    private void GetCentrPosition()
    {
        GetComponent<Camera>().transform.position = new Vector3(
            gridField.cells[centerPosition].position.x, 
            limitPosition.y, 
            gridField.cells[centerPosition].position.z);
    }
}
