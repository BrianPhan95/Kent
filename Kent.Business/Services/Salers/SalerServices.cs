using Kent.Entities.Model;
using Kent.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Business.Services
{
    public class SalerServices : ISalerServices
    {
        public readonly ISalerRepository _salerRepository;
        public SalerServices(ISalerRepository salerRepository)
        {
            _salerRepository = salerRepository;
        }

        public List<Saler> GetList()
        {
            return _salerRepository.GetList();
        }
    }
}
