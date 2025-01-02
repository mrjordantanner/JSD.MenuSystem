/// <summary>
/// A data source for a MenuData model.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMenuDataProvider<TMenuData>
{
    string ProviderId { get; }
    void PopulateData(TMenuData data);
}
