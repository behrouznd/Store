using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Common
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}
