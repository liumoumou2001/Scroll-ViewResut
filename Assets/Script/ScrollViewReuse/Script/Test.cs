using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScrollViewModel
{
    public Sprite sprite;
    public string name;
    public ScrollViewModel(Sprite sprite, string name)
    {
        this.sprite = sprite;
        this.name = name;
    }
}

public class Test : MonoBehaviour
{
    public ScrollViewType scrollViewType;
    public Sprite sprite;
    List<ScrollViewModel> list = new List<ScrollViewModel>();
    // Start is called before the first frame update
    void Start()
    {
        list.Add(new ScrollViewModel(sprite, "1"));
        list.Add(new ScrollViewModel(sprite, "2"));
        list.Add(new ScrollViewModel(sprite, "3"));
        list.Add(new ScrollViewModel(sprite, "4"));
        list.Add(new ScrollViewModel(sprite, "5"));
        list.Add(new ScrollViewModel(sprite, "6"));
        list.Add(new ScrollViewModel(sprite, "7"));
        list.Add(new ScrollViewModel(sprite, "8"));
        list.Add(new ScrollViewModel(sprite, "9"));
        list.Add(new ScrollViewModel(sprite, "10"));
        list.Add(new ScrollViewModel(sprite, "11"));
        ScrollViewManager<ScrollViewModel, Item> scrollViewManager = new ScrollViewManager<ScrollViewModel, Item>();
        scrollViewManager.Init(list, scrollViewType, transform );
    }


    public ScrollViewModel GetData(int index)
    {

        if (index < 0 || index > list.Count - 1)
        {
            return default(ScrollViewModel);
        }
        else
        {
            return list[index];
        }
    }
}
