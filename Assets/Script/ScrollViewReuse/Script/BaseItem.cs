using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem<T> : MonoBehaviour, IScrollView
{
    private int _id = -1;
    private float _offset;
    private float spacing;
    private int _itemCount;
    public ScrollViewType scrollViewType;
    private RectTransform _content;
    private RectTransform content
    {
        get
        {
            if (_content == null)
            {
                _content = transform.parent.GetComponent<RectTransform>();
            }
            return _content;
        }
    }
    private RectTransform _rect;
    private RectTransform rect
    {
        get
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>();
            }
            return _rect;
        }
    }
    public void Init(int i, float offset, float spacing, int itemCount, ScrollViewType scrollViewType)
    {
        this.scrollViewType = scrollViewType;
        _offset = offset;
        this.spacing = spacing;
        _itemCount = itemCount;
        ChangeID(i);
    }
    private void UpdateIdRange(out int _startId, out int _endId)
    {
        if (scrollViewType == ScrollViewType.纵向单列)
        {
            _startId = Mathf.FloorToInt(content.anchoredPosition.y / (rect.rect.height + spacing));

        }
        else if (scrollViewType == ScrollViewType.横向单列)
        {
            _startId = Mathf.FloorToInt(-content.anchoredPosition.x / (rect.rect.width + spacing));
        }
        else
        {
            _startId = 0;
        }
        if (_startId < 0)
            _startId = 0;
        _endId = _startId + _itemCount - 1;
    }
    public void OnValueChange()
    {
        int _startId, _endId;
        UpdateIdRange(out _startId, out _endId);
        JudgeSelfId(_startId, _endId);
    }
    private void JudgeSelfId(int _startId, int _endId)
    {

        if (_id < _startId)
        {
            ChangeID(_endId);
        }
        else if (_id > _endId)
        {
            ChangeID(_startId);
        }
    }
    private void SetPos()
    {
        if (scrollViewType == ScrollViewType.纵向单列)
        {
            rect.anchoredPosition = new Vector2(rect.rect.width / 2, -_id * (spacing + rect.rect.height) - _offset - rect.rect.height / 2);
        }
        else if (scrollViewType == ScrollViewType.横向单列)
        {
            rect.anchoredPosition = new Vector2(_id * (spacing + rect.rect.height) + _offset + rect.rect.height / 2, -rect.rect.height / 2);
        }
    }
    private void ChangeID(int Id)
    {
        if (_id != Id && IsVaild(Id))
        {
            _id = Id;
            UpdateInfo(Id);
            SetPos();
        }
    }

    public abstract void UpdateInfo(int Id);

    public abstract bool IsVaild(int i);

    public abstract T _getData(int i);

}
