namespace ReminderManagerApi.Dto.Responses
{
    public class GetAllCalendarByUserIdResponse
    {
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
