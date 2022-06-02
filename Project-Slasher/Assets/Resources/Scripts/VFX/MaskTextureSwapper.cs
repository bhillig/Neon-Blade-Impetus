using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskTextureSwapper : MonoBehaviour
{
    public List<Texture2D> textures;
    public Renderer maskRenderer;
    public InputInfo input;
    public FMODUnity.StudioEventEmitter sfx;

    private int currentIndex = 0;

    private void Awake()
    {
        SetMaskTex(0);
        input.MaskRotateDownEvent.AddListener(Rotate);
    }

    private void OnDestroy()
    {
        input.MaskRotateDownEvent.RemoveListener(Rotate);
    }

    private void Rotate()
    {
        currentIndex = (currentIndex + 1) % textures.Count;
        SetMaskTex(currentIndex);
        sfx?.Play();
    }

    private void SetMaskTex(int index)
    {
        var propBlock = new MaterialPropertyBlock();
        maskRenderer.GetPropertyBlock(propBlock);
        propBlock.SetTexture("_EmissionMap", textures[index]);
        maskRenderer.SetPropertyBlock(propBlock);
    }

}
