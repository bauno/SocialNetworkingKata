namespace SocialNetwork.Core.Repositories.Interfaces
{
    public interface Dto<TDto, TEntity>
    {
        TDto ToDto();
        void Load(TDto dto);
    }
}