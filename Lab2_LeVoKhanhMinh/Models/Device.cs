namespace Lab2_LeVoKhanhMinh.Models;

public class Device
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string DeviceCode { get; set; }
    public DateTime DateOfEntry { get; set; }
    public Status Status { get; set; }
    public int DeviceCategoryId { get; set; }
    public DeviceCategory? DeviceCategory { get; set; }
    
}