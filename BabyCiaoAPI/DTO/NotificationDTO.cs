public class Notification
{
    public int Id { get; set; }
    public string UserAccount { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedTime { get; set; }
}

public class NotificationDTO
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedTime { get; set; }
}
