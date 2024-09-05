namespace GatherUp.Domain.Models.EventModels;
public class CreateEventModel
{
    public string Name { get; set; }
    public string EventDate { get; set; }
    public string EventTime { get; set; }
    public string EventPlace { get; set; }
    public string CommunityName { get; set; }
    public int CommunityId { get; set; }
    public string CreatorName { get; set; }
}
