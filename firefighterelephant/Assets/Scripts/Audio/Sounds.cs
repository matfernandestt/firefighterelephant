using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds library;

    private void Awake()
    {
        library = this;
    }

    public AudioClip genericSFX;
    public AudioClip genericBGM;

}
