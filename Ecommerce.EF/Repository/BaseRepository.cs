using Ecommerce.core;
using Ecommerce.core.Const;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF.Repository
{
    public class BaseRepository<T>:IBaseRepository<T>where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public MemoryStream checkImage(IFormFile image, out bool check, out string check2)
        {
            List<string> _AllowimageExtention = new List<string> { ".jpg", ".png" };
            long _MaxSizeAllowed = 1048576;
            MemoryStream memoryStream = new MemoryStream();
            image.CopyTo(memoryStream);
            var extension = Path.GetExtension(image.FileName).ToLower();

            if (!_AllowimageExtention.Contains(extension))
            {
                check = false;
                check2 = "Invalid file extension. Allowed extensions are: .jpg, .png";
                return memoryStream;
            }
            if (image.Length > _MaxSizeAllowed)
            {
                check = false;
                check2 = "The Size of Image large than the Allowed";
                return memoryStream;
            }
            check = true;
            check2 = "";
            return memoryStream;
        }

        public IEnumerable<T> GetAll(string[] include=null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                foreach (var item in include)
                {
                  //لاحظ هنا لازم اعمل query= واساويهبالحاجه بعد ما اعملها include
                  //كمان لازم تكون نوع ال query IQuerable
                  //مش شغاله معرفش ليه مع ان في ال debug كله تمام وبتوصل لل controllerتقريبا المشكيه في العرض
                  //المشكله اني كنت عامل list<User>في ال countrie
                    //query= query.Include(item);
                    query= query.Include(item);
                }
            }
            return query.ToList();
        }
        public T GetById(Expression<Func<T,bool>>expression ,string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                foreach (var item in include)
                {
                    //لاحظ هنا لازم اعمل query= واساويهبالحاجه بعد ما اعملها include
                    //كمان لازم تكون نوع ال query IQuerable<T>
                    //مش شغاله معرفش ليه مع ان في ال debug كله تمام وبتوصل لل controllerتقريبا المشكيه في العرض
                    //query= query.Include(item);
                  query=  query.Include(item);
                }

            }
            return query.SingleOrDefault(expression);
        }
        public IEnumerable<T> GetByExepressionQuery(Expression<Func<T, bool>> expression, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (include != null)
            {
                foreach (var item in include)
                {
                    //مش شغاله معرفش ليه مع ان في ال debug كله تمام وبتوصل لل controllerتقريبا المشكيه في العرض
                    //query= query.Include(item);
                  query=  query.Include(item);
                }
            }
            return query.Where(expression);
        }
        public T GetLastElement(Expression<Func<T, object>> orderBy, string order = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();
            if(order != OrderBy.Ascending)
                 query= query.OrderByDescending(orderBy);
            else query= query.OrderBy(orderBy);
            //Last لازم يكون قبلها orderBy
            return query.Last();
        }
        public IEnumerable<T>GetByExepressionQuery(Expression<Func<T, bool>> expression, int? skip, int? take)
        {
            var query = _context.Set<T>();
            if(skip.HasValue)
                query.Skip(skip.Value);
            if(take.HasValue)
                query.Take(take.Value);
            return query.Where(expression).ToList();

        }
        public IEnumerable<T> GetByExepressionQuery(Expression<Func<T, bool>> expression, int? skip, int? take, Expression<Func<T, object>> orderBy=null, string order = null)
        {
           
            var query = _context.Set<T>().Where(expression);
            if (skip.HasValue)
                query.Skip(skip.Value);
            if (take.HasValue)
                query.Take(take.Value);
            if (orderBy != null)
            {
                if (order == OrderBy.Ascending || order == null)
                    return query.OrderBy(orderBy);
                else
                    return query.OrderByDescending(orderBy);
            }
            return query.ToList();
            
        }
        public T Post(T entity)
        {
            _context.Set<T>().Add(entity);
                            
            return entity;
        }
        public IEnumerable<T> PostRange(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
            return(entity);
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return(entity);
        }
        public IEnumerable<T> DeleteRange(IEnumerable<T> entity) 
        {
            _context.Set<T>().RemoveRange(entity);
            return entity;
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}