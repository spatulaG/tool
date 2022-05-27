using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
    public BarrelType type;
    MaterialPropertyBlock _mpb;
    static readonly int shPropColor = Shader.PropertyToID("_Color");
    public MaterialPropertyBlock Mpb
    {
        get {
            if (_mpb == null) 
                _mpb = new MaterialPropertyBlock();
            return _mpb;
        }
    }

    public void tryApplyColor()
    {
        if (type == null)
            return;
        
        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mpb.SetColor(shPropColor, type.color);
        rnd.SetPropertyBlock(Mpb);
    }

    private void OnValidate()=>tryApplyColor();// called on property change

    private void OnEnable()
    {
        tryApplyColor();
        ExplosiveBarrelManager.allTheBarrels.Add(this);
        // use this to automate, very useful!
    }

    public void OnDisable() => ExplosiveBarrelManager.allTheBarrels.Remove(this);// expression body members

    private void OnDrawGizmos()
    {
        Handles.color = type.color;
        Handles.DrawWireDisc(transform.position,transform.up, type.radius);
        Handles.color = Color.blue;
    }
}
