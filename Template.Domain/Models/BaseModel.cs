namespace Template.Domain.Models
{
    public class BaseModel
    {
        public int CriadoPor { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? AlteradoPor { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Deletado { get; set; }
    }
}
