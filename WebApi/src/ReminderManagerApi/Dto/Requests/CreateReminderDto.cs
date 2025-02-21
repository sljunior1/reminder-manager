﻿namespace ReminderManagerApi.Dto.Requests
{
    public class CreateReminderDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public AttachReminderDto? Attach { get; set; }
        public int UserId { get; set; }
    }
}
