using GatherUp.Domain.Core;

namespace GatherUp.Domain.Entities;

public class EventUserRelation : EntityBase
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
}
