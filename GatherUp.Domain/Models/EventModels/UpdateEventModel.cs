namespace GatherUp.Domain.Models.EventModels;

public class UpdateEventModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EventDate { get; set; }
    public string EventTime { get; set; }
    public string EventPlace { get; set; }
    public string CommunityName { get; set; }
    public int CommunityId { get; set; }
    public string UpdaterName { get; set; }
}
