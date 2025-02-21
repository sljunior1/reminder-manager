namespace ReminderManagerApp.Controls
{
    public class CustomFilePickerFileType
    {
        public static readonly FilePickerFileType ImagesAndDocuments = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            {DevicePlatform.Android,new[]{ "image/*","application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", } }
        });
    }
}
