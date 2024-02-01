using Newtonsoft.Json;
using BLL.Contracts;
using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using System.Data.Entity;
using DAL.Repository;
using DAL;
using System.Net;

namespace BLL.Services
{
    public class CurrencyManagement : ICurrencyManagement
    {
        static HttpClient client = new HttpClient();
        string CurrencyURL = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/ka/json";

        public List<string> FillDBWithNew(string user)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new CurrencyDBContext());

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpResponseMessage response = client.GetAsync(CurrencyURL).Result;
            CurrencyRoot ct = new CurrencyRoot();
            if (response.IsSuccessStatusCode)
            {
                var ResponseContent = response.Content.ReadAsStringAsync().Result;
                ct = (JsonConvert.DeserializeObject<List<CurrencyRoot>>(ResponseContent))?.FirstOrDefault();
            }

            if (ct?.currencies?.Count > 0)
            {
                List<CurrencyDTO> newCurs = new List<CurrencyDTO>();
                List<CurrencyChangeLog> logs = new List<CurrencyChangeLog>();
                List<string> updated = new List<string>();
                Dictionary<string, CurrencyDTO> currencyDict = new Dictionary<string, CurrencyDTO>();

                foreach (CurrencyDTO cdt in ct.currencies)
                {
                    CurrencyDTO curr = _unitOfWork.CurrencyRepo.GetCurrency(cdt.code);

                    if (curr != null)
                    {
                        string diff = CheckDifferences(curr, cdt);
                        if (!string.IsNullOrWhiteSpace(diff))
                        {
                            logs.Add(new CurrencyChangeLog
                            {
                                User = user,
                                CurrencyName = cdt.code,
                                Updated_At = DateTime.Now,
                                Data = diff
                            });

                            currencyDict.Add(cdt.code, cdt);

                            updated.Add(cdt.code);
                        }
                    }
                    else
                    {
                        newCurs.Add(cdt);
                    }
                }
                _unitOfWork.BeginTransaction();

                if (newCurs.Any())
                    _unitOfWork.CurrencyRepo.AddCurrencies(newCurs);

                if (logs.Any())
                    _unitOfWork.LogRepo.AddLogs(logs);

                if (currencyDict.Any())
                    _unitOfWork.CurrencyRepo.EditCurrencies(currencyDict);

                _unitOfWork.Save();
                _unitOfWork.CommitTransaction();

                return updated;
            }
            return null;
        }

        public bool EditCurrency(string code, string user, CurrencyDTO dt)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new CurrencyDBContext());

            if (user.Equals(""))
                throw new Exception($"Forbidden request!");

            if (_unitOfWork.CurrencyRepo.GetCurrency(code) == null)
                throw new Exception($"Currency with code {code} not found!");

            CurrencyDTO curr = _unitOfWork.CurrencyRepo.GetCurrency(code);

            string diff = CheckDifferences(curr, dt);
            if (!diff.Equals("No Changes"))
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.LogRepo.AddLogs(new List<CurrencyChangeLog> { new CurrencyChangeLog
                {
                    User = user,
                    CurrencyName = code,
                    Updated_At = DateTime.Now,
                    Data = diff
                } });

                _unitOfWork.CurrencyRepo.EditCurrencies(new Dictionary<string, CurrencyDTO>() { { code, dt } });
                _unitOfWork.Save();
                _unitOfWork.CommitTransaction();
            }
            else
            {
                return false;
            }
            return true;
        }

        public IEnumerable<CurrencyDTO> GetAllCurrencies()
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new CurrencyDBContext());
            return _unitOfWork.CurrencyRepo.GetAllCurrencies();
        }

        public CurrencyDTO GetCurrency(string code)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new CurrencyDBContext());
            return _unitOfWork.CurrencyRepo.GetCurrency(code);
        }

        public string CheckDifferences(CurrencyDTO old, CurrencyDTO changed)
        {
            try
            {
                StringBuilder dat = new StringBuilder();

                dat.Append((old.quantity != changed.quantity) ? $"quantity:{old.quantity} -> {changed.quantity} " : "");
                dat.Append((old.rateFormated != changed.rateFormated) ? $"rateFormated:{old.rateFormated} -> {changed.rateFormated} " : "");
                dat.Append((old.diffFormated != changed.diffFormated) ? $"diffFormated:{old.diffFormated} -> {changed.diffFormated} " : "");
                dat.Append((old.rate != changed.rate) ? $"rate:{old.rate} -> {changed.rate} " : "");
                dat.Append((old.name != changed.name) ? $"name:{old.name} -> {changed.name} " : "");
                dat.Append((old.diff != changed.diff) ? $"diff:{old.diff} -> {changed.diff} " : "");
                dat.Append((old.date.Date != changed.date.Date) ? $"date:{old.date} -> {changed.date} " : "");
                dat.Append((old.validFromDate.Date != changed.validFromDate.Date) ? $"validFromDate:{old.validFromDate} -> {changed.validFromDate} " : "");

                return dat.ToString();
            }
            catch
            {
                return "Error When Differentiating";
            }
            
        }
    }
}
