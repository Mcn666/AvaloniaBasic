using ReactiveUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication1.ViewModels;

public class ViewModelBase : ReactiveObject
{
    /// <summary>
    /// 存储属性值
    /// </summary>
    private readonly Dictionary<string, object?> keyValuePairs = new Dictionary<string, object?>();

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
    protected bool SetValue<T>(T value, [CallerMemberName] string propertyName = "")
    {
        if (keyValuePairs.TryGetValue(propertyName, out var oldValue) && oldValue is T oldValueT)
        {
            if (EqualityComparer<T>.Default.Equals(oldValueT, value))
            {
                return false; // 值相同，不更新
            }
        }

        keyValuePairs[propertyName] = value;
        this.RaisePropertyChanged(propertyName);
        return true;
    }
}
