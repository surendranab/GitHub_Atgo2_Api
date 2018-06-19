using Atgo2.Api.DataRepository.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Text;

namespace Atgo2.Api.DataRepository
{
    public class ModelMapper
    {
        public static dynamic Mapping(object entity, int currentUserId)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(entity.GetType()))
                expando.Add(property.Name, property.GetValue(entity));

            expando.Add(DbKeywords.UserId, currentUserId);
            return expando;
        }
    }
}
