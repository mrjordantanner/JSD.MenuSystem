public interface IMenuData<T>
{
    T GetData();
    void UpdateData(T data);
    event System.Action<T> OnDataChanged;
}
