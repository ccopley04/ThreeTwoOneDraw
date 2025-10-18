using UnityEngine;
using UnityEngine.U2D.Animation;

public class ChangeSpriteLibraryAsset : MonoBehaviour
{
    public SpriteLibrary spriteLibrary;
    public SpriteLibraryAsset currentLibraryAsset;

    public SpriteLibraryAsset Cactus;


    void ChangeLibraryAsset(SpriteLibraryAsset spriteThing)
    {
        spriteLibrary.AddOverride(spriteThing, "Idle");
    }
    void Start()
    {
        currentLibraryAsset = gameObject.GetComponent<SpriteLibrary>().spriteLibraryAsset;

        ChangeLibraryAsset(Cactus);
    }
}
