namespace FlexiProject.Core.Entities.Abstractions;

public class EntityBase
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}