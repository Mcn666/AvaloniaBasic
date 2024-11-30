using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication3.ViewModels;

public class ViewModelBase : ObservableObject
{
    /// <summary>
    /// 存储属性值
    /// </summary>
    private readonly Dictionary<string, object?> keyValuePairs = [];
    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    protected T? GetValue<T>([CallerMemberName] string propertyName = "")
    {
        if (keyValuePairs.TryGetValue(propertyName, out var value) && value is T valueT)
        {
            return valueT;
        }

        return default;
    }
    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    protected void SetValue<T>(T value, [CallerMemberName] string propertyName = "")
    {
        if (keyValuePairs.TryGetValue(propertyName, out var oldValue) && oldValue is T oldValueT)
        {
            if (EqualityComparer<T>.Default.Equals(oldValueT, value))
            {
                return; // 值相同，不更新
            }
        }

        keyValuePairs[propertyName] = value;
        OnPropertyChanged(propertyName);
    }
}
