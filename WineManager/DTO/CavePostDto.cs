﻿namespace WineManager.DTO
{
    public class CavePostDto
    {
        public string CaveType { get; set; }
        public string Family { get; set; }
        public string Brand { get; set; }
        public int Temperature { get; set; }
        public int? UserId { get; set; }
    }
}