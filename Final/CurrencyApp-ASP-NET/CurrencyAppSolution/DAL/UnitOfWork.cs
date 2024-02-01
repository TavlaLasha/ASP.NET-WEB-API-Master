using DAL.EF;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork
    {
        private LogRepository _logRepo;
        private CurrencyRepository _currencyRepo;
        private CurrencyDBContext _db;

        private DbContextTransaction dbContextTransaction;

        public UnitOfWork(CurrencyDBContext db)
        {
            _db = db;
        }
        public CurrencyRepository CurrencyRepo
        {
            get
            {
                if(_currencyRepo == null)
                {
                    _currencyRepo = new CurrencyRepository(_db);
                }
                return _currencyRepo;
            }
        }
        public LogRepository LogRepo
        {
            get
            {
                if (_logRepo == null)
                {
                    _logRepo = new LogRepository(_db);
                }
                return _logRepo;
            }
        }

        public bool BeginTransaction()
        {
            bool jobDone = true;
            try
            {
                dbContextTransaction = _db.Database.BeginTransaction();
            }
            catch (Exception)
            {                 
                jobDone = false;
            }
            return jobDone;
        }
        public bool CommitTransaction()
        {
            bool jobDone = true;
            if (dbContextTransaction == null)
                throw new Exception("Transaction Not Started");

            try
            {
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                jobDone = false;
                dbContextTransaction.Rollback();
            }
            dbContextTransaction.Dispose();
            return jobDone;
        }
        public bool Save()
        {
            bool jobDone = true;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {                   
                jobDone = false;
            }
            return jobDone;
        }
    }
}
