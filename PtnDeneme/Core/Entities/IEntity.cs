﻿namespace Core.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
