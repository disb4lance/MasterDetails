using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Log
    {
        public Guid Id { get; set; } // Первичный ключ
        public DateTime ErrorDate { get; set; } // Дата и время возникновения ошибки
        public string ErrorMessage { get; set; } // Текст ошибки
    }
}
