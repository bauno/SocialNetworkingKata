namespace CSharp.Core
{
    public interface Dto<TDto, TEntity>
    {
        TDto ToDto();
        void Load(TDto dto);
    }
}