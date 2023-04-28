using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : BaseItem<ScrollViewModel>
{
    private Image _image;
    private ScrollViewModel model;
    private Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponentInChildren<Image>();
            }
            return _image;
        }
    }

    private Text _text;

    private Text text
    {
        get
        {
            if (_text == null)
            {
                _text = GetComponentInChildren<Text>();
            }
            return _text;
        }
    }
    public override void UpdateInfo(int Id)
    {
        model = _getData(Id);
        image.sprite = model.sprite;
        text.text = model.name;

    }
    public override bool IsVaild(int i)
    {
        return _getData(i) != (default(ScrollViewModel));
    }
    public override ScrollViewModel _getData(int i)
    {
        return transform.GetComponentInParent<Test>().GetData(i);
    }
}
