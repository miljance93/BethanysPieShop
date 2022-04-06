﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public bool Update<TInput>(TInput input) where TInput : class
        {
            bool result;

            try
            {
                _appDbContext.Set<TInput>().Update(input);
                _appDbContext.SaveChanges();

                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool Add(Pie pie)
        {
            bool result;

            try
            {
                _appDbContext.Pies.Add(pie);
                _appDbContext.SaveChanges();

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
