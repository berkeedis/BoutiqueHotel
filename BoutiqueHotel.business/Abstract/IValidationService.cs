namespace BoutiqueHotel.business.Abstract
{
    public interface IValidationService<T>
    {
        string ErrorMessage { get; set; }
        bool Validation(T entity);
    }
}