using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadStatSprite : MonoBehaviour
{
    [SerializeField] private Image background;

    [SerializeField] private Image statIcon;

    private List<Sprite> _backgroundSprites;

    public void LoadStat(Sprite icon)
    {
        _backgroundSprites = new List<Sprite>
        {
            Resources.Load<Sprite>("Sprites/StatBackground/Stat_frame_icon_1"),
            Resources.Load<Sprite>("Sprites/StatBackground/Stat_frame_icon_2"),
            Resources.Load<Sprite>("Sprites/StatBackground/Stat_frame_icon_3"),
            Resources.Load<Sprite>("Sprites/StatBackground/Stat_frame_icon_4")
            
        };
        
        statIcon.sprite = icon;

        var spriteIndex = Random.Range(0, _backgroundSprites.Count);

        background.sprite = _backgroundSprites[spriteIndex];
    }
}
