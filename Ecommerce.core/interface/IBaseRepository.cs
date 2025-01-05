using Ecommerce.core.Const;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core
{
    public interface IBaseRepository<T>where T : class
    {
        MemoryStream checkImage(IFormFile image,out bool check,out string check1);
        IEnumerable<T> GetAll(string[] include=null);
        T GetLastElement(Expression<Func<T,object>>orderBy,string order=OrderBy.Ascending);
        T GetById( Expression<Func<T, bool>> expression, string[] include = null);
        IEnumerable<T>GetByExepressionQuery(Expression<Func<T, bool>> expression, string[] include = null);
        IEnumerable<T> GetByExepressionQuery(Expression<Func<T, bool>> expression,int?skip,int?take,Expression<Func<T,object>>orderBy = null, string order=OrderBy.Ascending);
       
        T Post(T entity);
        IEnumerable<T> PostRange(IEnumerable<T> entity);
        T Delete(T entity);
        IEnumerable<T> DeleteRange(IEnumerable<T> entity);
        T Update(T entity);
        
        
       
    }
}