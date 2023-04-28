using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScrollViewType
{
    横向单列,
    纵向单列
}

public class ScrollViewManager<T, I> where I : IScrollView
{
    public Transform transform;

    public ScrollViewType scrollViewType;
    private RectTransform content;
    private GameObject Item;
    private GridLayoutGroup layoutGroup;
    private float offset, itemHeight, height, spacing;
    List<I> _items = new List<I>();
    List<T> models = new List<T>();
    public void ChangeValue(Vector2 data)
    {
        foreach (var item in _items)
        {
            item.OnValueChange();
        }
    }
    public void Init(List<T> _models, ScrollViewType scrollViewType, Transform tran)
    {
        this.scrollViewType = scrollViewType;
        transform = tran;
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        Item = content.GetChild(0).gameObject;
        layoutGroup = content.GetComponent<GridLayoutGroup>();
        models = _models;
        int itemCount = GetShowItemCount();
        SpawnItems(itemCount);
        SetContentSize();
        transform.GetComponent<ScrollRect>().onValueChanged.AddListener(ChangeValue);
    }
    private int GetShowItemCount()
    {
        if (scrollViewType == ScrollViewType.横向单列)
        {
            height = transform.GetComponent<RectTransform>().rect.width;
            offset = layoutGroup.padding.left;
            itemHeight = layoutGroup.cellSize.x;
            spacing = layoutGroup.spacing.x;

        }
        else if (scrollViewType == ScrollViewType.纵向单列)
        {
            height = transform.GetComponent<RectTransform>().rect.height;
            offset = layoutGroup.padding.top;
            itemHeight = layoutGroup.cellSize.y;
            spacing = layoutGroup.spacing.y;
        }
        return Mathf.CeilToInt(height / (spacing + itemHeight)) + 1;
    }
    private void SpawnItems(int itemCount)
    {
        GameObject itemPrefab;
        I item;
        for (int i = 0; i < itemCount; i++)
        {
            itemPrefab = GameObject.Instantiate(Item, content.transform);
            itemPrefab.SetActive(true);
            item = itemPrefab.GetComponent<I>();
            item.Init(i,offset , spacing, itemCount, scrollViewType);
            _items.Add(item);
        }
    }
    public T GetData(int index)
    {
        if (index < 0 || index > models.Count - 1)
        {
            return default(T);
        }
        else
        {
            return models[index];
        }
    }
    private void SetContentSize( )
    {
        float value = models.Count * (itemHeight+ spacing) + offset ;
        if (scrollViewType == ScrollViewType.横向单列)
        {
            content.sizeDelta = new Vector2(value, content.sizeDelta.y);
        }
        else if (scrollViewType == ScrollViewType.纵向单列)
        {
            content.sizeDelta = new Vector2(content.sizeDelta.x, value);
        }
    }

}
