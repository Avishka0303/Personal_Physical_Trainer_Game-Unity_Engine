using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float scroll_speed = 0.1f;
    private MeshRenderer mesh_renderer;
    private float x_scroll;

    private void Awake()
    {
        mesh_renderer = GetComponent<MeshRenderer>();
    }
    

    // Update is called once per frame
    void Update() {
        ScrollBackground();
    }

    void ScrollBackground() {
        x_scroll = Time.time * scroll_speed;
        Vector2 offset_vector=new Vector2(x_scroll,0f);
        mesh_renderer.sharedMaterial.SetTextureOffset("_MainTex",offset_vector);
    }
}
