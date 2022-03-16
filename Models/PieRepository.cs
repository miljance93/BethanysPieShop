using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext context;

        public PieRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return context.Pies.Include(c => c.Category);
            }
        }


        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return context.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return context.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
