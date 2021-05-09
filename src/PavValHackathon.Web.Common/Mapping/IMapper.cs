namespace PavValHackathon.Web.Common.Mapping
{
    public interface IMapper
    {
        TTo MapFrom<TFrom, TTo>(TFrom from);
        TTo MapFrom<TFrom, TTo>(TTo to, TFrom from);
    }
}