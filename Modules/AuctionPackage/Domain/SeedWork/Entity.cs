using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.SeedWork
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public bool Created { get; set; }
        public bool Modified { get; set; }
        public bool Deleted { get; set; }

        public void Create()
        {
            CreatedAt = DateTime.UtcNow;
            Created = true;
        }
        public void Modify()
        {
            ModifiedAt = DateTime.UtcNow;
            Modified = true;
        }
        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
            Deleted = true;
        }
    }
}
