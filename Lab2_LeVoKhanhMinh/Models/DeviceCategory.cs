namespace Lab2_LeVoKhanhMinh.Models;

public class DeviceCategory
{  
    public int Id { get; set; }
    public string CategoryTitle { get; set; }
    public string imageUrl { get; set; }
    public ICollection<Device> Devices { get; set; }
}