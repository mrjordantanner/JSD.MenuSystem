/// <summary>
/// To be implemented by Models.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMenuData<T>
{
    T GetData();
    void UpdateData(T data);
    event System.Action<T> OnDataChanged;
}
