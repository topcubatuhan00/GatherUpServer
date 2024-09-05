using GatherUp.Domain.Core;

namespace GatherUp.Domain.Entities;

public class Community : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}
