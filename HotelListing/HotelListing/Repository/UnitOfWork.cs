using HotelListing.Data;
using HotelListing.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private IGenericRepository<Hotel> _Hotels;
        private IGenericRepository<Country> _Countries;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<Country> countries => _Countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> hotels => _Hotels ??= new GenericRepository<Hotel>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
