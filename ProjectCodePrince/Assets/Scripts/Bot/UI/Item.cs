using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {

    public string ItemName;
    public Sprite Icon;
    public GameObject mesh;
    public Vector3 handRotation;
    public Vector3 handPosition;

}
