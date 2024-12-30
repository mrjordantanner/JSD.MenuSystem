public interface IMenuView<T>
{
    void Display(T data);
    event System.Action<T> OnUserInput;
}

