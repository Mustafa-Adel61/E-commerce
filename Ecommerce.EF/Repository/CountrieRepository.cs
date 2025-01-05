using Ecommerce.core;
using Ecommerce.core.Dto;
using Ecommerce.core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF.Repository
{
    public class CountrieRepository : BaseRepository<Countrie>, ICountrieRepository
    {
        private readonly ApplicationDbContext _context;

        public CountrieRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        //بعد كدا في الشغل انا مش هعمل ال method دي خالص عشان كدا كداهعمل update بش للحاجه الي جايه من ال front
      
        public Countrie UpdateCountrie(int id, CountrieDto dto)
        {
            Countrie countrie = _context.countries.Find(id);
            if (countrie == null)
                return null;
            if (dto.Name != null)
                countrie.Name=dto.Name;
            if (dto.Continent_Name != null)
                countrie.Continent_Name = dto.Continent_Name;
            return countrie;
        }
    }
}
