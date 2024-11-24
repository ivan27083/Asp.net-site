using Microsoft.EntityFrameworkCore;
using My_site.DAL.Repositories;
using My_site.ViewModels;
using My_stie.Domainn.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.Services.Implementations
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;
        public ComputerService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
            //_computerRepository.Create(new Computer()
            //{
            //    Url = "/content/images/ardor_gaming_rage_h359.webp",
            //    Name = "ARDOR GAMING RAGE H359",
            //    Description = "Компьютер ARDOR GAMING RAGE H359 — это мощное и эффективное решение для тех, кто ищет производительную систему для игр и задач, требующих высокой вычислительной мощности. Оснащенный процессором Intel Core i5-13400F, этот ПК обеспечивает отличную производительность для игр и приложений, справляясь с многозадачностью и ресурсоемкими задачами. оперативная память обеспечивает быструю и стабильную работу системы, позволяя эффективно работать с несколькими приложениями одновременно.",
            //    Price = 132999,
            //    GPU = "AMD",
            //    Rating = 3.8
            //});
            //_computerRepository.Create(new Computer()
            //{
            //    Url = "/content/images/ardor_gaming_neo_m157.webp",
            //    Name = "ARDOR GAMING NEO M157",
            //    Description = "ПК ARDOR GAMING NEO M157 в черном корпусе форм-фактора Mid-Tower не имеет предустановленной операционной системы, чтобы вы могли самостоятельно установить привычную ОС и нужное программное обеспечение. Конфигурация модели собрана на базе 6-ядерного процессора Intel Core i5-12400F, который способен одновременно обрабатывать до 12 вычислительных потоков информации. Вместе с 16 ГБ оперативной памяти чипсет обеспечит систему быстродействием при запуске требовательных игр и многозадачном режиме работы.",
            //    Price = 74999,
            //    GPU = "AMD",
            //    Rating = 5
            //});
        }
        public async Task<IEnumerable<Computer>> GetAll(CatalogFilterViewModel filter)
        {
            return await _computerRepository.GetAll()
                .Where(item =>
                    (item.Price >= filter.MinPrice) &&
                    (filter.MaxPrice == 0 || item.Price <= filter.MaxPrice) &&
                    (filter.GPU.Count == 0 || filter.GPU.Contains(item.GPU)) &&
                    (item.Rating >= filter.MinRating))
                .ToListAsync();
        }
        public async Task<IEnumerable<Computer>> GetForMain()
        {
            return await _computerRepository.GetAll().OrderByDescending(c => c.Rating).Take(3).ToListAsync();
        }
    }
}
