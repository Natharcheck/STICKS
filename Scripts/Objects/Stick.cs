using UnityEngine;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour
{
    public enum PositionSide
    {
       Up, Down, Left, Right 
    }
    
    public byte index;
    public PositionSide positionSide;

    private Animator animator; 
    private ParticleSystem effect;
    
    [HideInInspector] public MeshRenderer mesh;
    [HideInInspector] public EventTrigger eventTrigger;
    
    private Material material;
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        effect = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
        mesh = GetComponentInChildren<MeshRenderer>();
        eventTrigger = GetComponent<EventTrigger>();
        
        material = mesh.material;
        material = new Material(material);
        
        mesh.material = material;
        mesh.enabled = false;
    }

    public void SetEnable(Color32 _color)
    {
        effect.Play();
        animator.SetTrigger("Enable");
        
        mesh.enabled = true;
        mesh.material.color = _color;

        Destroy(GetComponent<BoxCollider>());
    }
    
}