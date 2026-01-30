using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Sprite[] foodSprites;
    public Sprite[] catSprites;
    public Sprite[] dogSprites;
    public Sprite[] drinkSprites;
    public Sprite[] memeSprites;

    private Sprite[][] themes;
    private int currentTheme = 0;

    void Awake()
    {
        themes = new Sprite[][]
        {
            foodSprites,
            catSprites,
            dogSprites,
            drinkSprites,
            memeSprites
        };
    }

    public Sprite GetCurrentSprite()
    {
        Sprite[] theme = themes[currentTheme];
        return theme[Random.Range(0, theme.Length)];
    }

    public void SwitchTheme()
    {
        currentTheme++;
        if (currentTheme >= themes.Length)
            currentTheme = 0;

        Debug.Log("Theme switched!");
    }
}
