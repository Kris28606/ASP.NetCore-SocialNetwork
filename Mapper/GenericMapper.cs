using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface GenericMapper<D,T> where D : MyDto where T : MyEntity
    {
        public T toEntity(D dto);
        public D toDto(T entity);
    }
}
