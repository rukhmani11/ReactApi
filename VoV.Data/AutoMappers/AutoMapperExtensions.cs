using AutoMapper;
using VoV.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Data.AutoMappers
{
    //public static class AutoMapperExtensions
    //{
    //    public TDestination MapTo<TSource, TDestination>(this TSource source)
    //    {
    //        return Mapper.Map<TSource, TDestination>(source);
    //    }

    //    public TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
    //    {
    //        return Mapper.Map(source, destination);
    //    }

    //    #region Employee
    //    public static EmployeeDTO ToModel(this Employee entity)
    //    {
    //        return entity.MapTo<Employee, EmployeeDTO>();
    //    }
    //    public static Employee ToEntity(this EmployeeDTO model)
    //    {
    //        return model.MapTo<EmployeeDTO, Employee>();
    //    }
    //    public static Employee ToEntity(this EmployeeDTO model, Employee destination)
    //    {
    //        return model.MapTo(destination);
    //    }
    //    #endregion
    //}
}
