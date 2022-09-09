using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Common
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
