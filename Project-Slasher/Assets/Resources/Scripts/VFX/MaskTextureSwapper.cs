using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskTextureSwapper : MonoBehaviour
{
    public List<Texture2D> textures;
    public Renderer maskRenderer;
    public InputInfo input;

    private int currentIndex = -1;

    private void Awake()
    {
        Rotate();
        input.MaskRotateDownEvent.AddListener(Rotate);
    }

    private void OnDestroy()
    {
        input.MaskRotateDownEvent.RemoveListener(Rotate);
    }

    private void Rotate()
    {
        currentIndex = (currentIndex + 1) % textures.Count;
        var propBlock = new MaterialPropertyBlock();
        maskRenderer.GetPropertyBlock(propBlock);
        propBlock.SetTexture("_EmissionMap", textures[currentIndex]);
        maskRenderer.SetPropertyBlock(propBlock);
    }

}
