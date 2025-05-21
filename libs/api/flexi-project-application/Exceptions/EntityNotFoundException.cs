namespace FlexiProject.Application.Exceptions;

public sealed class EntityNotFoundException(
    string entityName,
    Guid entityId
) : Exception($"{entityName} with ID: '{entityId}' was not found.");
