using FluentValidator;
using System;

namespace App.Paladinos.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }
    }
}
