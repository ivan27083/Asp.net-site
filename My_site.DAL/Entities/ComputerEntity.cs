﻿
namespace My_site.DAL.Entities
{
    public class ComputerEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string GPU { get; set; }
        public double Rating { get; set; }
    }
}
