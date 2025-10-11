using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FaceElementSets
{
    public List<Sprite> baseColors;
    public List<EyeSet> eyes;
    public List<MouthSet> mouths;

}

[System.Serializable]
public class FaceSet
{
    public Sprite baseColor;
    public EyeSet eye;
    public MouthSet mouth;

    public FaceSet(Sprite _baseColor, EyeSet _eye, MouthSet _mouth)
    {
        this.baseColor = _baseColor;
        this.eye = _eye;
        this.mouth = _mouth;
    }

}

[CreateAssetMenu(menuName = "Face/Face Generator Config")]
public class FaceGenConfig : ScriptableObject
{
    public FaceElementSets faceElements;

    public FaceSet GenerateFace()
    {
  // Pick a random base color
        Sprite baseColor = GetRandom(faceElements.baseColors);

        // Pick a random eye set
        EyeSet eye = GetRandom(faceElements.eyes);

        // Pick a random mouth set
        MouthSet mouth = GetRandom(faceElements.mouths);

        // Construct the face set
        return new FaceSet(baseColor, eye, mouth);
    }
    private Sprite GetRandom(List<Sprite> list)
    {
        if (list == null || list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }
    private EyeSet GetRandom(List<EyeSet> list)
    {
        if (list == null || list.Count == 0) return default;
        return list[Random.Range(0, list.Count)];
    }

    private MouthSet GetRandom(List<MouthSet> list)
    {
        if (list == null || list.Count == 0) return default;
        return list[Random.Range(0, list.Count)];
    }
}

[System.Serializable]
public struct EyeSet
{
    public Sprite open;
    public Sprite blink;
}
[System.Serializable]
public struct MouthSet
{
    public Sprite normal;
    public Sprite smile;
    public Sprite angry;
}


