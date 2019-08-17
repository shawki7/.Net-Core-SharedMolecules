
namespace SharedMolecules.Entities
{
   public class GenericSMEntity<T>
    {
        public bool IsValid { get; set; }
        public T Id { get; set; }
    }
}
