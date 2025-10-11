using UnityEngine;
using Sirenix.OdinInspector;

public class FaceGenerator : MonoBehaviour
{
    [SerializeField] private FaceGenConfig config;
    [SerializeField] private FaceAnimation faceAnimation;

    [SerializeField] private SpriteRenderer baseRenderer;
    [SerializeField] private SpriteRenderer eyesRenderer;
    [SerializeField] private SpriteRenderer mouthRenderer;
    [SerializeField] private SpriteRenderer overlayRenderer;

    private FaceSet FaceSet;

    private void Start()
    {
        GenerateNewFace();
    }

    [Button("GenerateNewFace")]
    public void GenerateNewFace()
    {
        FaceSet = config.GenerateFace();
        ApplyFace();
    }

    public void DisableFace()
    {
        baseRenderer.enabled = false;
        mouthRenderer.enabled = false;
        eyesRenderer.enabled = false;
    }

    public void EnableFace()
    {
        baseRenderer.enabled = true;
        mouthRenderer.enabled = true;
        eyesRenderer.enabled = true;
    }

    public void ApplyFace()
    {
        baseRenderer.sprite = FaceSet.baseColor;
        eyesRenderer.sprite = FaceSet.eye.open;
        mouthRenderer.sprite = FaceSet.mouth.normal;
    }
}
