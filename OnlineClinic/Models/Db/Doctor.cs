using System.ComponentModel.DataAnnotations;


namespace OnlineClinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Display(Name = "выберите фото")]
        public string Foto { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Специальность")]
        public string Specialty { get; set; }
        public int IdRecordDayTime { get; set; }
        [Display(Name = "Номер кабинета")]
        public string OfficeNumber { get; set; }
        public string UserId { get; set; }

    }

}
