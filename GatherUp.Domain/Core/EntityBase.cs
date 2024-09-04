namespace GatherUp.Domain.Core;

public class EntityBase
{
    public virtual int Id { get; set; }
    public virtual string? CreatorName { get; set; } = "Admin";
    public virtual DateTime? CreatedDate { get; set; } = DateTime.Now;
    public virtual DateTime? UpdatedDate { get; set; } = null;
    public virtual string? UpdaterName { get; set; } = null;
    public virtual string? DeleterName { get; set; } = null;
    public virtual DateTime? DeletedTime { get; set; } = null;
    public virtual bool? IsActive { get; set; } = false;
}
