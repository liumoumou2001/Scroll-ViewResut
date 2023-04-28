using System;

public interface IScrollView
{
    //void AddListener<T>(Func<int, T> getData);

    void Init(int i, float offset, float spacing, int itemCount, ScrollViewType scrollViewType);
    void OnValueChange();
}