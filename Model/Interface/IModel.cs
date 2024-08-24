namespace DomainModel.Model.Interface;

public interface IModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Removed { get; set; }
}
