using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public byte index;
    [HideInInspector] public Vector3 position;
    
    public byte quantitySticks;
    [Space] 
    private MeshRenderer mesh;
    [Space]
    public List<Stick> sticks;
    public List<Cell> cellsSide;
    
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        position = transform.position;
        mesh = GetComponent<MeshRenderer>();
    }

    public void FillingCellColor(Stick _stick)
    {
        mesh.material = _stick.mesh.material;
    }
    public void DeleteStick(int index)
    {
        Destroy(sticks[index].gameObject);
    }
}